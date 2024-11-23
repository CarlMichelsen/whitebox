export type MessageMedia = {
    
}

export type Bot = {
    botName: string; 
    iconUrl: string;
}

export type ConversationMessage = {
    id: string;
    previousMessageId: string|null;
    bot: Bot|null;
    text: string;
    media: MessageMedia[];
    created: number;
}

export type ConversationSection = {
    selectedMessageId: string|null;
    messages: { [key: string]: ConversationMessage };
}

export type Conversation = {
    id: string;
    creatorId: string;
    systemMessage: string;
    summary: string|null;
    sections: ConversationSection[];
    lastAltered: number;
    created: number;
}