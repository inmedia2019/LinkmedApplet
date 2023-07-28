<template>
	<view class="pf whtl bF5F6FA">
		<template v-if="issign">
			<view class="pf whtl animated fadeIn fast">
				<view class="cfff pr ovh">
					<image class="pa w100" src="../../static/bg.jpg" mode="widthFix"></image>
					<view class="body ovh pr z3">
						<view class="ovh">
							<view class="fl center">
								<image style="width: 90px; height: 56px;" src="../../static/logo.png" mode="widthFix"></image>
							</view>
							<view class="fr topfr">
								<view class="fr h25"></view>
								<view class="clear"></view>
								<view class="h5"></view>
								<view class="topinput ovh" @click="getUrl('../search/search',0)">
									<view class="iconfont icon-sousuo fl" style="height: 15px;"></view>
									<view class="w10 h5 fl"></view>
									<view class="fl">输入你的搜索</view>
								</view>
							</view>
						</view>
					</view>
				</view>
				<scroll-view class="pa" scroll-y @scrolltolower="updata">
					<view class="body" style="padding-top: 0px;">
						<view class="list">
							<block v-for="(list,inx) in listData" :key="inx">
								<view class="shadow listli pr animated fadeIn fast" @click="getUrl(list.url,0)" v-bind:style="{ 'background': 'url('+list.src+') no-repeat center','background-size':'cover' }">
									<view class="pa bannebtbg f15 cfff ellipsis" style=" background: url(http://h5.atline.cn/linkmedxcx/bannebtbg.png) no-repeat; background-size:100% 100%;">
										{{list.title}}
									</view>
								</view>
							</block>
						</view>
					</view>
				</scroll-view>
			</view>
			<reTurn></reTurn>
		</template>
	</view>
</template>

<script>
	import reTurn from '@/components/return/return.vue'
	import foTter from '@/components/footer.vue'
	import boView from '@/components/botview.vue'
	import uniLoadMore from '@/components/uni-load-more/uni-load-more.vue'
	import uniSection from '@/components/uni-section/uni-section.vue'
	import noNe from '@/components/none.vue'
	export default {
		components: {
			foTter,
			boView,
			uniLoadMore,
			uniSection,
			noNe,
			reTurn
		},
		data() {
			return {
				issign: false,
				isnone:false,
				page: 0,
				Dtime:"",
				screen: "最新直播",
				listData: [],
				status: 'more',
				isUpdata: true,
				statusTypes: [{
					value: 'more',
					text: '加载前',
					checked: true
				}, {
					value: 'loading',
					text: '加载中',
					checked: false
				}, {
					value: 'noMore',
					text: '没有更多',
					checked: false
				}]
			}
		},
		onLoad(e) {
			var sign = this.$conFig.issign('false',getCurrentPages()[0].route,e.id)
			if (sign) {
				
				if(uni.getStorageSync('timeDepartment').category=="普外科"){
					this.listData=[{
						src:this.$conFig.inmediaApi+"/sp2.png",
						title:"普外达人训练营",
						url:'../webview/webview?id='+this.$base64.Base64.encode('https://w.linkmed.cn/wx/View_s/default.aspx')
					}]
				}else{
					this.listData=[{
						src:this.$conFig.inmediaApi+"/sp1.png",
						title:"胸外菁英训练营 ",
						url:'../webview/webview?id='+this.$base64.Base64.encode('https://w.linkmed.cn/wx/View_jy/default.aspx')
					}]
				}
				
				
				this.issign=true
			}
		},
		
		onShow() {
			this.$conFig.onWeixin("领医迈","　",'/pages/index/index',"s.jpg")
		},
		onShareAppMessage(res) {
			return {
				title: "领医迈",
				path: '/pages/special/special'
			}
		},
		methods: {
			getUrl(e, u) {
				if(uni.getStorageSync('userInfo').uid==undefined){
					this.login(e)
					return
				}
				
				if(this.$conFig.onUrl(e)){
					uni.navigateTo({
						url: e
					});
				}
			},
			login(url){
				uni.setStorage({
					key: 'timeUrl',
					data: url
				});
				if(uni.getStorageSync('wxuserInfo')==""){
					uni.navigateTo({
						url: "../login/login"
					});
					return
				}
				if(uni.getStorageSync('userInfo').uid==undefined){
					uni.navigateTo({
						url: "../sign/sign"
					});
					return
				}
			},
		}
	}
</script>

<style>
	scroll-view {
		top: 110px;
		left: 0px;
		bottom: 0px;
		right: 0px;
	}
	
	.listli{height: 320rpx;}
</style>
