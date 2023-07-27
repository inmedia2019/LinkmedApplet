<template>
	<view class=" animated fadeIn fast bF5F6FA dn">
		<view class="" style="height: 90px;"></view>
		<view class="center">
			<image src="../../static/logo2.png" style="width: 100px;" mode="widthFix"></image>
		</view>
		<view class="" style="height: 90px;"></view>
		<view class="center">
			<button style=" width: 80%; border: none; background:rgba(185,39,39,1);" type="default" open-type="getUserInfo"
			 @getuserinfo="mpGetuserinfo">
				<view class="cfff f17">微信登录</view>
			</button>
		</view>
		<view class="" style="height: 30px;"></view>
		<view class="center ">
			<view @click="onfh" class="cfff f17 center" style=" font-size: 18px; height: 46px; border-radius: 5px; background: #CCC; width: 80%; border: none;">退出</view>
		</view>
	</view>
</template>
<script>
	export default {
		data() {
			return {
				oid:"",
				src: ""
			}
		},
		onLoad(e) {
			this.oid=e.oid
		},
		onShow() {
			var sign = this.$conFig.issign('false','pages/login/login',this.oid)
			console.log("////")
			if (sign) {
				setTimeout(()=>{
					uni.reLaunch({
						url: "../my/my"
					});
				},1000)
			}
		},
		methods: {
			mpGetuserinfo(e) {

				if (e.detail.errMsg != "getUserInfo:ok") {
					return
				}

				uni.login({
					provider: "weixin",
					success: (res) => {

						var code = res.code;
						var user = e.detail.userInfo
						this.$conFig.alert_Load()
						this.$reqUest.get('inmedia', '/linyimaiapi.ashx?Method=authorize', {
							js_code: code
						}).then(data => {
							var [error, res] = data;
							
							console.log(res)
							
							var res = JSON.parse(res.data.message)

							if (res.openid != "") {
								user.openid = res.openid
								user.unionid = res.unionid
								user.session_key = res.session_key
								
								uni.setStorage({
									key: '_wxuserInfo',
									data: user
								});
								
								this.$reqUest.get('linkmed', '/linkmed2020/userinfo', {
									unionid: uni.getStorageSync('_wxuserInfo').unionid
								}).then(data => {
									var [error, res] = data;
									if (res.data.code == 100000) {
										var info = res.data.result[0]
										
										if(info.mail.split('@')[1]=="its.jnj.com"){
											this.onnbyg(info.mail.split('@')[0],user)
										}else{
											this.onwbyh(user,info)
										}
										
									} else {
										
										uni.request({
											url: this.$conFig.inmediaunion + '?unionId=' + uni.getStorageSync('_wxuserInfo').unionid,
											header: {
												'content-type': "application/x-www-form-urlencoded",
											},
											success: (res) => {
												var _res=res.data
												
												if (res.data.result == "false") {
													uni.redirectTo({
														url: "../sign/sign?phone=''"
													});
												} else {
													if (res.data.isInSideMember == "0") {
														
														console.log(res.data.phone)
														console.log("////")
														
														
														this.$reqUest.get('linkmed', '/linkmed2020/userinfo', {
															phone: res.data.phone
														}).then(data => {
															var [error, res] = data;
															
															if (res.data.code == 100000) {
																var info = res.data.result[0]
																
																console.log(info)
																
																if(info.name==null||info.name==''||info.hospital==''||info.category==''||info.position==''){
																	uni.redirectTo({
																		url: "../sign/sign?phone=" + _res.phone
																	});
																}else{
																	this.$reqUest.post('linkmed', '/linkmed2020/userinfo', {
																		phone: info.mobile,
																		name: info.name,
																		hospital: info.hospital,
																		mail: info.mail,
																		category: info.category,
																		avatar: info.avatar,
																		position: info.position,
																		openid: uni.getStorageSync('_wxuserInfo').openid,
																		unionid: uni.getStorageSync('_wxuserInfo').unionid
																	}).then(data => {
																		var [error, res] = data;
																		uni.setStorage({
																			key: 'userInfo',
																			data: {
																				"uid": info.uid,
																				"type": info.type,
																				"mail": info.mail,
																				"mobile": info.mobile,
																				"name": info.name,
																				"category": info.category,
																				"hospital": info.hospital,
																				"position": info.position,
																				"openid": info.openid,
																				"unionid": info.unionid,
																				"autologin": info.autologin,
																				"expire": info.expire,
																				"avatar": info.avatar
																			}
																		});
																		uni.setStorage({
																			key: 'sign',
																			data: uni.getStorageSync('sign') + "/true"
																		});
																																				
																		uni.setStorage({
																			key: 'wxuserInfo',
																			data: user
																		});
																		
																		uni.setStorage({
																			key: 'timeDepartment',
																			data: {
																				"category":info.category,
																			}
																		});
																		this.$conFig.ClassType(2)
																																				
																		uni.reLaunch({
																			url: uni.getStorageSync('timeUrl')
																		});
																	})
																}
																
															}else{
																uni.redirectTo({
																	url: "../sign/sign?phone=" + _res.phone
																});
															}
														})
														
													}else{
														this.onnbyg(_res.trueName,user)
													}
												}
												
											}
										});
									}
								})
								
							}
						})
					}
				})
			},
			onfh() {
				uni.navigateBack({
					delta: 1
				});
			},
			onwbyh(user,info){
				uni.setStorage({
					key: 'userInfo',
					data: {
						"uid": info.uid,
						"type": info.type,
						"mail": info.mail,
						"mobile": info.mobile,
						"name": info.name,
						"category": info.category,
						"hospital": info.hospital,
						"position": info.position,
						"openid": info.openid,
						"unionid": info.unionid,
						"autologin": info.autologin,
						"expire": info.expire,
						"avatar": info.avatar
					}
				});
				uni.setStorage({
					key: 'sign',
					data: uni.getStorageSync('sign') + "/true"
				});
																			
				uni.setStorage({
					key: 'wxuserInfo',
					data: user
				});
				
				uni.setStorage({
					key: 'timeDepartment',
					data: {
						"category":info.category,
					}
				});
				this.$conFig.ClassType(2)
																			
				uni.reLaunch({
					url: uni.getStorageSync('timeUrl')
				});
			},
			onnbyg(name,user){
				uni.setStorage({
					key: 'userInfo',
					data: {
						"uid": "999999999",
						"type": "",
						"mail": "",
						"mobile": "",
						"name": name,
						"category": uni.getStorageSync('timeDepartment').category,
						"hospital": "强生",
						"position": "",
						"openid": uni.getStorageSync('_wxuserInfo').openid,
						"unionid": uni.getStorageSync('_wxuserInfo').unionid,
						"autologin": "",
						"expire": "",
						"avatar": this.$conFig.headsculpture,
						"categoryid": uni.getStorageSync('timeDepartment').categoryid,
						"categorynum": uni.getStorageSync('timeDepartment').categorynum,
						"positionid": "",
						"positionnum": ""
					}
				});
				uni.setStorage({
					key: 'isInSideMember',
					data: 1
				});
				
				
				uni.setStorage({
					key: 'wxuserInfo',
					data: user
				});
				
				uni.setStorage({
					key: 'sign',
					data: uni.getStorageSync('sign') + "/true"
				});
				
				uni.reLaunch({
					url: uni.getStorageSync('timeUrl')
				});
			}
		}
	}
</script>

<style>

</style>
