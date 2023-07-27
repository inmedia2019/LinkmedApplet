<template>
	<view class="pf whtl bF5F6FA">
		<template v-if="issign">
			<view class="pf whtl animated fadeIn fast">
				<view class="cfff pr ovh">
					<image class="pa w100" src="../../static/bg.jpg" mode="widthFix"></image>
					<view class="body ovh pr z3">
						<view class="fl center">
							<image style="width: 90px; height: 56px;" src="../../static/logo.png" mode="widthFix"></image>
						</view>
						<view class="fr topfr">
							<view class="fr h25">

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
				</view>
				<view class="body">
					<scroll-view scroll-x="true" class="videolisttitle" :scroll-into-view="scrollinto" scroll-with-animation="300">
						<view class="ovh h35 f12 cfff" style="width: max-content;">
							<block v-for="(_array,inx) in array" :key="inx">
								<view :id="'tab'+inx" @click="upScroll(inx,_array)" class="fl" :class="scrollindx==inx?'on':''">{{_array}}</view>
							</block>
					
							<view class="fl" style="padding: 0px; background: none; width: 0px;"></view>
						</view>
					</scroll-view>
					<view class="h10"></view>
					<view class="ovh f12 c9AA8B6">
						<view class="center fl videolistsx">
							<view :class="screen=='最近更新'?'on':''" @click="upScreen('最近更新')">最近更新</view>
							<view class="w5"></view>
							<view class="">|</view>
							<view class="w5"></view>
							<view :class="screen=='大家在看'?'on':''" @click="upScreen('大家在看')">大家在看</view>
							<view class="w5"></view>
							<view class="">|</view>
							<view class="w5"></view>
							<view :class="screen=='讨论最多'?'on':''" @click="upScreen('讨论最多')">讨论最多</view>
						</view>
					</view>
				</view>
				
				<scroll-view class="videoscrol pf" scroll-y @scrolltolower="updata">
					<view class="body" style="padding-top: 0px;">
						<block v-for="(list,inx) in listData" :key="inx">
							<view class="videolist shadow animated fadeIn fast" @click="getUrl('../details/details?id='+list.id,0)" style="background: url(../../static/yd.png) no-repeat bottom right #fff; background-size:90px;">
								<view class="">
									<view class="fl shadow videolistimg ovh pr"  v-bind:style="{ 'background': 'url('+list.thumbnail.src+') no-repeat center','background-size':'cover' }">
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
												<view class="videolistnametx" v-bind:style="{ 'background': 'url('+list.headImg+') no-repeat center','background-size':'cover' }"></view>
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
						</block>
					</view>
					
					
					<template v-if="!isnone||listData.length!=0">
						<view>
							<uni-load-more iconType="circle" :status="status" />
							
						</view>
					</template>
					<template v-if="isnone&&listData.length==0">
						<noNe tab="6"></noNe>
					</template>
					<boView></boView>
				</scroll-view>
				<foTter nav="1"></foTter>
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
				array: ["课程回放", "手术解析","空中圆桌派", "专题讲座"],
				scrollindx: 0,
				scrollinto: '',
				stitle:"全部",
				screen:"最近更新",
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
				// var num=0
				// console.log(e.id)
				// return
				// //if()
				
				var id=e.id
				if(id==undefined){
					id=0
				}
				
				if(e.tab==2){
					this.screen="大家在看"
				}
				if(e.tab==3){
					this.screen="讨论最多"
				}
				
				this.scrollindx = id
				this.scrollinto = 'tab' + id
				this.stitle=this.array[this.scrollindx]
				this.status = "loading"
				this.page="0"
				this.listData=[]
				//this.$conFig.alert_Load()
				this.updata()
				
				this.upreport(this.array[0],uni.getStorageSync('timeDepartment').category)
				//uni.report('title',0)
				uni.report(this.array[0],{category:uni.getStorageSync('timeDepartment').category})
				//console.log({title:this.array[0]})
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
			upScroll(e,t) {
				if (this.scrollindx == e||this.status == "loading") {
					return
				}
				this.scrollindx = e
				this.scrollinto = 'tab' + e
				this.stitle=t
				this.status = "loading"
				this.page="0"
				this.listData=[]
				this.updata()
				
				this.upreport(this.array[e],uni.getStorageSync('timeDepartment').category)
				//uni.report('title',this.array[e])
				uni.report(this.array[e],{category:uni.getStorageSync('timeDepartment').category})
				//uni.report('课程回放',{title:this.array[0]})
				
			},
			upScreen(e){
				if(this.screen==e||this.status == "loading"){
					return
				}
				
				this.screen=e
				this.status = "loading"
				this.page="0"
				this.listData=[]
				this.updata()
			},
			updata() {
				
				
				
				
				if (!this.isUpdata || this.status == "noMore") {
					return
				}
				//
				this.isUpdata = false
				this.isnone=false
				this.status = "loading"
				
				var opdata={
					category: uni.getStorageSync('timeDepartment').category,
					page:this.page
				}
				if(this.stitle!="课程回放"){
					opdata={
						tags:this.stitle,
						category: uni.getStorageSync('timeDepartment').category,
						page:this.page
					}
				}
				
				var url='/linyimaiapi.ashx?Method=videoinfo'
				if(this.screen=="大家在看"){
					url='/linyimaiapi.ashx?Method=videoinfoplay'
				}
				if(this.screen=="讨论最多"){
					url='/linyimaiapi.ashx?Method=videoinfocomment'
				}
				
				console.log(opdata)
				this.$reqUest.get('inmedia', url,opdata).then(data => {
					var [error, res] = data;
					this.page++
					console.log(res)
					var list=this.$conFig.newList(JSON.parse(res.data.message))
					console.log(list)
					this.listData = [...this.listData, ...list]
					this.isUpdata = true;

					this.status = "more"
					if (list.length != 20) {
						this.status = "noMore"
					}
					this.isnone=true
					this.issign = true
					uni.hideLoading()
				})
			},//&type='+type+'&category='+category
			upreport(type,category){
				this.$reqUest.get('inmedia', '/linyimaiapi.ashx?Method=labelclick',{
					type:type,
					category:category
				}).then(data => {
					var [error, res] = data;
					console.log("////")
					console.log(res)
				})
			}
		}
	}
</script>

<style>
	.videoscrol{ bottom: 0px; left: 0px; right: 0px; top: 185px; }
	.videolistsx {}

	.videolistsx view.on {
		color: #000;
	}
</style>
