import {FC} from "react";
import {ConversationMessage} from "../../../../model/conversation/conversation.ts";
import BotAvatar from "../BotAvatar.tsx";
import {escapeHTML, whiteBoxMarked} from "../../../../util/helpers/marked.ts";

type BotMessageProps = {
    message: ConversationMessage;
}

const BotMessage: FC<BotMessageProps> = ({ message }) => {
    const text = escapeHTML(message.content.map(c => c.value).join('\n'));
    return (
        <div id={"message-"+message.id}
             className="grid sm:grid-cols-[auto_1fr] sm:grid-rows-1 grid-cols-1 grid-rows-[auto_1fr] gap-1 sm:gap-2 mt-6">
            <BotAvatar llmModel={message.aiModel!} />
            <div>
                <span dangerouslySetInnerHTML={({__html: whiteBoxMarked.parse(text) as string})}></span>

                {message.usage ? (
                    <p className="text-xs opacity-65 mt-1">{message.usage.specificModelIdentifier} ({message.usage.inputTokens} {"->"} {message.usage.outputTokens})</p>
                ) : null}
            </div>
        </div>
    )
}

export default BotMessage;