import {createSlice, PayloadAction} from '@reduxjs/toolkit'
import {Conversation, ConversationSection} from "../../model/conversation/conversation.ts";
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

const toLastMessage = (sections: ConversationSection[]) => {
    let messageId: string|null = null;
    for (let i = 0; i < sections.length; i++) {
        const section: ConversationSection = sections[i];
        if (section.selectedMessageId !== null) {
            messageId = section.messages[section.selectedMessageId].id;
        }
    }
    
    if (!messageId) {
        return;
    }

    setTimeout(() => {
        const messageDomId = `message-${messageId}`;
        const messageDomElement = document.getElementById(messageDomId) as HTMLDivElement;
        messageDomElement?.scrollIntoView({
            behavior: 'instant',
            block: 'end',
            inline: 'start'
        });
        window.scrollBy(0, 100);
    }, 0);
}

const conversationSlice = createSlice({
    name: 'conversation',
    initialState,
    reducers: {
        setAttached: (state, action: PayloadAction<boolean>) => {
            state.attached = action.payload;
        },
        deleteMessage: (_, _1: PayloadAction<{conversationId: string, messageId: string}>) => {
            /*handleUserMessageAction(state, action.payload);
            if (state.attached && state.selectedConversation) {
                toLastMessage(state.selectedConversation.sections);
            }*/
            console.warn("deleteMessage not implemented!");
        },
        selectConversation: (state, action: PayloadAction<Conversation|null>) => {
            state.selectedConversation = action.payload;
            history.replaceState({}, '', action.payload?.id ? `/c/${action.payload?.id}` : '/c');
            if (state.attached && action.payload) {
                toLastMessage(action.payload.sections);
            }
        },
        setSectionSelectedMessage: (state, action: PayloadAction<{ sectionIndex: number, messageId: string }>) => {
            setSectionSelectedMessageAction(state, action.payload);
        },
        handleAssistantMessage: (state, action: PayloadAction<AssistantMessageEvent>) => {
            handleAssistantMessageAction(state, action.payload);
            if (state.attached && state.selectedConversation) {
                toLastMessage(state.selectedConversation.sections);
            }
        },
        handleAssistantMessageDelta: (state, action: PayloadAction<AssistantMessageDeltaEvent>) => {
            handleAssistantMessageDeltaAction(state, action.payload);
            if (state.attached && state.selectedConversation) {
                toLastMessage(state.selectedConversation.sections);
            }
        },
        handleAssistantUsage: (state, action: PayloadAction<AssistantUsageEvent>) => {
            handleAssistantUsageAction(state, action.payload);
            if (state.attached && state.selectedConversation) {
                toLastMessage(state.selectedConversation.sections);
            }
        },
        handleUserMessage: (state, action: PayloadAction<UserMessageEvent>) => {
            handleUserMessageAction(state, action.payload);
            if (state.attached && state.selectedConversation) {
                toLastMessage(state.selectedConversation.sections);
            }
        }
    },
})

export const {
    setAttached,
    deleteMessage,
    selectConversation,
    setSectionSelectedMessage,
    handleAssistantMessage,
    handleAssistantMessageDelta,
    handleAssistantUsage,
    handleUserMessage
} = conversationSlice.actions

export default conversationSlice.reducer