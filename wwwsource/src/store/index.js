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
    keeps:{}
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
    getKeeps({commit}){
      api.get('keeps')
        .then(res=>{
          console.log('keeps' ,res.data)
          commit('setKeeps',res.data)
        })
    }
      
  }
})