const LS_KEY = "selected_backend"; // dotnet | spring

export function getBackend() {
    const v = localStorage.getItem(LS_KEY);
    return v === "spring" || v === "dotnet" ? v : "dotnet";
}

export function setBackend(v) {
    localStorage.setItem(LS_KEY, v);
}

export function getBaseUrlByBackend(v) {
    const dotnet = import.meta.env.VITE_API_DOTNET;
    const spring = import.meta.env.VITE_API_SPRING;
    return v === "spring" ? spring : dotnet;
}

export function getCurrentBaseUrl() {
    return getBaseUrlByBackend(getBackend());
}
