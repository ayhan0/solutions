import api from "./apiClient";

export async function addEntity(payload) {
    // POST /api/entity/add
    const res = await api.post("/api/entity/add", payload);
    return res.data;
}

export async function deleteEntity(key) {
    // DELETE /api/entity/delete/{key}
    const res = await api.delete(`/api/entity/delete/${encodeURIComponent(key)}`);
    return res.data;
}

export async function listEntities() {
    // GET /api/entity/list
    const res = await api.get("/api/entity/list");
    return res.data;
}

export async function searchEntities(key) {
    // GET /api/entity/search/{key}
    const res = await api.get(`/api/entity/search/${encodeURIComponent(key)}`);
    return res.data;
}
