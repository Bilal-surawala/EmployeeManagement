import apiService from "@/common/axiosService.js";

// initial state
const state = () => ({
    employees: [],
    departments: []
})

// getters
const getters = {
    employees: state => state.employees,
    departments: state => state.departments
}

// actions
const actions = {
    async getAllDepartments({ commit }) {
        let result = await apiService.get("api/Departments");
        if (result && result.success) {
            commit('setDepartments', result.data)
        }
        return result;
    },
    async getAllEmployees({ commit }) {
        let result = await apiService.get("api/Employees");
        if (result && result.success) {
            commit('setEmployees', result.data)
        }
        return result;
    },
    async deletEmployee({ dispatch }, id) {
        let result = await apiService.delete("api/Employees/" + id);
        if (result.success) {
            dispatch("getAllEmployees")
        }
        return result;
        //commit('setEmployees', Employees)
    },
    async addOrEditEmployee({ dispatch }, payload) {
        let result;
        if (payload.id) {
            result = await apiService.put("api/Employees/" + payload.id, payload);
        } else {
            result = await apiService.post("api/Employees", payload);
        }
        if (result.success) {
            dispatch("getAllEmployees")
        }
        return result;
    }
}

// mutations
const mutations = {
    setEmployees(state, employees) {
        state.employees = employees
    },
    setDepartments(state, departments) {
        state.departments = departments
    },
}

export default {
    state,
    getters,
    actions,
    mutations
}