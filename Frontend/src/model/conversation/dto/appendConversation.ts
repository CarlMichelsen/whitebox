export type ReplyTo = {
    conversationId: string;
    replyToMessageId: string|null;
}

export type AppendConversation = {
    replyTo: ReplyTo|null;
    text: string;
}