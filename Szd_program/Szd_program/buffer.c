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
#include "buffer.h"

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
* Function:         void Add_msg_to_buffer(Node** head,Node** tail, uint8_t* buffer)
* Description:      Hozzaad egy elemet a bufferhez
* Input:			Node** head,Node** tail, uint8_t* buffer
* Output:
* Notes:
******************************************************************************/
void Add_msg_to_buffer(Node** head,Node** tail, uint8_t* buffer)
{
	Node* temp = (Node*)malloc(sizeof(Node));
	if (temp == NULL)
	{
		USART0_Transmit(99);
		free(temp);
		return;
	}
	
	temp->buffer = buffer;
	temp->next = NULL;
	
	if (*head == NULL)
	{
		*head = temp;
		*tail = temp;
	}
	else
	{
		(*tail)->next = temp;
		*tail = temp;
	}
}
/******************************************************************************
* Function:         void Send_and_Delete_msg(Node** head, Node** tail)
* Description:      Kikuldi az elso elemet a megfelelo iranyba és torli bufferbol
* Input:			Node** head, Node** tail
* Output:
* Notes:
******************************************************************************/
void Send_and_Delete_msg(Node** head, Node** tail)
{
	Node* temp = *head;	
	uint8_t* temp_p = temp->buffer;
	
	if (temp != NULL)
	{
		if (*(temp_p+2) == 1 || *(temp_p+2) == 3)
		{
			//PC to Can
			UBX_tx_checking(temp->buffer);
			can_tx(temp->buffer);
			*head = temp->next;
			free(temp->buffer);
			free(temp);
			return;
		}
		else
		{
			//Can to PC
			UBX_msg(temp->buffer);
			*head = temp->next;
			free(temp->buffer);
			free(temp);
			return;
		}
	}
}
