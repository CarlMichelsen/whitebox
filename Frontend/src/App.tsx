import {useAppDispatch, useAppSelector} from "./hooks.ts";
import {getUser} from "./util/helpers/getUser.ts";
import {useQuery} from "react-query";
import {FC, ReactNode, useEffect} from "react";
import {login, logout} from "./state/auth";
import LoggedOut from "./components/LoggedOut.tsx";
import Root from "./components/Root.tsx";
import LoggedIn from "./components/LoggedIn.tsx";

type AppProps = {
    children: ReactNode;
}

const App: FC<AppProps> = ({ children }) => {
    const auth = useAppSelector((state) => state.auth);
    const dispatch = useAppDispatch()

    const query = useQuery(
        {
            queryKey: ['auth'],
            queryFn: getUser,
            staleTime: 1000 * 60,
        });
    
    useEffect(() => {
        if (query.status === "success") {
            if (query.data.ok) {
                dispatch(login(query.data.value!));
            } else {
                dispatch(logout());
            }
        }
    }, [query])
    
    switch (auth.status) {
        case "loggedIn": return <Root><LoggedIn>{children}</LoggedIn></Root>;
        case "loggedOut": return <Root><LoggedOut /></Root>;
        case "pending": return null;
        default: throw new Error("Invalid auth status");
    }
}

export default App
