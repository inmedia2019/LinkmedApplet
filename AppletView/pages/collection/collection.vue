<template>
	<view class="pa whtl bF5F6FA">
		<template v-if="issign">
			<view class="pa whtl animated fadeIn fast">
				<view class="cfff pf colltop ovh" style="height: 95px;">
					<image src="../../static/bg.jpg" class="pa w100" mode="widthFix"></image>
					<view class="pr z3 body">
						<view class="fl center">
							<image style="width: 90px; height: 56px;" src="../../static/logo.png" mode="widthFix"></image>
						</view>
						<view class="fr" style="width: calc(100% - 110px); margin-top: 28px;">
							<template v-if="id==1||id==2">
								<scroll-view scroll-x="true" class="collect" :scroll-into-view="scrollinto" scroll-with-animation="300">
									<view class="ovh h35 f12 cfff" style="width: max-content;">
										<block v-for="(array,inx) in arrayData" :key="inx">
											<view :id="'tab'+inx" @click="upScroll(inx,array.title)" class="fl" :class="arrayindx==inx?'on':''">{{array.title}}</view>
										</block>
										<view class="fl" style="padding: 0px; background: none; width: 0px;"></view>
									</view>
								</scroll-view>
							</template>
							<template v-else>
								<view class="center fr">
									<view class="cfff jz">浏览记录仅显示最近30条</view>
								</view>
							</template>
						</view>
					</view>
				</view>
				
				<scroll-view scroll-y="true" class="pf scroll-view" @scrolltolower="loadContent">
					<view class="body">
						<template v-if="id==1||id==3">
							<block v-for="(list,index) in listData" :key="index">
								<view class="videolist shadow pr" @click="getUrl('../details/details?id='+list.id,0)">
									<image class="pa yd" src="../../static/yd.png" mode="widthFix"></image>
									<view class="pr whtl z3">
										<view class="">
											<view class="fl shadow videolistimg ovh pr" v-bind:style="{ 'background': 'url('+list.thumbnail.src+') no-repeat center','background-size':'cover' }">
												<view class="pa whtl center">
													<view class="iconfont icon-bofang cfff" style="font-size: 25px; height: 30px;"></view>
													<text class="pa cfff f10">{{list.time}}</text>
												</view>
				
											</view>
											<view class="fr videolistname f13">
												<view class="cB92727 videolistnametxt ovh">{{list.title}}</view>
												<view class="h5"></view>
												<view class="ovh">
													<view class="fl center">
														<view class="videolistnametx" v-bind:style="{ 'background': 'url('+list.headImg+') no-repeat center','background-size':'cover' }">
				
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
											<rich-text :nodes="list.introduce"></rich-text>
										</view>
										<view class="h10"></view>
										<view class="f12 cB92727">
											{{list.thumbup}}赞 · {{list.comment_count}}评论 · {{list.forwardNum}}转发
										</view>
									</view>
								</view>
							</block>
				
				
						</template>
						<template v-else>
							<view class="list">
								<block v-for="(calesw,inx) in listData" :key="inx">
									<view class="shadow listli pr animated fadeIn fast" v-bind:style="{ 'background': 'url('+calesw.thumbnail.src+') no-repeat center','background-size':'cover' }">
										<template v-if="Dtime<calesw.startTime">
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
										
										
										<template v-if="Dtime<calesw.startTime">
											<view class="pa whtl" @click="getUrl('../predetails/predetails?id='+calesw.id,0)"></view>
										</template>
										<template v-else-if="(Dtime>=calesw.startTime)&&(Dtime<=calesw.endTime)">
											<view class="pa whtl" @click="getUrl(calesw.IsMark!=true?'../predetails/predetails?id='+calesw.id:'../webview/webview?id='+''+calesw.link+'',0)"></view>
										</template>
										<template v-else-if="Dtime>calesw.endTime">
											<view class="pa whtl" @click="getUrl('../predetails/predetails?id='+calesw.id,0)"></view>
										</template>
										
									</view>
								</block>
							</view>
						</template>
						
						<template v-if="!isnone||listData.length!=0">
							<view>
								<uni-load-more iconType="circle" :status="status" />
							</view>
						</template>
					</view>
					
					<template v-if="id==1&&isnone&&listData.length==0">
						<noNe tab="2"></noNe>
					</template>
					<template v-if="id==2&&isnone&&listData.length==0">
						<noNe tab="3"></noNe>
					</template>
					
					
				</scroll-view>
			</view>
			<reTurn></reTurn>
		</template>
		
		
	</view>

</template>

<script>
	import reTurn from '@/components/return/return.vue'
	import uniLoadMore from '@/components/uni-load-more/uni-load-more.vue'
	import uniSection from '@/components/uni-section/uni-section.vue'
	import noNe from '@/components/none.vue'
	export default {
		components: {
			uniLoadMore,
			uniSection,
			noNe,
			reTurn
		},
		data() {
			return {
				issign:false,
				isnone:false,
				Dtime: '',
				page: 0,
				serverTime: '2020/04/15 03:03:03',
				id: 2,
				category:"全部",
				arrayData:[],
				listData: [],
				scrollinto: '',
				arrayindx: 0,
				scrollData: [],
				status: 'more',
				isUpdata:true,
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
			this.id=e.id
			
			//this.id=2
			
			var sign = this.$conFig.issign('false',getCurrentPages()[0].route)
			if (sign) {
				this.$conFig.alert_Load()
				
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
				if(this.$conFig.onUrl(e)){
					uni.navigateTo({
						url: e
					});
				}
			},
			upScroll(e,category) {
				if (this.category == category||this.status == "loading") {
					return
				}
				this.category = category
				this.scrollinto = 'tab' + e
				this.arrayindx=e
				
				this.status = "loading"
				this.page="0"
				this.listData=[]
				this.loadContent()
				
			},
			
			load(){
				this.$reqUest.get('linkmed', '/linkmed2020/categories').then(data => {
					var [error, res] = data;
					if (res.data.code == 100000) {
						uni.hideLoading()
						this.issign = true
						var add = res.data.result
						Object.keys(add).forEach((key) => {
							this.arrayData.push({
								title: add[key],
								id: key
							})
						})
						this.arrayData.unshift({
							title:"全部",
							id:0
						})
						
						this.loadContent()
						
						
						//
				
					} else {
						this.$conFig.alert_Tips('系统异常请稍后再试！')
					}
				})
			},
			loadContent(){
				var opdata={}
				var url=""
				
				if (!this.isUpdata || this.status == "noMore") {
					return
				}
				//
				this.isUpdata = false
				
				if(this.id==1){
					url='/linyimaiapi.ashx?Method=videoinfo'
					opdata={
						flag:1,
						page:this.page
					}
					if(this.category!="全部"){
						opdata={
							flag:1,
							category: this.category,
							page:this.page
						}
					}
				}else if(this.id==2){
					url='/linyimaiapi.ashx?Method=markclassinfo'
					opdata={
						page:this.page
					}
					if(this.category!="全部"){
						opdata={
							category: this.category,
							page:this.page
						}
					}
				}else if(this.id==3){
					url='/linyimaiapi.ashx?Method=userloginfo'
				}
				
				this.isnone=false
				this.status = "loading"
				
				this.$reqUest.get('inmedia', url,opdata).then(data => {
					var [error, res] = data;
					
					if(this.id==1){
						this.updata(this.$conFig.newList(JSON.parse(res.data.message)))
					}else if(this.id==2){
						this.updata(JSON.parse(res.data.message))
					}else if(this.id==3){
						this.updata(JSON.parse(res.data.message))
					}
				})
			},
			updata(list){
				this.page++
				
				console.log(list)
				
				if(this.id==2){
					for (var i = 0; i < list.length; i++) {
						var etime = list[i].endTime
						var stime = list[i].startTime
						var rtime = list[i].requestTime
						
						var imgSrc=list[i].thumbnail.src
						//list[i].thumbnail.src="https://www.linkmed.cn"+imgSrc.split("https://linkmed-test.jnjadc.com")[1]
						
						
						list[i].endTime = etime.substring(etime.indexOf('/Date(') + 6, etime.length - 2)
						list[i].startTime = stime.substring(stime.indexOf('/Date(') + 6, stime.length - 2)
						list[i].requestTime = rtime.substring(rtime.indexOf('/Date(') + 6, rtime.length - 2)
						this.serverTime = rtime.substring(rtime.indexOf('/Date(') + 6, rtime.length - 2)
						
						list[i].link=this.$base64.Base64.encode(list[i].link)
					}
				}
				
				if(this.id==3){
					for (var i = 0; i < list.length; i++) {
						var imgSrc=list[i].thumbnail.src
						//list[i].thumbnail.src="https://www.linkmed.cn"+imgSrc.split("https://linkmed-test.jnjadc.com")[1]
					}
				}
				
				this.listData = [...this.listData, ...list]
				this.isUpdata = true;
				this.status = "more"
				if (list.length != 20) {
					this.status = "noMore"
				}
				
				this.isnone=true
			}
		}
	}
</script>

<style>
	.colltop {
		width: 100%;
		top: 0px;
		left: 0px;
	}

	.jz {
		background: rgba(255, 255, 255, 0.2);
		padding: 5px 10px;
		border-radius: 5px;
	}

	.scroll-view {
		top: 115px;
		left: 0px;
		width: 100%;
		bottom: 0px;
	}
	.listli{height: 360rpx;}
</style>
