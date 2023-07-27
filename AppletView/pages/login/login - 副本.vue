<template>
	<view class=" animated fadeIn fast">
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

				src: ""
			}
		},
		onShow() {

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

								uni.request({
									url: this.$conFig.inmediaunion + '?unionId=' + res.unionid,
									header: {
										'content-type': "application/x-www-form-urlencoded",
									},
									success: (res) => {
										
										console.log(res.data)
										console.log("////")
										
										
										
										var _res=res.data

										uni.setStorage({
											key: '_wxuserInfo',
											data: user
										});

										if (res.data.result == "false") {
											uni.redirectTo({
												url: "../sign/sign?phone=''"
											});
										} else {
											if (res.data.isInSideMember == "0") {
												
												
												
												uni.redirectTo({
													url: "../sign/sign?phone=" + _res.phone
												});
												
											} else {
												
												
												this.$reqUest.post('linkmed', '/linkmed2020/internaluser', {
													"mail": _res.email,
													"unionid": uni.getStorageSync('_wxuserInfo').unionid,
													"openid": uni.getStorageSync('_wxuserInfo').openid
												}).then(data => {
													var [error, res] = data;
													this.$reqUest.get('inmedia', '/linyimaiapi.ashx?Method=notificationcount', {
														video_created: (new Date()).getTime(),
														comment_created: (new Date()).getTime(),
														flag_created: (new Date()).getTime()
													}).then(data => {
													
														var [error, res] = data;
													
														uni.setStorage({
															key: 'userInfo',
															data: {
																"uid": "999999999",
																"type": "",
																"mail": "",
																"mobile": "",
																"name": _res.trueName,
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
													})
												})
											}
										}
									}
								});
							}
						})
					}
				})


			},
			onfh() {
				uni.navigateBack({
					delta: 1
				});
			}
		}
	}
</script>

<style>

</style>
