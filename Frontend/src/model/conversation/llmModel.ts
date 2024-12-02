export type LlmProvider = "Anthropic"|"Google"|"OpenAi"|"X";

export type LlmModel = {
    provider: LlmProvider,
    modelName: string,
    modelDescription: string,
    modelIdentifier: string
} 

export type LlmProviderGroup = {
    provider: LlmProvider,
    models: LlmModel[];
}