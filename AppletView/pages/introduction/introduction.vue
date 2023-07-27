<template>
	<view class="pa whtl bF5F6FA">
		<scroll-view class="" scroll-y="auto">
			<template v-if="issign">
				<view class="animated fadeIn fast">
					<view class="cfff ovh" style=" height: 90px;">
						<image class="fl w100" style="" src="../../static/bg.jpg" mode="widthFix"></image>
					</view>
					<view class="body pr z3" style="margin-top: -70px;">
						<view class="intrbody body shadow pr" style="background: #fff;">
							<image class="pa yd" style="" src="../../static/yd.png" mode="widthFix"></image>
							<template v-if="info.ishot==1">
								<view class="hot pa center cfff f10">
									<view class="iconfont icon-huo center" style="height: 18px;"></view>
									<view class="w5"></view>
									热门
								</view>
							</template>
							<view class="pr">
		
								<view class="center">
									<view class="intrimg" v-bind:style="{ 'background': 'url('+info.headImg+') no-repeat center','background-size':'cover' }"></view>
									<view class="intrname">
										<view class="f18 c000">{{info.name}}</view>
										<view class="h3"></view>
										<template v-if="info.hospital!=''">
											<view class="ovh">
												<view class="f12 intrnameyy cfff bbs fl">{{info.hospital}}</view>
											</view>
										</template>
										<view class="h3"></view>
										<template v-if="info.clickNum>=0">
											<!-- <view class="f12 c000">访问量：<text class="cB92727">{{info.clickNum}}</text></view> -->
										</template>
									</view>
								</view>
								<view class="h20"></view>
								<template v-if="info.position.length!=0">
									<view class="h1 bDEDDDE"></view>
									<view class="h20"></view>
									<view class="ovh">
										<image class="fl" style="width: 20px;" src="../../static/intri1.png" mode="widthFix"></image>
										<view class="w10 h5 fl"></view>
										<view class="fl c000 fb" style="margin-top: 6px;">职称</view>
									</view>
									<view class="h5"></view>
									<view class="c4A4A4A">
										{{info.position}}
									</view>
								</template>
								<template v-if="info.introduce.length!=0">
									<view class="h20"></view>
									<view class="ovh">
										<image class="fl" style="width: 20px;" src="../../static/intri2.png" mode="widthFix"></image>
										<view class="w10 h5 fl"></view>
										<view class="fl c000 fb" style="margin-top: 6px;">简介</view>
									</view>
									<view class="h5"></view>
									<view class="c4A4A4A">
										{{info.introduce}}
									</view>
								</template>
								<view class="h10"></view>
							</view>
						</view>
						<view class="h25"></view>
						<template v-if="listData.length!=0">
							<view class="">
								<view class="ovh">
									<view class="fl center f18">
										<image style="width: 9px;" src="../../static/titlei.png" mode="widthFix"></image>
										<view class="w10 h5"></view>
										<view class="fb">作品列表</view>
									</view>
									<view class="fr">
										<view class="center c9B9B9B f12">
											<view :class="screen=='最新作品'?'cB92727':''" @click="upScreen('最新作品')">最新作品</view>
											<view class="w5 h5"></view>
											<view class="">|</view>
											<view class="w5 h5"></view>
											<view :class="screen=='浏览最多'?'cB92727':''" @click="upScreen('浏览最多')">浏览最多</view>
										</view>
									</view>
								</view>
								<view class="h18"></view>
								<block v-for="(list,inx) in listData" :key="inx">
									<view class="videolist shadow animated fadeIn fast pr" @click="getUrl('../details/details?id='+list.id,0)">
										<image class="pa yd" style="" src="../../static/yd.png" mode="widthFix"></image>
										<view class="pr z3">
											<view class="fl shadow videolistimg ovh pr" v-bind:style="{ 'background': 'url('+list.thumbnail.src+') no-repeat center','background-size':'cover' }">
												<view class="pa whtl center">
													<view class="iconfont icon-bofang cfff" style="font-size: 25px; height: 30px;"></view>
													<!-- <text class="pa cfff f10">22:23</text> -->
												</view>
									
											</view>
											<view class="fr videolistname f13">
												<view class="cB92727 videolistnametxt ovh">{{list.title}}</view>
												<view class="h5"></view>
												<view class="ovh">
													<view class="fl center">
														<view class="videolistnametx" v-bind:style="{ 'background': 'url('+info.headImg+') no-repeat center','background-size':'cover' }">
									
														</view>
														<view class="w10"></view>
														<view class="c000">{{list.expert}}</view>
													</view>
												</view>
											</view>
										</view>
										<view class="clear"></view>
										<view class="h15"></view>
										<view class="f12 c4A4A4A maxh ovh">
											<rich-text :nodes="list.expert_intro"></rich-text>
										</view>
										<view class="h10"></view>
										<view class="f12 cB92727">
											{{list.has_thumbup}}赞 · {{list.comment_count}}评论 · {{list.forwardNum}}转发
										</view>
									</view>
								</block>
							</view>
						</template>
						
					</view>
				</view>
			</template>
			
		</scroll-view>
		<reTurn></reTurn>
	</view>
</template>

<script>
	import reTurn from '@/components/return/return.vue'
	export default {
		components: {
			reTurn
		},
		data() {
			return {
				issign: false,
				info: {},
				uid:"",
				screen: "最新作品",
				listData: []
			}
		},
		onLoad(e) {
			//49077
			//console.log(e.id)
			
			this.uid=e.id
			var sign = this.$conFig.issign('false',getCurrentPages()[0].route)
			if (sign) {
				this.$conFig.alert_Load()
				this.load()
			}
		},
		
		onShow() {
			this.$conFig.onWeixin("领医迈","　",'/pages/index/index',"s.jpg")
		},
		onShareAppMessage(res) {
			
			return {
				title: "领医迈",
				path: '/pages/index/index'
			}
		},
		methods: {
			getUrl(e, u) {
				if(this.$conFig.onUrl(e)){
					uni.navigateTo({
						url: e
					});
				}
			},
			upScreen(e) {
				if (this.screen == e) {
					return
				}

				this.screen = e

				
				this.updata()
			},
			load(e) {
				this.$reqUest.get('inmedia', '/linyimaiapi.ashx?Method=expertinfodetail', {
					uid: this.uid
				}).then(data => {
					var [error, res] = data;
					
					console.log(this.$conFig.newList(JSON.parse(res.data.message))[0])
					console.log("/////")
					
					if(this.$conFig.newList(JSON.parse(res.data.message))[0]==undefined){
						this.info={
							headImg:uni.getStorageSync('userInfo').avatar,
							name:uni.getStorageSync('userInfo').name,
							hospital:uni.getStorageSync('userInfo').hospital,
							position:uni.getStorageSync('userInfo').position,
							introduce:"",
							clickNum:""
						}
					}else{
						this.info = this.$conFig.newList(JSON.parse(res.data.message))[0]
					}
					
					
					
					this.updata()

				})

			},
			updata(e) {
				this.$conFig.alert_Load()
				var url = '/linyimaiapi.ashx?Method=videoinfo'

				if (this.screen == "浏览最多") {
					url = '/linyimaiapi.ashx?Method=videoinfoplay'
				}
				this.$reqUest.get('inmedia', url, {
					uid: this.uid
				}).then(data => {
					var [error, res] = data;
					
					
					
					this.listData = []
					this.listData = this.$conFig.newList(JSON.parse(res.data.message))
					console.log(this.listData)
					this.issign = true
					uni.hideLoading()
					
					
					console.log(JSON.parse(res.data.message))
					console.log(this.$conFig.newList(JSON.parse(res.data.message)))
				})
			}
		}
	}
</script>

<style>
	.intrbody {
		border: #DEDDDE solid 1px;
		border-radius: 5px;
	}

	.hot {
		width: 54px;
		height: 18px;
		background: #BB2695;
		border-radius: 5px;
		top: 17px;
		right: -7px;
	}

	.intrimg {
		width: 80px;
		height: 80px;
		border-radius: 5px;
	}

	.intrname {
		width: calc(100% - 95px);
		margin-left: 15px;
	}

	.intrnameyy {
		border-radius: 5px;
		padding: 3px 10px;
	}
</style>
