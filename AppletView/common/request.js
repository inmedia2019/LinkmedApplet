import $conFig from "common/config.js"

//uni.getStorageSync('userInfo').mobile==undefined?'':uni.getStorageSync('userInfo').mobile18321454545
export default {
	common:{
		method:"GET",
		header:{
			'content-type':"application/x-www-form-urlencoded",
			'linkmed-token':'6CxkakZH',
			'phone': uni.getStorageSync('userInfo').mobile==undefined?'':uni.getStorageSync('userInfo').mobile,
			'unionid': uni.getStorageSync('userInfo').unionid==undefined?'':uni.getStorageSync('userInfo').unionid
		},
		data:{}
	},
	request(options={}){
		
		var _h={'content-type':"application/x-www-form-urlencoded",
			'linkmed-token':'6CxkakZH',
			'phone': uni.getStorageSync('userInfo').mobile==undefined?'':uni.getStorageSync('userInfo').mobile,
			'unionid': uni.getStorageSync('userInfo').unionid==undefined?'':uni.getStorageSync('userInfo').unionid
		}
		
		if(options.api=="inmedia"){
			options.url=$conFig.inmediaApi+options.url
		}else{
			options.url=$conFig.linkmedApi+options.url
		}
		options.method=options.method||this.common.method
		options.header=_h
		console.log(options)
		return uni.request(options)
	},
	get(api,url,data={},options={}){
		options.api=api
		options.url=url
		options.data=data
		options.method="GET"
		return this.request(options)
	},
	post(api,url,data={},options={}){
		options.api=api
		options.url=url
		options.data=data
		options.method="POST"
		return this.request(options)
	},
	userLog(data={},options={}){
		options.url="/linyimaiapi.ashx?Method=userlog"
		options.data=data
		options.method="POST"
		console.log(options)
		return this.logRequest(options)
	},
	logRequest(options={}){
		var _h={
			'content-type':"application/x-www-form-urlencoded",
			'linkmed-token':'6CxkakZH',
			'phone': uni.getStorageSync('userInfo').mobile==undefined?'':uni.getStorageSync('userInfo').mobile,
			'unionid': uni.getStorageSync('userInfo').unionid==undefined?'':uni.getStorageSync('userInfo').unionid
		}
		options.url=$conFig.inmediaApi+options.url
		options.method=options.method||this.common.method
		options.header=_h
		options.data.OpenId=uni.getStorageSync('userInfo').openid
		return uni.request(options)
	}
}