export type LlmProvider = "Anthropic"|"Google"|"OpenAi"|"X";

export type LlmModel = {
    Provider: LlmProvider,
    ModelName: string,
    ModelDescription: string,
    ModelIdentifier: string
} 

export type LlmProviderGroup = {
    provider: LlmProvider,
    models: LlmModel[];
}