import {BaseClient} from "../baseClient.ts";
import {hostUrl} from "../endpoints.ts";
import {AppendConversation} from "../../model/conversation/dto/appendConversation.ts";
import {ConversationStreamEvent} from "../../model/conversation/dto/conversationStream.ts";

export class ConversationClient extends BaseClient
{
    public readonly speechToTextPath: string = "api/v1/Conversation";

    protected host: string = hostUrl();

    public async appendConversation(
        appendConversation: AppendConversation,
        handler: (chunk: ConversationStreamEvent) => Promise<void>) {
        return await this.requestStream<ConversationStreamEvent>(
            "POST",
            this.speechToTextPath,
            handler,
            appendConversation);
    }
}