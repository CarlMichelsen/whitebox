import {ConversationMessage, MessageUsage} from "../conversation.ts";
import {LlmModel} from "../llmModel.ts";

export type ConversationEventType =
    "Conversation"
    |"SetSummary"
    |"Error"
    |"Ping"
    |"AssistantMessage"
    |"AssistantMessageDelta"
    |"AssistantUsage"
    |"UserMessage";

export type StreamEvent = {
    type: ConversationEventType;
    conversationId: string;
}

export type ConversationEvent = Omit<StreamEvent, 'type'> & {
    type: ConversationEventType[0];
}

export type SetSummaryEvent = Omit<StreamEvent, 'type'> & {
    type: ConversationEventType[1];
    summary: string;
}

export type ErrorEvent = Omit<StreamEvent, 'type'> & {
    type: ConversationEventType[2];
    error: string;
}

export type PingEvent = Omit<StreamEvent, 'type'> & {
    type: ConversationEventType[3];
}

export type AssistantMessageEvent = Omit<StreamEvent, 'type'> & {
    type: ConversationEventType[4];
    messageId: string;
    replyToMessageId: string;
    model: LlmModel;
}

export type AssistantMessageDeltaEvent = Omit<StreamEvent, 'type'> & {
    type: ConversationEventType[5];
    messageId: string;
    contentId: string;
    contentType: string;
    sortOrder: number;
    contentDelta: string;
}

export type AssistantUsageEvent = Omit<StreamEvent, 'type'> & {
    type: ConversationEventType[6];
    messageId: string;
    usage: MessageUsage;
}

export type UserMessageEvent = Omit<StreamEvent, 'type'> & {
    type: ConversationEventType[7];
    message: ConversationMessage;
}