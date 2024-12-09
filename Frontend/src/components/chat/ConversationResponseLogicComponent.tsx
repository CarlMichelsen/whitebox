import {forwardRef, useImperativeHandle} from "react";
import {ConversationMessage} from "../../model/conversation/conversation.ts";
import {findPreviousMessage, getLatestSelectedMessage} from "../../util/conversationUtil.ts";
import {ConversationClient} from "../../util/clients/conversationClient.ts";
import {AppendConversation, ReplyTo} from "../../model/conversation/dto/appendConversation.ts";
import {setInputState, setText} from "../../state/input";
import {useAppDispatch, useAppSelector} from "../../hooks.ts";
import {
    AssistantMessageDeltaEvent,
    AssistantMessageEvent, AssistantUsageEvent,
    ConversationEvent,
    SetSummaryEvent,
    StreamEvent, UserMessageEvent
} from "../../model/conversation/dto/conversationStream.ts";
import {
    handleAssistantMessage, handleAssistantMessageDelta,
    handleAssistantUsage,
    handleUserMessage,
    selectConversation
} from "../../state/conversation";
import {alteredNow, upsertSummary} from "../../state/sidebar";

export type ConversationResponseLogicComponentProps = {
    onSend: (text: string) => Promise<void>;
}

const ConversationResponseLogicComponent = forwardRef<ConversationResponseLogicComponentProps>((_, ref) => {
    useImperativeHandle(ref, () => ({ onSend }));
    const conversation = useAppSelector(state => state.conversation);
    const input = useAppSelector(state => state.input);
    const dispatch = useAppDispatch();

    const handle = async (chunk: StreamEvent) => {
        dispatch(setInputState("receiving"));
        
        switch (chunk.type) {
            case "Conversation":
                const conversationEvent = chunk as ConversationEvent;
                if (conversationEvent.conversationId === conversation.selectedConversation?.id) {
                    dispatch(alteredNow(conversationEvent.conversationId))
                    break;
                }
                
                const client = new ConversationClient();
                const conversationResponse = await client
                    .getConversation(conversationEvent.conversationId)
                
                if (conversationResponse.ok && conversationResponse.value) {
                    dispatch(selectConversation(conversationResponse.value));
                }
                break;

            case "SetSummary":
                const setSummaryEvent = chunk as SetSummaryEvent;
                dispatch(upsertSummary(setSummaryEvent))
                break;

            case "Error":
                console.error("Handling ErrorEvent", chunk);
                dispatch(setText(input.previousMessage ?? ""))
                break;

            case "Ping":
                console.log("Ping");
                break;

            case "AssistantMessage":
                const assistantMessage = chunk as AssistantMessageEvent;
                dispatch(handleAssistantMessage(assistantMessage));
                break;

            case "AssistantMessageDelta":
                const assistantMessageDelta = chunk as AssistantMessageDeltaEvent;
                dispatch(handleAssistantMessageDelta(assistantMessageDelta));
                break;

            case "AssistantUsage":
                const assistantUsage = chunk as AssistantUsageEvent;
                dispatch(handleAssistantUsage(assistantUsage));
                break;

            case "UserMessage":
                const userMessage = chunk as UserMessageEvent;
                dispatch(handleUserMessage(userMessage));
                break;

            default:
                console.error(`Unknown type: ${(chunk.type as string)}`);
                break;
        }
    }
    
    const onSend = async (text: string) => {
        if (input.inputState === "ready" && text.length > 1) {
            let replyToMessage: ConversationMessage | null = null;
            if (input.editingMessage !== null && conversation.selectedConversation !== null) {
                replyToMessage = input.editingMessage
                    ? findPreviousMessage(conversation.selectedConversation, input.editingMessage)
                    : null;
            } else {
                replyToMessage = conversation.selectedConversation
                    ? getLatestSelectedMessage(conversation.selectedConversation)
                    : null;
            }

            const client = new ConversationClient();
            let replyToObject: ReplyTo|null = !!replyToMessage
                ? { conversationId: conversation.selectedConversation!.id, replyToMessageId: replyToMessage.id }
                : null;
            
            // Handle editing root message.
            if (replyToObject === null && input.editingMessage !== null && conversation.selectedConversation !== null) {
                replyToObject = {
                    conversationId: conversation.selectedConversation!.id,
                    replyToMessageId: null
                };
            }
            
            const appendConversation: AppendConversation = {
                replyTo: replyToObject,
                text,
            }
            
            const appendPromise = client.appendConversation(appendConversation, handle);
            dispatch(setInputState("sending"));

            await appendPromise;
            dispatch(setInputState("ready"));
        }
    }
    
    return null;
});

export default ConversationResponseLogicComponent;