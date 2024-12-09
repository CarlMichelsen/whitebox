
import {
    Conversation,
    ConversationMessage,
    ConversationSection, MessageContent,
    MessageUsage
} from "../../model/conversation/conversation.ts";

export const findSectionIdOfMessage = (conversation: Conversation, messageId: string): number => {
    return conversation.sections.findIndex(s => !!s.messages[messageId]);
}

export const appendExistingMessage = (conversation: Conversation, messageId: string, contentId: string, contentType: string, sortOrder: number, delta: string): number => {
    const sectionId = findSectionIdOfMessage(conversation, messageId);
    if (sectionId === -1) {
        throw new Error(
            "Unable to find section of message to append on even tho the message should exist");
    }

    const message = conversation.sections[sectionId].messages[messageId] ?? null;
    if (!message) {
        throw new Error(
            "Unable to find message to append even tho the message should exist");
    }
    
    let content = message.content.find(c => c.id === contentId) ?? null;
    if (!content) {
        content = {
            id: contentId,
            type: contentType,
            value: delta,
            sortOrder: sortOrder,
        } satisfies MessageContent;
        message.content.push(content);
        message.content.sort((a, b) => b.sortOrder - a.sortOrder);
    } else {
        content.value += delta;
        if (content.sortOrder != sortOrder) {
            content.sortOrder = sortOrder;
            message.content.sort((a, b) => b.sortOrder - a.sortOrder);
        }
    }
    
    return sectionId;
}

export const setUsageOfMessage = (conversation: Conversation, messageId: string, usage: MessageUsage|null): number => {
    const sectionId = findSectionIdOfMessage(conversation, messageId);
    if (sectionId === -1) {
        throw new Error(
            "Unable to find section of message to set usage on even tho the message should exist");
    }
    
    const message = conversation.sections[sectionId].messages[messageId] ?? null;
    if (!message) {
        throw new Error(
            "Unable to find message to set usage on even tho the message should exist");
    }

    message.usage = usage;
    
    return sectionId;
}

export const appendMessage = (conversation: Conversation, message: ConversationMessage): number => {
    if (message.previousMessageId == null) {
        let section: ConversationSection | null = conversation.sections[0] ?? null;
        if (!section) {
            section = {
                selectedMessageId: message.id,
                messages: { [message.id]: message },
            } satisfies ConversationSection
            conversation.sections.push(section);
        } else {
            section.messages[message.id] = message;
            section.selectedMessageId = message.id;
        }
        
        return 0;
    } else {
        const previousSectionIndex = findSectionIdOfMessage(conversation, message.previousMessageId);
        if (previousSectionIndex === -1) {
            throw new Error(
                "Unable to find message to reply to even tho the message should exist");
        }
        
        let nextSection: ConversationSection | null = conversation.sections[previousSectionIndex + 1] ?? null;
        if (!nextSection) {
            nextSection = {
                selectedMessageId: message.id,
                messages: { [message.id]: message },
            } satisfies ConversationSection
            conversation.sections.push(nextSection);
        } else {
            nextSection.messages[message.id] = message;
            nextSection.selectedMessageId = message.id;
        }
        
        return previousSectionIndex + 1;
    }
}