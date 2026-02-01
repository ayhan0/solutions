import { reactive } from "vue";

export const toastState = reactive({
    show: false,
    type: "info", // success | error | info
    message: "",
});

export function showToast(message, type = "info") {
    toastState.show = true;
    toastState.type = type;
    toastState.message = message;

    window.setTimeout(() => {
        toastState.show = false;
    }, 2500);
}
