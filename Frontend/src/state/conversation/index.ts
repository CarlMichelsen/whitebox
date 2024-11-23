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
        },
        setSectionSelectedMessage: (state, action: PayloadAction<{ sectionIndex: number, messageId: string }>) => {
            if (state.selectedConversation === null) {
                return;
            }
            
            const section = state.selectedConversation.sections[action.payload.sectionIndex] ?? null;
            if (section === null) {
                return;
            }
            
            if (!!section.messages[action.payload.messageId]) {
                section.selectedMessageId = action.payload.messageId;
            }

            for (let i = action.payload.sectionIndex + 1; i < state.selectedConversation.sections.length; i++) {
                const prevSection = state.selectedConversation.sections[i-1];
                const section = state.selectedConversation.sections[i]!;
                
                if (prevSection.selectedMessageId === null) {
                    section.selectedMessageId = null;
                    continue;
                }

                let sectionMessageId: string|null = null;
                for (const msgId in section.messages) {
                    const msg = section.messages[msgId]
                    if (msg.previousMessageId === prevSection.selectedMessageId) {
                        sectionMessageId = msg.id;
                        break;
                    }
                }

                section.selectedMessageId = sectionMessageId;
            }
        }
    },
})

export const { selectConversation, setSectionSelectedMessage } = conversationSlice.actions

export default conversationSlice.reducer