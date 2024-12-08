import {ConversationState} from "./index";
import {AssistantMessageEvent} from "../../model/conversation/dto/conversationStream";

export const handleAssistantMessageAction = (state: ConversationState, payload: AssistantMessageEvent): void => {
    if (state.selectedConversation?.id !== payload.conversationId) {
        return;
    }
    
}