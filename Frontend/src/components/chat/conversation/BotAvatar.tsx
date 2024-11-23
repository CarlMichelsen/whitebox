import {FC} from "react";
import {Bot} from "../../../model/conversation/conversation.ts";

type BotAvatarProps = {
    bot: Bot;
}

const BotAvatar: FC<BotAvatarProps> = ({ bot }) => {
    
    return (
        <img
            className="aspect-square shadow-xl w-8 sm:w-10 dark:bg-neutral-700 bg-neutral-300 rounded-md sm:rounded-lg px-0.5 py-0.5 sm:px-1.5 sm:py-1.5"
            src={bot.iconUrl}
            alt="bot"/>
    )
}

export default BotAvatar