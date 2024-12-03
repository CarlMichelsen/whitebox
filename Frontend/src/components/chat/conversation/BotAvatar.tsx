import {FC} from "react";
import {LlmModel} from "../../../model/conversation/llmModel.ts";
import {getImageUrl} from "../../../util/helpers/providerIcon.ts";
import {useDarkMode} from "../../../hooks.ts";

type BotAvatarProps = {
    llmModel: LlmModel;
}

const BotAvatar: FC<BotAvatarProps> = ({ llmModel }) => {
    const darkMode = useDarkMode();
    
    return (
        <div className="relative">
            <p className="absolute -top-4 text-xs w-48">{llmModel.modelName}</p>
            <img
                className="aspect-square shadow-xl h-8 sm:h-10 bg-none rounded-md sm:rounded-full px-0.5 py-0.5 sm:px-1.5 sm:py-1.5"
                src={getImageUrl(llmModel.provider, darkMode)}
                alt={llmModel.modelName}/>
        </div>
    )
}

export default BotAvatar