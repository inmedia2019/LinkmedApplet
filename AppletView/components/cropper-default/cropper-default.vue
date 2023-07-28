<template>
	<view class="container">
		<tui-image-cropper :lockRatio="true" :imageUrl="imageUrl" @ready="ready" @cropper="cropper"></tui-image-cropper>
	</view>
</template>

<script>
export default {
	data() {
		return {
			src: '',
			imageUrl: '' //要裁剪的图片
		};
	},
	onLoad(options) {
		this.src = options.src || '';
		//如果从上个页面传值时可显示loading
		this.src &&
			uni.showLoading({
				title: '请稍候...',
				mask: true
			});
	},
	methods: {
		ready() {
			this.imageUrl = this.src;
		},
		cropper(e) {
			this.$conFig.alert_Load()
			var _this = this
			console.log(e.url)
			
			this.$reqUest.post('inmedia', '/linyimaiapi.ashx?Method=base64url', {
				"base64": e.url
			}).then(data => {
				var [error, res] = data;
				console.log(res)
				console.log("/////")
				
				uni.setStorage({
				    key: 'tximg',
				    data: res.data
				});
				uni.navigateBack({
					delta: 1
				});
			})
			
			return
			
			
			uni.uploadFile({
				url: this.$conFig.linkmedApi + '/linkmed2020/feedback/upload', //仅为示例，非真实的接口地址
				filePath: e.url,
				header:{
					'content-type':"application/x-www-form-urlencoded",
					'linkmed-token':'6CxkakZH',
					'phone': '',
					'openid': uni.getStorageSync('_wxuserInfo').openid,
					'unionid': uni.getStorageSync('_wxuserInfo').unionid
				},
				name: 'file',
				success: (uploadFileRes) => {
					
					console.log(uploadFileRes)
					
					uni.setStorage({
					    key: 'tximg',
					    data: uploadFileRes.data
					});
					uni.navigateBack({
						delta: 1
					});
				}
			});
		}
	}
};
</script>

<style></style>
