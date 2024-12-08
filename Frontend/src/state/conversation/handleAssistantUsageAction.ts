import {ConversationState} from "./index.ts";
import {AssistantUsageEvent} from "../../model/conversation/dto/conversationStream.ts";

export const handleAssistantUsageAction = (state: ConversationState, payload: AssistantUsageEvent): void => {
    if (state.selectedConversation?.id !== payload.conversationId) {
        return;
    }
}