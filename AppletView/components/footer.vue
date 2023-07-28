<template>
	<view class="pf navbottom center f12 z3">
		<image class="pa navbg" src="../static/navbg.png" mode="widthFix"></image>
		<view class="pr z3 center w100">
			<view class="navbottomnr1 center">
				<view class="tec" :class="nav==1?'on':''" @click="nav!=1?getUrl('../videolist/videolist?id=0',0):''">
					<view class="center navzi">
						<image class="navzii1" style="width: 26px;" src="../static/navzi1s.png" mode="widthFix"></image>
						<image class="navzii2" style="width: 26px;" src="../static/navzi1.png" mode="widthFix"></image>
					</view>
					<view class="h5"></view>
					<view class="center">
						课程回放
					</view>
				</view>
				<view class="tec" :class="nav==2?'on':''" @click="nav!=2?getUrl('../live/live',0):''">
					<view class="center navzi">
						<image class="navzii1" style="width: 30px;" src="../static/navzi2s.png" mode="widthFix"></image>
						<image class="navzii2" style="width: 30px;" src="../static/navzi2.png" mode="widthFix"></image>
					</view>
					<view class="h5"></view>
					<view class="center">
						今日课程
					</view>
				</view>
			</view>
			<view class="navbottomnr2" @click="nav!=0?getUrl('../index/index',0):''">
				<view class="center shadow2">
					<image style="width: 28px;" src="../static/home.png" mode="widthFix"></image>
					
				</view>
			</view>
			<view class="navbottomnr3 center">
				<view class="tec" :class="nav==3?'on':''" @click="nav!=3?getUrl('../calendar/calendar',0):''">
					<view class="center navzi">
						<image class="navzii1" style="width: 28px;" src="../static/navzi3s.png" mode="widthFix"></image>
						<image class="navzii2" style="width: 28px;" src="../static/navzi3.png" mode="widthFix"></image>
					</view>
					<view class="h5"></view>
					<view class="center">
						课程日历
					</view>
				</view>
				<view class="tec" :class="nav==4?'on':''" @click="nav!=4?getUrl('../my/my',0):''">
					<view class="center navzi">
						<image class="navzii1" style="width: 26px;" src="../static/navzi4s.png" mode="widthFix"></image>
						<image class="navzii2" style="width: 26px;" src="../static/navzi4.png" mode="widthFix"></image>
					</view>
					<view class="h5"></view>
					<view class="center">
						我的
					</view>
				</view>
			</view>
		</view>
	</view>
</template>

<script>
	export default {
		props: {
			nav: {
				type: String
			}
		},
		data() {
			return {
				
			}
		},
		methods: {
			
			getUrl(e, u) {
				
				if(this.$conFig.onUrl(e)){
					getApp().globalData.isshow = true
					
					if(e=="../index/index"){
						var _sign=uni.getStorageSync('sign').split("/")
						
						uni.redirectTo({
							url: e
						});
						
						// if(_sign[_sign.length-1]=='true'){
							
						// }else{
						// 	uni.navigateBack({
						// 		delta: 99
						// 	});
						// }
						
						return
					}
					
					if(getCurrentPages().length!=1){
						uni.redirectTo({
							url: e
						});
					}else{
						uni.navigateTo({
							url: e
						});
					}
				}
			}
		}
	}
</script>

<style>

</style>
