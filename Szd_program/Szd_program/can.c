/*
 * uart.c
 *
 * Created: 2024. 10. 13. 17:11:39
 *  Author: Török Bence
 */ 
/******************************************************************************
 * Created: 
 * Author :
******************************************************************************/
 /******************************************************************************
* Include files
******************************************************************************/
#include <avr/io.h>
#include <inttypes.h>
#include "can.h"

/******************************************************************************
* Macros
******************************************************************************/

/******************************************************************************
* Constants
******************************************************************************/

/******************************************************************************
* Global Variables
******************************************************************************/

/******************************************************************************
* External Variables
******************************************************************************/

/******************************************************************************
* Local Function Declarations
******************************************************************************/

/******************************************************************************
* Local Function Definitions
******************************************************************************/

/******************************************************************************
* Function:         void can_init(void)
* Description:      CAN kommunikáció inicializálása
* Input:
* Output:
* Notes:
******************************************************************************/
void can_init(void)
{
	CANGCON = (1 << SWRES);  // CAN vezérlõ reset
	
	//Mob-ok resetelése
	unsigned char mob;
	for(mob=0; mob<15; mob++)
	{
		CANPAGE = (mob<<MOBNB0);
		CANCDMOB = 0;
		CANSTMOB = 0;
		CANIDM1 = 0x00;
		CANIDM2 = 0x00;
		CANIDM3 = 0x00;
		CANIDM4 = 0x00;
	}

	//8Mhz,500kbps baud rate
	CANBT1 = 0x02;
	CANBT2 = 0x04;
	CANBT3 = 0x13;

	CANIE2 = 0xFF;
	CANIE1 = 0x7F;
	CANGIE = (1<<ENIT) | (1 << ENRX) | (1 << ENTX);
	
	CANGCON = (1 << ENASTB);  // CAN vezérlõ engedélyezése (normál mód)
}
/******************************************************************************
* Function:         void can_rx_init(void)
* Description:      CAN uzenet fogadasanak inicializálása
* Input:
* Output:
* Notes:
******************************************************************************/
void can_rx_init(void)
{
	// MOb0 konfigurálása CAN fogadásra
	CANPAGE = (0 << MOBNB0) |  (0 << MOBNB1) |  (0 << MOBNB2)|  (0 << MOBNB3); // MOb0 kiválasztása
	
	CANIDM1 = 0x00;   // Fogadási szûrõ (ID beállítás)
	CANIDM2 = 0x00;
	CANIDM3 = 0x00;
	CANIDM4 = 0x00;
	
	CANCDMOB = (1 << CONMOB1) |  (0 << CONMOB0) | (1 << DLC3); // Fogadás módba állítás, 8 byte-os üzenet (DLC = 8)
}
/******************************************************************************
* Function:         void can_tx(uint8_t* rx_buffer)
* Description:      CAN uzenet kuldesenek inicializálása
* Input:			uint8_t* rx_buffer
* Output:
* Notes:
******************************************************************************/
void can_tx(uint8_t* rx_buffer)
{
	
	uint16_t pl_in_length = (*(rx_buffer+3) << 8) | *(rx_buffer+4);
	CANPAGE = 0x10;		// Mob1 kivalasztasa
	while ( CANEN2 & ( 1 <<  ENMOB1 ) ); // Mob1-re varas
	CANSTMOB = 0x00;   // Mob status register torlese
	
	if (*(rx_buffer+2) == 1)
	{
		CANIDT4 = 0x00;
		CANIDT3 = 0x00;
		CANIDT2 = (*(rx_buffer+6) << 5);
		CANIDT1 = *(rx_buffer+6) >> 3 | *(rx_buffer+5) << 5;
		
		for (int8_t i = 0; i <  pl_in_length-2; i++)
		{
			CANMSG = *(rx_buffer+7+i);
		}
		CANCDMOB = ( 1 <<  CONMOB0 ) | ( 0 <<  IDE ) | ((pl_in_length-2) <<  DLC0 );
	}
	else
	{
		
		CANIDT4 = (*(rx_buffer+8) << 3);
		CANIDT3 = *(rx_buffer+8) >> 5 | *(rx_buffer+7) << 3;
		CANIDT2 = *(rx_buffer+7) >> 5 | *(rx_buffer+6) << 3;
		CANIDT1 = *(rx_buffer+6) >> 5 | *(rx_buffer+5) << 3;
		
		for (int8_t i = 0; i <  pl_in_length-4; i++)
		{
			CANMSG = *(rx_buffer+9+i);
		}
		CANCDMOB = ( 1 <<  CONMOB0 ) | ( 1 <<  IDE ) | ((pl_in_length-4) <<  DLC0 );
	}
	while ( ! ( CANSTMOB & ( 1 <<  TXOK ) ) ); //Varas a TXOK flag-re
	CANCDMOB = 0x00;	// Kuldes kikapcsolasa
	CANSTMOB = 0x00;	// TXOK flag torlese
}
