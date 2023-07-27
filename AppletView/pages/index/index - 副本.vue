<template>
	<scroll-view @scrolltolower="upper">
		<template v-if="issign">
			<view class="pr animated fadeIn fast">
				<image class="pa w100" style="" src="../../static/bg.jpg" mode="widthFix"></image>
				<view class="body pr z3">
					<view class="cfff">
						<view class="fl center">
							<image style="width: 90px;" src="../../static/logo.png" mode="widthFix"></image>
						</view>
						<view class="fr topfr">
							<view class="fr">
								<view class="center fl f15">
									<picker @change="bindPickerChange" :value="arrayIndex" :range="array" range-key="title">
										<view class="center">
											<view class="">{{array[arrayIndex].title}}</view>
											<view class="w5"></view>
											<view class="triangle-down"></view>
										</view>
									</picker>
								</view>
								<view class="w15 h5 fl"></view>
								<view class="center fl f15 pr" @click="getUrl('../notice/notice?id=1',0)">
									<view class="pr icon-tongzhi1 iconfont" style="font-size: 25px; height: 30px; margin-top: -5px; ">
										<view class="noticetxt pa f8 bD43D4C cfff center">4</view>
									</view>
								</view>
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
					<view class="banne">
						<swiper class="bannerBox ovh " autoplay="true" interval="5000" duration="500" @change="bannerChange">
							<block v-for="(banner,inx) in bannerData" :key="inx">
								<swiper-item>
									<view @click="getUrl(Dtime<=banner.startTime?'../predetails/predetails?id='+banner.id:'../webview/webview?id='+banner.id,0)"
									 class="bannerBoxhezi shadow pr" v-bind:style="{ 'background': 'url('+banner.src+') no-repeat center','background-size':'cover' }">
										<view class="pa zbz center cfff f16">

											<template class="">
												<view class="zbzdian on" style="background: #D43D4C;"></view>
												<view class="w15 h1"></view>
												<view class="center">已预约</view>
											</template>
											<!-- <template v-if="Dtime<banner.startTime">
												<view class="zbzdian on"></view>
												<view class="w15 h1"></view>
												预约
											</template>
											<template v-else-if="(Dtime>=banner.startTime)&&(Dtime<=banner.endTime)">
												<view class="zbzdian"></view>
												<view class="w15 h5"></view>
												直播中
											</template>
											<template v-else>
												<view class="zbzdian on"></view>
												<view class="w15 h5"></view>
												已结束
											</template> -->
										</view>
										<view class="pa bannebtbg f15 cfff ellipsis" style=" background: url(http://h5.atline.cn/linkmedxcx/bannebtbg.png) no-repeat; background-size:100% 100%;">
											{{banner.title}}
										</view>
									</view>
								</swiper-item>
							</block>
						</swiper>
						<view class="h15"></view>
						<view class="center">
							<view class="dot trall3" :class="{active: currentBannerIndex==inx}" v-for="(banner,inx) in bannerData" :key="banner"></view>
						</view>
					</view>
					<view class="h15"></view>
					<view class="nav">
						<swiper class="navBox ovh f12" @change="navChange">
							<block v-for="(nav,inx) in navData" :key="inx">
								<swiper-item>
									<view class="navtor ovh">
										<view @click="getUrl(navs.url,0)" v-for="(navs,inxs) in navData[inx]" :key="navs">
											<view class="m0auto center shadow">
												<image style="width: 40px;" :src="navs.src" mode="widthFix"></image>
											</view>
											<view class="h5"></view>
											<view class="tec">{{navs.title}}</view>
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
							<image style="width: 9px;" src="../../static/titlei.png" mode="widthFix"></image>
							<view class="w10 h5"></view>
							<view class="fb">课程日历</view>
						</view>
						<view class="more f12 c9AA8B6 fr" @click="getUrl('../calendar/calendar',0)">
							查看更多>>
						</view>
					</view>
					<view class="h18"></view>
					<scroll-view class="calendar f12 ovh" scroll-x="true" :scroll-into-view="scrollinto" scroll-with-animation="300">

						<view :id="'tab'+inx" @click="upcaleswindx(inx)" v-for="(calescr,inx) in caleswData" :key="inx" class="calendarli"
						 :class="caleswindx==inx?'on':''">
							<view class="calendarlizk center">
								<view class="cfff center">{{calescr.week}}</view>
								<view class="center tec">{{calescr.data}}</view>
							</view>
						</view>

					</scroll-view>
					<view class="h15" style=""></view>
					<view class="calesw">
						<!-- autoplay="true" -->
						<swiper class="caleswBox ovh" :current="currentCaleswIndex" interval="5000" duration="500" @change="caleswChange">
							<block v-for="(calesw,inx) in caleswData[caleswindx].list" :key="inx">
								<swiper-item class="">
									<view class="pr h100 caleswBoxhezi shadow" @click="getUrl(Dtime<=calesw.startTime?'../predetails/predetails?id='+calesw.id:'../webview/webview?id='+calesw.id,0)"
									 v-bind:style="{ 'background': 'url('+calesw.src+') no-repeat center','background-size':'cover' }">

										<template v-if="Dtime<calesw.startTime">
											<view class="caleswnav pa f12 cfff shadow2 bca021b center">
												<view class="iconfont cfff icon-yuyuezhongxin center" style="height: 18px;"></view>
												<view class="w5 h5"></view>
												<view class="fb">预约</view>
											</view>
										</template>
										<template v-else-if="(Dtime>=calesw.startTime)&&(Dtime<=calesw.endTime)">
											<view class="caleswnav pa f12 cfff shadow2 bca021b center">
												<view class="iconfont cfff icon-yuyuezhongxin center" style="height: 18px;"></view>
												<view class="w5 h5"></view>
												<view class="fb">直播中</view>
											</view>
										</template>
										<template v-else>
											<view class="caleswnav pa f12 cfff shadow2 bca021b center">
												<view class="iconfont cfff icon-yuyuezhongxin center" style="height: 18px;"></view>
												<view class="w5 h5"></view>
												<view class="fb">已结束</view>
											</view>
										</template>

									</view>
								</swiper-item>
							</block>
						</swiper>

						<view class="h20"></view>
						<view class="center">
							<view class="dot trall3" :class="{active: currentCaleswIndex==inx}" v-for="(caledot,inx) in caleswData[caleswindx].list"
							 :key="inx"></view>
						</view>
					</view>
					<view class="h5"></view>
				</view>
				<view class="h1 bEAECF1"></view>
				<view class="body">
					<view class="h5"></view>
					<view class="ovh">
						<view class="fl center f18">
							<image style="width: 9px;" src="../../static/titlei.png" mode="widthFix"></image>
							<view class="w10 h5"></view>
							<view class="fb">最新视频</view>
						</view>
						<view class="more f12 c9AA8B6 fr" @click="getUrl('../videolist/videolist?id=1',0)">
							查看更多>>
						</view>
					</view>
					<view class="h18"></view>
					<view class="list">
						<view class="center" v-for="(newVideo,index) in newVideoData" :key="index" @click="getUrl('../details/details?id='+newlist.id,0)">
							<view class="listimg pr shadow" v-bind:style="{ 'background': 'url('+newVideo.src+') no-repeat center','background-size':'cover' }">
								<view class="pa whtl center">
									<text class="iconfont icon-bofang cfff" style="font-size: 26px; height: 32px;"></text>
									<text class="pa listimgtime f8 cfff">{{newVideo.time}}</text>
								</view>
								<image class="imgnew pa" style="width: 24px;" src="../../static/new.png" mode="widthFix"></image>
							</view>
							<view class="listwb">
								<view class="listwbtitle ovh">
									{{newVideo.title}}
								</view>
								<view class="ovh c949597 f10">
									<view class="fr center">
										<view class="iconfont icon-liuyan center" style="font-size: 18px; line-height: 10px; margin-top: 1px;"></view>
										<view class="w5"></view>
										{{newVideo.comnum}}
									</view>
									<view class="w10 h5 fr"></view>
									<view class="fr center">
										<view class="iconfont icon-bofang1 center" style="font-size: 14px; line-height: 13px;"></view>
										<view class="w5"></view>
										{{newVideo.playnum}}
									</view>
								</view>
							</view>
						</view>
					</view>
				</view>
				<view class="h1 bEAECF1"></view>
				<view class="body">
					<view class="h5"></view>
					<view class="ovh">
						<view class="fl center f18">
							<image style="width: 9px;" src="../../static/titlei.png" mode="widthFix"></image>
							<view class="w10 h5"></view>
							<view class="fb">热点话题</view>
						</view>
						<view class="more f12 c9AA8B6 fr" @click="getUrl('../videolist/videolist?id=2',0)">
							查看更多>>
						</view>
					</view>
					<view class="h18"></view>
					<scroll-view scroll-x="true" class="hotlistscroll">
						<view class="hotlist">

							<view class="fl bfff shadow" v-for="(hotlist,index) in hotData" :key="index" @click="getUrl('../details/details?id='+hotlist.id,0)">
								<view class="hotlistimg pr shadow" v-bind:style="{ 'background': 'url('+hotlist.src+') no-repeat center','background-size':'cover' }">
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
											{{hotlist.playnum}}
										</view>
										<view class="w10 h5 fl"></view>
										<view class="fl center">
											<view class="iconfont icon-bofang1 center" style="font-size: 14px; line-height: 13px;"></view>
											<view class="w5"></view>
											{{hotlist.comnum}}
										</view>
									</view>
									<image class="pa hot" style="width: 18px;" src="../../static/hot.png" mode="widthFix"></image>
								</view>
							</view>

							<view class="h5 fl" style="width: 13px;"></view>
						</view>
					</scroll-view>
				</view>
				<boView></boView>
				<foTter></foTter>
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
				issign: false,
				Dtime: '',
				serverTime: '2020/04/15 03:03:03',
				bannerData: [{
						src: 'http://h5.atline.cn/linkmedxcx/banne1.png',
						title: '【肺越巅峰】“一期一段”肺段在线培训课程肺段在线培训课程',
						id: 10,
						startTime: '2020/04/15 20:03:03',
						endTime: '2020/04/15 22:03:03'
					},
					{
						src: 'http://h5.atline.cn/linkmedxcx/banne1.png',
						title: '【肺越巅峰】“一期一段”肺段在线培训课程肺段在线培训课程',
						id: 10,
						startTime: '2020/04/15 01:03:03',
						endTime: '2020/04/15 22:03:03'
					},
					{
						src: 'http://h5.atline.cn/linkmedxcx/banne1.png',
						title: '【肺越巅峰】“一期一段”肺段在线培训课程肺段在线培训课程',
						id: 10,
						startTime: '2020/04/15 01:03:03',
						endTime: '2020/04/15 02:03:03'
					}
				],
				currentBannerIndex: 0,
				navData: [
					[{
							src: 'http://h5.atline.cn/linkmedxcx/navi1.png',
							title: '课程直播',
							url: '../videolist/videolist',
							id: 10
						},
						{
							src: 'http://h5.atline.cn/linkmedxcx/navi2.png',
							title: '直播回放',
							url: '../index/index',
							id: 10
						},
						{
							src: 'http://h5.atline.cn/linkmedxcx/navi3.png',
							title: '强生微课',
							url: '../index/index',
							id: 10
						},
						{
							src: 'http://h5.atline.cn/linkmedxcx/navi4.png',
							title: '手术解析',
							url: '../index/index',
							id: 10
						},
						{
							src: 'http://h5.atline.cn/linkmedxcx/navi5.png',
							title: '空中圆桌派',
							url: '../index/index',
							id: 10
						},
						{
							src: 'http://h5.atline.cn/linkmedxcx/navi6.png',
							title: '专题课程',
							url: '../index/index',
							id: 10
						},
						{
							src: 'http://h5.atline.cn/linkmedxcx/navi7.png',
							title: '专家搜索',
							url: '../expertsearch/expertsearch',
							id: 10
						},
						{
							src: 'http://h5.atline.cn/linkmedxcx/navi8.png',
							title: '强生线上学院',
							url: '../index/index',
							id: 10
						}
					]
				],
				currentNavIndex: 0,
				caleswindx: 0,
				scrollinto: '',
				caleswData: [{
						data: "2020/04/15",
						list: [{
								src: 'http://h5.atline.cn/linkmedxcx/calesw.png',
								startTime: '2020/04/15 01:03:03',
								endTime: '2020/04/15 22:03:03',
								id: 0
							},
							{
								src: 'http://h5.atline.cn/linkmedxcx/calesw.png',
								startTime: '2020/04/15 18:03:03',
								endTime: '2020/04/15 20:03:03',
								id: 0
							},
							{
								src: 'http://h5.atline.cn/linkmedxcx/calesw.png',
								startTime: '2020/04/15 18:03:03',
								endTime: '2020/04/15 21:03:03',
								id: 0
							}
						]
					},
					{
						data: "2020/04/16",
						list: [{
							src: 'http://h5.atline.cn/linkmedxcx/calesw.png',
							startTime: '2020/04/15 01:03:03',
							endTime: '2020/04/15 02:03:03',
							id: 0
						}]
					},
					{
						data: "2020/04/17",
						list: [{
								src: 'http://h5.atline.cn/linkmedxcx/calesw.png',
								startTime: '2020/04/15 01:03:03',
								endTime: '2020/04/15 02:03:03',
								id: 0
							},
							{
								src: 'http://h5.atline.cn/linkmedxcx/calesw.png',
								startTime: '2020/04/15 01:03:03',
								endTime: '2020/04/15 02:03:03',
								id: 0
							},
							{
								src: 'http://h5.atline.cn/linkmedxcx/calesw.png',
								startTime: '2020/04/15 01:03:03',
								endTime: '2020/04/15 02:03:03',
								id: 0
							}
						]
					},
					{
						data: "2020/04/18",
						list: [{
								src: 'http://h5.atline.cn/linkmedxcx/calesw.png',
								startTime: '2020/04/15 01:03:03',
								endTime: '2020/04/15 02:03:03',
								id: 0
							},
							{
								src: 'http://h5.atline.cn/linkmedxcx/calesw.png',
								startTime: '2020/04/15 01:03:03',
								endTime: '2020/04/15 02:03:03',
								id: 0
							},
							{
								src: 'http://h5.atline.cn/linkmedxcx/calesw.png',
								startTime: '2020/04/15 01:03:03',
								endTime: '2020/04/15 02:03:03',
								id: 0
							}
						]
					},
					{
						data: "2020/04/19",
						list: [{
							src: 'http://h5.atline.cn/linkmedxcx/calesw.png',
							startTime: '2020/04/15 01:03:03',
							endTime: '2020/04/15 02:03:03',
							id: 0
						}]
					},
					{
						data: "2020/04/19",
						list: [{
							src: 'http://h5.atline.cn/linkmedxcx/calesw.png',
							startTime: '2020/04/15 01:03:03',
							endTime: '2020/04/15 02:03:03',
							id: 0
						}]
					},
					{
						data: "2020/04/19",
						list: [{
							src: 'http://h5.atline.cn/linkmedxcx/calesw.png',
							startTime: '2020/04/15 01:03:03',
							endTime: '2020/04/15 02:03:03',
							id: 0
						}]
					},
					{
						data: "2020/04/19",
						list: [{
							src: 'http://h5.atline.cn/linkmedxcx/calesw.png',
							startTime: '2020/04/15 01:03:03',
							endTime: '2020/04/15 02:03:03',
							id: 0
						}]
					}
				],
				currentCaleswIndex: 0,
				array: [],
				arrayIndex: 0,
				newVideoData: [{
					src: 'http://h5.atline.cn/linkmedxcx/calesw.png',
					time: '22:23',
					id: 10,
					title: "林明 | 眼眶骨折手术技巧及并发症处理",
					playnum: 561,
					comnum: 561
				}, {
					src: 'http://h5.atline.cn/linkmedxcx/calesw.png',
					time: '22:23',
					id: 10,
					title: "林明 | 眼眶骨折手术技巧及并发症处理",
					playnum: 561,
					comnum: 561
				}, {
					src: 'http://h5.atline.cn/linkmedxcx/calesw.png',
					time: '22:23',
					id: 10,
					title: "林明 | 眼眶骨折手术技巧及并发症处理",
					playnum: 561,
					comnum: 561
				}],
				hotData: [{
					src: 'http://h5.atline.cn/linkmedxcx/calesw.png',
					time: '22:23',
					id: 10,
					title: "林明 | 眼眶骨折手术技巧及并发症处理",
					playnum: 561,
					comnum: 561
				}, {
					src: 'http://h5.atline.cn/linkmedxcx/calesw.png',
					time: '22:23',
					id: 10,
					title: "林明 | 眼眶骨折手术技巧及并发症处理",
					playnum: 561,
					comnum: 561
				}, {
					src: 'http://h5.atline.cn/linkmedxcx/calesw.png',
					time: '22:23',
					id: 10,
					title: "林明 | 眼眶骨折手术技巧及并发症处理",
					playnum: 561,
					comnum: 561
				}]
			};
		},
		onLoad() {

		},
		onShow() {
			var sign = this.$conFig.issign()
			if (sign) {
				if (uni.getStorageSync('userInfo').category != uni.getStorageSync('timeDepartment').category) {
					if (!getApp().globalData.isshow) {
						uni.getStorageSync('timeDepartment').category = uni.getStorageSync('userInfo').category
						this.load()
						console.log("加载数据")
					}
				} else {
					this.load()
				}
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
				
				var newtimeDepartment={}
				newtimeDepartment.category=this.array[this.arrayIndex].title
				newtimeDepartment.categoryid=this.array[this.arrayIndex].id
				newtimeDepartment.categorynum=this.arrayIndex
				
				
				uni.setStorage({
					key: 'timeDepartment',
					data: newtimeDepartment
				});
				this.loadBanne()
			},
			getUrl(e) {
				getApp().globalData.isshow = true
				uni.navigateTo({
					url: e
				});
			},
			setTim: function(e) {
				this.Dtime = this.Dtime + 1000;
				setTimeout(() => {
					this.setTim()
				}, 1000)
			},

			timehs(e) {
				var mydate = new Date(e);
				var myddy = mydate.getDay(); //获取存储当前日期
				var weekday = ["星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六"];
				return weekday[myddy]
			},
			upcaleswindx(e) {
				if (this.caleswindx == e) {
					return
				}

				this.currentCaleswIndex = 0
				this.caleswindx = e
				setTimeout(() => {
					this.scrollinto = 'tab' + e
				}, 20)
			},
			load() {
				
				this.$conFig.alert_Load()
				//加载科室
				this.$reqUest.get('linkmed', '/linkmed2020/categories').then(data => {
					var [error, res] = data;

					console.log(res)

					if (res.data.code == "100000") {
						var add = res.data.result
						Object.keys(add).forEach((key) => {
							this.array.push({
								title: add[key],
								id: key
							})
						})
						this.arrayIndex=uni.getStorageSync('timeDepartment').categorynum
						this.loadBanne()

					} else {
						this.$conFig.alert_Tips('系统异常请稍后再试！')
					}
				})


			},
			loadBanne() {
				console.log(uni.getStorageSync('timeDepartment').category)
				//加载banner
				this.$reqUest.get('linkmed', '/linkmed2020/broadcast', {
					"ishot": "1"
				}).then(data => {
					var [error, res] = data;
					
					this.loadCalendar()
					
					//uni.hideLoading();
					console.log(res)
					return
					
					if (res.data.code == "100000") {

						var ListData = []
						var add = res.data

						Object.keys(add).forEach((key) => {
							ListData.push(add[key])
						})
						
						console.log(ListData)
					} else {
						this.$conFig.alert_Tips('系统异常请稍后再试！')
					}
					
			
					//console.log(res.data)
				})
			},
			loadCalendar() {
				this.loadVideo()
			},
			loadVideo(){
				this.$reqUest.get('linkmed', '/linkmed2020/videoinfo', {
					"tags": "强生微课",
					"category":uni.getStorageSync('timeDepartment').category
				}).then(data => {
					var [error, res] = data;
					
					var list=this.$conFig.newList(res.data)
					
					if(list.length!=0){
						
						this.issign=true
					}else{
						this.$conFig.alert_Tips('暂无信息！')
					}
					uni.hideLoading();
					
					console.log(list)
					return
					
					if (ListData.length != 1) {
						for(var i=0;i<3;i++){
							this.newVideoData.push(ListData[i])
						}
						
						console.log(this.newVideoData)
						
						
					} else {
						
					}
					
					
					
							
					//console.log(res.data)
				})
			},
			upper(e){
				console.log(e)
			}


		},
	};
</script>

<style>
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

	.bannebtbg {
		padding: 7px 10px;
		width: calc(100% - 20px);
		bottom: 0px;
		left: 0px;
	}

	.zbz {
		padding: 5px 10px 5px 10px;
		top: 15px;
		right: 0px;
		background: rgba(50, 54, 67, 0.45);
		border-radius: 5px 0px 0px 5px;
	}

	.zbzdian {
		background: #fff;
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
		margin-right: 15px;
		width: 60px;
		box-shadow: 0px 3px 5px rgba(0, 0, 0, 0);
	}

	.calendarli.on {
		width: 135px;
		box-shadow: 0px 3px 5px rgba(0, 0, 0, 0.2);
	}

	.calendarlizk {
		width: 135px;
	}

	.calendarlizk>view:nth-child(1) {
		width: 60px;
		height: 24px;
	}

	.calendarlizk>view:nth-child(2) {
		background: #fff;
		width: 75px;
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
		height: 330rpx;
	}

	.caleswBox .caleswBoxhezi {
		width: calc(100% - 40px);
		margin: 0px auto;
		height: 310rpx;
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
</style>
