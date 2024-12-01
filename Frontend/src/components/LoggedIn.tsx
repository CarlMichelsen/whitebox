import {FC, ReactNode, useEffect} from "react";
import {useQuery} from "react-query";
import {ChatConfigurationClient} from "../util/clients/chatConfigurationClient.ts";
import {useAppDispatch} from "../hooks.ts";
import {setChatConfiguration} from "../state/input";

type LoggedInProps = {
    children: ReactNode;
}


const LoggedIn: FC<LoggedInProps> = ({ children }) => {
    const dispatch = useAppDispatch()
    
    const getChatConfiguration = async () => {
        const client = new ChatConfigurationClient();
        const chatConfigurationResponse = await client.getChatConfiguration();
        
        if (!chatConfigurationResponse.ok || !chatConfigurationResponse.value) {
            throw new Error(chatConfigurationResponse.errors.join('\n'));
        }
        
        return chatConfigurationResponse.value;
    }
    
    const query = useQuery(
        {
            queryKey: ['chat-configuration'],
            queryFn: getChatConfiguration,
            staleTime: 1000 * 60 * 5,
        });

    useEffect(() => {
        if (query.data) {
            dispatch(setChatConfiguration(query.data));
        }
    }, [query.data]);
    
    return children;
}

export default LoggedIn;