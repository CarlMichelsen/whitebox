import { configureStore } from '@reduxjs/toolkit'
import authReducer from "./auth";
import sidebarReducer from "./sidebar";
import inputReducer from "./input";

const customLoggerMiddleware = (storeAPI: { getState: () => RootState }) => (next: (action: any) => any) => (action: any) => {
    if (import.meta.env.VITE_APP_ENV !== 'development') {
        return next(action);
    }
    
    console.log('Dispatching:', action);
    const result = next(action); // Let the action continue to the next middleware or reducer
    console.log('Next State:', storeAPI.getState());
    return result;
};

export const store = configureStore({
    reducer: {
        auth: authReducer,
        sidebar: sidebarReducer,
        input: inputReducer,
    },
    middleware: (getDefaultMiddleware) =>
        getDefaultMiddleware().concat(customLoggerMiddleware),
})

export type RootState = ReturnType<typeof store.getState>

export type AppDispatch = typeof store.dispatch