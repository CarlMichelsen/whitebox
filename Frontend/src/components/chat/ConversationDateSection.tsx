import {FC} from "react";
import {ConversationOption, ConversationOptionStructure} from "../../model/sidebar/conversationOption";
import ConversationCard from "./ConversationCard";

type ConversationDateSectionProps = {
    section: keyof ConversationOptionStructure
    options: ConversationOption[]
}

const sectionTitleMap: Record<keyof ConversationOptionStructure, string> = {
    today: "Today",
    yesterday: "Yesterday",
    thisWeek: "This Week",
    thisMonth: "This Month",
    lastMonth: "Last Month",
    thisYear: "This Year",
    olderThanAYear: "Older"
}

const ConversationDateSection: FC<ConversationDateSectionProps> = ({ section, options }) => {
    
    return (
        <>
            <h3 className="pl-1">
                {sectionTitleMap[section]}
            </h3>
            <ol className="space-y-0.5 py-1">
                {options.map(o => (
                    <li key={o.id}>
                        <ConversationCard option={o}/>
                    </li>
                ))}
            </ol>
        </>
    )
}

export default ConversationDateSection;