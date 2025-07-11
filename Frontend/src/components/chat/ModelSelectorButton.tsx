﻿import {FC} from "react";
import {useAppDispatch, useAppSelector, useDarkMode} from "../../hooks.ts";
import {ChatConfiguration} from "../../model/chatConfiguration/chatConfiguration.ts";
import {openModal} from "../../state/modal";
import {getImageUrl} from "../../util/helpers/providerIcon.ts";

const ModelSelectorButton: FC = () => {
    const input = useAppSelector(state => state.input);
    const darkMode = useDarkMode();
    const dispatch = useAppDispatch()
    
    const config: ChatConfiguration|null = input.chatConfiguration;
    
    return config ? <div className="fixed top-1 right-1 h-8 w-72 grid grid-cols-[1fr_2rem_auto]">
        <div></div>
        <img
            className="aspect-square p-1"
            draggable={false}
            src={getImageUrl(config.selectedModel.provider, darkMode)}
            alt={config.selectedModel.provider}/>
        <button
            className="w-full h-full rounded-xs dark:bg-neutral-600 bg-neutral-300 px-2"
            onClick={() => dispatch(openModal("model-selector"))}>
            {config.selectedModel.modelName}
        </button>
    </div> : null;
}

export default ModelSelectorButton;