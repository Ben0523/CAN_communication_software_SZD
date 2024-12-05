/*
 * buffer.h
 *
 * Created: 2024. 10. 13. 18:36:12
 *  Author: T�r�k Bence
 */ 


#ifndef BUFFER_H_
#define BUFFER_H_

/******************************************************************************
* Include files
******************************************************************************/
#include <inttypes.h>
#include <stdlib.h>
/******************************************************************************
* Types
******************************************************************************/

/******************************************************************************
* Constants
******************************************************************************/
typedef struct Node
{
	uint8_t* buffer;
	struct Node* next;
}Node;
/******************************************************************************
* Macros
******************************************************************************/

/******************************************************************************
* Global Function Declarations
******************************************************************************/
void Add_msg_to_buffer(Node** head,Node** tail, uint8_t* buffer);
void Send_and_Delete_msg(Node** head,Node** tail);

#endif /* BUFFER_H_ */