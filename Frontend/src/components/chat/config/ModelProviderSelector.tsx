import {FC} from "react";
import {LlmModel, LlmProviderGroup} from "../../../model/conversation/llmModel";
import {getImageUrl} from "../../../util/helpers/providerIcon";
import {useDarkMode} from "../../../hooks";

type ModelProviderSelectorProps = {
    selectedModel: LlmModel;
    providerGroup: LlmProviderGroup;
    modelClicked: (model: LlmModel) => void;
}

const ModelProviderSelector: FC<ModelProviderSelectorProps> = ({ selectedModel, providerGroup, modelClicked }) => {
    const darkMode = useDarkMode();
    
    const getBgClasses = (m: LlmModel) => {
        return selectedModel.modelIdentifier === m.modelIdentifier
            ? "dark:bg-neutral-600 bg-neutral-300"
            : "dark:hover:bg-neutral-600 hover:bg-neutral-300";
    }
    
    return (
        <div className="border border-color rounded-sm p-1">
            <div className="grid grid-cols-[1.5rem_auto]">
                <img
                    className="aspect-square pr-1"
                    draggable={false}
                    src={getImageUrl(providerGroup.provider, darkMode)}
                    alt={providerGroup.provider}/>
                <h3>{providerGroup.provider}</h3>
            </div>
            
            <ol className="ml-2 space-y-1">
                {providerGroup.models.map((m) => (
                    <li key={m.modelIdentifier} id={"selectable-model-" + m.modelIdentifier}>
                        <button
                            className={`p-1 text-left w-full rounded-sm block ${getBgClasses(m)}`}
                            onClick={() => modelClicked(m)}>
                            <h4 className="italic">{m.modelName}</h4>
                            <p className="text-xs">{m.modelDescription}</p>
                        </button>
                    </li>
                ))}
            </ol>
        </div>
    );
}

export default ModelProviderSelector;