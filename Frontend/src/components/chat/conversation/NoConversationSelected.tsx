import {FC} from "react";
import ConnectedModelProviderSelector from "../config/ConnectedModelProviderSelector.tsx";
import SystemMessageEditor from "../system/SystemMessageEditor.tsx";
import {useAppDispatch, useAppSelector} from "../../../hooks.ts";
import {ChatConfiguration} from "../../../model/chatConfiguration/chatConfiguration.ts";
import {ChatConfigurationClient} from "../../../util/clients/chatConfigurationClient.ts";
import {SetDefaultSystemMessage} from "../../../model/chatConfiguration/dto/setDefaultSystemMessage.ts";
import {InputState, setChatConfiguration} from "../../../state/input";

const NoConversationSelected: FC = () => {
    const input: InputState = useAppSelector(state => state.input);
    const dispatch = useAppDispatch()
    const config: ChatConfiguration|null = input.chatConfiguration;
    
    return (
        <>
            <h1 className="text-xl mb-5">Send a message to start a conversation</h1>
            <div className="sm:block hidden">
                <ConnectedModelProviderSelector />
            </div>
            
            <br/>

            <h2 className="text-lg mb-1">Edit default system message</h2>

            {config !== null ? (
                <SystemMessageEditor
                    initialMessage={config.defaultSystemMessage ?? ""}
                    saveChanges={async (m) => {
                    const client = new ChatConfigurationClient();
                    const payload: SetDefaultSystemMessage = {systemMessage: m};
                    const res = await client.setDefaultSystemMessage(payload);
                    if (res.ok && res.value && input.chatConfiguration) {
                        const newChatConfig = {
                            ...input.chatConfiguration,
                            defaultSystemMessage: res.value
                        } satisfies ChatConfiguration;
                        
                        dispatch(setChatConfiguration(newChatConfig));
                    }
                }}/>
            ) : (
                <p>Loading...</p>
            )}
        </>
    );
}

export default NoConversationSelected;