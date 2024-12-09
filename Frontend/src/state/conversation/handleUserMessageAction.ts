import {ConversationState} from "./index";
import {UserMessageEvent} from "../../model/conversation/dto/conversationStream";
import {appendMessage} from "./shared.ts";
import {setSectionSelectedMessageAction} from "./setSectionSelectedMessageAction.ts";

export const handleUserMessageAction = (state: ConversationState, payload: UserMessageEvent): void => {
    if (state.selectedConversation?.id !== payload.conversationId) {
        return;
    }

    const sectionIndex = appendMessage(state.selectedConversation, payload.message);
    setSectionSelectedMessageAction(state, { sectionIndex, messageId: payload.message.id });
}