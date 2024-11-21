﻿import {Bot, Conversation, ConversationMessage} from "./conversation.ts";

const createMessage = (id: string, nextMessageId: string|null, isBot: boolean, text: string): ConversationMessage => {
    const bot: Bot = { botName: "Bot", iconUrl: "" };
    
    return {
        id,
        nextMessageId,
        bot: isBot ? bot : null,
        text,
        media: [],
        created: new Date(2024, 10, 19, 11, 1, 0, 0).getTime()
    }
}

export const conversationTestData: Conversation = {
    id: "1",
    creatorId: "1",
    systemMessage: "Do as you're told please!",
    summary: null,
    sections: [
        {
            selectedMessageId: "1",
            messages: {
                "1": createMessage("1", "2", false, "Hello"),
                "3": createMessage("3", "4", false, "Go away!")
            }
        },
        {
            selectedMessageId: "2",
            messages: {
                "2": createMessage("2", "5", true, "Hi there! How can i help you?"),
                "4": createMessage("4", null, true, "Alright, let me know if you need anything.")
            }
        },
        {
            selectedMessageId: "5",
            messages: {
                "5": createMessage("5", "6", false, "I would like to hear a joke please")
            }
        },
        {
            selectedMessageId: "6",
            messages: {
                "6": createMessage("6", "7", true, "Sure, here's one for you:\n" +
                    "\n" +
                    "Why don't skeletons fight each other?\n" +
                    "\n" +
                    "Because they don't have the guts!")
            }
        },
        {
            selectedMessageId: "7",
            messages: {
                "7": createMessage("7", "8", false, "Very funny.")
            }
        },
        {
            selectedMessageId: "8",
            messages: {
                "8": createMessage("8", null, true, "Why thankyou")
            }
        },
    ],
    lastAltered: new Date(2024, 10, 19, 11, 0, 0, 0).getTime(),
    created: new Date(2024, 10, 14, 11, 0, 0, 0).getTime()
}