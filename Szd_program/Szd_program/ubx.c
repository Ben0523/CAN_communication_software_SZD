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
* Function:         void UBX_msg(uint8_t* temp_can_rx)
* Description:      Bejovo CAN uzenet UBX uzenette alakitasa
* Input:			uint8_t* temp_can_rx
* Output:			
* Notes:
******************************************************************************/
void UBX_msg(uint8_t* temp_can_rx)
{
	uint16_t pl_length = (*(temp_can_rx+3) << 8) | *(temp_can_rx+4);
	uint8_t *payload = malloc((pl_length)*sizeof(uint8_t));
	if (payload == NULL)
	{
		USART0_Transmit(99);
		free(payload);
		return;
	}
	for (uint8_t i = 0; i < 4; i++)
	{
		payload[i] = *(temp_can_rx+5+i);
	}
	for (uint8_t i = 0; i < pl_length-4; i++)
	{
		payload[4+i] =*(temp_can_rx+9+i);
	}
	
	//UBX keret feltöltése
	uint8_t *ubx = malloc((7+pl_length)*sizeof(uint8_t));
	if (ubx == NULL)
	{
		USART0_Transmit(99);
		free(ubx);
		return;
	}
	ubx[0] = SYNC1;
	ubx[1] = SYNC2;
	ubx[2] = *(temp_can_rx+2);
	ubx[3] = (pl_length >> 8) & 0xFF;
	ubx[4] = pl_length & 0xFF;
	for (uint8_t i = 0; i < pl_length; i++)
	{
		ubx[5+i] = payload[i];
	}
	uint8_t *ubx_ck = calculateChecksum(ubx[2],(uint8_t[]){ubx[3],ubx[4]},2,payload,pl_length);
	ubx[5+pl_length]=ubx_ck[0];
	ubx[6+pl_length]=ubx_ck[1];
	
	for (uint8_t i = 0; i < 7+pl_length;i++)
	{
		USART0_Transmit(ubx[i]);
	}
	
	free(payload);
	free(ubx);
	
}
/******************************************************************************
* Function:         void UBX_tx_checking(uint8_t* rx_buffer)
* Description:      Bejovo UBX uzenet ellenorzese
* Input:			uint8_t* rx_buffer
* Output:
* Notes:
******************************************************************************/
void UBX_tx_checking(uint8_t* rx_buffer)
{
	uint16_t pl_in_length = (*(rx_buffer+3) << 8) | *(rx_buffer+4);
	uint8_t *payload = malloc((pl_in_length)*sizeof(uint8_t));
	if (payload == NULL)
	{
		USART0_Transmit(99);
		free(payload);
		return;
	}
	for (uint8_t i = 0; i < pl_in_length; i++)
	{
		payload[i] = *(rx_buffer+5+i);
	}
	
	uint8_t *ubx_ck = calculateChecksum(*(rx_buffer+2),(uint8_t[]){*(rx_buffer+3),*(rx_buffer+4)},2,payload,pl_in_length);
	if (*(rx_buffer+5+pl_in_length) == ubx_ck[0] && *(rx_buffer+6+pl_in_length) == ubx_ck[1])
	{
		ck_ok = TRUE;
		PORTA ^= (1<<PA1);
	}
	
	free(payload);
}
/******************************************************************************
* Function:         uint8_t* calculateChecksum(uint8_t id, const uint8_t* length, size_t length_size, const uint8_t* payload, uint16_t payload_size)
* Description:      Checksum szamitas
* Input:			uint8_t id, const uint8_t* length, size_t length_size, const uint8_t* payload, uint16_t payload_size
* Output:			uint8_t* checksum
* Notes:
******************************************************************************/
uint8_t* calculateChecksum(uint8_t id, const uint8_t* length, size_t length_size, const uint8_t* payload, uint16_t payload_size)
{
	uint8_t ckA = 0;
	uint8_t ckB = 0;
	uint8_t *checksum = malloc(2 * sizeof(uint8_t));
	if (checksum == NULL)
	{
		USART0_Transmit(99);
		free(checksum);
		return NULL;
	}

	// Ck_buffer létrehozása
	size_t ck_buffer_size = 1 + length_size + payload_size;
	uint8_t *ck_buffer = malloc(ck_buffer_size * sizeof(uint8_t));
	if (ck_buffer == NULL)
	{
		USART0_Transmit(99);
		free(ck_buffer);
		return NULL;
	}
	
	ck_buffer[0] = id;
	for (size_t i = 0; i < length_size; i++)
	{
		ck_buffer[1 + i] = length[i];
	}
	for (size_t i = 0; i < payload_size; i++)
	{
		ck_buffer[1 + length_size + i] = payload[i];
	}
	
	// Checksum kiszámítása
	for (size_t i = 0; i < ck_buffer_size; i++)
	{
		ckA += ck_buffer[i];
		ckB += ckA;
	}
	
	checksum[0] = ckA;
	checksum[1] = ckB;
	
	free(ck_buffer);

	return checksum;
}
