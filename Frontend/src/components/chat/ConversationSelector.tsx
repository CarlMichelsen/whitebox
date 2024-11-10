import {FC} from "react";
import ConversationCard from "./ConversationCard";

const ConversationSelector: FC = () => {
    
    return (
        <ol className="mx-1 space-y-0.5">
            <li><ConversationCard tempTitle="Help with stuff" /></li>
            <li><ConversationCard tempTitle="Make me a game about memes" /></li>
            <li><ConversationCard tempTitle="Why is this a problem?" /></li>
            <li><ConversationCard tempTitle="I don't know anything about anything hahah please help" /></li>
        </ol>
    )
}

export default ConversationSelector