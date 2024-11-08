import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App.tsx'
import {Provider} from "react-redux";
import {store} from "./state/store.ts";
import {QueryClient, QueryClientProvider} from "react-query";
import {router} from "./router.tsx";
import {RouterProvider} from "react-router-dom";

const queryClient = new QueryClient()

createRoot(document.getElementById('root')!).render(
    <StrictMode>
        <QueryClientProvider client={queryClient}>
            <Provider store={store}>
                <App>
                    <RouterProvider router={router} />
                </App>
            </Provider>
        </QueryClientProvider>
    </StrictMode>,
)
