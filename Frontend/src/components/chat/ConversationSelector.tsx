import {FC} from "react";
import {useAppSelector} from "../../hooks.ts";
import {ConversationOptionStructure} from "../../model/sidebar/conversationOption.ts";
import ConversationDateSection from "./ConversationDateSection.tsx";

const ConversationSelector: FC = () => {
    const sidebar = useAppSelector(store => store.sidebar)
    
    return sidebar.conversationStructure !== null ? (
        <ol className="mx-1 space-y-2">
            {Object.keys(sidebar.conversationStructure)
                .map(key => key as keyof ConversationOptionStructure)
                .filter(key => sidebar.conversationStructure![key].length > 0)
                .map(key => (
                    <li key={key}>
                        <ConversationDateSection section={key} options={sidebar.conversationStructure![key]}/>
                    </li>
            ))}
        </ol>
    ) : null;
}

export default ConversationSelector