<template>
	<view class="pf whtl bF5F6FA">
		<template v-if="issign">
			<view class="animated fadeIn fast pf whtl">
				<view class="cfff pr ovh">
					<image class="pa w100" src="../../static/bg.jpg" mode="widthFix"></image>
					<view class="body ovh z3 pr">
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
				<view class="bodys">
					<view class="h5"></view>
					<view class="dn">
						<view class="c4A4A4A f12">
							热门专家
						</view>
						<view class="h15"></view>
						<scroll-view scroll-x="true" class="experlisttitle">
							<view class="ovh f12 cfff" style="width: max-content; height: 125px; ">
								<block v-for="(hot,inx) in hotData" :key="inx">
									<view class="fl on shadow bfff">
										<view class="h5"></view>
										<view class="experlisttitletx m0auto" v-bind:style="{ 'background': 'url('+hot.headImg+') no-repeat center','background-size':'cover' }">
						
										</view>
										<view class="h3"></view>
										<view class="tec c4A4A4A">{{hot.name}}</view>
										<view class="h3"></view>
										<view class="center">
											<view class="iconfont icon-favorites cFCCF0A" style="font-size: 11px;"></view>
										</view>
									</view>
								</block>
						
								<view class="fl" style="padding: 0px; background: none; width: 0px;"></view>
							</view>
						</scroll-view>
					</view>
					<scroll-view scroll-x="true" class="videolisttitle" :scroll-into-view="scrollinto" scroll-with-animation="300">
						<view class="ovh h35 f12 cfff" style="width: max-content;">

							<block v-for="(_array,inx) in array" :key="inx">
								<view :id="'tab'+inx" @click="upScroll(inx,_array.title)" class="fl" :class="scrollindx==inx?'on':''">{{_array.title}}</view>
							</block>
							<view class="fl" style="padding: 0px; background: none; width: 0px;"></view>
						</view>
					</scroll-view>
				</view>
				<scroll-view class="expertlist" scroll-y @scrolltolower="updata">
					<view class="h5"></view>
					
					<block v-for="(list,inx) in listData" :key="inx">
						<view class="bodys bfff ovh animated fadeIn fast" @click="getUrl('../introduction/introduction?id='+list.uid,0)">
							<view class="expertlistimg fl"  v-bind:style="{ 'background': 'url('+list.headImg+') no-repeat center','background-size':'cover' }"></view>
							<view class="expertlistname fr">
								<view class="ovh">
									<view class="fl center">
										<view class="f16 fb c000">{{list.name}}</view>
										<view class="w10"></view>
										<template v-if="list.position.length!=0">
											<view class="f10 expertlistzk cB92727">{{list.position}}</view>
										</template>
										
									</view>
								</view>
								<view class="f12 c4A4A4A">
									{{list.introduce}}
								</view>
							</view>
						</view>
						<view class="h5"></view>
					</block>
					
					<view class="h20"></view>
					<uni-load-more iconType="circle" :status="status" />
					<view class="h20"></view>
					<boView></boView>
				</scroll-view>
				<foTter nav="9"></foTter>
				
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
				hotData: [],
				page: 0,
				array: [],
				scrollindx: 0,
				scrollinto: '',
				category:"",
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
		onShareAppMessage(res) {
			
			return {
				title: "领医迈",
				path: '/pages/index/index'
			}
		},
		onLoad() {
			var sign = this.$conFig.issign('false',getCurrentPages()[0].route)
			if (sign) {
				this.load()
			}
		},
		onShow() {
			this.$conFig.onWeixin("领医迈","　",'/pages/index/index',"s.jpg")
		},
		methods: {
			load() {
				this.$conFig.alert_Load()

				//加载科室
				this.$reqUest.get('linkmed', '/linkmed2020/categories').then(data => {
					var [error, res] = data;
					console.log(res)
					var add = res.data.result
					
					Object.keys(add).forEach((key) => {
						this.array.push({
							title: add[key],
							id: key
						})
					})
					this.category=this.array[0].title
					//this.uphot()
					this.updata()
				})
			},
			uphot() {
				
				
				this.$reqUest.get('inmedia', '/linyimaiapi.ashx?Method=expertinfo', {
					ishot: 1
				}).then(data => {
					var [error, res] = data;
					
					this.hotData = this.$conFig.newList(JSON.parse(res.data.message))
				
				})
				
				
				
			},
			updata() {
				if (!this.isUpdata || this.status == "noMore") {
					return
				}
				this.isUpdata = false
				this.status = "loading"
				
				var options={
					category:this.category,
					page:this.page
				}
				
				this.isnone=false
				this.$reqUest.get('inmedia', '/linyimaiapi.ashx?Method=expertinfo',options).then(data => {
					var [error, res] = data;
					this.isnone=true
					var list = this.$conFig.newList(JSON.parse(res.data.message))
					
					this.page++
					this.listData = [...this.listData, ...list]
					this.isUpdata = true;
					console.log(this.listData)
					this.status = "more"
					if (list.length != 20) {
						this.status = "noMore"
					}
					
					this.issign = true
					uni.hideLoading()
				})
			},
			upScroll(e,t) {
				console.log(e)
				if (this.scrollindx == e||this.status == "loading") {
					return
				}
				this.scrollindx = e
				this.scrollinto = 'tab' + e
				
				this.category=t
				this.status="more"
				this.listData=[]
				this.page=0
				
				this.updata()
				//console.log(this.currentCaleswIndex)

				//console.log()
			},
			getUrl(e, u) {
				if(this.$conFig.onUrl(e)){
					uni.navigateTo({
						url: e
					});
				}
			},
		}
	}
</script>

<style>
	.experlisttitle {
		width: calc(100% + 20px);
		height: 125px;
	}

	.experlisttitle .fl {
		width: 65px;
		height: 98px;
		margin-left: 3px;
		border-radius: 5px;
		padding: 5px 10px;
		margin-right: 10px;
	}


	.experlisttitletx {
		width: 55px;
		height: 55px;
		border-radius: 100px;
	}

	.expertlist {
		height: calc(100% - 150px);
	}

	.expertlistpx {
		padding: 0px 17px;
	}

	.expertlistimg {
		width: 54px;
		height: 54px;
		border-radius: 100%;
	}

	.expertlistname {
		width: calc(100% - 70px);
	}

	.expertlistzk {
		border: #B92727 solid 1px;
		padding: 0px 5px;
		border-radius: 5px;
	}

	.expertxs {
		top: 308px;
		right: 15px;
		height: calc(100% - 308px);
	}

	.expertxsbk {
		max-height: 90%;
		border-radius: 10px;
		padding: 10px 5px;
		box-shadow: 0px 0px 5px rgba(0, 0, 0, 0.2);
		line-height: 1.4;
	}

	.expertxsbk .on {
		color: #B92727;
	}
</style>
