<template>

	<view class="">
		<view class="cfff pr ovh">
			<image src="../../static/bg.jpg" class="pa w100" mode="widthFix"></image>
			<view class="body ovh pr z3" style="padding-bottom: 7px;">
				<view class="ovh">
					<view class="fl center">
						<image style="width: 90px;height: 56px;" src="../../static/logo.png" mode="widthFix"></image>
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
				<view class="h10"></view>
			</view>
		</view>
		<view class="body">
			<tui-calendar :arrowType="2" isChange :status="monthStatus" @change="monthChange"></tui-calendar>
			
		</view>

		<view class="body" style="padding-top: 0px;">
			<view class="list">
				<block class="" v-for="(data,index) in listData">
					<view class="shadow listli" @click="_getUrl(data.link,0)" style="background: url(https://w.linkmed.cn/linkmed-Applet-API/miniprogram/static/yd.png) no-repeat bottom left #fff; background-size:115px ;">
						<view class="">
							<view class="fl shadow listliimg" v-bind:style="{ 'background': 'url('+data.thumbnail.src+') center center / cover no-repeat' }"></view>
							<view class="fr f14" style="width: calc(100% - 125px);">
								{{data.title}}
							</view>
						</view>
						<view style="clear:both;"></view>
						<view class="h5"></view>
						<view class="pr" style=" height: 42px;">
							<view class="pa" style=" top: 5px; right: -25px;">
								<template v-if="Dtime>data.endTime">
									<image style="width: 235px; height: 42px;" src="../../static/timgts.png" mode="widthFix"></image>
								</template>
								<template v-else>
									<image style="width: 235px; height: 42px;" src="../../static/timgt.png" mode="widthFix"></image>
								</template>
								<view class="pa whtl center cfff" style="height: 40px;">
									{{datetime('',data.startTime).split(" ")[1]}}-{{datetime('',data.endTime).split(" ")[1]}}<view class="w10"></view>|<view class="w10"></view>进入直播间<view class="w10"></view><image style="width: 17px; height: 17px;" src="../../static/timgicon.png" mode=""></image>
								</view>
							</view>
						</view>
					</view>
				</block>
				<template v-if="isnone&&listData.length==0">
					<noNe tab="4"></noNe>
				</template>
				
				<template v-if="!isnone||listData.length!=0">
					<view>
						<uni-load-more iconType="circle" :status="status" />
						<view class="h20"></view>
					</view>
				</template>
			</view>
			
		</view>
		<foTter nav="3"></foTter>
		<view class="h30"></view>
		<!-- <boView></boView> -->
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
			const currentDate = this.getDate({
				format: true
			})
			return {
				newTime: "",
				oldTime: "",
				issign: false,
				isnone: false,
				date: currentDate,
				serverTime: "",
				timeData: [],
				timeW: "173",
				timeIndex: 0,
				page: 0,
				Dtime: "",
				isClickday: false,
				monthStatus: [],
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
			var sign = this.$conFig.issign('false', getCurrentPages()[0].route)
			if (sign) {
				this.issign = true
				//this.$conFig.alert_Load()
				this.load()

				this.Dtime = (new Date()).getTime();
				this.setTim()
			}
		},
		onShow() {
			this.$conFig.onWeixin("领医迈", "　", "/pages/index/index", "s.jpg")
		},
		onShareAppMessage(res) {

			return {
				title: "领医迈",
				path: '/pages/index/index'
			}
		},
		methods: {
			monthChange(e) {

				if (e.switch) {
					this.monthStatus = []


					this.$conFig.alert_Load()
					this.$reqUest.get('linkmed', '/linkmed2020/broadcast-calendar', {
						"category": uni.getStorageSync('timeDepartment').category,
						"date": e.year + "-" + e.month,
					}).then(data => {
						var [error, res] = data;

						uni.hideLoading();
						var calendarday = []
						for (var i = 0; i < res.data.length; i++) {

							var _month = e.month
							if (_month < 10) {
								_month = "0" + _month
							}
							var newday = (res.data[i].startTime.split(" ")[0]).split(e.year + "-" + _month + "-")[1]
							calendarday.push(newday < 10 ? newday.substr(1) : newday)

						}

						function unique(arr) {
							let hash = [];
							for (let i = 0; i < arr.length; i++) {
								if (hash.indexOf(arr[i]) === -1) {
									hash.push(arr[i]);
								}
							}
							return hash;
						}
						calendarday = unique(calendarday)

						if (e.switch) {
							let now = new Date();
							let year = now.getFullYear();
							let month = now.getMonth() + 1;
							let day = now.getDate() - 1
							if (e.year == year && e.month == month) {
								let days = e.days;
								let monthStatus = []
								for (let i = 0; i < days; i++) {
									let bgColor = "";
									let color = ""
									let check = false;
									var data = false
									if (i <= day) {
										if (!this.isClickday) {
											bgColor = i === day ? "#ca021b" : "";
											color = i === day ? "#fff" : "";
											check = i === day ? true : false;
										}
									} else {
										bgColor = "";
										color = "";
									}
									//data = i === day ? true : false;

									monthStatus.push({
										bgColor: "",
										color: "",
										check: check,
										data: data,
										calendar: calendarday
									})

								}
								this.monthStatus = monthStatus


							} else {
								//this.monthStatus = []

								let days = e.days;
								let monthStatus = []
								for (let i = 0; i < days; i++) {
									let bgColor = "";
									let color = ""
									let check = false;
									var data = false


									monthStatus.push({
										bgColor: "",
										color: "",
										check: "",
										data: "",
										calendar: calendarday
									})

								}
								this.monthStatus = monthStatus
							}
						}
					})
				} else {
					this.isUpdata = true
					this.status = "loading"
					this.page = "0"
					this.updata(e.year, e.month, e.day)
					this.isClickday = true

					//this.monthStatus = monthStatus
				}
			},
			setTim: function(e) {
				setTimeout(() => {
					this.Dtime = this.Dtime + 1000;
					this.setTim()
				}, 1000)
			},
			load() {
				const date = new Date();
				let year = date.getFullYear();
				let month = date.getMonth() + 1;
				let day = date.getDate();

				this.isUpdata = true
				this.status = "loading"
				this.page = "0"
				this.updata(year, month, day)
				this.getDatenum(year, month)
			},
			_getUrl(e) {
				window.location.href = this.$base64.Base64.decode(e)
			},
			getUrl(e, u) {
				if (this.$conFig.onUrl(e)) {
					uni.navigateTo({
						url: e
					});
				}
			},
			onsw(i) {
				this.timeIndex = i
			},
			timeDataChange: function(e) {
				let current = e.mp.detail.current;
				this.timeIndex = current;

				this.isUpdata = true
				this.status = "loading"
				this.page = "0"
				this.updata(this.date.split("-")[0], this.date.split("-")[1], this.timeIndex + 1)
			},
			bindDateChange: function(e) {
				this.date = e.target.value
				this.getDatenum(this.date.split("-")[0], this.date.split("-")[1])

				this.isUpdata = true
				this.status = "loading"
				this.page = "0"
				this.updata(this.date.split("-")[0], this.date.split("-")[1], this.timeIndex + 1)
			},
			getDate(type) {
				const date = new Date();
				let year = date.getFullYear();
				let month = date.getMonth() + 1;
				let day = date.getDate();

				if (type === 'start') {
					year = year - 60;
				} else if (type === 'end') {
					year = year + 2;
				}
				month = month > 9 ? month : '0' + month;;
				day = day > 9 ? day : '0' + day;

				this.getDatenum(year, month)
				return `${year}-${month}`;
			},
			getDatenum(year, month) {
				var d = new Date(year, month, 0);

				var time = []
				for (var i = 0; i < d.getDate(); i++) {
					var _i = i + 1
					time.push({
						"time": _i < 10 ? '0' + _i : _i,
						"weekday": this.timehs(year + "-" + month + "-" + _i)
					})

				}



				const date = new Date();
				let _year = date.getFullYear();
				let _month = date.getMonth() + 1;
				let _day = date.getDate();

				if (year == _year && month == _month) {
					this.timeIndex = _day - 1
				} else {
					this.timeIndex = 0
				}
				this.timeData = time
			},
			timehs(e) {
				var mydate = new Date(e.replace(/-/g, '/'));
				var myddy = mydate.getDay(); //获取存储当前日期
				var weekday = ["星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六"];

				return weekday[myddy]
			},
			dateClick(e) {

			},
			datetime(format, timestamp){ 
				var now = new Date(parseInt(timestamp));
					Date.prototype.toLocaleString = function() {
						
						var min=this.getMinutes()
						if(min<10){
							min="0"+min
						}
						
						return this.getFullYear() + "/" + (this.getMonth() + 1) + "/" + this.getDate() + "/ " + this.getHours() + ":" + min;
					};
			      return now.toLocaleString();
			},
			
			updata(y, m, d) {

				if (!this.isUpdata || this.status == "noMore") {
					return
				}
				this.isUpdata = false
				this.status = "loading"
				this.isnone = false

				this.listData = []

				this.$reqUest.get('inmedia', '/linyimaiapi.ashx?Method=broadcast', {
					"category": uni.getStorageSync('timeDepartment').category,
					"openid": uni.getStorageSync('wxuserInfo').openid,
					"tags": "强生微课",
					"date": y + "-" + m + "-" + d,
					page: this.page
				}).then(data => {
					var [error, res] = data;
					this.isnone = true
					


					this.currentCaleswIndex = 0


					this.issign = true
					uni.hideLoading();



					this.newTime = y + "-" + m + "-" + d

					if (this.newTime != this.oldTime) {
						this.listData = []
					}
					this.oldTime = this.newTime


					this.status = "more"

					if (res.data.result != "true") {
						this.status = "noMore"
						return
					}

					this.page++

					var list = this.$conFig.newList(JSON.parse(res.data.message))
					console.log(list)


					for (var i = 0; i < list.length; i++) {
						var etime = list[i].endTime
						var stime = list[i].startTime
						var rtime = list[i].requestTime

						list[i].endTime = etime.substring(etime.indexOf('/Date(') + 6, etime.length - 2)
						list[i].startTime = stime.substring(stime.indexOf('/Date(') + 6, stime.length - 2)
						list[i].requestTime = rtime.substring(rtime.indexOf('/Date(') + 6, rtime.length - 2)
						this.serverTime = rtime.substring(rtime.indexOf('/Date(') + 6, rtime.length - 2)

						list[i].link = this.$base64.Base64.encode(list[i].link)
					}

					var newList = [...this.listData, ...list]
					this.listData=[]
					for(var i=0;i<newList.length;i++){
						this.listData.unshift(newList[i])
					}
					
					
					this.isUpdata = true;

					if (this.$conFig.newListLength(res.data) != 21) {
						this.status = "noMore"
					}



				})


			}
		}
	}
</script>

<style>
	page {
		background: rgba(249, 251, 254, 1);
	}

	.swiper {
		color: #3E3D3D;
		height: 57px;
	}

	.swiperzhou {}

	.swiperr {
		font-size: 22px;
	}

	.swiper .tec.on {
		color: #B92727;
	}

	.swiper .tec.on .swiperr {
		font-size: 32px;
		line-height: 1;
	}

	.calejbl,
	.calejbr {
		width: 128px;
		height: 48px;
		top: 0px;
		height: 100%;
		pointer-events: none;
	}

	.calejbl {
		left: 0px;
		background: linear-gradient(90deg, rgba(249, 251, 254, 1) 0%, rgba(249, 251, 254, 0) 100%);
	}

	.calejbr {
		right: 0px;
		background: linear-gradient(90deg, rgba(249, 251, 254, 0) 0%, rgba(249, 251, 254, 1) 100%);
	}

	.listli {
		height: auto;
		background: #fff;
		padding: 10px;
	}
	.listliimg{ width: 113px; height: 57px; border-radius: 5px; }
</style>
