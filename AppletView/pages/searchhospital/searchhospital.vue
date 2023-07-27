<template>
	<view class="pf whtl bF5F6FA">
		<view class="cfff ovh pr">
			<image class="pa w100" style="" src="../../static/bg.jpg" mode="widthFix"></image>
			<view class="pr body">
				<input class="searinput" type="text" placeholder-style="color: #fff;" confirm-type="search" value="" @input="onsearchtxt" @confirm="search" placeholder="输入医院名称" />
				<view class="pa searinputicon center">
					<view class="iconfont icon-sousuo fl center" style="height: 20px;"></view>
				</view>
			</view>
		</view>
		<scroll-view scroll-y>
			
			<template v-if="issearch">
				<template v-if="isnone&&searchData.length==0">
					 <noNe tab="1"></noNe>
				</template>
				
				<template v-if="!isnone||searchData.length!=0">
					<view class="bfff body">
						<view class="ovh ssjllist">
							<block v-for="(search,index) in searchData" :key="index">
								<view class="center animated fadeIn fast" @click="getnavigateTo(search.title)">
									<view class="iconfont icon-sousuo c9DA4B3 center" style="height: 20px;"></view>
									<view class="w10"></view>
									<view class="ellipsis c000 ssjllistname">
										{{search.title}}
									</view>
									<view class="w20"></view>
									
									
									<!-- <view class="iconfont icon-guanbi c9DA4B3 center" style="height: 20px;"></view> -->
								</view>
							</block>
						</view>
						<!-- <view>
							<uni-load-more iconType="circle" :status="status" />
							<view class="h20"></view>
						</view> -->
					</view>
					
				</template>
				
				<template v-if="searchData.length==0">
					
				</template>
			</template>
			
			
			
		</scroll-view>
		<reTurn></reTurn>
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
				issearch:false,
				isnone:false,
				searchtxt:"",
				searchData:[],
				page:0,
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
		onShareAppMessage(res) {
			return {
				title: "领医迈",
				path: '/pages/index/index'
			}
		},
		onLoad(){
			//this.load("")
		},
		onShow() {
			this.$conFig.onWeixin("领医迈","　",'/pages/index/index',"s.jpg")
		},
		methods: {
			onsearchtxt(e){
				this.searchtxt=e.detail.value
			},
			search(){
				
				if(this.searchtxt==""){
					return
				}
				
				this.load(this.searchtxt)
				
			},
			load(e){
				console.log(e)
				this.$conFig.alert_Load()
				this.isnone=false
				this.$reqUest.get('linkmed', '/linkmed2020/hospitals',{query:e}).then(data => {
					var [error, res] = data;
					this.isnone=true
					console.log(res)
					if(res.data.code=="100000"){
						var add=res.data.result
						this.issearch=true
						
						this.searchData=[]
						Object.keys(add).forEach((key)=>{
							this.searchData.push({
								title:add[key],
								id: key
							})
						})
						console.log(this.searchData)
					}else{
						this.$conFig.alert_Tips('系统异常请稍后再试！')
					}
					uni.hideLoading();
				})
			},
			getnavigateTo(e){
				uni.setStorage({
				    key: 'Modifyhospital',
				    data: e
				});
				uni.navigateBack({
				    delta: 1
				});
			}
		}
	}
</script>

<style>
	scroll-view{ height: calc(100% - 76px); }
	
	.searinput{ background: rgba(255,255,255,0.5); color: #fff; padding: 10px 10px 10px 40px; border-radius:5px ; }
	
	.searinputicon{ top: 0px; left: 17px; height: 100%; width: 40px; }
	
	.rmsslist{}
	.rmsslist>view{ padding: 3px 10px; border-radius: 5px; margin-right: 10px; margin-bottom: 10px; }
	.rmsslist>view:nth-child(3n-2){ background:rgba(185,39,39,0.05); color: rgba(185,39,39,0.8); }
	.rmsslist>view:nth-child(3n-1){ background:rgba(187,38,149,0.05); color: rgba(187,38,149,0.8); }
	.rmsslist>view:nth-child(3n){ background:rgba(2,2,147,0.05); color: rgba(2,2,147,0.8); }
	
	.ssjllist{}
	.ssjllist>view{ padding: 10px 0px; }
	.ssjllistname{ width: calc(100% - 52px); }
	.ssjllistimg{ width: 40px; height: 40px; border-radius: 100px; }
	.ssjllistname2{ width: calc(100% - 76px); }
	
	.searlist{}
	.searlistli{ border-radius: 5px; }
	.searlistliion{ width: 30px; height: 30px; background:rgba(216,216,216,0.4); border-radius: 100px; }
	.searlistlinr{ width: calc(100% - 45px); margin-left: 15px; }
	.searlistliks{ border-radius: 5px; }
	
	.searlistliks{ top: 10px; right: 10px; padding: 3px 5px; border-radius: 5px; border: #B92727 solid 1px; }

	.searlistliimg{ width: 45px; height: 45px; border-radius: 100px; }
	.searlistlinr2{width: calc(100% - 60px); margin-left: 15px;}

</style>
