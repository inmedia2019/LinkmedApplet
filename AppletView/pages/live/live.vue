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
				<!-- <view class="body">
					<view class="ovh">
						<view class="fl center f12 c9B9B9B">
							<view :class="screen=='最新直播'?'c000':''" @click="upScreen('最新直播')">最新直播</view>
							<view class="w5"></view>
							<view>|</view>
							<view class="w5"></view>
							<view :class="screen=='预约最多'?'c000':''" @click="upScreen('预约最多')">预约最多</view>
						</view>
					</view>
				</view> -->
				<scroll-view class="pa" scroll-y @scrolltolower="updata">
					<view class="body" style="padding-top: 0px;">

						<view class="list">
							<block v-for="(calesw,inx) in listData" :key="inx">
								<view class="shadow listli pr animated fadeIn fast" v-bind:style="{ 'background': 'url('+calesw.thumbnail.src+') no-repeat center','background-size':'cover' }">
									
									<view>
										<template v-if="Dtime<calesw.endTime">
											<view class="caleswnav pa f12 cfff shadow2 bca021b center">
												
												<view class="fb">进入直播间</view>
											</view>
										</template>
										<template v-else-if="Dtime>calesw.endTime">
											<view class="caleswnav pa f12 cfff shadow bAAB2B7 center">
												
												<view class="fb">已结束</view>
											</view>
										</template>
									</view>
									<view>
										<view class="pa whtl" @click="_getUrl(calesw.link,0)"></view>
									</view>
									
								</view>
							</block>
						</view>
					</view>
					<template v-if="!isnone||listData.length!=0">
						<view>
							<uni-load-more iconType="circle" :status="status" />
							<view class="h20"></view>
						</view>
					</template>
					<template v-if="isnone&&listData.length==0">
						<noNe tab="5"></noNe>
					</template>
					<boView></boView>
				</scroll-view>
				<foTter nav="2"></foTter>
			</view>
		</template>
	</view>
</template>

<script>
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
			noNe
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
			var sign = this.$conFig.issign('false',getCurrentPages()[0].route)
			if (sign) {
				this.issign=true
				this.load()
				this.Dtime = (new Date()).getTime();
				this.setTim()
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
			setTim: function(e) {
				setTimeout(() => {
					this.Dtime = this.Dtime + 1000;
					this.setTim()
				}, 1000)
			},
			_getUrl(e){
				console.log(e)
				window.location.href = this.$base64.Base64.decode(e)
			},
			getUrl(e, u) {
				if(this.$conFig.onUrl(e)){
					uni.navigateTo({
						url: e
					});
				}
			},
			load() {
				console.log("//")
				//this.$conFig.alert_Load()
				this.updata()
			},
			upScreen(e) {
				
				console.log(this.screen,e,this.status)
				
				if(this.screen==e||this.status == "loading"){
					return
				}
				this.screen = e
				this.status = "more"
				this.page = "0"
				this.listData = []
				this.updata()
			},
			getDate(date) {
				var arr = date.split('-');
				            var year = arr[0]; //获取当前日期的年份
				            var month = arr[1]; //获取当前日期的月份
				            var day = arr[2]; //获取当前日期的日
				            var days = new Date(year, month, 0);
				            days = days.getDate(); //获取当前日期中月的天数
				            var year2 = year;
				            var month2 = parseInt(month) - 3;
				            if (month2 == 0) {
				                year2 = parseInt(year2) - 3;
				                month2 = 12;
				            }
				            var day2 = day;
				            var days2 = new Date(year2, month2, 0);
				            days2 = days2.getDate();
				            if (day2 > days2) {
				                day2 = days2;
				            }
				            if (month2 < 10) {
				                month2 = '0' + month2;
				            }
				            var t2 = year2 + '-' + month2 + '-' + day2;
				            return t2;
			},
			updata() {
				
				var nowDate = new Date();
				 var year = nowDate.getFullYear();
				 var month = nowDate.getMonth() + 1 < 10 ? "0" + (nowDate.getMonth() + 1)
				  : nowDate.getMonth() + 1;
				 var day = nowDate.getDate() < 10 ? "0" + nowDate.getDate() : nowDate
				  .getDate();
				 var dateStr = year + "-" + month + "-" + day;
				
				if (!this.isUpdata || this.status == "noMore") {
					return
				}
				this.isUpdata = false
				
				
				var url='/linyimaiapi.ashx?Method=broadcast'
				
				if(this.screen=="预约最多"){
					url='/linyimaiapi.ashx?Method=broadcastappointment'
				}

				this.isnone=false
				
				console.log(dateStr)
				
				var sj=this.getDate(dateStr).split('-')
				console.log(sj)
				
				this.status = "loading"
				this.$reqUest.get('inmedia', url, {
					'start_time[value][date]':sj[1]+"/"+sj[2]+"/"+sj[0],
					"category": uni.getStorageSync('timeDepartment').category,
					// "tags": "强生微课",
					page:this.page
				}).then(data => {
					
					
					
					var [error, res] = data;
					
					
					this.listData = []
					this.currentCaleswIndex=0
					
					
					this.issign = true
					uni.hideLoading();
					
					console.log(res.data.result)
					
					
					
					
					this.status = "more"
					if (res.data.result != "true") {
						this.isnone=true
						this.status = "noMore"
						return
					}
					
					this.page++
					
					var list = this.$conFig.newList(JSON.parse(res.data.message))
					
					
					
					for (var i = 0; i < list.length; i++) {
						var etime = list[i].endTime
						var stime = list[i].startTime
						var rtime = list[i].requestTime
					
					
						list[i].endTime = etime.substring(etime.indexOf('/Date(') + 6, etime.length - 2)
						list[i].startTime = stime.substring(stime.indexOf('/Date(') + 6, stime.length - 2)
						list[i].requestTime = rtime.substring(rtime.indexOf('/Date(') + 6, rtime.length - 2)
						this.serverTime = rtime.substring(rtime.indexOf('/Date(') + 6, rtime.length - 2)
						
						list[i].link=this.$base64.Base64.encode(list[i].link)
					}
					
					
					
					
					
					this.listData = [...this.listData, ...list]
					this.isUpdata = true;
					
					
					if (this.$conFig.newListLength(res.data) != 21) {
						this.status = "noMore"
					}
					
					this.isnone=true
					
				
				})

				
				
			}
		}
	}
</script>

<style>
	scroll-view {
		top: 105px;
		left: 0px;
		bottom: 0px;
		right: 0px;
	}
	
	.listli{height: 360rpx;}
</style>
