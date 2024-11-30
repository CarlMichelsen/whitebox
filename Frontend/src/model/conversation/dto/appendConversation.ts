export type ReplyTo = {
    conversationId: string;
    replyToMessageId: string;
}

export type AppendConversation = {
    replyTo: ReplyTo|null;
    text: string;
}