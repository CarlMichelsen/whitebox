import {createSlice, PayloadAction} from '@reduxjs/toolkit'
import {Conversation} from "../../model/conversation/conversation.ts";
import {conversationTestData} from "../../model/conversation/conversationTestData.ts";
import {setSectionSelectedMessageAction} from "./setSectionSelectedMessageAction.ts";
import {
    AssistantMessageDeltaEvent,
    AssistantMessageEvent, AssistantUsageEvent,
    UserMessageEvent
} from "../../model/conversation/dto/conversationStream.ts";
import {handleAssistantMessageAction} from "./handleAssistantMessageAction.ts";
import {handleUserMessageAction} from "./handleUserMessageAction.ts";
import {handleAssistantMessageDeltaAction} from "./handleAssistantMessageDeltaAction.ts";
import {handleAssistantUsageAction} from "./handleAssistantUsageAction.ts";

// Define a type for the slice state
export type ConversationState = {
    selectedConversation: Conversation|null;
    attached: boolean;
}

const initialState: ConversationState = {
    selectedConversation: conversationTestData,
    attached: true,
}

const toBottom = () => {
    window.scroll({
        top: document.body.scrollHeight,
        behavior: 'instant'
    });
}

const conversationSlice = createSlice({
    name: 'conversation',
    initialState,
    reducers: {
        setAttached: (state, action: PayloadAction<boolean>) => {
            state.attached = action.payload;
        },
        selectConversation: (state, action: PayloadAction<Conversation|null>) => {
            state.selectedConversation = action.payload;
            if (state.attached) {
                toBottom();
            }
        },
        setSectionSelectedMessage: (state, action: PayloadAction<{ sectionIndex: number, messageId: string }>) => {
            setSectionSelectedMessageAction(state, action.payload);
        },
        handleAssistantMessage: (state, action: PayloadAction<AssistantMessageEvent>) => {
            handleAssistantMessageAction(state, action.payload);
            if (state.attached) {
                toBottom();
            }
        },
        handleAssistantMessageDelta: (state, action: PayloadAction<AssistantMessageDeltaEvent>) => {
            handleAssistantMessageDeltaAction(state, action.payload);
            if (state.attached) {
                toBottom();
            }
        },
        handleAssistantUsage: (state, action: PayloadAction<AssistantUsageEvent>) => {
            handleAssistantUsageAction(state, action.payload);
            if (state.attached) {
                toBottom();
            }
        },
        handleUserMessage: (state, action: PayloadAction<UserMessageEvent>) => {
            handleUserMessageAction(state, action.payload);
            if (state.attached) {
                toBottom();
            }
        }
    },
})

export const {
    setAttached,
    selectConversation,
    setSectionSelectedMessage,
    handleAssistantMessage,
    handleAssistantMessageDelta,
    handleAssistantUsage,
    handleUserMessage
} = conversationSlice.actions

export default conversationSlice.reducer