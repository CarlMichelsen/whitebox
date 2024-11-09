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
        path: "chat",
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

// opting into router v7 early, remove when v7 is out.
const opts: object = {
    future: {
        v7_startTransition: true,
        v7_skipActionErrorRevalidation: true,
        v7_fetcherPersist: true,
        v7_normalizeFormMethod: true,
        v7_relativeSplatPath: true,
        v7_partialHydration: true,
    },
}

export const router = createBrowserRouter(routes, opts);