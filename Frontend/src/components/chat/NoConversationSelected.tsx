import {FC} from "react";
import ConnectedModelProviderSelector from "./config/ConnectedModelProviderSelector.tsx";
import SystemMessageEditor from "./system/SystemMessageEditor.tsx";

const NoConversationSelected: FC = () => {
    
    return (
        <>
            <h2 className="text-xl mb-5">Start a new conversation</h2>
            <div className="sm:block hidden">
                <ConnectedModelProviderSelector onNewSelection={() => null} />
            </div>
            
            <SystemMessageEditor />
        </>
    );
}

export default NoConversationSelected;