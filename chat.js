// chat.js

const state = {
    messages: [],
  };
  
  const mutations = {
    addMessage(state, message) {
      state.messages.push(message);
    },
  };
  
  const actions = {
    sendMessage({ commit }, message) {
      commit('addMessage', message);
    },
  };
  
  export default {
    namespaced: true,
    state,
    mutations,
    actions,
  };
  