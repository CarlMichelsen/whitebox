import { ConversationState } from "./index";

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