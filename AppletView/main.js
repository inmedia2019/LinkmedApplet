import Vue from 'vue'
import App from './App'

Vue.config.productionTip = false

App.mpType = 'app'


import $util from 'common/util.js'
Vue.prototype.$util = $util

import $conFig from 'common/config.js'
Vue.prototype.$conFig = $conFig

import $reqUest from 'common/request.js'
Vue.prototype.$reqUest = $reqUest

import $des from 'common/des.js'
Vue.prototype.$des = $des

import $base64 from 'common/base64.min.js'
Vue.prototype.$base64 = $base64


const app = new Vue({
    ...App
})
app.$mount()
