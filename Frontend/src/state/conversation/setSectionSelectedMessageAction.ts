import { ConversationState } from "./index";
import {ConversationMessage} from "../../model/conversation/conversation.ts";

export const setSectionSelectedMessageAction = (state: ConversationState, action: { sectionIndex: number, messageId: string }) => {
    if (state.selectedConversation === null) {
        return;
    }

    const section = state.selectedConversation.sections[action.sectionIndex] ?? null;
    if (section === null) {
        return;
    }

    if (!!section.messages[action.messageId]) {
        section.selectedMessageId = action.messageId;
    }

    for (let i = action.sectionIndex + 1; i < state.selectedConversation.sections.length; i++) {
        const prevSection = state.selectedConversation.sections[i-1];
        const section = state.selectedConversation.sections[i]!;

        if (prevSection.selectedMessageId === null) {
            section.selectedMessageId = null;
            continue;
        }
        
        let max = -1;
        let newestMsg: ConversationMessage|null = null;
        for (const msgId in section.messages) {
            const msg = section.messages[msgId]
            if (msg.previousMessageId === prevSection.selectedMessageId) {
                if (msg.createdUtc > max) {
                    max = msg.createdUtc;
                    newestMsg = msg;
                }
            }
        }
        
        section.selectedMessageId = newestMsg?.id ?? null;
    }
}