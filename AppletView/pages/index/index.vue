<template>
	<scroll-view @scrolltolower="upper" class="">
		<view class="pr animated fadeIn fast">
			<image class="dn" style="width: 0px; height: 0px;" src="../../static/wym.png" mode="widthFix"></image>
			<image class="pa w100" style="" src="../../static/bg.jpg" mode="widthFix"></image>
			<view class="body pr z3">
				<view class="cfff">
					<view class="fl center">
						<image style="width: 90px; height: 56px;" src="../../static/logo.png" mode="widthFix"></image>
					</view>
					<view class="fr topfr">
						<view class="fr">
							<view class="center fl f15">
								<picker @change="bindPickerChange" :value="arrayIndex" :range="array" range-key="title">
									<view class="center">
										<view class="">查看更多学科</view>
										<view class="w5"></view>
										<view class="triangle-down"></view>
									</view>
								</picker>
							</view>
							<template v-if="uid>0">
								<view class="w15 h5 fl"></view>
								<view class="center fl f15 pr" @click="getUrl('../notice/notice?id=1',0)">
									<view class="pr icon-tongzhi1 iconfont" style="font-size: 25px; height: 30px; margin-top: -5px; ">
										<template v-if="messages!=0">
											<view class="noticetxt pa f8 bD43D4C cfff center">{{messages}}</view>
										</template>
									</view>
								</view>
							</template>

						</view>
						<view class="clear"></view>
						<view class="h5"></view>
						<view class="topinput ovh" @click="getUrl('../search/search',0)">
							<view class="iconfont icon-sousuo fl" style="height: 15px;"></view>
							<view class="w10 h5 fl"></view>
							<view class="fl">输入你的搜索</view>
						</view>
					</view>
				</view>
				<view class="clear"></view>
				<view class="h20"></view>
				<view class="banne pr">
					<template v-if="bannerData.length>0">
						<swiper class="bannerBox ovh" :current="timeIndex" autoplay="true" interval="5000" duration="500" @change="bannerChange">
							<block v-for="(banner,inx) in bannerData">
								<swiper-item>
									<template v-if="banner.type=='网络直播'">
										<view class="bannerBoxhezi shadow pr" v-bind:style="{ 'background': 'url('+banner.thumbnail.src+') no-repeat center #fff','background-size':'cover' }">
											<view class="pa bannebtbg f15 cfff ellipsis" style=" background: url(https://w.linkmed.cn/linkmed-Applet-API/miniprogram/static/bannebtbg.png) no-repeat; background-size:100% 100%;">
												{{banner.title}}
											</view>
											<!-- <template v-if="Dtime<banner.startTime&&banner.IsMark==false">
												<view class="pa zbz on center cfff f18">
													预约
												</view>
											</template>
											<template v-if="Dtime<banner.startTime&&banner.IsMark==true">
												<view class="pa zbz on center cfff f18">
													已预约
												</view>
											</template> -->
											<!-- <view>
												<template v-if="(Dtime>=banner.startTime)&&(Dtime<=banner.endTime)">
													<view class="pa zbz on center cfff f14">
														<view class="zbzdian"></view>
														<view class="w10 h5"></view>
														进入直播间
													</view>
												</template>
												<template v-else-if="Dtime>banner.endTime">
													<view class="pa zbz center cfff f14">
														<view class="zbzdian on"></view>
														<view class="w10 h5"></view>
														已结束
													</view>
												</template>
											</view> -->
											<view>
												<view class="pa whtl" @click="_getUrl(banner.link,0)"></view>
												<!-- <view class="pa whtl" @click="getUrl('../webview/webview?id='+banner.link,0)"></view> -->
											</view>  
										</view>
									</template>
									<template v-else>
										<view class="bannerBoxhezi shadow pr" v-bind:style="{ 'background': 'url('+banner.thumbnail.src+') no-repeat center #fff','background-size':'cover' }">
											<view class="pa bannebtbg f15 cfff ellipsis" style=" background: url(https://w.linkmed.cn/linkmed-Applet-API/miniprogram/static/bannebtbg.png) no-repeat; background-size:100% 100%;">
												{{banner.title}}
											</view>
											<view class="pa whtl" @click="getUrl('../details/details?id='+banner.id,0)"></view>
										</view>
									</template>
								</swiper-item>
							</block>
						</swiper>
						<view class="h15"></view>
						<view class="center">
							<view class="dot trall3" :class="{active: currentBannerIndex==inx}" v-for="(banner,inx) in bannerData"></view>
						</view>
					</template>
					<template v-else>
						<view class="bannerBoxhezi shadow pr" style="background:#fff;width: 100%;">

						</view>
					</template>
				</view>
				<view class="h15"></view>
				<view class="nav">
					<swiper class="navBox ovh f12" @change="navChange">
						<block v-for="(nav,inx) in navData">
							<swiper-item>
								<view class="navtor ovh">
									<view @click="getUrl(navs.url,1)" v-for="(navs,inxs) in navData[inx]">
										<view class="m0auto center shadow">
											<image class="animated fadeIn fast" style="width: 40px;height: 40px;" :src="navs.src" mode="widthFix"></image>
										</view>
										<view class="h5"></view>
										<view class="tec">{{navs.title}}</view>
									</view>

									<view @click="uploadpade(true)">
										<view class="m0auto center shadow">
											<image class="animated fadeIn fast" style="width: 40px;height: 40px;" src="../../static/navi3.png" mode="widthFix"></image>
										</view>
										<view class="h5"></view>
										<view class="tec">系列课程</view>
									</view>
								</view>
							</swiper-item>
						</block>
					</swiper>
					<!-- <view class="h15"></view>
					<view class="center">
						<view class="dot trall3" :class="{active: currentNavIndex==inx}" v-for="(nav,inx) in navData" :key="nav"></view>
					</view> -->
				</view>
			</view>
			<view class="h1 bEAECF1"></view>
			<view class="body">
				<view class="h5"></view>
				<view class="ovh">
					<view class="fl center f18">
						<image style="width: 9px; height: 18px;" src="../../static/titlei.png" mode="widthFix"></image>
						<view class="w10 h5"></view>
						<view class="fb">最新上线</view>
					</view>
					<view class="more f12 c9AA8B6 fr" @click="getUrl('../videolist/videolist?tab=1',0)">
						查看更多>>
					</view>
				</view>
				<view class="h18"></view>
				<template v-if="newVideoData!=0">
					<view class="list">
						<view class="center animated fadeIn fast" v-for="(newVideo,index) in newVideoData" @click="getUrl('../details/details?id='+newVideo.id+'&type=2',0)">
							<view class="listimg pr shadow" v-bind:style="{ 'background': 'url('+newVideo.thumbnail.src+') 0% 0% / cover no-repeat' }">
								<view class="pa whtl center">
									<text class="iconfont icon-bofang cfff" style="font-size: 26px; height: 32px;"></text>
									<text class="pa listimgtime f8 cfff">{{newVideo.time}}</text>
								</view>
								<image class="imgnew pa" style="width: 24px; height: 24px;" src="../../static/new.png" mode="widthFix"></image>
							</view>
							<view class="listwb">
								<view class="listwbtitle ovh">
									{{newVideo.title}}
								</view>
								<view class="ovh c949597 f10">
									<view class="fr center">
										<view class="iconfont icon-liuyan center" style="font-size: 18px; line-height: 10px; margin-top: 1px;"></view>
										<view class="w5"></view>
										{{newVideo.comment_count}}
									</view>
									<view class="w10 h5 fr"></view>
									<view class="fr center">
										<view class="iconfont icon-bofang1 center" style="font-size: 14px; line-height: 13px;"></view>
										<view class="w5"></view>
										{{newVideo.play}}
									</view>
								</view>
							</view>
						</view>
					</view>
				</template>
				<template v-else>
					<view class="center c999 animated fadeIn fast" style="height: 100px;">
						暂无信息
					</view>
				</template>
			</view>


			<view class="h10 dn"></view>

			<view class="h1 bEAECF1 dn"></view>
			<view class="body dn">
				<view class="h5"></view>
				<view class="ovh">
					<view class="fl center f18">
						<image style="width: 9px; height: 18px;" src="../../static/titlei.png" mode="widthFix"></image>
						<view class="w10 h5"></view>
						<view class="fb">课程日历</view>
					</view>
					<view class="more f12 c9AA8B6 fr" @click="getUrl('../calendar/calendar',0)">
						查看更多>>
					</view>
				</view>
				<view class="h18"></view>
				<scroll-view class="calendar f12 ovh" scroll-x="true" :scroll-into-view="scrollinto" scroll-with-animation="300">

					<view :id="'tab'+inx" @click="upcaleswindx(inx,calescr.time)" v-for="(calescr,inx) in caleswtime" class="calendarli"
					 :class="caleswindx==inx?'on':''">
						<view class="calendarlizk center">
							<view class="cfff center">{{calescr.date}}</view>
							<view class="center tec">{{calescr.time}}</view>
						</view>
					</view>

				</scroll-view>
				<view class="h15" style=""></view>
				<template v-if="caleswData.length!=0">
					<view class="calesw dn">
						<!-- autoplay="true" -->
						<swiper class="caleswBox ovh animated fadeIn fast" :current="currentCaleswIndex" interval="5000" duration="500"
						 @change="caleswChange">
							<block v-for="(calesw,inx) in caleswData">
								<swiper-item class="">
									<view class="pr h100 caleswBoxhezi shadow" v-bind:style="{ 'background': 'url('+calesw.thumbnail.src+') no-repeat center','background-size':'cover' }">
										<view>
											<template v-if="Dtime<calesw.startTime&&calesw.IsMark==false">
												<view class="caleswnav pa f12 cfff shadow2 bca021b center">
													<view class="iconfont cfff icon-yuyuezhongxin center" style="height: 18px;"></view>
													<view class="w5 h5"></view>
													<view class="fb">预约</view>
												</view>
											</template>
											<template v-if="Dtime<calesw.startTime&&calesw.IsMark==true">
												<view class="caleswnav pa f12 cfff shadow2 bca021b center">
													<view class="iconfont cfff icon-yuyuezhongxin center" style="height: 18px;"></view>
													<view class="w5 h5"></view>
													<view class="fb">已预约</view>
												</view>
											</template>
											<template v-else-if="(Dtime>=calesw.startTime)&&(Dtime<=calesw.endTime)">
												<view class="caleswnav pa f12 cfff shadow2 bca021b center">
													<view class="iconfont cfff icon-yuyuezhongxin center" style="height: 18px;"></view>
													<view class="w5 h5"></view>
													<view class="fb">直播中</view>
												</view>
											</template>
											<template v-else-if="Dtime>calesw.endTime">
												<view class="caleswnav pa f12 cfff shadow bAAB2B7 center">
													<view class="iconfont cfff icon-yuyuezhongxin center" style="height: 18px;"></view>
													<view class="w5 h5"></view>
													<view class="fb">已结束</view>
												</view>
											</template>
										</view>

										<view>
											<template v-if="(Dtime>=calesw.startTime)&&(Dtime<=calesw.endTime)">
												<view class="pa whtl" @click="getUrl('../webview/webview?id='+calesw.link,0)"></view>
											</template>
											<template v-else>
												<view class="pa whtl" @click="getUrl('../predetails/predetails?id='+calesw.id,0)"></view>
											</template>
										</view>
									</view>
								</swiper-item>
							</block>
						</swiper>

						<view class="h20"></view>
						<view class="center">
							<view class="dot trall3" :class="{active: currentCaleswIndex==inx}" v-for="(caledot,inx) in caleswData"></view>
						</view>
					</view>
				</template>
				<template v-else>
					<view class="center c999 animated fadeIn fast" style="height: 160px;">
						<image src="../../static/kong1.png" style="width: 82px; margin-right: 20px;" mode="widthFix"></image>
						暂无课程安排
					</view>
				</template>
				<view class="h5"></view>
			</view>

			<view class="h1 bEAECF1"></view>
			<view class="body">
				<view class="h5"></view>
				<view class="ovh">
					<view class="fl center f18">
						<image style="width: 9px;" src="../../static/titlei.png" mode="widthFix"></image>
						<view class="w10 h5"></view>
						<view class="fb">手术解析</view>
					</view>
					<view class="more f12 c9AA8B6 fr" @click="getUrl('../videolist/videolist?id=1',0)">
						查看更多>>
					</view>
				</view>
				<view class="h18"></view>
				<template v-if="hotVideoData.length!=0">
					<scroll-view scroll-x="true" class="hotlistscroll">
						<view class="hotlist">

							<view class="fl bfff shadow animated fadeIn fast" v-for="(hotlist,index) in hotVideoData" @click="getUrl('../details/details?id='+hotlist.id+'&type=2',0)">
								<view class="hotlistimg pr shadow" v-bind:style="{ 'background': 'url('+hotlist.thumbnail.src+') 0% 0% / cover no-repeat' }">
									<view class="pa whtl center">
										<text class="iconfont icon-bofang cfff" style="font-size: 26px; height: 32px;"></text>
									</view>
								</view>
								<view class="hotlistwb pr">
									<view class="hotlistwbtitle ovh f12">
										{{hotlist.title}}
									</view>
									<view class="ovh c949597 f10">

										<view class="fl center">
											<view class="iconfont icon-liuyan center" style="font-size: 18px; line-height: 10px; margin-top: 1px;"></view>
											<view class="w5"></view>
											{{hotlist.comment_count}}
										</view>
										<view class="w10 h5 fl"></view>
										<view class="fl center">
											<view class="iconfont icon-bofang1 center" style="font-size: 14px; line-height: 13px;"></view>
											<view class="w5"></view>
											{{hotlist.play}}
										</view>
									</view>
									<image class="pa hot" style="width: 18px; height: 22px;" src="../../static/hot.png" mode="widthFix"></image>
								</view>
							</view>
							<view class="h5 fl" style="width: 13px;"></view>
						</view>
					</scroll-view>
				</template>
				<template v-else>
					<view class="center c999 animated fadeIn fast" style="height: 100px;">
						暂无信息
					</view>
				</template>
			</view>

			<boView></boView>
			<foTter nav="0"></foTter>
		</view>

		<template v-if="ists">
			<view class="pf whtl z3" style="background: rgba(0,0,0,0.8);">
				<template v-if="jx==0">
					<image class="pa" @click="onts" style="top: 644rpx;right: 70rpx;width: 240rpx;" src="../../static/si1.png" mode="widthFix"></image>
				</template>
				<template v-else>
					<image class="pa" @click="onts" style="top: 848rpx;left: 236rpx;width: 270rpx;" src="../../static/si1.png" mode="widthFix"></image>
				</template>

				<image class="pa" style="top: 486rpx;left: 54rpx;width: 470rpx;" src="../../static/si2.png" mode="widthFix"></image>
				<image class="pa" style="bottom: 40rpx;right: 20rpx;width: 520rpx;" src="../../static/si3.png" mode="widthFix"></image>
				<image class="pa" style="top: 80rpx; right: 34rpx; width: 480rpx;" src="../../static/si4.png" mode="widthFix"></image>
				<image class="pa" style="top: 20rpx; right: 30rpx; width: 520rpx;" src="../../static/si5.png" mode="widthFix"></image>
			</view>
		</template>
		<template v-if="iswym">
			<view class="pf whtl tk animated fadeIn faster">
				<view class="center h100">
					<view class="center pr" style="width: 80%;">
						<image style="" src="../../static/wym.png" mode="widthFix"></image>
						<view class="center pa" style="width: 100%; left: 0px; bottom: 30px;">
							<view class="cfff f18 shadow3 bbs netbut" @click="uploadpade(false)">
								确定
							</view>
						</view>
					</view>
				</view>
			</view>
		</template>


	</scroll-view>
</template>

<script>
	import foTter from '@/components/footer.vue'
	import boView from '@/components/botview.vue'
	export default {
		components: {
			foTter,
			boView
		},
		data() {
			return {
				jx: 0,
				src: "",
				ists: false,
				xqarrayindex: -1,
				PushData: [],
				issign: 0,
				iswym: false,
				timeIndex: 0,
				timeDepartments: 0,
				usecat: "",
				uid: "",
				Dtime: '',
				messages: "",
				serverTime: '2020/04/15 03:03:03',
				bannerData: [],
				bannerData1: [],
				bannerData2: [],
				currentBannerIndex: 0,
				navData: [
					[{
							src: '../../static/navi1.png',
							title: '今日课程',
							url: '../live/live',
							id: 10
						},
						{
							src: '../../static/navi2.png',
							title: '课程回放',
							url: '../videolist/videolist',
							id: 10
						},
						{
							src: '../../static/navi4.png',
							title: '手术解析',
							url: '../videolist/videolist?id=1',
							id: 10
						},
						{
							src: '../../static/navi8.png',
							title: '专题讲座',
							url: '../videolist/videolist?id=3',
							id: 10
						},
						{
							src: '../../static/navi5.png',
							title: '空中圆桌派',
							url: '../videolist/videolist?id=2',
							id: 10
						},
						{
							src: '../../static/navi6.png',
							title: '品牌活动',
							url: '../activity/activity',
							id: 10
						},
						{
							src: '../../static/navi7.png',
							title: '专家搜索',
							url: '../expertsearch/expertsearch',
							id: 10
						}
					]
				],
				currentNavIndex: 0,
				caleswindx: 0,
				scrollinto: '',
				HostDepartmentData: [],
				caleswtime: [],
				caleswData: [],
				currentCaleswIndex: 0,
				array: [],
				arrayIndex: 0,
				newVideoData: [],
				hotVideoData: []
			};
		},
		onLoad(e) {
			this.ists = false
			//this.$conFig.alert_Load()
			
			console.log(getCurrentPages()[0].route,e.oid)
			
			var sign = this.$conFig.issign('false', getCurrentPages()[0].route, e.oid)
			if (sign) {
				this.Dtime = (new Date()).getTime();
				this.setTim()
				this.load()
			}
		},
		onShow(e) {
			this.$conFig.onWeixin("领医迈","　","/pages/index/index","s.jpg")
			
			this.ists=false
			
			var _this=this
			uni.getSystemInfo({
			    success: function (res) {
					if(res.windowHeight>700){
						_this.jx=1
					}
			    }
			});
			if(!uni.getStorageSync('czts')){
				this.ists=true
			}
		},
		onShareAppMessage(res) {
			return {
				title: "领医迈",
				path: '/pages/index/index'
			}
		},
		methods: {
			bannerChange: function(e) {
				let current = e.mp.detail.current;
				this.currentBannerIndex = current;
			},
			navChange: function(e) {

				let current = e.mp.detail.current;
				this.currentNavIndex = current;
			},
			caleswChange: function(e) {
				let current = e.mp.detail.current;
				this.currentCaleswIndex = current;
			},
			bindPickerChange: function(e) {
				this.arrayIndex = e.target.value

				var newtimeDepartment = {}
				newtimeDepartment.category = this.array[this.arrayIndex].title
				newtimeDepartment.categoryid = this.array[this.arrayIndex].id
				newtimeDepartment.categorynum = this.arrayIndex


				uni.setStorage({
					key: 'timeDepartment',
					data: newtimeDepartment
				});

				this.$conFig.alert_Load()
				this.loadBanne()
				this.loadNewsVideo()
				this.loadHotVideo()
				//uni.hideLoading();
			},
			
			_getUrl(e){
				
				window.location.href = this.$base64.Base64.decode(e)
			},
			getUrl(e, u) {
				//


				//timeDepartment
				


				if (e.indexOf('special') > 0) {
					this.uploadpade(true)
					return
					// if(uni.getStorageSync('timeDepartment').category=="普外科"||uni.getStorageSync('timeDepartment').category=="心胸外科"){

					// }else{

					// }
				}


				if (this.$conFig.onUrl(e)) {
					getApp().globalData.isshow = true
					uni.navigateTo({
						url: e
					});
				}
			},
			setTim: function(e) {
				setTimeout(() => {
					this.Dtime = this.Dtime + 1000;
					this.setTim()
				}, 1000)
			},
			getDay(day) {
				var today = new Date();
				var targetday_milliseconds = today.getTime() + 1000 * 60 * 60 * 24 * day;
				today.setTime(targetday_milliseconds); //注意，这行是关键代码
				var tYear = today.getFullYear();

				var tMonth = today.getMonth();

				var tDate = today.getDate();

				tMonth = this.doHandleMonth(tMonth + 1);

				tDate = this.doHandleMonth(tDate);

				return tYear + "-" + tMonth + "-" + tDate;

			},
			doHandleMonth(month) {

				var m = month;

				if (month.toString().length == 1) {

					m = "0" + month;

				}

				return m;

			},
			timehs(e) {
				var mydate = new Date(e);
				var myddy = mydate.getDay(); //获取存储当前日期
				var weekday = ["星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六"];

				return weekday[myddy]
			},
			upcaleswindx(e, time) {
				if (this.caleswindx == e) {
					return
				}

				this.currentCaleswIndex = 0
				this.caleswindx = e
				setTimeout(() => {
					this.scrollinto = 'tab' + e
				}, 20)

				this.loadCalendar(time)
				//uni.hideLoading();
			},
			load() {

				for (var i = 0; i < 7; i++) {
					this.caleswtime.push({
						time: this.getDay(i),
						date: this.timehs(this.getDay(i))
					})
				}

				this.$conFig.alert_Load()
				//加载科室
				this.$reqUest.get('linkmed', '/linkmed2020/categories').then(data => {
					var [error, res] = data;

					if (res.data.code == "100000") {
						this.array = []
						var add = res.data.result
						Object.keys(add).forEach((key) => {
							this.array.push({
								title: add[key],
								id: key
							})
						})

						for (var i = 0; i < this.array.length; i++) {

							if (this.array[i].title == uni.getStorageSync('timeDepartment').category) {
								this.arrayIndex = i
							}
						}


						this.loadBanne()
						this.loadNewsVideo()
						this.loadHotVideo()
						
						
						
					} else {
						this.$conFig.alert_Tips('系统异常请稍后再试！')
					}
				})
			},
			loadHostDepartment() {
				this.$reqUest.get('inmedia', '/linyimaiapi.ashx?Method=popularcategories', {
					"category": uni.getStorageSync('timeDepartment').category
				}).then(data => {
					var [error, res] = data;

					this.HostDepartmentData = this.$conFig.newList(JSON.parse(res.data.message))

				})


			},
			wdxx() {
				var mydate = new Date();
				var myddy = mydate.getDay();

				this.$reqUest.get('inmedia', '/linyimaiapi.ashx?Method=notificationcount').then(data => {
					var [error, res] = data;

					this.messages = JSON.parse(res.data.message).comment_count + JSON.parse(res.data.message).flag_count + JSON.parse(
						res.data.message).video_count


				})
			},
			loadBanne() {

				this.bannerData = []
				this.bannerData1 = []
				this.bannerData2 = []


				this.timeIndex = 0
				//加载banner
				this.currentBannerIndex = 0
				this.$reqUest.get('inmedia', '/linyimaiapi.ashx?Method=broadcast', {
					"category": uni.getStorageSync('timeDepartment').category,
					"openid": uni.getStorageSync('wxuserInfo').openid,
					"ishot": 1
				}).then(data => {
					var [error, res] = data;

					if (res.data.result != "true") {
						this.loadbannerData2()
						return
					}


					var list = this.$conFig.newList(JSON.parse(res.data.message))

					this.bannerData1 = []
					var fornum = list.length
					if (fornum >= 7) {
						fornum = 5
					}

					for (var i = 0; i < fornum; i++) {
						this.bannerData1.push(list[i])

						//this.bannerData1[i].link=this.$base64.Base64.encode("https://www.arkenvmc.com.cn/JBOX-test")
						this.bannerData1[i].link = this.$base64.Base64.encode(this.bannerData1[i].link)
					}


					for (i = 0; i < this.bannerData1.length; i++) {
						var etime = this.bannerData1[i].endTime
						var stime = this.bannerData1[i].startTime
						var rtime = this.bannerData1[i].requestTime

						this.bannerData1[i].endTime = etime.substring(etime.indexOf('/Date(') + 6, etime.length - 2)
						this.bannerData1[i].startTime = stime.substring(stime.indexOf('/Date(') + 6, stime.length - 2)
						this.bannerData1[i].requestTime = rtime.substring(rtime.indexOf('/Date(') + 6, rtime.length - 2)
						this.serverTime = rtime.substring(rtime.indexOf('/Date(') + 6, rtime.length - 2)
					}


					this.loadbannerData2()
				})
			},
			loadbannerData2() {


				this.$reqUest.get('linkmed', '/linkmed2020/videoinfo', {
					"category": uni.getStorageSync('timeDepartment').category,
					"ishot": 1
				}).then(data => {
					var [error, res] = data;
					var list = this.$conFig.newList(res.data)
					this.bannerData2 = []
					for (var i = 0; i < list.length; i++) {
						this.bannerData2.push(list[i])
					}
					var newlist = [...this.bannerData1, ...this.bannerData2]
					var newlistnum = newlist.length
					if (newlistnum > 7) {
						newlistnum = 7
					}

					this.bannerData = []
					for (var i = 0; i < newlistnum; i++) {
						this.bannerData.push(newlist[i])
					}

					this.timeIndex = 0
					this.currentBannerIndex = 0

					this.issign = 2
					uni.hideLoading();
					

				})
			},
			loadCalendar(e) {

				//加载日历
				this.$reqUest.get('inmedia', '/linyimaiapi.ashx?Method=broadcast', {
					"category": uni.getStorageSync('timeDepartment').category,
					"openid": uni.getStorageSync('wxuserInfo').openid,
					"date": e
				}).then(data => {
					var [error, res] = data;

					this.caleswData = []
					this.currentCaleswIndex = 0

					this.issign = 2
					uni.hideLoading();

					if (res.data.result != "true") {

						return
					}

					var list = this.$conFig.newList(JSON.parse(res.data.message))

					var fornum = list.length
					for (var i = 0; i < fornum; i++) {
						this.caleswData.push(list[i])
						this.caleswData[i].link = this.$base64.Base64.encode(this.caleswData[i].link)
						//this.caleswData[i].link=this.$base64.Base64.encode("https://webcasting.bizconfstreaming.com/watch/6171157")
					}

					for (i = 0; i < this.caleswData.length; i++) {
						var etime = this.caleswData[i].endTime
						var stime = this.caleswData[i].startTime
						var rtime = this.caleswData[i].requestTime

						this.caleswData[i].endTime = etime.substring(etime.indexOf('/Date(') + 6, etime.length - 2)
						this.caleswData[i].startTime = stime.substring(stime.indexOf('/Date(') + 6, stime.length - 2)
						this.caleswData[i].requestTime = rtime.substring(rtime.indexOf('/Date(') + 6, rtime.length - 2)
						this.serverTime = rtime.substring(rtime.indexOf('/Date(') + 6, rtime.length - 2)
					}

				})

			},
			loadNewsVideo() {
				
				this.$reqUest.get('linkmed', '/linkmed2020/videoinfo', {
					"category": uni.getStorageSync('timeDepartment').category
				}).then(data => {
					var [error, res] = data;
					var list = this.$conFig.newList(res.data)
					var fornum = list.length
					if (fornum >= 3) {
						fornum = 3
					}
					this.newVideoData = []
					for (var i = 0; i < fornum; i++) {
						this.newVideoData.push(list[i])
					}
					
				})
			},
			loadHotVideo() {
				
				this.$reqUest.get('linkmed', '/linkmed2020/videoinfo', {
					"tags": "手术解析",
					"category": uni.getStorageSync('timeDepartment').category
				}).then(data => {
					var [error, res] = data;

					var list = this.$conFig.newList(res.data)

					var newlistnum = list.length
					if (newlistnum > 7) {
						newlistnum = 7
					}
					this.hotVideoData = []
					for (var i = 0; i < newlistnum; i++) {
						this.hotVideoData.push(list[i])
					}
					
				})
			},
			tabDepartment(e) {
				for (var i = 0; i < this.array.length; i++) {
					if (this.array[i].title == e) {
						this.arrayIndex = i
						var timeDepartment = {
							category: this.array[i].title,
							categoryid: this.array[i].id,
							categorynum: i
						}
						uni.setStorage({
							key: 'timeDepartment',
							data: timeDepartment
						});
					}
				}
				uni.pageScrollTo({
					scrollTop: 0,
					duration: 300
				});
				this.$conFig.alert_Load()
				this.loadBanne()
				this.loadNewsVideo()
				this.loadHotVideo()
			},
			uploadpade(e) {
				this.iswym = e
			},
			login(url) {
				uni.setStorage({
					key: 'timeUrl',
					data: url
				});
				if (uni.getStorageSync('wxuserInfo') == "") {
					uni.navigateTo({
						url: "../login/login"
					});
					return
				}
				if (!uni.getStorageSync('userInfo')) {
					uni.navigateTo({
						url: "../sign/sign"
					});
					return
				}
			},
			onts(){
				uni.setStorage({
					key: 'czts',
					data: true
				});
				this.ists=false
			}


		},
	};
</script>

<style>
	.banne {
		height: 418rpx;
	}

	.bannerBox {
		height: 380rpx;
		width: calc(100% + 34px);
		margin-left: -17px;
	}

	.bannerBoxhezi {
		width: calc(100% - 34px);
		margin: 0px auto;
		height: 360rpx;
		border-radius: 5px;
		border: #fff solid 2px;
	}



	.zbz {
		padding: 4px 8px 4px 8px;
		top: 15px;
		right: 0px;
		background: rgba(50, 54, 67, 0.45);
		border-radius: 5px 0px 0px 5px;
	}

	.zbz.on {
		background: rgba(185, 39, 39, 0.45);
		color: #fff;
	}

	.zbzdian {
		background: rgba(185, 39, 39, 1);
		width: 6px;
		height: 6px;
		border-radius: 5px;
	}

	.zbzdian.on {
		background: #C0C0C0;
	}

	.navBox {
		height: 170px;
	}

	.navtor>view {
		float: left;
		width: 25%;
		margin-bottom: 8px;
	}

	.navtor>view .shadow {
		width: 57px;
		height: 57px;
		background: #fff;
		border-radius: 5px;
	}


	.Department {
		height: 54px;
		width: calc(100% + 17px);
		white-space: nowrap;
	}

	.Departmentli {
		display: inline-block;
		border-radius: 8px;
		overflow: hidden;
		margin-left: 2px;
		margin-right: 13px;
		width: 126px;
		height: 54px;
	}

	.Departmentlititle {
		top: 10px;
		left: 10px;
	}

	.Departmentliimg {
		bottom: 5px;
		right: 5px;
		width: 38px;
	}

	.Department .Departmentli:nth-child(3n-2) {
		background: linear-gradient(135deg, rgba(202, 2, 27, 1) 0%, rgba(255, 94, 100, 1) 100%);
	}

	.Department .Departmentli:nth-child(3n-1) {
		background: linear-gradient(135deg, rgba(204, 0, 153, 1) 0%, rgba(226, 127, 232, 1) 100%);
	}

	.Department .Departmentli:nth-child(3n) {
		background: linear-gradient(135deg, rgba(2, 2, 153, 1) 0%, rgba(107, 107, 228, 1) 100%);
	}

	.calendar {
		height: 35px;
		width: calc(100% + 17px);
		white-space: nowrap;
	}

	.calendarview {
		width: max-content;
	}

	.calendarli {
		display: inline-block;
		border-radius: 5px;
		overflow: hidden;
		margin-left: 2px;
		margin-right: 13px;
		width: 60px;
		box-shadow: 0px 3px 5px rgba(0, 0, 0, 0);
	}

	.calendarli.on {
		width: 155px;
		box-shadow: 0px 3px 5px rgba(0, 0, 0, 0.2);
	}

	.calendarlizk {
		width: 155px;
	}

	.calendarlizk>view:nth-child(1) {
		width: 60px;
		height: 24px;
	}

	.calendarlizk>view:nth-child(2) {
		background: #fff;
		width: 95px;
		height: 24px;
	}

	.calendar .calendarli:nth-child(3n-2) .calendarlizk>view:nth-child(1) {
		background: rgba(202, 2, 27, 0.15);
	}

	.calendar .calendarli:nth-child(3n-1) .calendarlizk>view:nth-child(1) {
		background: rgba(2, 1, 147, 0.15);
	}

	.calendar .calendarli:nth-child(3n) .calendarlizk>view:nth-child(1) {
		background: rgba(187, 38, 149, 0.15);
	}

	.calendar .calendarli.on:nth-child(3n-1) .calendarlizk>view:nth-child(1) {
		background: rgba(202, 2, 27, 1);
	}

	.calendar .calendarli.on:nth-child(3n-2) .calendarlizk>view:nth-child(1) {
		background: rgba(2, 1, 147, 1);
	}

	.calendar .calendarli.on:nth-child(3n) .calendarlizk>view:nth-child(1) {
		background: rgba(187, 38, 149, 1);
	}

	.caleswBox {
		width: calc(100% + 34px);
		margin-left: -17px;
		height: 370rpx;
	}

	.caleswBox .caleswBoxhezi {
		width: calc(100% - 40px);
		margin: 0px auto;
		height: 350rpx;
		border-radius: 5px;
		border: #fff solid 2px;
	}


	.dot {
		width: 4px;
		height: 4px;
		margin: 0 4px;
		border-radius: 3px;
		background-color: #9B9B9B;
	}

	.dot.active {
		width: 10px;
		background-color: #BB2695;
	}




	.hotlistscroll {
		height: 175px;
		width: calc(100% + 17px);
	}

	.hotlist {
		width: max-content;
	}

	.hotlist>view {
		width: 157px;
		border-radius: 5px;
		margin: 0px 4px;
	}

	.hotlistimg {
		width: 100%;
		height: 86px;
		border-radius: 5px 5px 0px 0px;
		overflow: hidden;
	}

	.hotlistimg view {
		background: rgba(0, 0, 0, 0.6);
	}

	.hotlistwb {
		padding: 7px 10px;
	}

	.hotlistwbtitle {
		height: 38px;
		margin-bottom: 7px;
	}

	.hot {
		bottom: -11px;
		right: 12px;
	}


	.tw>view {
		width: calc(50% - 20px);
		float: left;
		margin-left: 10px;

		margin-bottom: 15px;
	}

	.tw .bannerBoxhezis {
		height: 156rpx;
		border: #fff solid 1px;
	}
</style>
