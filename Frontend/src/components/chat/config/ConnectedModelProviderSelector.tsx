import {FC} from "react";
import {useAppSelector} from "../../../hooks";
import {useQueryClient} from "react-query";
import {ChatConfiguration} from "../../../model/chatConfiguration/chatConfiguration";
import {LlmModel} from "../../../model/conversation/llmModel";
import {ChatConfigurationClient} from "../../../util/clients/chatConfigurationClient";
import ModelProviderSelector from "./ModelProviderSelector.tsx";

const ConnectedModelProviderSelector: FC<{onNewSelection?: () => void}> = ({ onNewSelection }) => {
    const input = useAppSelector(state => state.input);
    const queryClient = useQueryClient();

    const config: ChatConfiguration|null = input.chatConfiguration;

    if (!config) {
        return <p>loading...</p>
    }

    const selectModel = async (m: LlmModel): Promise<void> => {
        const client = new ChatConfigurationClient();
        const res =await client.setSelectedModelIdentifier({ modelIdentifier: m.modelIdentifier });
        if (res.ok) {
            await queryClient.invalidateQueries("chat-configuration");
            onNewSelection && onNewSelection();
        }
    }

    return (
        <div className="grid grid-cols-1 sm:grid-cols-2 gap-1">
            {config.availableProviders.map(p => (
                <ModelProviderSelector
                    key={p.provider}
                    providerGroup={p}
                    selectedModel={config.selectedModel}
                    modelClicked={selectModel}/>
            ))}
        </div>
    );
}


export default ConnectedModelProviderSelector;