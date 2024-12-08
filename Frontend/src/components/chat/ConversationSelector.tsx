import {FC} from "react";
import {useAppSelector} from "../../hooks.ts";
import {ConversationOptionSection} from "../../model/sidebar/conversationOption.ts";
import ConversationCard from "./ConversationCard.tsx";

const ConversationSelector: FC = () => {
    const sidebar = useAppSelector(store => store.sidebar)
    
    return sidebar.conversationSections !== null ? (
        <ol className="mx-1 space-y-2">
            {sidebar.conversationSections
                .filter((section: ConversationOptionSection) => section.options.length > 0)
                .map((section: ConversationOptionSection) => (
                <li key={section.title}>
                    <h3 className="pl-1">
                        {section.title}
                    </h3>
                    
                    <ol className="space-y-0.5 pl-2 pr-1">
                        {section.options.map(o => (
                            <li key={o.id}>
                                <ConversationCard option={o}/>
                            </li>
                        ))}
                    </ol>
                </li>
            ))}
        </ol>
    ) : null;
}

export default ConversationSelector