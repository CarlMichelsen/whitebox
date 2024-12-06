import {
    createBrowserRouter, RouteObject,
} from "react-router-dom";
import Home from "./routes/Home";
import NotFound from "./routes/NotFound.tsx";
import Chat from "./routes/Chat.tsx";

const routes: RouteObject[] = [
    {
        path: "",
        element: <Home />,
    },
    {
        path: "c",
        element: <Chat />,
        children: [
            {
                path: ":id",
                element: <Chat />,
            },
        ]
    },
    {
        path: "*",
        element: <NotFound />
    }
];

export const router = createBrowserRouter(routes, {});