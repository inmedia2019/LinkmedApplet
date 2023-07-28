<template>
	<view class="pf return center" @click="reTurn">
		返回
	</view>
</template>

<script>
	export default {
		data() {
			return {
				
			}
		},
		methods: {
			reTurn(e){
				if(getCurrentPages().length>1){
					if(getCurrentPages()[0].route=="pages/interest/interest"){
						uni.reLaunch({
						    url: '/pages/index/index'
						});
					}else{
						uni.navigateBack({
							delta: 1
						});
					}
				}else{
					uni.reLaunch({
					    url: '/pages/index/index'
					});
				}
			}
		}
	}
</script>

<style>
	.return{ bottom: 100px; right: 20px; width: 50px; height: 50px; border-radius: 100px; background: #fff; box-shadow: 0px 0px 10px rgba(0,0,0,0.3); }
</style>
