
// initial state
const state = () => ({
    name: "sffsdsfdffdf"
})

// getters
const getters = {
    nameState: state => state.name
}

// actions
const actions = {
    //   getAllProducts ({ commit }) {
    //     shop.getProducts(products => {
    //       commit('setProducts', products)
    //     })
    //   }
}

// mutations
const mutations = {
    //   setProducts (state, products) {
    //     state.all = products
    //   },

    //   decrementProductInventory (state, { id }) {
    //     const product = state.all.find(product => product.id === id)
    //     product.inventory--
    //   }
}

export default {
    state,
    getters,
    actions,
    mutations
}