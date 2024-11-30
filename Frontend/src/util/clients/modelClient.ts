import {BaseClient} from "../baseClient";
import {hostUrl} from "../endpoints";
import {LlmProviderGroup} from "../../model/conversation/llmModel";

export class ModelClient extends BaseClient
{
    public readonly modelPath: string = "api/v1/Model";
    
    protected host: string = hostUrl();
    
    public async getModels() {
        return await this.request<LlmProviderGroup[]>("GET", this.modelPath);
    }
}