<template>
	<view class="pf tk center whtl">
		<view @click="subscribeMessage">订阅</view>
	</view>
</template>

<script>
	export default {
		data() {
			return {

			}
		},
		onLoad() {

		},
		
		onShow() {
			this.$conFig.onWeixin("领医迈","　",'/pages/index/index',"s.jpg")
		},
		methods: {
			subscribeMessage() {
				var that=this
				wx.getSetting({
					// withSubscriptions: true,//是否获取用户订阅消息的订阅状态，默认false不返回
					success(res) {
						if (res.authSetting['scope.subscribeMessage']) {
							uni.openSetting({ // 打开设置页
								success(res) {
									console.log("11111")
									console.log(res.authSetting)
								}
							});
						} else { // 用户没有点击“总是保持以上，不再询问”则每次都会调起订阅消息
							uni.requestSubscribeMessage({
								tmplIds: ['oDxpzlHK3qcsrdZxUY9w6FlWazzxrkOjXBfBEStGiS8'], // 
								success(res) {
									
									if (res['oDxpzlHK3qcsrdZxUY9w6FlWazzxrkOjXBfBEStGiS8'] == "accept") { // 字段就是tmplIds模板id
										that.$conFig.alert_Load()
										that.$reqUest.get('inmedia', '/linyimaiapi.ashx?Method=wechasubscription', {
											"openid": uni.getStorageSync('userInfo').openid
										}).then(data => {
											var [error, res] = data;
											if(res.data.result=="true"){
												
											}
											
										})
									}else{
										console.log("---")
									}
								}
							})
						}
					}
				})
			}


		}
	}
</script>

<style>

</style>
