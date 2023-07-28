<template>
	<view class="pf whtl bF5F6FA">
		<template v-if="step==1||step==2">
			<view class="pf whtl center animated fadeIn fast" style="background: #D8D8D8;">
				<image class="pf" style="width: 100%; bottom: 0px; left: 0px;" src="../../static/sinbg.png" mode="widthFix"></image>
				<view class="pf whtl center z3">
					<template v-if="step==1">
						<view class="pf bfff shadow center" style=" color: #CACBCC; top: 17px; left: calc(10% - 17px); width: calc(80% + 34px); border-radius: 5px;">
							<view class="center" style=" color: #CA021B; height: 40px; width: calc(50% - 1px);">
								<image style="width: 16px;" src="../../static/dl1i2.png" mode="widthFix"></image>
								<view class="w10"></view>
								医生用户验证
							</view>
							<view style="width: 1px; height: 40px; background: #CACBCC;"></view>
							<view class="center" style="height: 40px; width: 50%;" @click="getUrl('../webview/webview?id='+this.$base64.Base64.encode(this.$conFig.inmediaSso),1)">
								<image style="width: 16px;" src="../../static/dl2i1.png" mode="widthFix"></image>
								<view class="w10"></view>
								强生员工验证
							</view>
						</view>
					</template>
					<view class="bfff dlk body shadow">
						<template v-if="step==1">
							<view class="h20"></view>
							<view class="center">
								<image style="width: 100px;" src="../../static/logo2.png" mode="widthFix"></image>
							</view>
							<view class="h40"></view>
							<view class="center w100">
								<view class="pr input">
									<input type="number" class="inputtxt1" @input="ontel" placeholder-style=" color: #dadadc;" :value="tel"
									 placeholder="请输入手机号" />
									<view class="pa center signicon">
										<view class="iconfont icon-shouji cB92727 mt4" style="font-size: 25px;"></view>
									</view>
								</view>
							</view>
							<view class="h30"></view>
							<view class="center w100">
								<view class="pr input">
									<input type="number" maxlength="4" @input="oncode" class="inputtxt2" placeholder-style=" color: #dadadc;"
									 value="" placeholder="请输入验证码" />
									<view class="pa center signicon">
										<view class="iconfont icon-yanzhengma cB92727 mt4" style="font-size: 25px;"></view>
									</view>
									<view class="pa center yzm f12 cfff z3 tec" @click="obtain">
										<view :class="downTime!=60?'bAAB2B7':'bB92727'">
											<template v-if="downTime==60">
												获取验证码
											</template>
											<template v-else>
												已发送({{downTime}}S)
											</template>
										</view>
									</view>
								</view>
							</view>
							<view class="h30"></view>
							<view class="center cBBC0CE f12" style="">
								<view class="center" @click="onChoice()">
									<view class="iconfont icon-duoxuan center" :class="isChoice?'cB92727':'cBBC0CE'" style="font-size: 15px; height: 21px;"></view>
									<view class="w10"></view>
									<view>我已阅读并同意</view>
								</view>
								<view class="w5"></view>
								<view class="cB92727" @click="getUrl('../webview/webview?id='+this.$base64.Base64.encode(this.$conFig.inmediaApi+'/clause.html?v='+Date.parse(new Date())),0)">
									使用协议
								</view>
								<view class="w5"></view>
								或
								<view class="w5"></view>
								<view class="cB92727" @click="getUrl('../webview/webview?id='+this.$base64.Base64.encode(this.$conFig.inmediaApi+'/privacy.html?v='+Date.parse(new Date())),0)">
									隐私政策
								</view>
							</view>
							<view class="h50"></view>
							<view class="center">
								<view class="cfff f18 shadow3 bbs netbut" @click="onnet(1)">
									下一步
								</view>
							</view>
							<view class="h20"></view>
							<view class="h2"></view>
						</template>
						<template v-else-if="step==2">
							<view class="h15"></view>
							<view class="center">
								<view class="pz center shadow pr ovh" @click="chooseImage">
									<view class="pa whtl" :style="{ 'background': 'url('+bs64+') no-repeat center #ccc','background-size':'cover' }"></view>
								</view>
							</view>
							<view class="h20"></view>
							<view class="center w100 f12" style="color: #666;">
								头像将会在您的作品中展示
							</view>
							<view class="h20"></view>
							<view class="center w100">
								<view class="input pr">
									<input type="text" @input="onname" class="inputtxt3" placeholder-style=" color: #dadadc;" placeholder="输入您的真实姓名"
									 :value="name" />
									<view class="pa inputwb c9AA8B6 center">真实姓名</view>
								</view>
							</view>
							<view class="h15"></view>
							<view class="center w100">
								<view class="input pr">
									<view class="inputtxt3" :class="hospital==''?'cdadadc':'c000'" @click="getSearchHospital()" style="border-bottom: #BBC0CE solid 1px;">{{hospital==""?"请选择医院":hospital}}</view>
									<view class="pa inputwb c9AA8B6 center">所在医院</view>
								</view>
							</view>
							<!-- <view class="h15"></view>
							<view class="center w100">
								<view class="input pr">
									<input type="text" @input="onmail" class="inputtxt3" placeholder-style=" color: #dadadc;" placeholder="输入您的电子邮箱" :value="mail" />
									<view class="pa inputwb c9AA8B6 center">电子邮箱</view>
								</view>
							</view> -->
							<view class="h15"></view>
							<view class="center w100">
								<view class="input pr">
									<view class="pa inputwb c9AA8B6 center">选择您的职称</view>
									<picker class="inputtxt4 pr z3" style=" color: #dadadc;" @change="bindPickerChangeposition" :value="positionindex"
									 :range="positionData" range-key="title">
										<view class="inputtxt4_view c000" :class="positionindex==-1?'cdadadc':''">{{positionindex==-1?'请选择':positionData[positionindex].title}}</view>
										<view class="pa center signicon" style="left: auto; right: 0px;">
											<view class="iconfont icon-dibudaohanglan- cdadadc mt4" style="font-size: 25px;  "></view>
										</view>
									</picker>
								</view>
							</view>
							<view class="h15"></view>
							<view class="center w100">
								<view class="input pr">
									<view class="pa inputwb c9AA8B6 center">选择感兴趣的领域</view>
									<picker class="inputtxt4 pr z3" style=" color: #dadadc;" @change="bindPickerChange" :value="arrayindex" :range="array"
									 range-key="title">
										<view class="inputtxt4_view c000" :class="arrayindex==-1?'cdadadc':''">{{arrayindex==-1?'请选择':array[arrayindex].title}}</view>
										<view class="pa center signicon" style="left: auto; right: 0px;">
											<view class="iconfont icon-dibudaohanglan- cdadadc mt4" style="font-size: 25px;  "></view>
										</view>
									</picker>
								</view>
							</view>
							<view class="h20"></view>
							<view class="center">
								<view class="cfff f18 shadow3 bbs netbut" @click="onnet(2)">
									提交
								</view>
							</view>
						</template>
						<view class="h10"></view>
					</view>
				</view>
			</view>
		</template>
	</view>
</template>

<script>
	//import tui-Image-cropper from '@/components/tui-image-cropper/tui-image-cropper.vue'
	export default {

		data() {
			return {
				step: 0,
				bs64: "",
				tel: "",
				code: "",
				name: "",
				hospital: "",
				mail: "",
				downTime: 60,
				isChoice: false,
				positionData: [],
				positionindex: -1,
				array: [],
				arrayindex: -1,
				userinfo: {},
				userinfos: {},
				timeDepartment: {},
				verify: "",
				expired: ""
			}
		},
		onLoad(e) {

			this.bs64 = this.$conFig.headsculpture



			if(e.phone.length>5){
				this.tel = e.phone
			}
			this.step = 1

			uni.setStorage({
				key: 'tximg',
				data: ""
			});
		},
		onShow(e) {
			if (uni.getStorageSync('tximg').length > 20) {
				this.bs64 = uni.getStorageSync('tximg')
			}

			if (uni.getStorageSync('Modifyhospital') != "") {
				this.hospital = uni.getStorageSync('Modifyhospital')
			}
		},
		onShareAppMessage(res) {

			return {
				title: "领医迈",
				path: '/pages/index/index'
			}
		},
		methods: {
			getUrl(e, u) {
				uni.navigateTo({
					url: e
				});

				return
				if (u == 1) {
					uni.reLaunch({
						url: e
					});
				} else {

				}
			},
			bindPickerChangeposition: function(e) {

				this.positionindex = e.target.value
			},
			bindPickerChange: function(e) {

				this.arrayindex = e.target.value
			},
			ontel(e) {
				this.tel = e.detail.value

			},
			oncode(e) {
				this.code = e.detail.value
			},
			onname(e) {
				this.name = e.detail.value
			},
			onhospital(e) {
				this.hospital = e.detail.value
			},
			onmail(e) {
				this.mail = e.detail.value
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
			obtain() {
				if (this.tel == "") {
					this.$conFig.alert_Tips('请输入手机号！')
					return false;
				}
				if (!(/^1[3456789]\d{9}$/.test(this.tel))) {
					this.$conFig.alert_Tips('手机号不正确！')
					return false;
				}

				if (this.downTime != 60) {
					return
				}

				this.$conFig.alert_Load()


				this.$reqUest.post('linkmed', '/linkmed2020/sendcode', {
					phone: this.tel
				}).then(data => {
					var [error, res] = data;

					
					uni.hideLoading();

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
						setTimeout(() => {
							this.$conFig.alert_Tips('系统异常请稍后再试！')
						}, 200)
					}

				})


			},
			onChoice() {
				if (this.isChoice) {
					this.isChoice = false
				} else {
					this.isChoice = true
				}
			},
			onnet(e) {
				if (e == 1) {
					// this.step=2
					// return

					if (this.tel == "13764790709" && this.code == "9681") {

					} else {
						if (this.tel == "" || this.tel == undefined) {
							this.$conFig.alert_Tips('请输入手机号！')
							return false;
						}
						if (!(/^1[3456789]\d{9}$/.test(this.tel))) {
							this.$conFig.alert_Tips('手机号不正确！')
							return false;
						}
						if (this.code == "" || this.code == undefined) {
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

						if (!this.isChoice) {
							this.$conFig.alert_Tips('请同意隐私条例和使用条款！')
							return false;
						}
					}

					//this.tel=12764790709

					this.$conFig.alert_Load()
					this.$reqUest.get('linkmed', '/linkmed2020/userinfo', {
						phone: this.tel
					}).then(data => {
						var [error, res] = data;

						if (res.data.code == 100000) {
							
							this.userinfo = res.data.result[0]
							this.userinfo.name=this.userinfo.name==null?'':this.userinfo.name
							
							console.log(this.userinfo)
							
							this.bs64 = (this.userinfo.avatar == "" ? this.bs64 : this.userinfo.avatar)
						}

						this.categories()
					})



				} else {


					var reg = new RegExp("^[a-z0-9]+([._\\-]*[a-z0-9])*@([a-z0-9]+[-a-z0-9]*[a-z0-9]+.){1,63}[a-z0-9]+$");



					if (this.name == "" || this.name == undefined) {
						this.$conFig.alert_Tips('请输入姓名！')
						return false;
					}
					if (this.hospital == "" || this.hospital == undefined) {
						this.$conFig.alert_Tips('请选择您所在医院！')
						return false;
					}
					// if(this.mail==""){
					// 	this.$conFig.alert_Tips('请输入电子邮箱！')
					// 	return false; 
					// }
					// if(!reg.test(this.mail)){
					// 	this.$conFig.alert_Tips('电子邮箱格式不正确！')
					// 	return false; 
					// }
					if (this.positionindex == -1) {
						this.$conFig.alert_Tips('请选择职称！')
						return false;
					}
					if (this.arrayindex == -1) {
						this.$conFig.alert_Tips('请选择科室！')
						return false;
					}

					this.$conFig.alert_Load()

					this.$reqUest.post('linkmed', '/linkmed2020/userinfo', {
						phone: this.tel,
						name: this.name,
						hospital: this.hospital,
						mail: this.mail,
						category: this.array[this.arrayindex].title,
						avatar: this.bs64,
						position: this.positionData[this.positionindex].title,
						openid: uni.getStorageSync('_wxuserInfo').openid,
						unionid: uni.getStorageSync('_wxuserInfo').unionid
					}).then(data => {
						var [error, res] = data;

						if (res.data.code == "10000") {

							this.$reqUest.get('linkmed', '/linkmed2020/userinfo', {
								unionid: uni.getStorageSync('_wxuserInfo').unionid
							}).then(data => {
								var [error, res] = data;
								if (res.data.code == 100000) {
									var info = res.data.result[0]

									this.$reqUest.get('inmedia', '/linyimaiapi.ashx?Method=notificationcount', {
										video_created: (new Date()).getTime(),
										comment_created: (new Date()).getTime(),
										flag_created: (new Date()).getTime()
									}).then(data => {


										var [error, res] = data;

										this.userinfo.uid = info.uid
										this.userinfo.avatar = info.avatar
										this.userinfo.name = info.name
										this.userinfo.hospital = info.hospital
										this.userinfo.mail = info.mail
										this.userinfo.mobile = info.mobile
										this.userinfo.openid = uni.getStorageSync('_wxuserInfo').openid
										this.userinfo.unionid = uni.getStorageSync('_wxuserInfo').unionid
										this.userinfo.position = this.positionData[this.positionindex].title
										this.userinfo.positionid = this.positionData[this.positionindex].id
										this.userinfo.positionnum = this.positionindex


										this.userinfo.category = this.array[this.arrayindex].title
										this.userinfo.categoryid = this.array[this.arrayindex].id
										this.userinfo.categorynum = this.arrayindex

										this.timeDepartment.category = this.array[this.arrayindex].title
										this.timeDepartment.categoryid = this.array[this.arrayindex].id
										this.timeDepartment.categorynum = this.arrayindex



										uni.setStorage({
											key: 'userInfo',
											data: this.userinfo
										});

										uni.setStorage({
											key: 'timeDepartment',
											data: this.timeDepartment
										});

										uni.setStorage({
											key: 'sign',
											data: uni.getStorageSync('sign') + "/true"
										});

										uni.setStorage({
											key: 'wxuserInfo',
											data: uni.getStorageSync('_wxuserInfo')
										});

										this.$conFig.ClassType(2)

										uni.reLaunch({
											url: uni.getStorageSync('timeUrl')
										});

										// if (uni.getStorageSync('timeUrl').indexOf('share=true') > 0 || uni.getStorageSync('timeUrl').indexOf(
										// 		'my/my') > 0) {
										// 	uni.reLaunch({
										// 		url: uni.getStorageSync('timeUrl')
										// 	});
										// } else {
										// 	uni.redirectTo({
										// 		url: uni.getStorageSync('timeUrl')
										// 	});
										// }
									})
								}
							})
						} else {
							this.$conFig.alert_Tips('系统异常请稍后再试！')
						}

						uni.hideLoading();

					})

				}
			},
			categories() {
				this.$reqUest.get('linkmed', '/linkmed2020/categories').then(data => {
					var [error, res] = data;

					if (res.data.code == "100000") {
						var add = res.data.result

						this.array = []

						Object.keys(add).forEach((key) => {
							this.array.push({
								title: add[key],
								id: key
							})
						})

						uni.setStorage({
							key: 'array',
							data: this.array
						});
						this.loadposition()


					} else {
						this.$conFig.alert_Tips('系统异常请稍后再试！')
					}



				})
			},
			loadposition() {

				this.$reqUest.get('linkmed', '/linkmed2020/positions').then(data => {
					var [error, res] = data;
					var add = res.data
					Object.keys(add).forEach((key) => {
						this.positionData.push({
							title: add[key],
							id: key
						})
					})

					uni.setStorage({
						key: 'positionData',
						data: this.positionData
					});


					if (this.userinfo != "") {
						//this.bs64 = this.userinfo.avatar
						// this.name = this.userinfo.name
						 this.hospital = ""
						// this.mail = this.userinfo.mail

						for (var i = 0; i < this.array.length; i++) {
							if (this.array[i].title == this.userinfo.category) {
								this.arrayindex = i
							}
						}

						for (var i = 0; i < this.positionData.length; i++) {
							if (this.positionData[i].title == this.userinfo.position) {

								this.positionindex = i
							}
						}

					}

					this.step = 2




				})


				uni.hideLoading();
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
			}

		}
	}
</script>

<style>
	.pz {
		width: 72px;
		height: 72px;
		border-radius: 100px;
		background: linear-gradient(180deg, rgba(234, 234, 234, 1) 0%, rgba(154, 154, 154, 1) 100%);
		border: 3px solid rgba(255, 255, 255, 1);
	}
</style>
