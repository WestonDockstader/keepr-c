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

export default new vuex.Store({
  state:{},
  mutations:{},
  actions:{}
})