<template>
	<view class="pf whtl">
		<template v-if="issign">
			<view class="pr animated fadeIn fast ">
				<scroll-view class="pf bfff" scroll-y>
					<view class="">
						<view class="center">
							<image class="w100" :src="info.thumbnail.src" mode="widthFix"></image>
						</view>
						<view class="bodys cB92727 f16" style="background: #F5F6FA;">
							{{info.title}}
						</view>
						<view class="pr ovh" style="background: #fff;">
							<!-- <image class="w100 pa" style="bottom: 0px; left: 0px;" src="../../static/betgrbg2.png" mode="widthFix"></image> -->
							<view class="body f15 c4A4A4A pr z3">
								<!-- <view class="h10"></view> -->
								<view class="ovh">
									<view class="fl">
										直播时间
									</view>
									<view class="fr">
										{{info.startTimes}}
									</view>
								</view>
								<view class="h10"></view>
								<view class="h2 bDEDDDE op7"></view>
								<!-- <view class="h15"></view> -->
								<!-- <view class="ovh">
									<view class="fl preimg" v-bind:style="{ 'background': 'url('+info.headImg+') no-repeat center','background-size':'cover' }">

									</view>
									<view class="fr pernr">
										<view class="f16">{{info.expert}}</view>
										<view class="h5"></view>
										<view class="c999">
											{{info.expert_intro}}
										</view>
									</view>
								</view> -->
							</view>
						</view>
						<view class="body bfff" style="padding-top: 0px;">
							<rich-text :nodes="info.content|formatRichText"></rich-text>
							<!-- {{info.content}} -->
						</view>
						<view class=" bfff" style="height: 17px;"></view>
					</view>
				</scroll-view>

				<view class="pf prebutt center">
					<view class="prenr1 center">
						<view class="w100 ovh">
							<view class="center fl c999 f12">
								<view class="w15 h5"></view>
								<view>#{{info.category}}</view>
								<view class="w15 h5"></view>
								<view>|</view>
								<view class="w15 h5"></view>
								<view class="f18 cD0021B">{{info.reservationCount}}</view>
								<view class="w5 h5"></view>
								<view>人已预约</view>
							</view>
						</view>
					</view>
					<view class="prenr2 bbs center cfff" @click="onResult">
						<view>
							<template v-if="countEnd>0">
								<template v-if="countDown>0">
									<view class="tec fb f16">{{info.reservationState=="未预约!"?'点击预约':'尚未开播'}}</view>
									<view class="tec f10">{{tiemtxt}}后开始</view>
								</template>
								<template v-else>
									<view class="tec fb f16">{{info.reservationState=="未预约!"?'点击预约':'点击进入'}}</view>
									<view class="tec f10">已开始</view>
								</template>
							</template>
							<template v-else>
								<view class="tec fb f16">课程已结束</view>
							</template>
						</view>
					</view>
				</view>
			</view>
		</template>
		<reTurn>
			
		</reTurn>
		
	</view>
</template>

<script>
	import reTurn from '@/components/return/return.vue'
	export default {
		components: {
			reTurn
		},
		data() {
			return {
				issign: false,
				search:"",
				info: {},
				serverTime: "",
				countDown: "",
				countEnd: "",
				tiemtxt: "",
				result: false,
				resultid: "",
				code: ""
			}
		},
		onLoad(e) {
			
			var sign = this.$conFig.issign(e.share, 'pages/predetails/predetails?share=true&id=' + e.id,e.oid)
			if (sign) {
				//70452   71137
				this.resultid = e.id
				console.log(this.resultid)
				//this.resultid = 70452
				this.load()
			}
		},
		onShow() {
			this.$conFig.onWeixin("领医迈","　",'/pages/predetails/predetails?share=true&id=' + this.resultid,"s.jpg")
		},
		onShareAppMessage(res) {
			if (res.from === 'button') { // 来自页面内分享按钮
				this.userlog(6)
			}
			return {
				title: this.info.title,
				path: '/pages/predetails/predetails?share=true&id=' + this.resultid
			}
		},
		filters: {
			formatRichText(html) { //控制小程序中图片大小

				try {
					let newContent = html.replace(/<img[^>]*>/gi, function(match, capture) {
						match = match.replace(/style="[^"]+"/gi, '').replace(/style='[^']+'/gi, '');
						match = match.replace(/width="[^"]+"/gi, '').replace(/width='[^']+'/gi, '');
						match = match.replace(/height="[^"]+"/gi, '').replace(/height='[^']+'/gi, '');
						return match;
					});
					newContent = newContent.replace(/style="[^"]+"/gi, function(match, capture) {
						match = match.replace(/width:[^;]+;/gi, 'max-width:100%;').replace(/width:[^;]+;/gi, 'max-width:100%;');
						return match;
					});
					newContent = newContent.replace(/<br[^>]*\/>/gi, '');
					newContent = newContent.replace(/\<img/gi,
						'<img style="max-width:100%;height:auto;display:inline-block;margin:10rpx auto;"');
					return newContent;

				} catch (err) {
					//在这里处理错误
				}


			}
		},
		methods: {

			setTim(e) {
				//格式化时间
				function num(n) {
					return n < 10 ? '0' + n : n;
				};

				//倒计时函数
				setInterval(() => {
					//当前时间
					var startTime = new Date();
					//结束时间
					//算出中间差，以毫秒数返回.
					
					this.countDown = (this.info.startTime - startTime.getTime());
					this.countEnd = (this.info.endTime - startTime.getTime())

					//获取天数
					var oDay = parseInt(this.countDown / 1000 / 60 / 60 / 24);
					//获取小时数
					var oHour = parseInt(this.countDown / 1000 / 60 / 60 % 24);
					//获取分钟数
					var oMinute = parseInt(this.countDown / 1000 / 60 % 60);
					//获取秒数
					var oSecond = parseInt(this.countDown / 1000 % 60);
					//获取标签

					if (oDay == 0) {
						oDay = ""
					} else {
						oDay = oDay + '天 '
					}

					//输出时间
					this.tiemtxt = oDay + num(oHour) + ':' + num(oMinute) + ':' + num(oSecond) + " ";

				}, 1000);
			},
			formatDate(date) {
				var date = new Date();
				var seperator = "-";
				var year = date.getFullYear();
				var month = date.getMonth() + 1;
				var nowDate = date.getDate();
				if (month >= 1 && month <= 9) {
					month = "0" + month;
				}
				if (nowDate >= 0 && nowDate <= 9) {
					nowDate = "0" + nowDate;
				}
				var newDate = year + seperator + month + seperator + nowDate;
				return newDate;
			},
			load(e) {
				this.$conFig.alert_Load()
				this.$reqUest.get('inmedia', '/linyimaiapi.ashx?Method=markcheck', {
					"classid": this.resultid
				}).then(data => {
					var [error, res] = data;
					this.info = this.$conFig.newList(JSON.parse(res.data.message))[0]


					var etime = this.info.endTime
					var stime = this.info.startTime
					var rtime = this.info.requestTime

					this.info.appointment = this.info.appointment == "" ? 0 : this.info.appointment

					this.info.endTime = etime.substring(etime.indexOf('/Date(') + 6, etime.length - 2)
					this.info.startTime = stime.substring(stime.indexOf('/Date(') + 6, stime.length - 2)
					this.info.startTimes = new Date(parseInt(this.info.startTime / 1000) * 1000).toLocaleString().replace(
						/:\d{1,2}$/, ' ')
					this.serverTime = rtime.substring(rtime.indexOf('/Date(') + 6, rtime.length - 2)

					console.log(this.info)

					this.setTim()
					setTimeout(() => {
						this.issign = true
						uni.hideLoading();
					}, 1000)

					if (this.search == true) {
						this.userlog(2)
					} else {
						this.userlog(1)
					}
				})
			},
			userlog(e) {
				
				console.log(this.info.id,this.info.title,this.info.expert,e,this.info.expert_id)
				
				this.$reqUest.userLog({
					ClassType: "课程直播",
					ClassId: this.info.id,
					ClassTitle: this.info.title,
					ClassAuthor: this.info.expert,
					OperationType: e,
					doctorId: this.info.expert_id
				}).then(data => {
					var [error, res] = data;
					console.log(res)
					console.log("课程直播")
				})
			},
			onResult() {
				console.log("////")
				var templateId = 'oDxpzlHK3qcsrdZxUY9w6FlWazzxrkOjXBfBEStGiS8'
				if (this.countEnd < 0) {
					this.$conFig.alert_Tips('已结束，请预约其他课程！')
					return
				}
				
				if (this.info.reservationState != "未预约!") {
					console.log("1111")
					if (this.countDown < 0) {
						
						console.log("-----")
						window.location.href = this.info.link
						return
						uni.navigateTo({
							url: '../webview/webview?id=' + this.$base64.Base64.encode(this.info.link)
						});
					} else {
						this.$conFig.alert_Tips('您已预约，请等待直播开始！')
					}
					return
				}else{
					if (this.countDown < 0) {
						window.location.href = this.info.link
						return
						uni.navigateTo({
							url: '../webview/webview?id=' + this.$base64.Base64.encode(this.info.link)
						});
						return
					}
				}
				
				this.$conFig.alert_Load()
				this.savemark()
				return
				
				wx.getSetting({
					withSubscriptions: true,//是否获取用户订阅消息的订阅状态，默认false不返回
					success(res) {
						if (res.authSetting['scope.subscribeMessage']) {
							uni.openSetting({ // 打开设置页
								success(res) {
									
								}
							});
						} else { // 用户没有点击“总是保持以上，不再询问”则每次都会调起订阅消息
							uni.requestSubscribeMessage({
								tmplIds: [templateId], // 
								success(res) {
									if (res[templateId] == "accept") { // 字段就是tmplIds模板id
										that.$conFig.alert_Load()
										that.$reqUest.get('inmedia', '/linyimaiapi.ashx?Method=wechasubscription', {
											"openid": uni.getStorageSync('userInfo').openid,
											"Page": '/pages/predetails/predetails?share=true&id=' + that.resultid,
											"QuestionLabel": that.info.title,
											"Time": that.info.startTime,
											"MainLecturer":that.info.expert
										}).then(data => {
											var [error, res] = data;
											if(res.data.result=="true"){
												that.savemark()
												//that.$conFig.alert_Tips('您已成功订阅信息！')
											}
										})
									}else{
										that.$conFig.alert_Tips('请允许接收订阅消息！')
									}
								}
							})
						}
					}
				})
			},
			savemark(){
				var that=this
				this.$reqUest.get('inmedia', '/linyimaiapi.ashx?Method=wechasubscription', {
					"openid": uni.getStorageSync('userInfo').openid,
					"classid": this.resultid,
					"Page": '/pages/predetails/predetails?share=true&id=' + that.resultid,
					"QuestionLabel": that.info.title,
					"Time": that.info.startTime,
					"MainLecturer":that.info.expert
				}).then(data => {
					var [error, res] = data;
					this.info.reservationCount++
					this.info.reservationState = "已预约!"
					uni.hideLoading();
					this.$conFig.alert_Tips('预约成功！')
					if (this.countDown < 0) {
						setTimeout(()=>{
							window.location.href = this.info.link
							return
							uni.navigateTo({
								url: '../webview/webview?id=' + this.$base64.Base64.encode(this.info.link)
							});
						},2000)
					}
				})
			}
		}
	}
</script>

<style>
	scroll-view {
		top: 0px;
		left: 0px;
		bottom: 64px;
		right: 0px;
	}

	.preimg {
		width: 51px;
		height: 51px;
		border-radius: 50px;
	}

	.pernr {
		width: calc(100% - 65px);
	}

	.prebutt {
		height: 64px;
		background: #313131;
		bottom: 0px;
		left: 0px;
		width: 100%;
		box-shadow: 0px -5px 20px 0px rgba(0, 0, 0, 0.09);
	}

	.prenr1 {
		width: calc(100% - 130px);
		height: 64px;
	}

	.prenr2 {
		width: 130px;
		height: 64px;
	}
</style>
