import {FC} from "react";
import ConnectedModelProviderSelector from "../config/ConnectedModelProviderSelector.tsx";
import {delay} from "../../../util/delay.ts";
import SystemMessageEditor from "../system/SystemMessageEditor.tsx";
import {useAppSelector} from "../../../hooks.ts";
import {ChatConfiguration} from "../../../model/chatConfiguration/chatConfiguration.ts";

const NoConversationSelected: FC = () => {
    const input = useAppSelector(state => state.input);
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
                <SystemMessageEditor initialMessage={config.defaultSystemMessage ?? ""} saveChanges={async (m) => {
                    await delay(300);
                    console.log("saved", m);
                }}/>
            ) : (
                <p>Loading...</p>
            )}
        </>
    );
}

export default NoConversationSelected;