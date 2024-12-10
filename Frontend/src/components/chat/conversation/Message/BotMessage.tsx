import {FC} from "react";
import {ConversationMessage} from "../../../../model/conversation/conversation.ts";
import BotAvatar from "../BotAvatar.tsx";
import {formatDate} from "../../../../util/helpers/formatDate.ts";
import {whiteBoxMarked} from "../../../../util/helpers/marked.ts";

type BotMessageProps = {
    message: ConversationMessage;
}

const BotMessage: FC<BotMessageProps> = ({ message }) => {
    const createdDate = new Date(message.createdUtc);
    
    const text = message.content.map(c => c.value).join('\n');
    return (
        <div id={"message-"+message.id}
             className="grid grid-cols-[auto_1fr] gap-1 sm:gap-2 mt-6">
            <BotAvatar llmModel={message.aiModel!} />
            <div>
                <span dangerouslySetInnerHTML={({__html: whiteBoxMarked.parse(text) as string})}></span>
                
                <div className="grid grid-rows-2 sm:grid-rows-1 sm:grid-cols-[auto_1fr] sm:gap-4 text-xs opacity-25 mt-1">
                    <time
                        dateTime={createdDate.toISOString()}>
                        {formatDate(createdDate)}
                    </time>

                    {message.usage ? (
                        <p>{message.usage.specificModelIdentifier} ({message.usage.inputTokens} {"->"} {message.usage.outputTokens})</p>
                    ) : null}
                </div>
            </div>
        </div>
    )
}

export default BotMessage;