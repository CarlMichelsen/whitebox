import {ConversationState} from "./index";
import {AssistantMessageEvent} from "../../model/conversation/dto/conversationStream";
import {appendMessage} from "./shared.ts";
import {ConversationMessage} from "../../model/conversation/conversation.ts";

export const handleAssistantMessageAction = (state: ConversationState, payload: AssistantMessageEvent): void => {
    if (state.selectedConversation?.id !== payload.conversationId) {
        return;
    }
    
    const initialAssistantMessage = {
        id: payload.messageId,
        previousMessageId: payload.replyToMessageId,
        aiModel: payload.model,
        usage: null,
        content: [],
        createdUtc: new Date().getTime(),
    } satisfies ConversationMessage;

    appendMessage(state.selectedConversation, initialAssistantMessage);
}