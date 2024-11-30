import {createSlice, PayloadAction} from '@reduxjs/toolkit'
import {Conversation} from "../../model/conversation/conversation.ts";
import {conversationTestData} from "../../model/conversation/conversationTestData.ts";
import {setSectionSelectedMessageAction} from "./setSectionSelectedMessageAction.ts";
import {ConversationStreamEvent} from "../../model/conversation/dto/conversationStream.ts";
import {handleStreamEventAction} from "./handleStreamEventAction.ts";

// Define a type for the slice state
export type ConversationState = {
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
        },
        setSectionSelectedMessage: (state, action: PayloadAction<{ sectionIndex: number, messageId: string }>) => {
            setSectionSelectedMessageAction(state, action.payload);
        },
        handleStreamEvent: (state, action: PayloadAction<ConversationStreamEvent>) => {
            handleStreamEventAction(state, action.payload);
        }
    },
})

export const { selectConversation, setSectionSelectedMessage, handleStreamEvent } = conversationSlice.actions

export default conversationSlice.reducer