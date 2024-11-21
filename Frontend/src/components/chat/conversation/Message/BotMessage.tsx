import {FC} from "react";
import {ConversationMessage} from "../../../../model/conversation/conversation.ts";
import BotAvatar from "../BotAvatar.tsx";

type BotMessageProps = {
    message: ConversationMessage;
}

const BotMessage: FC<BotMessageProps> = ({ message }) => {
    return (
        <div id={"message-"+message.id}
             className="grid grid-cols-[auto_1fr] gap-1 sm:gap-2">
            <BotAvatar bot={message.bot!} />
            <pre className="message-text">{message.text}</pre>
        </div>
    )
}

export default BotMessage;