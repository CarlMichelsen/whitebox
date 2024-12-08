import {ConversationState} from "./index.ts";
import {AssistantMessageDeltaEvent} from "../../model/conversation/dto/conversationStream.ts";

export const handleAssistantMessageDeltaAction = (state: ConversationState, payload: AssistantMessageDeltaEvent): void => {
    if (state.selectedConversation?.id !== payload.conversationId) {
        return;
    }
}