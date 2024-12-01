import {LlmModel, LlmProviderGroup} from "../conversation/llmModel.ts";

export type ChatConfiguration = {
    id: string;
    defaultSystemMessage: string|null;
    selectedModel: LlmModel;
    maxTokens: number;
    availableModels: LlmProviderGroup[];
}