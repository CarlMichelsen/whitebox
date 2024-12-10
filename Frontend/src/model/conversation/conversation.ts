import {LlmModel} from "./llmModel.ts";
import {AuthenticatedUser} from "../user.ts";

export type MessageContent = {
    id: string;
    type: "text"|string;
    value: string;
    sortOrder: number;
}

export type MessageUsage = {
    id: string;
    inputTokens: number;
    outputTokens: number;
    initialModelIdentifier: string;
    specificModelIdentifier: string;
}

export type ConversationMessage = {
    id: string;
    previousMessageId: string|null;
    aiModel: LlmModel|null;
    usage: MessageUsage|null;
    content: MessageContent[];
    createdUtc: number;
}

export type ConversationSection = {
    selectedMessageId: string|null;
    messages: { [key: string]: ConversationMessage };
}

export type Conversation = {
    id: string;
    creator: AuthenticatedUser;
    systemMessage: string|null;
    summary: string|null;
    sections: ConversationSection[];
    lastAltered: number;
    created: number;
}