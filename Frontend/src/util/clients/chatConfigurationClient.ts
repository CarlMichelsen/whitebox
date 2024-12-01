import {BaseClient} from "../baseClient.ts";
import {hostUrl} from "../endpoints.ts";
import {ChatConfiguration} from "../../model/chatConfiguration/chatConfiguration.ts";
import {SetDefaultSystemMessage} from "../../model/chatConfiguration/dto/setDefaultSystemMessage.ts";
import {SetSelectedModel} from "../../model/chatConfiguration/dto/setSelectedModel.ts";
import {LlmModel} from "../../model/conversation/llmModel.ts";

export class ChatConfigurationClient extends BaseClient
{
    public readonly modelPath: string = "api/v1/ChatConfiguration";

    protected host: string = hostUrl();
    
    public async getChatConfiguration() {
        return await this.request<ChatConfiguration>("GET", this.modelPath);
    }

    public async setDefaultSystemMessage(systemMessage: SetDefaultSystemMessage) {
        return await this.request<string>("POST", `${this.modelPath}/system`, systemMessage);
    }

    public async setSelectedModelIdentifier(modelIdentifier: SetSelectedModel) {
        return await this.request<LlmModel>("POST", `${this.modelPath}/model`, modelIdentifier);
    }
}