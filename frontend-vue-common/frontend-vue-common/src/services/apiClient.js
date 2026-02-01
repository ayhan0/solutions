import axios from "axios";
import { getCurrentBaseUrl } from "./backendSwitcher";
import { showToast } from "@/stores/toast";

function normalizeBaseUrl(url) {
    return (url || "").replace(/\/+$/, "");
}

function extractErrorMessage(err) {
    const data = err?.response?.data;

    if (!data) return err?.message || "Request failed";
    if (typeof data === "string") return data;
    if (data.message) return data.message;
    if (data.title) return data.title;

    if (data.errors && typeof data.errors === "object") {
        const firstKey = Object.keys(data.errors)[0];
        const firstArr = data.errors[firstKey];
        if (Array.isArray(firstArr) && firstArr.length) return firstArr[0];
    }
    return "Request failed";
}

const api = axios.create({
    baseURL: normalizeBaseUrl(getCurrentBaseUrl()),
    timeout: 15000,
});


api.interceptors.request.use((config) => {
    config.baseURL = normalizeBaseUrl(getCurrentBaseUrl());
    return config;
});

api.interceptors.response.use(
    (res) => res,
    (err) => {
        const status = err?.response?.status;
        const msg = extractErrorMessage(err);
        showToast(`(${status ?? "ERR"}) ${msg}`, "error");
        return Promise.reject(err);
    }
);

export default api;
