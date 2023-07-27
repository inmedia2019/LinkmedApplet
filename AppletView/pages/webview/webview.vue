<template>
	<view class="bF5F6FA">
		<web-view :src="url" @message="getMessage" @onPostMessage="getPostMessage"></web-view>
	</view>
</template>

<script>
	export default {
		data() {
			return {
				url: ''
			}
		},
		onShow() {
			this.$conFig.onWeixin("领医迈","　",'/pages/index/index',"s.jpg")
		},
		onLoad(options) {
			this.url = this.$base64.Base64.decode(options.id);
		},
		methods: {
			getMessage(event) {
				console.log(event)
				uni.showModal({
					content: JSON.stringify(event.detail),
					showCancel: false
				});
			},
			getPostMessage(event){
				console.log(event)
			}
		}
	}
</script>

<style>

</style>