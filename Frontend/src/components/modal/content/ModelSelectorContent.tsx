import {FC} from "react";
import {ModalContentProps} from "./ModalContentProps.ts";
import {useAppSelector} from "../../../hooks.ts";
import {ChatConfiguration} from "../../../model/chatConfiguration/chatConfiguration.ts";
import LargeLanguageModelProviderSelector from "../../chat/LargeLanguageModelProviderSelector.tsx";
import {LlmModel} from "../../../model/conversation/llmModel.ts";
import {ChatConfigurationClient} from "../../../util/clients/chatConfigurationClient.ts";
import {useQueryClient} from "react-query";

const ModelSelectorContent: FC<ModalContentProps> = ( { closeModal }) => {
    const input = useAppSelector(state => state.input);
    const queryClient = useQueryClient();

    const config: ChatConfiguration|null = input.chatConfiguration;
    
    if (!config) {
        throw new Error("Chat-configuration not available when viewing model selector modal.");
    }
    
    const selectModel = async (m: LlmModel): Promise<void> => {
        const client = new ChatConfigurationClient();
        const res =await client.setSelectedModelIdentifier({ modelIdentifier: m.modelIdentifier });
        if (res.ok) {
            await queryClient.invalidateQueries("chat-configuration");
            closeModal();
        }
    }
    
    return (
        <div className="grid grid-cols-1 sm:grid-cols-2 gap-1">
            {config.availableProviders.map(p => (
                <LargeLanguageModelProviderSelector
                    key={p.provider}
                    providerGroup={p}
                    selectedModel={config.selectedModel}
                    modelClicked={selectModel}/>
            ))}
        </div>
    );
}

export default ModelSelectorContent;