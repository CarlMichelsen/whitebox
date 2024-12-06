import {LlmProvider} from "../../model/conversation/llmModel";
import AnthropicWhite from "../../assets/icons/llm/anthropic-white.svg";
import AnthropicBlack from "../../assets/icons/llm/anthropic-black.svg";
import GoogleWhite from "../../assets/icons/llm/google-gemini-white.svg";
import GoogleBlack from "../../assets/icons/llm/google-gemini-black.svg";
import OpenAiWhite from "../../assets/icons/llm/openai-white.svg";
import OpenAiBlack from "../../assets/icons/llm/openai-black.svg";
import XWhite from "../../assets/icons/llm/x-white.svg";
import XBlack from "../../assets/icons/llm/x-black.svg";

const modelImages: Map<LlmProvider, { white: string, black: string }> = new Map();
modelImages.set("Anthropic", {
    white: AnthropicWhite,
    black: AnthropicBlack
});

modelImages.set("Google", {
    white: GoogleWhite,
    black: GoogleBlack
});

modelImages.set("OpenAi", {
    white: OpenAiWhite,
    black: OpenAiBlack
});

modelImages.set("X", {
    white: XWhite,
    black: XBlack
});

export const getImageUrl = (provider: LlmProvider, darkmode: boolean): string => {
    const pair = modelImages.get(provider);
    if (!pair) {
        throw new Error("Unknown provider");
    }

    return darkmode ? pair.white : pair.black;
}