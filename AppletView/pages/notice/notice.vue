<template>
	<view class="pf whtl bF5F6FA">
			<view class="pf whtl animated fadeIn fast">
				<view class="cfff ovh pr" style=" height: 48px;">
					<image class="w100" style="" src="../../static/bg.jpg" mode="widthFix"></image>
				</view>
				<view class="center notnav f16 tec c9B9B9B bfff">
					<view :class="step==1?'on':''" @click="onstep(1)">
						<view class="pr">
							系统通知
							<!-- <template v-if="video_created!=0">
								<text class="pa center cfff f8">{{video_created}}</text>
							</template> -->
						</view>
					</view>
					<view class="center" :class="step==2?'on':''" @click="onstep(2)">
						<view class="pr">
							留言回复
							<!-- <template v-if="comment_created!=0">
								<text class="pa center cfff f8">{{comment_created}}</text>
							</template> -->
						</view>
					</view>
					<view class="center" :class="step==3?'on':''" @click="onstep(3)">
						<view class="pr">
							赞与收藏
							<!-- <template v-if="flag_created!=0">
								<text class="pa center cfff f8">{{flag_created}}</text>
							</template> -->
						</view>
					</view>
				</view>
				<view class="h1 b979797 op2"></view>
				<scroll-view scroll-y="true" @scrolltolower="updata" class="pf" style="top: 105px; left: 0px; bottom: 0px; right: 0px;">
					<view class="body pr">
						<template v-if="step==1">
							<block v-for="(list,inx) in listData" :key="inx">
								<view class="animated fadeIn fast">
									<view class="h5"></view>
									<view class="tec f12 cC1C9D6">{{list.date}}</view>
									<view class="h5"></view>
									<view class="h3"></view>
									<view class="shadow body notlist bfff ovh" @click="getUrl('../details/details?id='+list.id,0)">
										<view class="notlisticon center fl">
											<view class="iconfont icon-duanxin fb fl center" style="font-size: 16px; height: 20px;"></view>
										</view>
										<view class="notlistnr fl">
											<view class="h3"></view>
											<view class="c000">系统通知</view>
											<view class="h5"></view>
											<view class="c000 op6 f12">{{list.title}}</view><!--，点击进入直播间！！-->
										</view>
									</view>
									<view class="h20"></view>
								</view>
							</block>
						</template>
						
						<template v-if="step==2">
							<block v-for="(list,inx) in listData" :key="inx">
								<view class="animated fadeIn fast">
									<view class="h5"></view>
									<view class="tec f12 cC1C9D6">{{list.created}}</view>
									<view class="h5"></view>
									<view class="h3"></view>
									<view class="shadow body notlist bfff ovh" @click="getUrl('../details/details?id='+list.nid,0)">
										<view class="notlisticon center fl" v-bind:style="{ 'background': 'url('+list.wechat_avatar.src+') no-repeat center','background-size':'cover' }">
									
										</view>
										<view class="notlistnr fl">
											<view class="h3"></view>
											<view class="c000">{{list.name}} 回复你：</view>
											<view class="h5"></view>
											<view class="c000 op6 f12">{{list.comment}}</view>
											<view class="h15"></view>
											<view class="f10 op6 cB92727 ellipsis">
												{{list.title}}
											</view>
										</view>
									</view>
									<view class="h20"></view>
								</view>
							</block>
						</template>
						<template v-if="step==3">
							<block v-for="(list,inx) in listData" :key="inx">
								<view class="animated fadeIn fast">
									<view class="h5"></view>
									<view class="tec f12 cC1C9D6">{{list.created}}</view>
									<view class="h5"></view>
									<view class="h3"></view>
									<view class="shadow body notlist bfff ovh" @click="getUrl('../details/details?id='+list.nid,0)">
										<view class="notlisticon center fl" v-bind:style="{ 'background': 'url('+list.avatar+') no-repeat center','background-size':'cover' }">
												
										</view>
										<view class="notlistnr fl">
											<view class="h3"></view>
											<view class="c000">{{list.name}} {{list.type=="thumbup"?"赞":"收藏"}}了你的回复：</view>
											<view class="h15"></view>
											<view class="f10 op6 cB92727 ellipsis">
												{{list.title}}
											</view>
										</view>
									</view>
									<view class="h20"></view>
								</view>
							</block>
						</template>
					</view>
					
					<template v-if="!isnone||listData.length!=0">
						<view>
							<uni-load-more iconType="circle" :status="status" />
							<view class="h20"></view>
						</view>
					</template>
					<template v-if="isnone&&listData.length==0">
						<noNe tab="7"></noNe>
					</template>
				</scroll-view>
			</view>
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
				issign:true,
				isnone:false,
				video_created:"",
				comment_created:"",
				flag_created:"",
				listData:[],
				page:0,
				step:1,
				Newstep:"",
				status: 'loading',
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
		
		
		onShow() {
			this.$conFig.onWeixin("领医迈","　",'/pages/index/index',"s.jpg")
		},
		onLoad(){
			
			this.$conFig.alert_Load()
			
			var mydate = new Date();
			var myddy = mydate.getDay();
			
			
			uni.hideLoading()
			//this.onstep(1)
			this.load(this.step)
		},
		methods: {
			getDay(day){
			　　var today = new Date();
			　　var targetday_milliseconds=today.getTime() + 1000*60*60*24*day;
			　　today.setTime(targetday_milliseconds); //注意，这行是关键代码
			　　var tYear = today.getFullYear();
			 
			　　var tMonth = today.getMonth();
			 
			　　var tDate = today.getDate();
			 
			　　tMonth = this.doHandleMonth(tMonth + 1);
			 
			　　tDate = this.doHandleMonth(tDate);
			 
			　　return tYear+"-"+tMonth+"-"+tDate;
			 
			},
			doHandleMonth(month){
			 
			　　var m = month;
			 
			　　if(month.toString().length == 1){
			 
			　　　　m = "0" + month;
			 
			　　}
			 
			　　return m;
			 
			},
			
			getUrl(e, u) {
				if(this.$conFig.onUrl(e)){
					uni.navigateTo({
						url: e
					});
				}
			},
			onstep(e){
				
				if(this.step==e||this.status == "loading"){
					return
				}
				
				
				this.step=e
				this.status="loading"
				
				this.page=0
				this.listData=[]
				this.load(e)
				
			},
			getDate(date) {
				var arr = date.split('-');
				            var year = arr[0]; //获取当前日期的年份
				            var month = arr[1]; //获取当前日期的月份
				            var day = arr[2]; //获取当前日期的日
				            var days = new Date(year, month, 0);
				            days = days.getDate(); //获取当前日期中月的天数
				            var year2 = year;
				            var month2 = parseInt(month) - 1;
				            if (month2 == 0) {
				                year2 = parseInt(year2) - 1;
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
			load(e){
				var nowDate = new Date();
				 var year = nowDate.getFullYear();
				 var month = nowDate.getMonth() + 1 < 10 ? "0" + (nowDate.getMonth() + 1)
				  : nowDate.getMonth() + 1;
				 var day = nowDate.getDate() < 10 ? "0" + nowDate.getDate() : nowDate
				  .getDate();
				 var dateStr = year + "-" + month + "-" + day;
				
				
				this.isnone=false
				
				var url=""
				
				var options={}
				
				console.log(this.getDate(dateStr))
				
				if(e==1){
					//加载系统通知
					url="/linkmed2020/videoinfo"
					options={
						created:this.getDate(dateStr),
						category: uni.getStorageSync('userInfo').category,
						page:this.page
					}
				}else if(e==2){
					//加载留言回复
					url="/linkmed2020/notification_comment"
					options={
						page:this.page
					}
				}else if(e==3){
					//加载赞与收藏
					url="/linkmed2020/notification_flag"
					options={
						created:'1900-01-01',
						page:this.page
					}
				}
				
				this.$reqUest.get('linkmed', url, options).then(data => {
					var [error, res] = data;
					if(e==1||e==2){
						this.funupdata(this.$conFig.newList(res.data))
					}else{
						this.funupdata(res.data)
					}
				})
				
			},
			updata() {
				
				if (!this.isUpdata || this.status == "noMore") {
					return
				}
				//
				this.isUpdata = false
				this.status = "loading"
				
				this.load(this.step)
				
			},
			funupdata(res){
				
				this.isnone=true
				console.log(this.Newstep,this.step)
				
				
				this.page++
				
				this.listData = [...this.listData, ...res]
				
				this.status = "more"
				if (res.length != 20) {
					this.status = "noMore"
				}
				
				console.log(this.listData)
				
				this.isUpdata = true;
			}
			
		}
	}
</script>

<style>
	page {
		background: #F9FBFE;
	}

	.notnav {}

	.notnav>view {
		width: 33%;
		border-bottom: rgba(255, 255, 255, 0) solid 4px;
		padding: 10px 0px;
		margin-top: 7px;
	}

	.notnav>view.on {
		color: #B92727;
		border-bottom: #B92727 solid 4px;
	}

	.notnav text {
		top: -9px;
		right: -9px;
		width: 15px;
		height: 15px;
		background: linear-gradient(135deg, rgba(243, 130, 130, 1) 0%, rgba(227, 95, 75, 1) 100%);
		border-radius: 8px;
	}

	.notlist {
		border-radius: 5px;
	}

	.notlisticon {
		width: 30px;
		height: 30px;
		border-radius: 30px;
		background: #efeef1;
		margin-right: 15px;
	}

	.notlistnr {
		width: calc(100% - 45px);
	}
</style>
