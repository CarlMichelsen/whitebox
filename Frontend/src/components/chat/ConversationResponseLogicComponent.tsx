import {forwardRef, useImperativeHandle} from "react";
import {ConversationMessage} from "../../model/conversation/conversation.ts";
import {findPreviousMessage, getLatestSelectedMessage} from "../../util/conversationUtil.ts";
import {ConversationClient} from "../../util/clients/conversationClient.ts";
import {AppendConversation, ReplyTo} from "../../model/conversation/dto/appendConversation.ts";
import {setInputState} from "../../state/input";
import {useAppDispatch, useAppSelector} from "../../hooks.ts";
import {
    ConversationEvent,
    SetSummaryEvent,
    StreamEvent
} from "../../model/conversation/dto/conversationStream.ts";
import {selectConversation} from "../../state/conversation";
import {addConversation} from "../../state/sidebar";
import {ConversationOption} from "../../model/sidebar/conversationOption.ts";

export type ConversationResponseLogicComponentProps = {
    onSend: (text: string) => Promise<void>;
}

const ConversationResponseLogicComponent = forwardRef<ConversationResponseLogicComponentProps>((_, ref) => {
    useImperativeHandle(ref, () => ({ onSend }));
    const conversation = useAppSelector(state => state.conversation);
    const input = useAppSelector(state => state.input);
    const dispatch = useAppDispatch();

    const handle = async (chunk: StreamEvent) => {
        if (input.inputState === "ready") {
            dispatch(setInputState("receiving"));
        }
        
        console.warn(chunk)
        switch (chunk.type) {
            case "Conversation":
                const conversationEvent = chunk as ConversationEvent;
                const client = new ConversationClient();
                const conversationResponse = await client
                    .getConversation(conversationEvent.conversationId)
                
                if (conversationResponse.ok && conversationResponse.value) {
                    dispatch(selectConversation(conversationResponse.value));
                }
                break;

            case "SetSummary":
                const setSummaryEvent = chunk as SetSummaryEvent;
                const conversationOption: ConversationOption = {
                    id: setSummaryEvent.conversationId,
                    title: setSummaryEvent.summary,
                    lastAltered: new Date().getTime()
                }
                dispatch(addConversation(conversationOption))
                break;

            case "Error":
                console.error("Handling ErrorEvent", chunk);
                break;

            case "Ping":
                console.log("Ping");
                break;

            case "AssistantMessage":
                //const assistantMessage = chunk as AssistantMessageEvent;
                
                console.log("Handling AssistantMessageEvent", chunk);
                break;

            case "AssistantMessageDelta":
                console.log("Handling AssistantMessageDeltaEvent", chunk);
                break;

            case "AssistantUsage":
                console.log("Handling AssistantUsageEvent", chunk);
                break;

            case "UserMessage":
                console.log("Handling UserMessageEvent", chunk);
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
                    : null
            } else {
                replyToMessage = conversation.selectedConversation
                    ? getLatestSelectedMessage(conversation.selectedConversation)
                    : null;
            }

            const client = new ConversationClient();
            const replyToObject: ReplyTo|null = !!replyToMessage
                ? { conversationId: conversation.selectedConversation!.id, replyToMessageId: replyToMessage.id }
                : null;

            const appendConversation: AppendConversation = {
                replyTo: replyToObject,
                text,
            }
            console.error(appendConversation)
            const appendPromise = client.appendConversation(appendConversation, handle)
            dispatch(setInputState("sending"))

            await appendPromise;
            dispatch(setInputState("ready"));
        }
    }
    
    return null;
});

export default ConversationResponseLogicComponent;