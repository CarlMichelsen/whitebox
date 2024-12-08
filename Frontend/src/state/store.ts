import { configureStore } from '@reduxjs/toolkit'
import authReducer from "./auth";
import sidebarReducer from "./sidebar";
import inputReducer from "./input";
import modalReducer from "./modal";
import conversationReducer from "./conversation";

const customLoggerMiddleware = (storeAPI: { getState: () => RootState }) => (next: (action: any) => any) => (action: any) => {
    if (import.meta.env.VITE_APP_ENV !== 'development') {
        return next(action);
    }
    
    const result = next(action);
    console.log(action, '->', storeAPI.getState());
    return result;
};

export const store = configureStore({
    reducer: {
        auth: authReducer,
        sidebar: sidebarReducer,
        input: inputReducer,
        modal: modalReducer,
        conversation: conversationReducer
    },
    middleware: (getDefaultMiddleware) =>
        getDefaultMiddleware().concat(customLoggerMiddleware),
})

export type RootState = ReturnType<typeof store.getState>

export type AppDispatch = typeof store.dispatch