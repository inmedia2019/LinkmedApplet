<template>
	<view class="pf whtl bF5F6FA">
		<template v-if="issign">
			<view class="pr animated fadeIn fastpf whtl">
				<view class="cfff ovh pr">
					<image class="pa w100" style="" src="../../static/bg.jpg" mode="widthFix"></image>
					<view class="pr body">
						<input class="searinput" type="text" placeholder-style="color: #fff;" confirm-type="search" value="" @input="onsearchtxt"
						 @confirm="search" placeholder="输入你的搜索" :value="searchtxt" />
						<view class="pa searinputicon center">
							<view class="iconfont icon-sousuo fl center" style="height: 20px;"></view>
						</view>
					</view>
				</view>
				<scroll-view class="pf" scroll-y="true" @scrolltolower="loadList">
					
					<template v-if="issearch&&isnone&&listData.length==0">
						<noNe tab="1"></noNe>
					</template>
					
					<template v-if="!issearch">
						<view class="bfff animated fadeIn fastpf">
							<template v-if="hotData.length!=0">
								<view class="body" style="padding-bottom: 5px;">
									<view class="c768196 fb">热门搜索</view>
									<view class="h10"></view>
									<view class="ovh rmsslist">
										<block v-for="(hot,index) in hotData" :key="index">
											<view class="fl" @click="onsearch(hot.PopularSearchContent)">
												{{hot.PopularSearchContent}}
											</view>
										</block>
									</view>
								</view>
							</template>
							<template v-if="recordData.length!=0">
								<view class="body ovh">
									<text class="fl fb c768196">搜索记录</text>
									<text class="fr f12 c9B9B9B" @click="rem(0,0)">清除历史</text>
									<view class="h10"></view>
									<view class="ovh ssjllist w100">
										<block v-for="(record,index) in recordData" :key="index">
											<view class="center">
												<view class="iconfont icon-sousuo c9DA4B3 center" style="height: 20px;"></view>
												<view class="w10"></view>
												<view class="ellipsis c000 ssjllistname" @click="onsearch(record.SearchHistoryContent)">
													{{record.SearchHistoryContent}}
												</view>
												<view class="w20"></view>
												<view class="iconfont icon-guanbi c9DA4B3 center" @click="rem(record.ID,index)" style="height: 20px;"></view>
											</view>
										</block>
									</view>
								</view>
							</template>
							
						</view>
					</template>
					<template v-else>
						<view class="body searlist">
							<block v-for="(list,index) in listData" :key="index">
								<template v-if="list.type!='专家'">
									<view @click="getUrl(list.type=='手术视频'?'../details/details?search=true&id='+list.nid:list.link,list.type=='手术视频'?0:1)" class="searlistli ovh body bfff shadow pr animated fadeIn fastpf">
										<view class="searlistliion center fl">
											<view class="iconfont icon-tupian fb fl center" style="font-size: 16px; height: 16px;"></view>
										</view>
										<view class="searlistlinr fl">
											<view class="h5"></view>
											<view class="ellipsis"><rich-text :nodes="list.title"></rich-text></view>
											<view class="h5"></view>
											<view class="op6 ovh" style="max-height: 60px;">
												<rich-text :nodes="list.body"></rich-text>
											</view>
										</view>
									</view>
								</template>
								<template v-else>
									<view @click="getUrl('../introduction/introduction?id='+list.uid,0)" class="searlistli ovh body bfff shadow pr animated fadeIn fastpf">
										<template v-if="list.position!=''">
											<view class="pa f10 cB92727 searlistliks">{{list.position}}</view>
										</template>
										<view class="searlistliimg center fl" :style="{ 'background': 'url('+list.headImg+') no-repeat center','background-size':'cover' }">
									
										</view>
										<view class="searlistlinr2 fl">
											<view class="h5"></view>
											<view class="ellipsis"><rich-text :nodes="list.name"></rich-text></view>
											<view class="h5"></view>
											<view class="op6 ovh" style="max-height: 60px;">
												<rich-text :nodes="list.body"></rich-text>
											</view>
										</view>
									</view>
								</template>
								<view class="h20"></view>
							</block>
							
						</view>
						<template v-if="!isnone||listData.length!=0">
							<view>
								<uni-load-more iconType="circle" :status="status" />
								<view class="h20"></view>
							</view>
						</template>
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
				issearch: false,
				isnone:false,
				searchtxt: "",
				hotData:[],
				recordData:[],
				listData:[],
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
		onLoad() {
			var sign = this.$conFig.issign('false',getCurrentPages()[0].route)
			if (sign) {
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
				if(u==1){
					window.location.href = e
					return
				}
				if(this.$conFig.onUrl(e)){
					uni.navigateTo({
						url: e
					});
				}
			},
			rem(id,index){
				this.$conFig.alert_Load()
				this.$reqUest.get('inmedia', '/linyimaiapi.ashx?Method=cleanhistory', {
					id: id
				}).then(data => {
					var [error, res] = data;
					if(res.data.result=="true"){
						this.recordData.splice(index,1);
						if(id==0){
							this.recordData=[]
						}
					}else{
						this.$conFig.alert_Tips('系统异常请稍后再试！')
					}
					uni.hideLoading();
				})
			},
			onsearch(e){
				this.searchtxt = e
				
				this.issearch = true
				this.listData=[]
				this.status="loading"
				this.loadList()
			},
			onsearchtxt(e) {
				this.searchtxt = e.detail.value
				if(this.searchtxt==""){
					this.load()
					this.issearch = false
					return
				}
			},
			search() {
				if(this.searchtxt==""){
					this.load()
					this.issearch = false
					return
				}
				this.issearch = true
				this.listData=[]
				this.status="loading"
				this.loadList()
			},
			load(){
				this.issign=true
				
				this.$reqUest.get('inmedia', '/linyimaiapi.ashx?Method=popularsearch', {
					key: this.searchtxt,
					page:this.page
				}).then(data => {
					var [error, res] = data;
					console.log(res)
					console.log("////")
					
					this.hotData=JSON.parse(res.data.message).PopularSearchs
					this.recordData=JSON.parse(res.data.message).SearchHistorys
					
					console.log(this.hotData)
					console.log(this.recordData)
				})
				
			},
			loadList(){
				if(!this.isUpdata||this.status=="noMore"){
					return
				}
				this.isnone=false
				this.isUpdata=false
				this.status="loading"
				
				
				this.$reqUest.get('inmedia', '/linyimaiapi.ashx?Method=searchhistory', {
					key: this.searchtxt,
					page:this.page
				}).then(data => {
					var [error, res] = data;
					this.page++
					
					console.log(res)
					
					var list=this.$conFig.newList(JSON.parse(res.data.message))
					console.log(list)
					this.listData=[...this.listData,...list]
					this.isUpdata=true;
					
					for(var i=0;i<this.listData.length;i++){
						var txt=this.searchtxt
						
						try {
						    this.listData[i].body=this.listData[i].body.replace(new RegExp(txt,'g'),"<span style='color:red;'>"+txt+"</span>")
						}
						catch(err) {
						    
						}
						
						try {
						   this.listData[i].title=this.listData[i].title.replace(new RegExp(txt,'g'),"<span style='color:red;'>"+txt+"</span>")
						}
						catch(err) {
						    
						}
						
						try {
						    this.listData[i].name=this.listData[i].name.replace(new RegExp(txt,'g'),"<span style='color:red;'>"+txt+"</span>")
						}
						catch(err) {
						    
						}
						
						
						
					}
					this.status="more"
					if(list.length!=20){
						this.status="noMore"
					}
					
					this.isnone=true
					
				})
				
				
			}
		}
	}
</script>

<style>
	
	scroll-view{ top: 77px; left: 0px; bottom: 0px; right: 0px; }
	
	.searinput {
		background: rgba(255, 255, 255, 0.5);
		color: #fff;
		padding: 10px 10px 10px 40px;
		border-radius: 5px;
	}

	.searinputicon {
		top: 0px;
		left: 17px;
		height: 100%;
		width: 40px;
	}

	.rmsslist {}

	.rmsslist>view {
		padding: 3px 10px;
		border-radius: 5px;
		margin-right: 10px;
		margin-bottom: 10px;
	}

	.rmsslist>view:nth-child(3n-2) {
		background: rgba(185, 39, 39, 0.05);
		color: rgba(185, 39, 39, 0.8);
	}

	.rmsslist>view:nth-child(3n-1) {
		background: rgba(187, 38, 149, 0.05);
		color: rgba(187, 38, 149, 0.8);
	}

	.rmsslist>view:nth-child(3n) {
		background: rgba(2, 2, 147, 0.05);
		color: rgba(2, 2, 147, 0.8);
	}

	.ssjllist {}

	.ssjllist>view {
		padding: 10px 0px;
	}

	.ssjllistname {
		width: calc(100% - 52px);
	}

	.ssjllistimg {
		width: 40px;
		height: 40px;
		border-radius: 100px;
	}

	.ssjllistname2 {
		width: calc(100% - 76px);
	}

	.searlist {}

	.searlistli {
		border-radius: 5px;
	}

	.searlistliion {
		width: 30px;
		height: 30px;
		background: rgba(216, 216, 216, 0.4);
		border-radius: 100px;
	}

	.searlistlinr {
		width: calc(100% - 45px);
		margin-left: 15px;
	}

	.searlistliks {
		border-radius: 5px;
	}

	.searlistliks {
		top: 10px;
		right: 10px;
		padding: 3px 5px;
		border-radius: 5px;
		border: #B92727 solid 1px;
	}

	.searlistliimg {
		width: 45px;
		height: 45px;
		border-radius: 100px;
	}

	.searlistlinr2 {
		width: calc(100% - 60px);
		margin-left: 15px;
	}
</style>
