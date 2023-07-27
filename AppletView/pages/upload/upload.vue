<template>
	<view class="pa whtl bF5F6FA">
		<view class="cfff ovh pr" style=" height: 90px;">
			<image src="../../static/bg.jpg" class="w100 pa" mode="widthFix"></image>
		</view>
		<view class="pr body z3" style="margin-top: -70px;">
			<textarea @input="inputText" class="bfff shadow body" value="" placeholder-style="color: #BBC2CA;" placeholder="欢迎您向领医迈小程序提出您的意见与建议，清在这里输入您的内容" />
			<view class="h20"></view>
			<view class="body">
				<view class="upimg">
					
					<block v-for="(image,index) in imageList" :key="index">
						<view class="upimgli pr center bfff on">
							<view class="center whtl pa ovh">
								<image :src="image" mode="widthFix" :data-src="image" @tap="previewImage"></image>
							</view>
							<view class="pa center cfff f8 tb" @tap="removeImage(index)">
								<view class="iconfont icon-guanbi cfff" style="font-size: 8px; height: 10px;"></view>
							</view>
						</view>
					</block>
					
					<template v-if="imageList.length<9">
						<view class="upimgli pr center bfff" @tap="chooseImage">
							<view class="" style="color: #9b9a9d;">
								<view class="center">
									<view class="iconfont icon-tianjia" style="font-size: 25px; height: 30px; "></view>
								</view>
								<view class="f12 tec">上传照片</view>
							</view>
						</view>
					</template>
					
					
					
				</view>
			</view>
			<view class="clear"></view>
			<view class="h10"></view>
			<view class="bbs f16 cfff tec but" @click="butconfirm">
				发送
			</view>
			
		</view>
		<reTurn></reTurn>
	</view>
</template>

<script>
	import reTurn from '@/components/return/return.vue'
	import permision from "@/common/permission.js"
	var sourceType = [
		['camera'],
		['album'],
		['camera', 'album']
	]
	var sizeType = [
		['compressed'],
		['original'],
		['compressed', 'original']
	]
	
	
	export default {
		components: {
			reTurn
		},
		data() {
			return {
				title: '',
				imageList: [],
				sourceTypeIndex: 2,
				sourceType: ['拍照', '相册', '拍照或相册'],
				sizeTypeIndex: 2,
				sizeType: ['压缩', '原图', '压缩或原图'],
				countIndex: 8,
				count: [1, 2, 3, 4, 5, 6, 7, 8, 9],
				serverImg:[]
			}
		},
		
		onLoad(e) {
			var sign = this.$conFig.issign('false', 'pages/upload/upload',e.oid)
			if (sign) {
				
			}
		},
		
		onShow() {
			this.$conFig.onWeixin("领医迈","　",'/pages/index/index',"s.jpg")
		},
		onUnload() {
			this.imageList = [],
				this.sourceTypeIndex = 2,
				this.sourceType = ['拍照', '相册', '拍照或相册'],
				this.sizeTypeIndex = 2,
				this.sizeType = ['压缩', '原图', '压缩或原图'],
				this.countIndex = 8;
		},
		onShareAppMessage(res) {
			
			return {
				title: "领医迈",
				path: '/pages/index/index'
			}
		},
		methods: {
			sourceTypeChange: function(e) {
				this.sourceTypeIndex = parseInt(e.detail.value)
			},
			sizeTypeChange: function(e) {
				this.sizeTypeIndex = parseInt(e.detail.value)
			},
			countChange: function(e) {
				this.countIndex = e.detail.value;
			},
			chooseImage: async function() {
				// #ifdef APP-PLUS
				// TODO 选择相机或相册时 需要弹出actionsheet，目前无法获得是相机还是相册，在失败回调中处理
				if (this.sourceTypeIndex !== 2) {
					let status = await this.checkPermission();
					if (status !== 1) {
						return;
					}
				}
				// #endif
	
				if (this.imageList.length === 9) {
					let isContinue = await this.isFullImg();
					console.log("是否继续?", isContinue);
					if (!isContinue) {
						return;
					}
				}
				uni.chooseImage({
					sourceType: sourceType[this.sourceTypeIndex],
					sizeType: sizeType[this.sizeTypeIndex],
					count: this.imageList.length + this.count[this.countIndex] > 9 ? 9 - this.imageList.length : this.count[this.countIndex],
					success: (res) => {
						this.imageList = this.imageList.concat(res.tempFilePaths);
					},
					fail: (err) => {
						// #ifdef APP-PLUS
						if (err['code'] && err.code !== 0 && this.sourceTypeIndex === 2) {
							this.checkPermission(err.code);
						}
						// #endif
						// #ifdef MP
						uni.getSetting({
							success: (res) => {
								let authStatus = false;
								switch (this.sourceTypeIndex) {
									case 0:
										authStatus = res.authSetting['scope.camera'];
										break;
									case 1:
										authStatus = res.authSetting['scope.album'];
										break;
									case 2:
										authStatus = res.authSetting['scope.album'] && res.authSetting['scope.camera'];
										break;
									default:
										break;
								}
								if (!authStatus) {
									// uni.showModal({
									// 	title: '授权失败',
									// 	content: '领医迈需要从您的相机或相册获取图片，请在设置界面打开相关权限',
									// 	success: (res) => {
									// 		if (res.confirm) {
									// 			uni.openSetting()
									// 		}
									// 	}
									// })
								}
							}
						})
						// #endif
					}
				})
			},
			isFullImg: function() {
				return new Promise((res) => {
					uni.showModal({
						content: "已经有9张图片了,是否清空现有图片？",
						success: (e) => {
							if (e.confirm) {
								this.imageList = [];
								res(true);
							} else {
								res(false)
							}
						},
						fail: () => {
							res(false)
						}
					})
				})
			},
			previewImage: function(e) {
				var current = e.target.dataset.src
				uni.previewImage({
					current: current,
					urls: this.imageList
				})
			},
			async checkPermission(code) {
				let type = code ? code - 1 : this.sourceTypeIndex;
				let status = permision.isIOS ? await permision.requestIOS(sourceType[type][0]) :
					await permision.requestAndroid(type === 0 ? 'android.permission.CAMERA' :
						'android.permission.READ_EXTERNAL_STORAGE');
	
				if (status === null || status === 1) {
					status = 1;
				} else {
					uni.showModal({
						content: "没有开启权限",
						confirmText: "设置",
						success: function(res) {
							if (res.confirm) {
								permision.gotoAppSetting();
							}
						}
					})
				}
	
				return status;
			},
			removeImage(e){
				this.imageList.splice(e,1)
			},
			inputText(e){
				this.title=e.detail.value
				
			},
			upimg(e){
				var _this = this
				
				uni.uploadFile({
					url: _this.$conFig.linkmedApi + '/linkmed2020/feedback/upload', //仅为示例，非真实的接口地址
					filePath: _this.imageList[e],
					header:{
						'content-type':"application/x-www-form-urlencoded",
						'linkmed-token':'6CxkakZH',
						'phone': '',
						'openid': uni.getStorageSync('_wxuserInfo').openid,
						'unionid': uni.getStorageSync('_wxuserInfo').unionid
					},
					name: 'file',
					success: (uploadFileRes) => {
						console.log(uni.getStorageSync('_wxuserInfo').openid)
						console.log(_this.imageList[e])
						console.log(uploadFileRes)
						console.log("////")
						_this.serverImg.push(uploadFileRes.data)
						if (_this.serverImg.length == _this.imageList.length) {
							_this.updata()
						} else {
							
							var num = e + 1
							_this.upimg(num)
						}
					}
				});
			},
			butconfirm(e){
				if(this.title==""){
					uni.showToast({
						icon:"none",
					    title: '请输入意见和建议',
					    duration: 2000,
						mask:true
					});
					return
				}
				this.$conFig.alert_Load()
				
				if(this.imageList.length==0){
					this.updata()
				}else{
					this.upimg(0)
				}
				
				console.log(this.title)
				console.log(this.imageList)
			},
			updata(){
				this.$reqUest.post('linkmed', '/linkmed2020/feedback/request', {
					content: this.title,
					images:this.serverImg.toString()
				}).then(data => {
					var [error, res] = data;
					
					console.log(res)
					
					uni.hideLoading();
					if(res.data.code==10000){
						this.$conFig.alert_Tips('上传成功！')
						
						setTimeout(()=>{
							uni.navigateBack({
								delta: 1
							});
						},2500)
						
					}else{
						this.$conFig.alert_Tips('系统异常请稍后再试！')
					}
					
				})
			}
		}
	}
</script>

<style>
	textarea{ border-radius:5px ; line-height: 1.5; width: calc(100% - 34px); height: 130px;  }
	
	
	
	.upimgli{ width: 170rpx; height: 165rpx; border-radius: 5px; border: #9b9a9d dashed 1px; float: left; margin-right: 40rpx; margin-bottom: 40rpx; }
	.upimgli:nth-child(3n){ margin-right: 0px; }
	.upimgli.on{border: rgba(255,255,255,0) dashed 1px;}
	.upimgli .tb {
		top: -8px;
		right: -8px;
		width: 20px;
		height: 20px;
		background: linear-gradient(135deg, rgba(243, 130, 130, 1) 0%, rgba(227, 95, 75, 1) 100%);
		border-radius: 12px;
	}
	
	.but{ border-radius: 5px; padding: 8px 0px; }
	
	
	.cell-pd {
		padding: 22rpx 30rpx;
	}
	
	.list-pd {
		margin-top: 50rpx;
	}
</style>
