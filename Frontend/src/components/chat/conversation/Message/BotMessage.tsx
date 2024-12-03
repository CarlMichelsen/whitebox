import {FC} from "react";
import {ConversationMessage} from "../../../../model/conversation/conversation.ts";
import BotAvatar from "../BotAvatar.tsx";

type BotMessageProps = {
    message: ConversationMessage;
}

const BotMessage: FC<BotMessageProps> = ({ message }) => {
    return (
        <div id={"message-"+message.id}
             className="grid grid-cols-[auto_1fr] gap-1 sm:gap-2 mt-6">
            <BotAvatar llmModel={message.aiModel!} />
            <pre className="message-text">{message.content.map(c => c.value).join('\n')}</pre>
        </div>
    )
}

export default BotMessage;