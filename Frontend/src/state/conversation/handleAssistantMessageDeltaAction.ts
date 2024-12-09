import {ConversationState} from "./index.ts";
import {AssistantMessageDeltaEvent} from "../../model/conversation/dto/conversationStream.ts";
import {appendExistingMessage} from "./shared.ts";

export const handleAssistantMessageDeltaAction = (state: ConversationState, payload: AssistantMessageDeltaEvent): void => {
    if (state.selectedConversation?.id !== payload.conversationId) {
        return;
    }

    appendExistingMessage(
        state.selectedConversation,
        payload.messageId,
        payload.contentId,
        payload.contentType,
        payload.sortOrder,
        payload.contentDelta);
}