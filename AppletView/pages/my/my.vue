<template>
	<view class="pf whtl bF5F6FA">
		<template v-if="issign">
			<view class="pr animated fadeIn fast">
				<scroll-view class="pf whtl" scroll-y="true">
					<view class="cfff ovh pr" style=" height: 115px;">
						<image src="../../static/bg.jpg" class="w100 pa" mode="widthFix"></image>
					</view>
					<view class="body pr z3" style="margin-top: -115px;">
						<view class="cfff">
							<view class="fl center">
								<image style="width: 90px; height: 56px;" src="../../static/logo.png" mode="widthFix"></image>
							</view>
							<template v-if="uid>0">
								<view class="fr pr icon-tongzhi1 iconfont" style="font-size: 25px;" @click="getUrl('../notice/notice?id=1',0)">
									<template v-if="messages!=0">
										<view class="noticetxt pa f8 bD43D4C cfff center">{{messages}}</view>
									</template>
								</view>
							</template>
							<!-- <view class="fr w10 h5"></view> -->
							<!-- <view class="fr icon-shezhi iconfont" style="font-size: 25px;" @click="modify(1)"></view> -->
						</view>
						<view class="clear"></view>
						<view class="h20"></view>
						<view class="bfff body shadow myk">
							<view>
								<template v-if="uid>0">
									<view class="ovh center" @click="modify(1)">
										<view class="mykimg center ovh">
											<image style="width: 100%;" :src="mysrc" mode="widthFix"></image>
										</view>
										<view class="myknr">
											<view class="f18 ovh">
												<view class="center fl">
													<view>{{myname}}</view>
													<view class="w10 h10"></view>
													
												</view>
											</view>
											<view class="h5"></view>
											<view class="ovh">
												<view class="bbs cfff f12 fl yym">{{myHospital}}</view>
											</view>
										</view>
										<view class="" style="width: 30px; height: 48px; margin-top: -10px;">
											<image style="width: 30px;" src="../../static/bj.png" mode="widthFix"></image>
										</view>
									</view>
								</template>
								<template v-else>
									<view class="ovh center" @click="login('../my/my')">
										<view class="mykimg center">
											<image class="w100" src="../../static/default.png" mode="widthFix"></image>
										</view>
										<view class="myknr">
											<view class="f14 ovh">
												<view class="center fl">
													<view>请登录</view>
												</view>
											</view>
										</view>
									</view>
								</template>
							</view>
							<!-- <template v-if="markclass.length!=0">
								<view class="h20"></view>
								<view class="h1 bDEDDDE"></view>
								<view class="h10"></view>
								<view class="h3"></view>
								<swiper class="animated fadeIn fast" style="height: 18px;" vertical autoplay="true" interval="5000" duration="500">
									<swiper-item v-for="(mark,inx) in markclass" :key="inx">
										<view class="center c999" @click="getUrl('../predetails/predetails?id='+mark.id,0)">
											<view class="icon-tongzhi iconfont center" style="font-size: 16px; height: 18px;"></view>
											<view class="w10 h5"></view>
											<view class="f12 tz ovh">
												<view class="fl">您预约的</view>
												<view class="w5 h5 fl"></view>
												<view class="c000 ellipsis fl" style="width: calc(100% - 200px);">{{mark.title}}</view>
												<view class="w5 h5 fl"></view>
												<view class="fl">即将开始，点击进入直播</view>
											</view>
										</view>
									</swiper-item>
								</swiper>
							</template> -->
						</view>
						<view class="h20"></view>
						<view class="h10"></view>
						<view class="bfff body shadow myk mynav center" style="padding: 17px 0px 10px 0px;">
							<view class="tec" @click="getUrl('../collection/collection?id=1',0)">
								<view class="center mynavimg">
									<image style="width: 32px; height: 32px; margin-left: 5px;" src="../../static/myi1.png" mode="widthFix"></image>
								</view>
								<view class="h3"></view>
								<view class="">收藏</view>
							</view>
							<template v-if="userinfo.type!='employee'">
								<view class="tec" @click="getUrl('../introduction/introduction?u=true&id='+userinfo.uid,0)"><!-- @click="uploadpade(true)" -->
									<view class="center mynavimg">
										<image style="width: 31px; height: 31px; margin-left: 5px;" src="../../static/myi2.png" mode="widthFix"></image>
									</view>
									<view class="h3"></view>
									<view class="">作品</view>
								</view>
							</template>
							<!-- <view class="tec" @click="getUrl('../collection/collection?id=2',0)">
								<view class="center mynavimg">
									<image style="width: 31px; height: 30px;" src="../../static/myi3.png" mode="widthFix"></image>
								</view>
								<view class="h3"></view>
								<view class="">预约</view>
							</view> -->
							<view class="tec" @click="getUrl('../collection/collection?id=3',0)">
								<view class="center mynavimg">
									<image style="width: 30px; height: 32px;" src="../../static/myi4.png" mode="widthFix"></image>
								</view>
								<view class="h3"></view>
								<view class="">浏览</view>
							</view>
						</view>
						<view class="h20"></view>
						<view class="h10"></view>
						<view class="cA2A3AB">关于领医迈</view>
						<view class="h5"></view>
						<view class="bfff body shadow myk mylist">
							<view class="center" @click="getUrl('../upload/upload',0)">
								<view class="c4A4A4A mylistname">意见反馈</view>
								<view class="iconfont icon-dibudaohanglan- cDEDDDE center" style="font-size: 24px; height: 21px;"></view>
							</view>
							<view class="center" @click="getUrls('../webview/webview?id='+aboutUrl,0)"><!--this.$conFig.inmediaApi+'/about.html?v1'-->
								<view class="c4A4A4A mylistname">关于</view><!--  -->
								<view class="iconfont icon-dibudaohanglan- cDEDDDE center" style="font-size: 24px; height: 21px;"></view>
							</view>
						</view>
						<template v-if="uid>0">
							<view class="h20"></view>
							<view class="bfff body shadow myk mylist center" style="padding: 12px 17px;" @click="signout">
								<view class="f15">安全退出</view>
							</view>
						</template>
					</view>
					<boView></boView>
				</scroll-view>
				<foTter nav="4"></foTter>
			</view>
		</template>
		<template v-if="ismodify">
			<view class="pf whtl tk animated fadeIn faster">
				<view class="center h100">
					<view class="pa whtl" @click="gbtk(1)">
					</view>
					<view class="bfff dlk body shadow pr z3">
						<view class="h10"></view>
						<view class="center">
							<view class="pz center shadow pr ovh" @click="chooseImage">
								<template v-if="bs64.length==0">
									<image style="width: 30px;" src="../../static/pz.png" mode="widthFix"></image>
								</template>
								<template v-else>
									<view class="pa whtl center">
										<image style="width: 100%;" :src="bs64" mode="widthFix"></image>
									</view>
								</template>
							</view>
						</view>
						<view class="h20"></view>
						<view class="center w100 f12" style="color: #666;">
							头像将会在您的作品中展示
						</view>
						<view class="h20"></view>
						
						<view class="">
							<template v-if="userinfo.type!='employee'">
								<view class="center w100">
									<view class="input pr">
										<input type="text" @input="onname" class="inputtxt3" placeholder-style=" color: #dadadc;" placeholder="输入您的真实姓名"
										 :value="myname" />
										<view class="pa inputwb c9AA8B6 center">真实姓名</view>
									</view>
								</view>
								<view class="h15"></view>
								<view class="center w100">
									<view class="input pr">
										<view class="inputtxt3" @click="getSearchHospital()" style="border-bottom: #BBC0CE solid 1px;">{{NewHospital}}　</view>
										<view class="pa inputwb c9AA8B6 center">所在医院</view>
									</view>
								</view>
								<view class="h15"></view>
								<view class="center w100">
									<view class="input pr">
										<view class="pa inputwb c9AA8B6 center">选择您的职称</view>
										 <picker class="inputtxt4 pr z3" style=" color: #dadadc;" @change="bindPickerChangeposition" :value="positionindex" :range="positionData" range-key="title">
										     <view class="inputtxt4_view c000" :class="positionindex==-1?'cdadadc':''">{{positionindex==-1?'请选择':positionData[positionindex].title}}</view>
											 <view class="pa center signicon" style="left: auto; right: 0px;">
											 	<view class="iconfont icon-dibudaohanglan- cdadadc mt4" style="font-size: 25px;  "></view>
											 </view>
										 </picker>
									</view>
								</view>
								<view class="h15"></view>
							</template>
							<view class="center w100">
								<view class="input pr">
									<view class="pa inputwb c9AA8B6 center">选择感兴趣的领域</view>
									<picker class="inputtxt4 pr z3" @change="bindPickerChange" :value="arrayIndex" :range="array" range-key="title">
										<view class="inputtxt4_view">{{array[arrayIndex].title}}</view>
										<view class="pa center signicon" style="left: auto; right: 0px;">
											<view class="iconfont icon-dibudaohanglan- cdadadc mt4" style="font-size: 25px;  "></view>
										</view>
									</picker>
								</view>
							</view>
							<view class="h15"></view>
							<template v-if="userinfo.type!='employee'">
								<view class="center w100">
									<view class="input pr" @click="modifytel(1)">
										<input type="text" class="inputtxt5" :value="mytel" disabled="true" />
										<view class="pa inputwb c9AA8B6 center">修改绑定手机</view>
										<view class="pa center signicon" style="left: auto; right: 0px;">
											<view class="iconfont icon-dibudaohanglan- cdadadc mt4" style="font-size: 25px;"></view>
										</view>
									</view>
								</view>
							</template>
							<view class="h15"></view>
							<view class="center">
								<view class="cfff f18 shadow3 bbs netbut" @click="modify(2)">
									提交
								</view>
							</view>
						</view>
						<view class="h10"></view>
					</view>
				
				</view>
			</view>
		</template>

		<template v-if="ismodifytel">
			<view class="pf whtl tk animated fadeIn faster">
				<view class="center h100">
					
					<view class="modifytel bF3F6F8 pr z3">
						<view class="" style="padding: 25px;">
							<view class="pr modifytelinput">
								<input type="tel" @input="onNewtel" class="bfff" placeholder-style=" color: #9B9B9B;" value="" placeholder="请填写新的手机号" />
							</view>
							<view class="h20"></view>
							<view class="pr modifytelinput">
								<input type="tel" @input="oncode" maxlength="4" class="bfff" placeholder-style=" color: #9B9B9B;" value=""
								 placeholder="请填写验证码" />
								<view class="pa center yzm f12 cfff z3" @click="obtain">
									<view :class="downTime!=60?'bAAB2B7':'bB92727'">
										获取验证码
									</view>
								</view>
							</view>
							<view class="h20"></view>
							<template v-if="downTime==60">
								<view class="f12 c000">请输入修改的绑定手机号码及验证信息</view>
							</template>
							<template v-else>
								<view class="f12 c000">验证码已发送，再次获取请等待 <text style="padding: 0px 5px;" class="cB92727">{{downTime}}</text> S</view>
							</template>
						</view>
						<view class="center f18 modifytelbut">
							<view class="center c9AA8B6" style="width: 50%;" @click="modifytel(2)">
								取消
							</view>
							<view style="width: 1px; background: #D6D6D6;"></view>
							<view class="center cB92727" style="width: 50%;" @click="modifytel(3)">
								确定
							</view>
						</view>
					</view>

				</view>
			</view>
		</template>
		<template v-if="iswym">
			<view class="pf whtl tk animated fadeIn faster">
				<view class="center h100">
					<view class="center pr" style="width: 80%;">
						<image style="" src="../../static/wym.png" mode="widthFix"></image>
						<view class="center pa" style="width: 100%; left: 0px; bottom: 30px;">
							<view class="cfff f18 shadow3 bbs netbut"  @click="uploadpade(false)">
								确定
							</view>
						</view>
					</view>
				</view>
			</view>
		</template>
	</view>
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
				uid:"",
				issign: false,
				iswym:false,
				messages: "",
				markclass: [],
				ismodify: false,
				ismodifytel: false,
				downTime: 60,
				positionData:[],
				positionindex: -1,
				array: [],
				arrayIndex: 0,
				isInSideMember:"",
				timeDepartment: "",
				userinfo: "",
				bs64:"",
				mysrc:"",
				myname:"",
				Newname:"",
				myHospital:"",
				NewHospital:"",
				mymail:"",
				Newmail:"",
				mytel:"",
				Newtel:"",
				code: "",
				verify: "",
				expired: "",
				mymake: "预约名称",
				makeid: 10,
				aboutUrl:this.$base64.Base64.encode(this.$conFig.inmediaApi+'/about.html?v='+Date.parse(new Date()))
			}
		},
		onLoad(e) {
			this.uid=uni.getStorageSync('userInfo').uid
			
			var sign = this.$conFig.issign('false',getCurrentPages()[0].route)
			
			if (sign) {
				this.issign = true
			}
			if(this.uid!=undefined){
				
				this.isInSideMember=uni.getStorageSync('isInSideMember')
				this.timeDepartment= uni.getStorageSync('timeDepartment')
				this.userinfo= uni.getStorageSync('userInfo')
				this.bs64=uni.getStorageSync('userInfo').avatar
				this.mysrc=uni.getStorageSync('userInfo').avatar
				this.myname= uni.getStorageSync('userInfo').name
				this.Newname=uni.getStorageSync('userInfo').name
				this.myHospital= uni.getStorageSync('userInfo').hospital
				this.NewHospital= uni.getStorageSync('userInfo').hospital
				this.mymail= uni.getStorageSync('userInfo').mail
				this.Newmail= uni.getStorageSync('userInfo').mail
				this.mytel= uni.getStorageSync('userInfo').mobile
				this.Newtel=uni.getStorageSync('userInfo').mobile
			}
			uni.setStorage({
				key: 'tximg',
				data: ""
			});
			uni.setStorage({
				key: 'Modifyhospital',
				data: ""
			});

			//this.modify(1)
		},
		onShow(e) {
			this.$conFig.onWeixin("领医迈","　",'/pages/index/index',"s.jpg")
			this.uid=uni.getStorageSync('userInfo').uid
			
			if(this.uid!=undefined){
				this.myname= uni.getStorageSync('userInfo').name
				this.Newname=uni.getStorageSync('userInfo').name
				
				if(uni.getStorageSync('tximg').length>10){
					this.bs64=uni.getStorageSync('tximg')
				}
				if (uni.getStorageSync('Modifyhospital') != "") {
					this.NewHospital = uni.getStorageSync('Modifyhospital')
				}
				
				//this.wdxx()
				
				// this.$reqUest.get('inmedia', '/linyimaiapi.ashx?Method=markclassinfo', {
				// 	appointmentTime: (new Date()).getTime()
				// }).then(data => {
				// 	var [error, res] = data;
				// 	this.markclass = JSON.parse(res.data.message)
				// })
			}
		},
		onHide() {
			
		},
		onShareAppMessage(res) {
			return {
				title: "领医迈",
				path: '/pages/index/index'
			}
		},
		methods: {
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
			wdxx() {
				var mydate = new Date();
				var myddy = mydate.getDay();

				this.$reqUest.get('inmedia', '/linyimaiapi.ashx?Method=notificationcount').then(data => {
					var [error, res] = data;
					console.log(JSON.parse(res.data.message).video_count, JSON.parse(res.data.message).comment_count, JSON.parse(res
						.data.message).flag_count)
					this.messages = JSON.parse(res.data.message).comment_count + JSON.parse(res.data.message).flag_count + JSON.parse(
						res.data.message).video_count
				})
			},
			onNewtel(e) {
				this.Newtel = e.detail.value
			},
			oncode(e) {
				this.code = e.detail.value
			},
			onname(e) {
				this.Newname = e.detail.value
			},
			onhospital(e) {
				this.NewHospital = e.detail.value
			},
			onmail(e) {
				this.Newmail = e.detail.value
			},
			getSearchHospital() {
				uni.setStorage({
					key: 'Modifyhospital',
					data: ""
				});
				uni.navigateTo({
					url: "../searchhospital/searchhospital"
				});
			},
			bindPickerChangeposition: function(e) {
			    console.log('picker发送选择改变，携带值为', e.target.value)
			    this.positionindex = e.target.value
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

				//uni.hideLoading();
			},
			getUrl(e, u) {
				console.log(uni.getStorageSync('userInfo'))
				//upload/upload
				//webview/webview
				
				if(uni.getStorageSync('userInfo').uid==undefined){
					this.login(e)
					return
				}
				
				if(this.$conFig.onUrl(e)){
					uni.navigateTo({
						url: e
					});
				}
			},
			getUrls(e, u) {
				uni.navigateTo({
					url: e
				});
			},
			modify(e) {
				if (e == 1) {
					this.load()

				} else {
					
					this.$conFig.alert_Load()
					
					if(this.userinfo.type=='employee'){
						this.userinfo.avatar=this.bs64
						this.userinfo.openid=uni.getStorageSync('userInfo').openid
						this.userinfo.unionid=uni.getStorageSync('userInfo').unionid
						this.userinfo.category = this.array[this.arrayIndex].title
						this.userinfo.categoryid = this.array[this.arrayIndex].id
						this.userinfo.categorynum = this.arrayIndex
						
						this.timeDepartment.category = this.array[this.arrayIndex].title
						this.timeDepartment.categoryid = this.array[this.arrayIndex].id
						this.timeDepartment.categorynum = this.arrayIndex
						
						uni.request({
							url: this.$conFig.inmediaInfo,
							header: {
								'content-type': "application/x-www-form-urlencoded",
							},
							method:"POST",
							data:{
								action:"opMember",
								headImg: this.bs64,
								type: uni.getStorageSync('userInfo').type,
								kId: this.array[this.arrayIndex].title,
								phone:uni.getStorageSync('userInfo').mobile,
								zhichen:uni.getStorageSync('userInfo').position,
								email: uni.getStorageSync('userInfo').mail,
								hospitail:uni.getStorageSync('userInfo').hospital,
								trueName:uni.getStorageSync('userInfo').name,
								openId:uni.getStorageSync('userInfo').openid,
								unionId:uni.getStorageSync('userInfo').unionid,
							},
							success: (res) => {
								if(res.data.result=='true'){
									uni.setStorage({
										key: 'userInfo',
										data: this.userinfo
									});
									uni.setStorage({
										key: 'timeDepartment',
										data: this.timeDepartment
									});
									uni.setStorage({
										key: 'Modifyhospital',
										data: ""
									});
									
									this.myHospital = this.NewHospital
									this.mysrc= this.bs64
								} else {
									this.$conFig.alert_Tips('系统异常请稍后再试！')
								}
								
								this.ismodify = false
								uni.hideLoading();
							}
						})
						return
					}

					var reg = new RegExp("^[a-z0-9]+([._\\-]*[a-z0-9])*@([a-z0-9]+[-a-z0-9]*[a-z0-9]+.){1,63}[a-z0-9]+$");
					if (this.myname == ""||this.myname==undefined) {
						this.$conFig.alert_Tips('请输入姓名！')
						return false;
					}
					
					

					this.userinfo.avatar=this.bs64
					this.userinfo.name = this.Newname
					this.userinfo.hospital = this.NewHospital
					this.userinfo.openid=uni.getStorageSync('userInfo').openid
					this.userinfo.unionid=uni.getStorageSync('userInfo').unionid
					
					this.userinfo.position=this.positionData[this.positionindex].title
					this.userinfo.positionid=this.positionData[this.positionindex].id
					this.userinfo.positionnum=this.positionindex
					
					this.userinfo.category = this.array[this.arrayIndex].title
					this.userinfo.categoryid = this.array[this.arrayIndex].id
					this.userinfo.categorynum = this.arrayIndex
					

					// this.timeDepartment.push(this.array[this.arrayindex].title)
					// this.timeDepartment.push(this.array[this.arrayindex].id)
					// this.timeDepartment.push(this.arrayindex)

					this.timeDepartment.category = this.array[this.arrayIndex].title
					this.timeDepartment.categoryid = this.array[this.arrayIndex].id
					this.timeDepartment.categorynum = this.arrayIndex
					
					uni.request({
						url: this.$conFig.inmediaInfo,
						header: {
							'content-type': "application/x-www-form-urlencoded",
						},
						method:"POST",
						data:{
							action:"opMember",
							headImg: this.bs64,
							type: uni.getStorageSync('userInfo').type,
							kId: this.array[this.arrayIndex].title,
							phone:this.mytel,
							zhichen:this.userinfo.position,
							email: uni.getStorageSync('userInfo').mail,
							hospitail:this.userinfo.hospital,
							trueName:this.userinfo.name,
							openId:uni.getStorageSync('userInfo').openid,
							unionId:uni.getStorageSync('userInfo').unionid,
						},
						success: (res) => {
							if(res.data.result=='true'){
								uni.setStorage({
									key: 'userInfo',
									data: this.userinfo
								});
								uni.setStorage({
									key: 'timeDepartment',
									data: this.timeDepartment
								});
								uni.setStorage({
									key: 'Modifyhospital',
									data: ""
								});
								
								this.myname = this.Newname
								this.myHospital = this.NewHospital
								this.mysrc=this.bs64
							} else {
								this.$conFig.alert_Tips('系统异常请稍后再试！')
							}
							
							this.ismodify = false
							uni.hideLoading();
						}
					})
				}

			},
			modifytel(e) {
				if (e == 1) {
					this.ismodifytel = true
				} else if (e == 2) {
					this.ismodifytel = false
				} else {
					if (this.Newtel == "") {
						this.$conFig.alert_Tips('请输入手机号！')
						return false;
					}
					if (!(/^1[3456789]\d{9}$/.test(this.Newtel))) {
						this.$conFig.alert_Tips('手机号不正确！')
						return false;
					}
					if (this.Newtel == this.mytel) {
						this.$conFig.alert_Tips('新手机号不能和老手机号重复！')
						return false;
					}
					if (this.code == "") {
						this.$conFig.alert_Tips('请输入验证码！')
						return false;
					}
					if (this.code != this.verify) {
						this.$conFig.alert_Tips('验证码输入有误！')
						return false;
					}

					if (Date.parse(new Date()) / 1000 > this.expired) {
						this.$conFig.alert_Tips('验证码已过期！')
						return false;
					}

					this.$conFig.alert_Load()
					uni.request({
						url: this.$conFig.inmediaInfo,
						header: {
							'content-type': "application/x-www-form-urlencoded",
						},
						method:"POST",
						data:{
							action:"updatemobile",
							new_phone: this.Newtel,
							old_phone: this.mytel,
							verify_code: this.code,
							unionId:uni.getStorageSync('userInfo').unionid,
						},
						success: (res) => {
							console.log(res)
							if(res.data.result=='true'){
								this.mytel=this.Newtel
								this.userinfo.mobile = this.Newtel
								uni.setStorage({
									key: 'userInfo',
									data: this.userinfo
								});
							} else {
								this.$conFig.alert_Tips('系统异常请稍后再试！')
							}
							
							uni.hideLoading();
							this.ismodifytel = false
						}
					})
					
					
					return
					this.$reqUest.post('linkmed', '/linkmed2020/updatemobile', {
						new_phone: this.Newtel,
						old_phone: this.mytel,
						verify_code: this.code
					}).then(data => {
						var [error, res] = data;
						if (res.data.code == 10000) {
							this.userinfo.mobile = this.Newtel
							uni.setStorage({
								key: 'userInfo',
								data: this.userinfo
							});
						} else {
							this.$conFig.alert_Tips('系统异常请稍后再试！')
						}
						uni.hideLoading();
						this.ismodifytel = false
					})
				}
			},
			obtain() {
				if (this.Newtel == "") {
					this.$conFig.alert_Tips('请输入手机号！')
					return false;
				}
				if (!(/^1[3456789]\d{9}$/.test(this.Newtel))) {
					this.$conFig.alert_Tips('手机号不正确！')
					return false;
				}
				if (this.Newtel == this.mytel) {
					this.$conFig.alert_Tips('新手机号不能和老手机号重复！')
					return false;
				}

				if (this.downTime != 60) {
					return
				}

				this.$conFig.alert_Load()
				
				this.$reqUest.post('linkmed', '/linkmed2020/sendcode', {
					phone: this.Newtel
				}).then(data => {
					var [error, res] = data;

					console.log(res)
					if (res.data.code == "100000") {
						this.verify = res.data.verify
						this.expired = res.data.expired
						this.downTime--
						var settime = setInterval(() => {
							this.downTime--
							if (this.downTime == 0) {
								this.downTime = 60
								clearInterval(settime);
							}
						}, 1000)
					} else {
						this.$conFig.alert_Tips('系统异常请稍后再试！')
					}
					uni.hideLoading();
				})
			},
			load() {

				this.$conFig.alert_Load()
				//加载科室
				this.$reqUest.get('linkmed', '/linkmed2020/categories').then(data => {
					var [error, res] = data;
					
					this.array=[]
					this.positionData=[]

					if (res.data.code == "100000") {
						var add = res.data.result
						Object.keys(add).forEach((key) => {
							this.array.push({
								title: add[key],
								id: key
							})
						})
						
						this.loadposition()
						
					} else {
						this.$conFig.alert_Tips('系统异常请稍后再试！')
					}
				})
			},
			loadposition(){
				
				this.$reqUest.get('linkmed', '/linkmed2020/positions').then(data => {
					var [error, res] = data;
					var add=res.data
					Object.keys(add).forEach((key)=>{
						this.positionData.push({
							title:add[key],
							id: key
						})
					})
					uni.hideLoading();
					console.log(uni.getStorageSync('userInfo').category)
					if(uni.getStorageSync('userInfo').category!=""){
						for(var i=0;i<this.array.length;i++){
							if(this.array[i].title==uni.getStorageSync('userInfo').category){
								this.arrayIndex=i
							}
						}
						for(var i=0;i<this.positionData.length;i++){
							if(this.positionData[i].title==uni.getStorageSync('userInfo').position){
								
								this.positionindex=i
							}
						}
					}else{
						this.arrayIndex=0
						this.positionindex=0
					}
					this.ismodify = true
				})
				
				
				uni.hideLoading();
				
				
			},
			gbtk(e){
				if(e==1){
					this.ismodify=false
				}else{
					this.ismodifytel=false
				}
				
			},
			uploadpade(e){
				this.iswym=e
			},
			chooseImage() {
				uni.chooseImage({
					count: 1,
					sizeType: ['original', 'compressed'],
					sourceType: ['album', 'camera'],
					success: res => {
						const tempFilePaths = res.tempFilePaths[0];
						
						uni.navigateTo({
							url: '../../components/cropper-default/cropper-default?src=' + tempFilePaths
						});
						//this.tui.href();
					}
				});
			},
			login(url){
				uni.reLaunch({
					url: "../login/login"
				});
			},
			signout(){
				
				
				uni.clearStorage();
				
				this.$conFig.alert_Load()
				setTimeout(function(){
					uni.reLaunch({
						url: "../index/index"
					});
				},2000)
			}
		}
	}
</script>

<style>
	.myk {
		border-radius: 5px;
	}

	.mykimg {
		width: 60px;
		height: 60px;
		border-radius: 60px;
		margin-right: 15px;
	}

	.myknr {
		width: calc(100% - 105px);
	}

	.yym {
		padding: 2px 10px;
		border-radius: 50px;
	}

	.tz {
		width: calc(100% - 26px);
	}

	.tz text {
		padding: 0px 10px;
	}

	.mynav {}

	.mynav>view {
		width: 25%;
	}

	.mynavimg {
		height: 32px;
	}

	.mylist {}

	.mylist>view {
		border-bottom: #DEDDDE solid 1px;
		margin-bottom: 10px;
		padding-bottom: 10px;
	}

	.mylist>view:last-child {
		border-bottom: #DEDDDE solid 0px;
		margin-bottom: 0px;
		padding-bottom: 0px;
	}

	.mylistname {
		width: calc(100% - 24px);
	}
	.pz{ width: 72px; height: 72px; border-radius: 100px; background:linear-gradient(180deg,rgba(234,234,234,1) 0%,rgba(154,154,154,1) 100%);
	border:3px solid rgba(255,255,255,1); }
</style>
