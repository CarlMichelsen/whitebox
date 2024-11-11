import {FC, useEffect} from "react";
import {useAppDispatch, useAppSelector} from "../../hooks.ts";
import {ConversationOption, ConversationOptionStructure} from "../../model/sidebar/conversationOption.ts";
import {setConversations} from "../../state/sidebar";
import ConversationDateSection from "./ConversationDateSection.tsx";
import {v4 as uuidV4} from "uuid";

function getDateDaysAgo(daysAgo: number): Date {
    const now = new Date();
    // Create a new Date instance to manipulate
    const pastDate = new Date(now);
    // Subtract the specified number of days
    pastDate.setDate(now.getDate() - daysAgo);
    return pastDate;
}

const ConversationSelector: FC = () => {
    const sidebar = useAppSelector(store => store.sidebar)
    const dispatch = useAppDispatch()

    useEffect(() => {
        console.log("once");
        const conversations: ConversationOption[] = [
            {
                id: uuidV4(),
                title: "Told about pepe",
                lastAltered: getDateDaysAgo(0).getTime(),
            },
            {
                id: uuidV4(),
                title: "Told about memes",
                lastAltered: getDateDaysAgo(1).getTime(),
            },
            {
                id: uuidV4(),
                title: "Hello there general kenobi",
                lastAltered: getDateDaysAgo(6).getTime(),
            },
            {
                id: uuidV4(),
                title: "Star Wars!",
                lastAltered: getDateDaysAgo(15).getTime(),
            },
            {
                id: uuidV4(),
                title: "Star Trek",
                lastAltered: getDateDaysAgo(57).getTime(),
            },
            {
                id: uuidV4(),
                title: "Meme",
                lastAltered: getDateDaysAgo(67).getTime(),
            },
            {
                id: uuidV4(),
                title: "Asshat",
                lastAltered: getDateDaysAgo(167).getTime(),
            },
            {
                id: uuidV4(),
                title: "Asshats are fun",
                lastAltered: getDateDaysAgo(367).getTime(),
            },
            {
                id: uuidV4(),
                title: "yeehaw",
                lastAltered: getDateDaysAgo(377).getTime(),
            },
            {
                id: uuidV4(),
                title: "stopit",
                lastAltered: getDateDaysAgo(397).getTime(),
            }
        ]

        dispatch(setConversations(conversations));
    }, []);
    
    return sidebar.conversationStructure !== null ? (
        <ol className="mx-1 space-y-2">
            {Object.keys(sidebar.conversationStructure).map(key => key as keyof ConversationOptionStructure).filter(key => sidebar.conversationStructure![key].length > 0).map(key => (
                <li className="border border-color rounded-sm" key={key}>
                    <ConversationDateSection section={key} options={sidebar.conversationStructure![key]}/>
                </li>
            ))}
        </ol>
    ) : null;
}

export default ConversationSelector