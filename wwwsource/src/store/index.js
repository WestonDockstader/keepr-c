import vue from 'vue'
import vuex from 'vuex'
import axios from 'axios'

var production = !window.location.host.includes('localhost');
var baseUrl = production ? 'heroku address' : '//localhost:5000';

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
    user: {}
  },
  mutations:{
    setUser(state,payload){
      state.user=payload
    }
  },
  actions:{
    register({commit}, payload){
      console.log("register action",payload)
      auth.post('register',payload)
        .then(res=>{
          console.log(res)
          commit('setUser',res.data)
        })
        .catch(err=>{
          console.log(err)
        })
    }
  }
})