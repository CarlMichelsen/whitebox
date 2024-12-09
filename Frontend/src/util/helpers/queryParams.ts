const removeQueryParameter = (key: string) => {
    const searchParams = new URLSearchParams(window.location.search);
    searchParams.delete(key);
    const newRelativePathQuery = `${window.location.pathname}?${searchParams.toString()}`;
    window.history.replaceState(null, '', newRelativePathQuery);
};

export const setQueryParameter = (key: string, value: string|null) => {
    if (!value) {
        removeQueryParameter(key);
        return;
    }
    
    const searchParams = new URLSearchParams(window.location.search);
    searchParams.set(key, value);
    const newRelativePathQuery = `${window.location.pathname}?${searchParams.toString()}`;
    window.history.replaceState(null, '', newRelativePathQuery);
};

export const getQueryParameter = (key: string): string | null => {
    const searchParams = new URLSearchParams(window.location.search);
    return searchParams.get(key);
};