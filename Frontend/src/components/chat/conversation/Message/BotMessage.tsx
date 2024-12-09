import {FC} from "react";
import {ConversationMessage} from "../../../../model/conversation/conversation.ts";
import BotAvatar from "../BotAvatar.tsx";
import {marked} from "marked";

type BotMessageProps = {
    message: ConversationMessage;
}

const BotMessage: FC<BotMessageProps> = ({ message }) => {
    const text = message.content.map(c => c.value).join('\n');
    return (
        <div id={"message-"+message.id}
             className="grid grid-cols-[auto_1fr] gap-1 sm:gap-2 mt-6">
            <BotAvatar llmModel={message.aiModel!} />
            <span dangerouslySetInnerHTML={({__html: marked.parse(text) as string})}></span>
        </div>
    )
}

export default BotMessage;