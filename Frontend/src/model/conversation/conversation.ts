import {LlmModel} from "./llmModel.ts";
import {AuthenticatedUser} from "../user.ts";

export type MessageContent = {
    id: string;
    type: "text";
    value: string;
    sortOrder: number;
}

export type ConversationMessage = {
    id: string;
    previousMessageId: string|null;
    aiModel: LlmModel|null;
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
    systemMessage: string;
    summary: string|null;
    sections: ConversationSection[];
    lastAltered: number;
    created: number;
}