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
								<view class="shadow listli pr animated fadeIn fast" @click="getUrl('../actlist/actlist?id='+list.Id,0)" v-bind:style="{ 'background': 'url('+list.moreCol+') no-repeat center','background-size':'cover' }">
									<view class="pa bannebtbg f15 cfff ellipsis">
										<image class="pa whtl" src="../../static/bannebtbg.png" mode="widthFix"></image>
										<view class="pr whtl z3">
											{{list.stitle}}
										</view>
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
				isnone: false,
				page: 0,
				Dtime: "",
				screen: "全部",
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
		onLoad() {
			var sign = this.$conFig.issign('false',getCurrentPages()[0].route)
			if (sign) {
				this.$conFig.alert_Load()
				this.issign = true
				this.load()
				this.Dtime = (new Date()).getTime();
				this.setTim()
			}
		},
		onShow() {
			this.$conFig.onWeixin("领医迈","　","/pages/index/index","s.jpg")
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
			getUrl(e, u) {
				if (this.$conFig.onUrl(e)) {
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

				console.log(this.screen, e, this.status)

				if (this.screen == e || this.status == "loading") {
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
				var month = nowDate.getMonth() + 1 < 10 ? "0" + (nowDate.getMonth() + 1) :
					nowDate.getMonth() + 1;
				var day = nowDate.getDate() < 10 ? "0" + nowDate.getDate() : nowDate
					.getDate();
				var dateStr = year + "-" + month + "-" + day;

				if (!this.isUpdata || this.status == "noMore") {
					return
				}
				this.isUpdata = false

				this.isnone = false

				var sj = this.getDate(dateStr).split('-')

				console.log(sj[1] + "/" + sj[2] + "/" + sj[0])
				this.status = "loading"
				this.$reqUest.get('inmedia', "/linyimaiapi.ashx?Method=activityinfo", {
					category:uni.getStorageSync('timeDepartment').category,
					page: this.page
				}).then(data => {
					var [error, res] = data;

					
					

					this.page++

					var list = JSON.parse(res.data.message)
					
					console.log(list)
					uni.hideLoading()


					this.listData = [...this.listData, ...list]
					this.isUpdata = true;


					if (this.$conFig.newListLength(res.data) != 21) {
						this.status = "noMore"
					}

					this.isnone = true

				})
			}
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

	.listli {
		height: 360rpx;
	}
</style>
