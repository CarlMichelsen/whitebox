import {Conversation, ConversationMessage, ConversationSection} from "../model/conversation/conversation";

export const findMessage = (c: Conversation, messageId: string): ConversationMessage|null => {
    for (const section of c.sections) {
        const message = section.messages[messageId];
        if (!!message) {
            return message;
        }
    }
    
    return null;
}

export const findPreviousMessage = (c: Conversation, messageId: string): ConversationMessage|null => {
    for (let i = 0; i < c.sections.length; i++) {
        const section = c.sections[i];
        const message = section.messages[messageId];
        
        if (!!message) {
            const prevSection = c.sections[i - 1]
            if (!prevSection) {
                return null;
            }
            
            if (message.previousMessageId === null) {
                return null;
            }
            
            return prevSection.messages[message.previousMessageId] ?? null;
        }
    }

    return null;
}

export const getLatestSelectedMessage = (c: Conversation): ConversationMessage|null => {
    let message: ConversationMessage|null = null;

    for (let i = c.sections.length-1; i > 0; i--) {
        const section: ConversationSection = c.sections[i]!;
        if (section.selectedMessageId === null) {
            continue;
        }
        
        message = section.messages[section.selectedMessageId] ?? null;
        break;
    }
    
    return message;
}