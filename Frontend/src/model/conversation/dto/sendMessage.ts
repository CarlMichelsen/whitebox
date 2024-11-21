export type ReplyTo = {
    conversationId: string;
    replyTo: string;
}

export type SendMessage = {
    replyTo: ReplyTo;
    text: string;
}