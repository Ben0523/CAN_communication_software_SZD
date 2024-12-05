/******************************************************************************
 * Created: 
 * Author :
******************************************************************************/
 /******************************************************************************
* Include files
******************************************************************************/
#define F_CPU 8000000UL
#define BAUD9600 51
#define BUFFER_LENGTH 19
#define TRUE 1
#define FALSE 0
#define SYNC1 181
#define SYNC2 98
#include <avr/io.h>
#include <avr/interrupt.h>
#include <util/delay.h>
#include <inttypes.h>
#include <stdlib.h>
#include "uart.h"
#include "can.h"
#include "buffer.h"
#include "ubx.h"
/******************************************************************************
* Macros
******************************************************************************/


/******************************************************************************
* Constants
******************************************************************************/


/******************************************************************************
* Global Variables
******************************************************************************/
uint8_t can_read, ubx_id, cnt_rx, mess_ok, ck_ok = 0;
uint8_t can_data[8];
uint8_t can_id_array[4];
uint8_t pl_in[12];
uint8_t can_ext = 1;
uint8_t task_10ms, task_100ms, task_1s, task_10s=0;
uint16_t cnt_0 = 0, pl_ISR = 0;
uint32_t can_id,id1,id2,id3,id4 = 0;
uint8_t *rx_buffer, *temp_can_rx = 0;
uint8_t sync_bytes[2]; 

Node* head = NULL;
Node* tail = NULL;
/******************************************************************************
* External Variables
******************************************************************************/


/******************************************************************************
* Local Function Declarations
******************************************************************************/
void port_init(void);
void timer_ctc_mode_init(void);
/******************************************************************************
* Local Function Definitions
******************************************************************************/

/******************************************************************************
* Function:         int main(void)
* Description:      main function
* Input:            
* Output:           
* Notes:            
******************************************************************************/
int main(void)
{
	port_init();
	can_init();
	USART0_Init(BAUD9600);
	timer_ctc_mode_init();
	sei();
	_delay_ms(200);
	
    while (1)
    {
		can_rx_init();
		if (head != NULL)
		{
			Send_and_Delete_msg(&head,&tail);
		}
		if(task_10ms)
		{
			task_10ms = 0;
		}
		if(task_100ms)
		{
			task_100ms = 0;
		}
		if(task_1s)
		{
			PORTA ^= (1<<PA0);
			if (ck_ok)
			{
				PORTA ^= (1<<PA1);
				ck_ok = FALSE;
			}
			task_1s = 0;
		}
		if(task_10s)
		{
			task_10s = 0;
		}
    }
}
/******************************************************************************
* Function:         void timer_ctc_mode_init(void)
* Description:      Timer 0 felkonfigurálása CTC módba
* Input:
* Output:
* Notes:
******************************************************************************/
void timer_ctc_mode_init(void)
{
	TCCR0A = (1<<WGM01) | (0<<WGM00) | (1<<CS02) | (0<<CS01) | (1<<CS00);
	OCR0A = 77;
	TIMSK0 = (1<<OCIE0A);
}
/******************************************************************************
* Function:         void port_init(void)
* Description:      I/O portok inicializálása
* Input:
* Output:
* Notes:
******************************************************************************/
void port_init(void)
{
	DDRA = (1<<PA4) |(1<<PA3) | (1<<PA2) | (1<<PA1) | (1<<PA0);
	DDRE = 0x00;
	PORTE = 0xff;
}

/******************************************************************************
* Interrupt Routines
******************************************************************************/
ISR(TIMER0_COMP_vect)
{
	cnt_0++;
	task_10ms = 1;
	if((cnt_0 % 10)==0) task_100ms =1;
	if((cnt_0 % 100)==0) task_1s =1;
	if((cnt_0 % 1000)==0) task_10s =1;
	if(cnt_0>1000) cnt_0=0;
}
ISR(CANIT_vect) 
{
	int8_t save_page;
	save_page = CANPAGE;
	temp_can_rx = (uint8_t*)malloc(BUFFER_LENGTH*sizeof(uint8_t));
	if (temp_can_rx == NULL)
	{
		USART0_Transmit(99);
		free(temp_can_rx);
		return;
	}
	CANPAGE = (0 << MOBNB0);
	// Fogadási megszakítás kezelése
	if (CANSTMOB & (1 << RXOK)) 
	{
		// Fogadott CAN üzenet kezelése
		if (CANCDMOB & (1 << IDE)) 
		{
			can_ext = TRUE;
			*(temp_can_rx+2) = 4;
			// 29 bites kiterjesztett azonosító
			*(temp_can_rx+5) = CANIDT1 >> 3;
			*(temp_can_rx+6) = CANIDT2 >> 3 | CANIDT1 << 5;
			*(temp_can_rx+7) = CANIDT3 >> 3 | CANIDT2 << 5;
			*(temp_can_rx+8) = CANIDT4 >> 3 | CANIDT3 << 5;
		} 
		else
		{
			can_ext = FALSE;
			*(temp_can_rx+2) = 2;
			// 11 bites normál azonosító
			*(temp_can_rx+5) = 0;
			*(temp_can_rx+6) = 0;
			*(temp_can_rx+7) = CANIDT1 >> 5;
			*(temp_can_rx+8) = CANIDT2 >> 5 | CANIDT1 << 3;
		}
		uint8_t can_length = CANCDMOB & 0x0F;
		uint16_t pl_length = 4 + can_length;
		  // Adathossz olvasása
		*(temp_can_rx+3) = (pl_length >> 8) & 0xFF;
		*(temp_can_rx+4) = pl_length & 0xFF;

		for (uint8_t i = 0; i < can_length; i++) {
			*(temp_can_rx+9+i) = CANMSG;  // Üzenet adatok olvasása
		}
		
		//Bufferbe csak a can_id-t és can_data-t toltom be gyorsan
		Add_msg_to_buffer(&head,&tail,temp_can_rx);
		// RXOK bit törlése
		CANSTMOB = 0x00;
		// Fogadó MOb újra engedélyezése
		CANCDMOB = (1 << CONMOB1) |  (0 << CONMOB0) | (1 << DLC3);
		can_read = TRUE;
	}	CANPAGE = save_page;
}ISR(USART0_RX_vect)
{
	uint8_t rx_data = UDR0;
	sync_bytes[cnt_rx] = rx_data;
	if (sync_bytes[0] == 181 && sync_bytes[1] == 98)
	{
		mess_ok =TRUE;
		//USART0_Transmit(98);
		rx_buffer = (uint8_t*)malloc(BUFFER_LENGTH*sizeof(uint8_t));
		if (rx_buffer == NULL)
		{
			USART0_Transmit(99);
			free(rx_buffer);
			return;
		}
		sync_bytes[0] = 0;
		sync_bytes[1] = 0;
	}
	if (mess_ok)
	{
		*(rx_buffer + cnt_rx) = rx_data;
		if (cnt_rx == 3)
		{
			
			pl_ISR |= (rx_data << 8);
			cnt_rx++;
		}
		else if (cnt_rx == 4)
		{
			pl_ISR = rx_data;
			cnt_rx++;
		}
		else if (cnt_rx == 6+pl_ISR)
		{
			Add_msg_to_buffer(&head,&tail,rx_buffer);
			cnt_rx = 0;
			mess_ok = FALSE;
		}
		else
		{
			cnt_rx++;
		}
	}
	else
	{
		cnt_rx++;
	}
	
}

