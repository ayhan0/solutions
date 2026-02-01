import { createRouter, createWebHistory } from "vue-router";

import EntitiesAdd from "@/pages/EntitiesAdd.vue";
import EntitiesDelete from "@/pages/EntitiesDelete.vue";
import EntitiesList from "@/pages/EntitiesList.vue";
import EntitiesSearch from "@/pages/EntitiesSearch.vue";
import Ops from "@/pages/Ops.vue";

const router = createRouter({
    history: createWebHistory(),
    routes: [
        { path: "/", redirect: "/entities/list" },
        { path: "/entities/add", component: EntitiesAdd },
        { path: "/entities/delete", component: EntitiesDelete },
        { path: "/entities/list", component: EntitiesList },
        { path: "/entities/search", component: EntitiesSearch },
        { path: "/ops", component: Ops },
    ],
});

export default router;
