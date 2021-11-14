import Vuex from 'vuex'
import employee from "@/store/modules/employee.js";

const store = new Vuex.Store({
    modules: {
        employee
    }
})

export default store;