<template>
	<view class="pf whtl bF5F6FA">
		<template v-if="issign">
			<view class="pf whtl animated fadeIn faster">
				<view class="body pr z3">
					<view class="h20"></view>
					
					<view class="f18">
						选择兴趣标签
					</view>
					<view class="h20"></view>
					
					<view class="bfff body xqarray ovh" style="border-radius: 10px;padding: 17px 17px 0px 17px;">
						<block v-for="(data,index) in array">
							<view class="fl" :class="xqarrayindex==index?'on':''" @click="onxqarray(index)">{{data.title}}</view>
						</block>
					</view>
					<view class="h20"></view>
					<view class="center">
						<button style=" width: 100%; border: none; background:rgba(185,39,39,1);" @click="getqr">
							<view class="cfff f17">确认</view>
						</button>
					</view>
				</view>
			</view>
		</template>
	</view>
</template>

<script>
	export default {
		data() {
			return {
				xqarrayindex: -1,
				array:[],
				issign:false
			}
		},
		onLoad() {
			this.$conFig.alert_Load()
			//加载科室
			this.$reqUest.get('linkmed', '/linkmed2020/categories').then(data => {
				var [error, res] = data;
			
				if (res.data.code == "100000") {
					this.array = []
					var add = res.data.result
					Object.keys(add).forEach((key) => {
						this.array.push({
							title: add[key],
							id: key
						})
					})
					
					this.issign=true
					uni.hideLoading()
					
				} else {
					this.$conFig.alert_Tips('系统异常请稍后再试！')
				}
			})
		},
		
		onShow() {
			this.$conFig.onWeixin("领医迈","　",'/pages/index/index',"s.jpg")
		},
		methods: {
			onxqarray(index) {
				this.xqarrayindex = index
			},
			onxqarray(index) {
				this.xqarrayindex = index
			},
			getqr() {
				var _this=this
				uni.getSystemInfo({
				    success: function (res) {
						if(res.windowHeight>700){
							_this.jx=1
						}
				    }
				});
				
				if(this.xqarrayindex==-1){
					this.$conFig.alert_Tips('请先选择兴趣标签')
					return
				}
				
				var userinfos = {
					category: this.array[this.xqarrayindex].title,
					categoryid: this.array[this.xqarrayindex].id,
					categorynum: this.xqarrayindex
				}
				
				uni.setStorage({
					key: 'userInfo',
					data: userinfos
				});
				
				var timeDepartment = {
					category: this.array[this.xqarrayindex].title
				}
				uni.setStorage({
					key: 'timeDepartment',
					data: timeDepartment
				});
				
				this.usecat = uni.getStorageSync('timeDepartment').category
			
				uni.reLaunch({
					url: "/"+uni.getStorageSync('timeUrl')
				});
				console.log(uni.getStorageSync('timeUrl'))
			
			},
		}
	}
</script>

<style>
.xqarray {}
	.xqarray>view {
		padding: 8px 15px;
		background: rgba(0, 0, 0, 0.1);
		border-radius: 5px;
		margin-right: 15px;
		margin-bottom: 20px;
	}

	.xqarray>view.on {
		background: rgba(202, 2, 27, 1);
		color: #fff;
	}
</style>
