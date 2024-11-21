import {createSlice, PayloadAction} from '@reduxjs/toolkit'
import {Conversation} from "../../model/conversation/conversation.ts";
import {conversationTestData} from "../../model/conversation/conversationTestData.ts";

// Define a type for the slice state
type ConversationState = {
    selectedConversation: Conversation|null;
}

const initialState: ConversationState = {
    selectedConversation: conversationTestData,
}

export const conversationSlice = createSlice({
    name: 'conversation',
    initialState,
    reducers: {
        selectConversation: (state, action: PayloadAction<Conversation|null>) => {
            state.selectedConversation = action.payload;
        }
    },
})

export const { selectConversation } = conversationSlice.actions

export default conversationSlice.reducer