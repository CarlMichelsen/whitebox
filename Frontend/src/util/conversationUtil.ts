import {Conversation, ConversationMessage, ConversationSection} from "../model/conversation/conversation";

export const findMessage = (c: Conversation, messageId: string): ConversationMessage|null => {
    for (const section of c.sections) {
        if (!!section.messages[messageId]) {
            return section.messages[messageId];
        }
    }
    
    return null;
}

export const getLatestSelectedMessage = (c: Conversation): ConversationMessage|null => {
    let message: ConversationMessage|null = null;

    for (let i = 0; i < c.sections.length; i++) {
        const section: ConversationSection|null = c.sections[i] ?? null;
        if (section === null) {
            return message;
        }
        
        if (message?.nextMessageId === null) {
            return message;
        }
        
        message = section.messages[section.selectedMessageId] ?? null;
    }
    
    return message;
}