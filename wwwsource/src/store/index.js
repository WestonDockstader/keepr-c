import vue from 'vue'
import vuex from 'vuex'
import axios from 'axios'

var production = !window.location.host.includes('localhost');
var baseUrl = production ? '//keepr-dock.herokuapp.com' : '//localhost:5000/';

vue.use(vuex)

var api=axios.create({
  baseURL: baseUrl,
  timeout: 3000,
  withCredentials: true
})

var auth = axios.create({
  baseURL: baseUrl + 'account/',
  timeout: 3000,
  withCredentials: true
})

export default new vuex.Store({
  state:{
    user: {},
    keeps:{},
    vaults: {},
    //testing
    vaultkeeps: {}
  },
  mutations:{
    setUser(state,payload){
      state.user=payload
    },
    clearUser(state,payload){
      state.user=payload
    },
    setKeeps(state,payload){
      state.keeps=payload
    },
    setVaults(state,payload){
      state.vaults=payload
    },
    // testing
    setVaultKeeps(state, payload){
      state.vaultkeeps=payload
    }
  },
  actions:{
    register({commit}, payload){
      console.log(payload)
      auth.post('register',payload)
        .then(res=>{
          console.log(res)
          commit('setUser',res.data)
        })
        .catch(err=>{
          console.log(err)
        })
    },
    login({commit}, payload){
      console.log(payload)
      auth.post('login',payload)
        .then(res=>{
          console.log(res.data)
          commit('setUser',res.data)
        })
        .catch(err=>{
          console.log(err)
        })
    },
    logout({commit}){
      auth.delete('logout')
        .then(res=>{
          var log={};
          commit('clearUser',log)
        })
        .catch(err=>{
          console.log(err)
        })
    },
    authenticate({commit}){
      auth.get('/authenticate')
        .then(res=>{
          commit('setUser',res.data)
        })
        .catch(err=>{
          console.log(err)
        })
    },
    getKeeps({commit}){
      api.get('keeps')
        .then(res=>{
          commit('setKeeps',res.data)
        })
        .catch(err=>{
          console.log(err)
        })
    },
    createKeep({dispatch},payload){
      api.post('keeps',payload)
        .then(res=>{
          dispatch('getKeeps')
        })
        .catch(err=>{
          console.log(err)
        })
    },
    deleteKeep({dispatch},payload){
      api.delete('keeps/'+payload)
        .then(res=>{
          dispatch('getKeeps')
        })
        .catch(err=>{
          console.log(err)
        })
    },
    editKeep({dispatch},payload){
      api.put('keeps/'+payload.id,payload)
        .then(res=>{
          dispatch('getKeeps')
        })
        .catch(err=>{
          console.log(err)
        })
    },
    getVaults({commit},payload){
      api.get('vaults/'+payload)
        .then(res=>{
          commit('setVaults',res.data)
        })
        .catch(err=>{
          console.log(err)
        })
    },
    addVault({dispatch,state},payload){
      api.post('vaults',payload)
        .then(res=>{
          dispatch('getVaults',state.user.id)
        })
        .catch(err=>{
          console.log(err)
        })
    },
    deleteVault({dispatch,state},payload){
      api.delete('vaults/'+payload)
        .then(res=>{
          dispatch('getVaults',state.user.id)
        })
        .catch(err=>{
          console.log(err)
        })
    },
    getUserVaultKeeps({commit,state}){
      api.get('vaultkeeps/vk/'+state.user.id)
        .then(res=>{
          console.log('vaultkeeps',res.data)
          commit('setVaultKeeps',res.data)
        })
        .catch(err=>{
          console.log(err)
        })
    },
    addToVaultKeep({dispatch},payload){
      api.post('vaultkeeps',payload)
        .then(res=>{
          console.log(res)
          dispatch('getUserVaultKeeps')
        })
        .catch(err=>{
          console.log(err)
        })
    }
  }
})