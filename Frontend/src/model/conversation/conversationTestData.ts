import {Conversation} from "./conversation.ts";

/*const createMessage = (id: string, previousMessageId: string|null, isBot: boolean, text: string): ConversationMessage => {
    const bot: LlmModel = {
        provider: "OpenAi",
        modelName: "GPT-4o",
        modelDescription: "Fake description",
        modelIdentifier: "fake-model-identifier"
    };
    
    return {
        id,
        previousMessageId,
        aiModel: isBot ? bot : null,
        content: [{id: "identifier", type:"text", value: text, sortOrder: 1}],
        createdUtc: new Date(2024, 10, 19, 11, 1, 0, 0).getTime()
    }
}*/

export const conversationTestData: Conversation|null = null;


/* = {
    id: "1",
    creator: {
        id: "1"
        authenticationMethod: "1"
        username: string
        avatarUrl: string
        authenticationId: string
        email: string
    },
    systemMessage: "Do as you're told please!",
    summary: null,
    sections: [
        {
            selectedMessageId: "1",
            messages: {
                "1": createMessage("1", null, false, "Hello"),
                "3": createMessage("3", null, false, "Go away!")
            }
        },
        {
            selectedMessageId: "2",
            messages: {
                "2": createMessage("2", "1", true, "Hi there! How can i help you?"),
                "4": createMessage("4", "3", true, "Alright, let me know if you need anything.")
            }
        },
        {
            selectedMessageId: "5",
            messages: {
                "5": createMessage("5", "2", false, "I would like to hear a joke please")
            }
        },
        {
            selectedMessageId: "6",
            messages: {
                "6": createMessage("6", "5", true, "Sure, here's one for you:\n" +
                    "\n" +
                    "Why don't skeletons fight each other?\n" +
                    "\n" +
                    "Because they don't have the guts!")
            }
        },
        {
            selectedMessageId: "7",
            messages: {
                "7": createMessage("7", "6", false, "Very funny.")
            }
        },
        {
            selectedMessageId: "8",
            messages: {
                "8": createMessage("8", "7", true, "Why thankyou")
            }
        },
    ],
    lastAltered: new Date(2024, 10, 19, 11, 0, 0, 0).getTime(),
    created: new Date(2024, 10, 14, 11, 0, 0, 0).getTime()
}*/