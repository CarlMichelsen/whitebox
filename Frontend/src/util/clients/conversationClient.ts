import {BaseClient} from "../baseClient.ts";
import {hostUrl} from "../endpoints.ts";
import {AppendConversation} from "../../model/conversation/dto/appendConversation.ts";
import {StreamEvent} from "../../model/conversation/dto/conversationStream.ts";
import {ConversationOption} from "../../model/sidebar/conversationOption.ts";
import {Conversation} from "../../model/conversation/conversation.ts";

export class ConversationClient extends BaseClient
{
    public readonly conversationPath: string = "api/v1/Conversation";

    protected host: string = hostUrl();

    public async appendConversation(
        appendConversation: AppendConversation,
        handler: (chunk: StreamEvent) => Promise<void>) {
        return await this.requestStream<StreamEvent>(
            "POST",
            this.conversationPath,
            handler,
            appendConversation);
    }

    public async getConversationOptions() {
        return await this.request<ConversationOption[]>("GET", this.conversationPath);
    }

    public async getConversation(id: string) {
        return await this.request<Conversation>("GET", `${this.conversationPath}/${id}`);
    }
}