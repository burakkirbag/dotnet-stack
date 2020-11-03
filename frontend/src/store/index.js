import { createStore } from 'vuex'
import axios from 'axios'

axios.defaults.baseURL = process.env.BACKEND_APP_PATH

axios.interceptors.response.use(
  function (response) {
    return response
  },
  function (error) {
    if (error.response) {
      alert(error.response.data.message);
      throw error.response.data
    }
  })

const mutations = {
  SET_PROPERTY: 'setProperty',
  UPDATE_BOOKS: 'updateBooks'
}

export default createStore({
  state: {
    loading: false,
    books: [],
  },
  mutations: {
    [mutations.SET_PROPERTY](state, data) {
      for (var key in data) {
        state[key] = data[key]
      }
    },
    [mutations.UPDATE_BOOKS](state, data) {
      state.books = data
    }
  },
  actions: {
    async fetchBooks({ commit }) {
      const result = await axios.get('/api/v1/book')
      commit(mutations.UPDATE_BOOKS, result.data)
    },
    async setProperty({ commit }, data) {
      commit(mutations.SET_PROPERTY, data)
    }
  },
  modules: {},
})
