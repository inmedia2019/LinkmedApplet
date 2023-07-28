<template>
	<view class="h100 pf w100 bF9FBFE">
		<template v-if="issign">
			<view class="pf whtl animated fadeIn fast">
				<view class="dettop">
					<view class="center">
						<video @timeupdate="timeupdate" class="video" :src="videosrc" :poster="videoimg"></video>
					</view>
					<view class="bodys cB92727 f16">
						{{expert.title}}
					</view>
					<view class="center cfff f12 pr ovh">
						<image src="../../static/bg.jpg" class="w100 pa" mode="widthFix"></image>
						<view class="bodys pr z3 w100 center">
							<view class="ovh derbq">
								<view class="fl">#{{expert.category}}</view>
								<view class="fl w20 h5"></view>
								<view class="fl iconfont icon-bofang1 center" style="height: 18px;"></view>
								<view class="fl w5 h5"></view>
								<view class="fl">{{expert.play}}播放</view>
							</view>
							<view class="center">
								<view class="iconfont center" :class="expert.has_flag==1?'icon-favorites cfff':'icon-favorites-o'" @click="onCollection"
								 style="height: 28px; font-size: 22px;"></view>
							</view>

							<view class="w15 h5"></view>
							<view class="w5 h5"></view>
							<view class="center pr">
								<view class="iconfont icon-share center cfff" style="height: 28px; font-size: 22px;" @click="shareImg"></view>
							</view>
						</view>
					</view>
					<view class="detnav center">
						<view class="center dh1" :class="tabon==1?'on':''" @click="tab(1)">
							<view class="iconfont icon-icon_gerenxinxi center" style="font-size: 21px; height: 15px;"></view>
							<view class="w5"></view>
							专家信息
						</view>
						<!-- <view class="center dh2" :class="tabon==2?'on':''" @click="tab(2)">
					<view class="iconfont icon-zhibo center" style="font-size: 21px; height: 14px;"></view>
					<view class="w5"></view>
					直播互动
				</view> -->
						<view class="center dh3" :class="tabon==3?'on':''" @click="tab(3)">
							<view class="iconfont icon-chat-copy center" style="font-size: 21px; height: 16px;"></view>
							<view class="w5"></view>
							评论留言
						</view>
					</view>
				</view>
				<view class="bfff" v-bind:style="{height: vH+'px'}">
					<template v-if="tabon==1">
						<scroll-view class="h100" scroll-y="true">
							<!-- <view class="detgr ovh pr">
								<image src="../../static/betgrbg.jpg" class="w100 pa" mode="widthFix"></image>
								<view class="ovh pr z3 detgrview">
									<view class="fl detgrimg" v-bind:style="{ 'background': 'url('+expert.headImg+') no-repeat center','background-size':'cover' }">

									</view>
									<view class="fl detgrxx">
										<view class="f16">{{expert.expert}}</view>
										<view class="f14 c999">{{expert.expert_intro}}</view>
									</view>
								</view>
							</view>
							 -->
							<template v-if="expert.introduce.length!=0">
								<view class="h5"></view>
								<view class="body c4A4A4A">
									<rich-text :nodes="expert.introduce"></rich-text>
								</view>
							</template>
							<template v-if="hotVideoData.length!=0">
								<view class="h10"></view>

								<view class="h5 bECF3FC op7"></view>
								<view class="body">
									<view class="h5"></view>
									<view class="ovh">
										<view class="fl center f18">
											<view class="yz"></view>
											<view class="w10 h5"></view>
											<view class="fb">为你推荐</view>
										</view>
										<view class="more f12 c9AA8B6 fr" @click="getUrl('../videolist/videolist',1)">
											查看更多>>
										</view>
									</view>
									<view class="h18"></view>
									<view class="list">
										<view class="center" v-for="(newlist,index) in hotVideoData" :key="index" @click="getUrl('../details/details?id='+newlist.id,1)">
											<view class="listimg pr shadow" v-bind:style="{ 'background': 'url('+newlist.thumbnail.src+') no-repeat center','background-size':'cover' }">
												<view class="pa whtl center">
													<text class="iconfont icon-bofang cfff" style="font-size: 26px; height: 32px;"></text>
													<text class="pa listimgtime f8 cfff">{{newlist.time}}</text>
												</view>
												<!-- <image class="imgnew pa" style="width: 24px;" src="../../static/new.png" mode="widthFix"></image> -->
											</view>
											<view class="listwb">
												<view class="listwbtitle ovh">
													{{newlist.title}}
												</view>
												<view class="ovh c949597 f10">
													<view class="fl f12 c999">{{newlist.expert}}</view>
													<view class="fr center">
														{{newlist.Thumbup}}赞 · {{newlist.comment_count}}评论
													</view>
												</view>
											</view>
										</view>
									</view>
								</view>
							</template>
						</scroll-view>
					</template>
					<template v-if="tabon==2">
						<scroll-view class="h100" scroll-y="true">
							<view class="body">
								<view class="zblist">
									<view class="ovh">
										<view class="fl zblistimg" style="background: url(../../static/banne.png) no-repeat center; background-size:cover;">

										</view>
										<view class="fr zblistnr">
											<view class="ovh">
												<view class="fl">程凯</view>
												<view class="fl w10 h5"></view>
												<view class="fl f12 c999">1分钟前</view>
											</view>
											<view class="h3"></view>
											<view class="c999">
												妈呀，我佩服的五体投地，太厉害了吧
											</view>
										</view>
									</view>
									<view class="ovh">
										<view class="fl zblistimg" style="background: url(../../static/banne.png) no-repeat center; background-size:cover;">

										</view>
										<view class="fr zblistnr">
											<view class="ovh">
												<view class="fl zblistnrname">程凯</view>
												<view class="fl w10 h5"></view>
												<view class="fl f12 c999">1分钟前</view>
											</view>
											<view class="h3"></view>
											<view class="c999 zblistnrpl fl">
												妈呀，我佩服的五体投地，太厉害了吧
											</view>
											<view class="clear"></view>
										</view>
									</view>
									<view class="ovh">
										<view class="center fl f12 c999">
											<view>19:37:52</view>
											<view class="w10 h5"></view>
											<view class="jrzbj">
												Randall Torres 进入直播间
											</view>
										</view>
									</view>
									<view class="ovh on">
										<view class="fl zblistimg" style="background: url(../../static/banne.png) no-repeat center; background-size:cover;">

										</view>
										<view class="fr zblistnr">
											<view class="ovh">
												<view class="fl zblistnrname">程凯</view>
												<view class="fl w10 h5"></view>
												<view class="fl f12 c999">1分钟前</view>
											</view>
											<view class="h3"></view>
											<view class="c999 zblistnrpl fl">
												妈呀，我佩服的五体投地，太厉害了吧
											</view>
											<view class="clear"></view>
										</view>
									</view>
								</view>
							</view>
						</scroll-view>
					</template>
					<template v-if="tabon==3">
						<scroll-view class="h100" scroll-y="true">
							<view class="body">
								<view class="lylist">
									<block v-for="(comment,index) in commentIofo" :key="index">
										<view class="ovh">
											<view class="fl zblistimg" v-bind:style="{ 'background': 'url('+comment.avatar+') no-repeat center','background-size':'cover' }">

											</view>
											<view class="fr zblistnr">
												<view class="ovh center">
													<view class="lylistnrname">
														<view class="cB92727">{{comment.name}}</view>
														<view class="f12 c999">{{comment.updated}}</view>
													</view>
													<view class="lylistnrdz center f12 c999" :class="comment.state==1?'cB92727':''" @click="dianzan(comment.cid,index,comment.state)">
														<view class="iconfont icon-dianzan1 center" style="font-size: 18px; height: 16px;"></view>
														<view class="w5 h5"></view>
														{{comment.count}}
													</view>
												</view>
												<view class="h5"></view>
												<view class="c4A4A4A" @click="oncomReply(comment.cid,index)">
													{{comment.content}}
												</view>
												<view class="h15"></view>
												<view class="ovh">
													<template v-if="comment.children.length!=0">
														<template v-if="!comment.open">
															<view @click="onopen(index)" class="center c999 fl">共{{comment.children.length}}回复<view class="w10 h5"></view>
																<view class="triangle-right"></view>
															</view>
														</template>
														<template v-else="comment.open">
															<view @click="onopen(index)" class="center c999 fl">收起<view class="w10 h5"></view>
																<view class="triangle-down" style="border-top: 8px solid #999999;"></view>
															</view>
														</template>
													</template>
												</view>
												<view class="ovh">
													<template v-if="comment.open">
														<view class="h10"></view>
														<block v-for="(comments,indexs) in comment.children" :key="indexs">
															<view class="ovh" style="margin-bottom: 5px;">
																<text class="cB92727" style="margin-right: 5px;">{{comments.name}}</text>
																<text style="color: #AAB2B7;margin-right: 5px;">回复:{{comment.name}}</text>
																<text style="color: #4A4A4A;margin-right: 5px;">{{comments.content}}</text>
															</view>
														</block>
													</template>
												</view>
											</view>
										</view>
										<view class="h15"></view>
										<view class="lylistjg"></view>
										<view class="h15"></view>
									</block>


								</view>
							</view>
						</scroll-view>
					</template>
					<template v-if="tabon==2||tabon==3">
						<view class="pf lyk c9A9EAA ovh bfff">
							<view class="lykinput fl" @click="onpltxt">来说点什么吧…</view>
							<view class="center lyksj fr" @click="onuppl">
								<view class="iconfont icon-liuyan center" style="font-size: 24px; line-height: 16px;"></view>
								<view class="w5"></view>
								<view>{{expert.comment_count}}</view>
							</view>
							<view class="w20 h5 fr"></view>
							<view class="center lyksj fr" :class="expert.has_thumbup=='1'?'cD0021B':''" @click="onupdz">
								<view class="iconfont icon-dianzan center" style="font-size: 16px; line-height: 16px;"></view>
								<view class="w5"></view>
								<view>{{expert.thumbup}}</view>
							</view>
						</view>
					</template>
				</view>
				<template v-if="frame">
					<view class="frame pa whtl z3">
						<view class="pa whtl" style="background: rgba(0,0,0,0.5);" @click="onpltxts"></view>
						<view class="pa shuru body bfff z3" fixed="true">
							<view>
								<textarea type="text" @confirm="Send" style="width: calc(100% - 20px);" focus @input="oninput"
							 class="lykinput" cursor-spacing="25" confirm-type="send" />
							</view>
							<view style="height: 10px;"></view>
							<view class="ovh">
								<button class="fr" style="font-size: 14px; width: 60px; height: 34px; padding: 0px;" @click="Send" type="primary">发送</button>
							</view>
						</view>

					</view>
				</template>
			</view>
			<template v-if="!frame">
			<reTurn></reTurn>
			</template>
		</template>
		<template v-if="share">
			<view class="tk pf whtl animated fadeIn fast">
				<view class="sharenr pf">
					<view class="">
						<view class="h30"></view>
						<view class="center">
							你想直接分享给好友或是分享朋友圈？
						</view>
						<view class="h30"></view>
						<view class="center">
							<view class="pr" style="color: #3a3a3a;">
								<view class="center fxbut">
									<image style="width: 29px;" src="../../static/fx1.png" mode="widthFix"></image>
								</view>
								<view>发送给好友</view>
								<button class="pa z3 whtl" open-type="share" style=" top: 0px; left: 0px; background: none; border: none; opacity: 0;"></button>
							</view>
							<view style="width: 60px;"></view>
							<view class="pr" style="color: #3a3a3a;" @click="shareImg()">
								<view class="center fxbut">
									<image style="width: 29px;" src="../../static/fx2.png" mode="widthFix"></image>
								</view>
								<view>分享朋友圈</view>
							</view>
						</view>
						<view class="h30"></view>
					</view>
					<view class="center shareqx f16" @click="onshare">取 消</view>
				</view>
			</view>
		</template>


		
		

		<template v-if="qrShow">
			<view class="pf tk center whtl">
				<view class="">
					
					<view class="" style=" position: fixed; top: -10000px; left: -10000px; width: 1px; height: 1px; overflow: hidden; opacity: 0;">
						<canvas canvas-id='testCanvas' class='testCanvas' :style="{'height':cH+'px','opacity':'0'}"></canvas>
					</view>
					<view class="center ovh" style="border-radius: 7px;">
						<image :src="fxtsrc" mode="widthFix"></image>
					</view>
					<view class="h20"></view>
					<template v-if="qrShowan">
						<view class="center animated fadeIn fast">
							<!-- <button style=" width: 45%; border: none; background:rgba(185,39,39,1);" @click="save">
								<view class="cfff f17">保存到相册</view>
							</button>
							<view class="" style="width: 10%;"></view> -->
							<button style=" width: 45%; border: none; background:rgb(150, 150, 150);" @click="quxiao">
								<view class="cfff f17">关闭</view>
							</button>
						</view>
					</template>
				</view>
			</view>
		</template>
		
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
				fxtsrc: "",
				qrShow:false,
				qrShowan:false,
				cH: 500,
				issign: false,
				jpH: "0px",
				share: false,
				issearch:false,
				id: "",
				expert: {},
				tabon: 1,
				videosrc: "",
				vH: 0,
				nodes: "",
				hotVideoData: [],
				commentIofo: [],
				timedate: 0,
				AreaBlur: "",
				frame: false,
				iszan: false,
				comReply: "",
				comIndex: ""
			};
		},
		onLoad(e) {
			
			var sign = this.$conFig.issign(e.share, 'pages/details/details?share=true&id=' + e.id,e.oid)
			if (sign) {
				
				if (e.search == 'true') {
					this.issearch=true
				}
				
				this.id = e.id
				this.$conFig.ClassType(e.type)
				console.log(this.id)
				
				//this.id = 79708
				this.$conFig.ClassType(2)
				this.load()
				
				
				
			}


		},
		onUnload(){
			this.userlog(0)
		},
		onShow() {
			this.$conFig.onWeixin("领医迈","　",'/pages/details/details?share=true&id=' + this.id,"s.jpg")
		},
		onShareAppMessage(res) {
			if (res.from === 'button') { // 来自页面内分享按钮
				this.userlog(6)
			}
			return {
				title: this.expert.title,
				path: '/pages/details/details?share=true&id=' + this.id
			}
		},
		methods: {
			oninput(e) {
				this.AreaBlur = e.detail.value
			},
			timeupdate(res) {
				
				this.timedate = res.detail.currentTime
			},
			upH() {
				var that = this
				uni.getSystemInfo({
					success: function(res) {
						var wH = res.windowHeight
						uni.createSelectorQuery().select(".dettop").boundingClientRect(data => {
							if (that.tabon == 1) {
								that.vH = wH - data.height
							} else {
								that.vH = wH - data.height - 85
							}
						}).exec();
					}
				});
			},
			tab(e) {
				this.tabon = e
				this.upH()
			},
			load() {

				this.$conFig.alert_Load()
				this.$reqUest.get('inmedia', '/linyimaiapi.ashx?Method=videoinfo', {
					nid: this.id
				}).then(data => {
					var [error, res] = data;
					this.expert = this.$conFig.newList(JSON.parse(res.data.message))[0]
					
					console.log(this.expert)
					
					this.comment()
				})
			},
			formatTime(number, format) {

				var formateArr = ['Y', 'M', 'D', 'h', 'm', 's'];
				var returnArr = [];

				var date = new Date(number * 1000);
				returnArr.push(date.getFullYear());
				returnArr.push(this.formatNumber(date.getMonth() + 1));
				returnArr.push(this.formatNumber(date.getDate()));

				returnArr.push(this.formatNumber(date.getHours()));
				returnArr.push(this.formatNumber(date.getMinutes()));
				returnArr.push(this.formatNumber(date.getSeconds()));

				for (var i in returnArr) {
					format = format.replace(formateArr[i], returnArr[i]);
				}
				return format;
			},
			formatNumber(n) {
				n = n.toString()
				return n[1] ? n : '0' + n
			},
			getTimer(stringTime) {
				var minute = 1000 * 60;
				var hour = minute * 60;
				var day = hour * 24;
				var week = day * 7;
				var month = day * 30;

				var time1 = new Date().getTime(); //当前的时间戳
				var time2 = Date.parse(new Date(stringTime*1000)); //指定时间的时间戳
				var time = time1 - time2;

				var result = null;
				if (time < 0) {

				} else if (time / month >= 1) {
					result = "发布于" + parseInt(time / month) + "月前！";
				} else if (time / week >= 1) {
					result = "发布于" + parseInt(time / week) + "周前！";
				} else if (time / day >= 1) {
					result = "发布于" + parseInt(time / day) + "天前！";
				} else if (time / hour >= 1) {
					result = "发布于" + parseInt(time / hour) + "小时前！";
				} else if (time / minute >= 1) {
					result = "发布于" + parseInt(time / minute) + "分钟前！";
				} else {
					result = "刚刚发布！";
				}
				return result
			},
			comment() {
				this.$reqUest.get('inmedia', '/linyimaiapi.ashx?Method=comment', {
					"nid": this.id
				}).then(data => {
					var [error, res] = data;
					
					if (res.data.result == "true") {
						var list = JSON.parse(res.data.message)
						for (var i = 0; i < list.length; i++) {
							
							list[i].updated = this.getTimer(list[i].updated)
							
							
							list[i].open = false

							var commentIofo = []
							Object.keys(list[i].children).forEach((key) => {
								commentIofo.push(list[i].children[key])
							})
							list[i].children = []
							for (var j = 0; j < commentIofo.length; j++) {

								list[i].children[j] = commentIofo[j]
							}
						}
						this.commentIofo = list
					}
					this.bokecc()
				})
			},
			bokecc() {
				var _this = this
				
				this.$reqUest.get('inmedia', '/linyimaiapi.ashx?Method=videofile', {
					vid: _this.expert.video,
					siteid: "37E10D032BB342FB",
					hlssupport: 1,
					useragent: "iPhone"
				}).then(data => {
					
					var data=data[1].data
					var data = data.substring(data.indexOf('(') + 1, data.length - 1);
					
					let dataObj = JSON.parse(data);
					_this.videosrc = dataObj.copies[0].playurl
					_this.videoimg = dataObj.img
					
					_this.loadHotVideo()
				})
			},
			loadHotVideo() {

				this.$reqUest.get('linkmed', '/linkmed2020/videoinfo', {
					//"tags": uni.getStorageSync('timeClassType').Class,
					"category": this.expert.category,
					"ishot": 1
				}).then(data => {
					var [error, res] = data;

					var list = this.$conFig.newList(res.data)

					this.hotVideoData = []
					for (var i = 0; i < list.length; i++) {
						if (this.expert.id != list[i].id) {
							this.hotVideoData.push(list[i])
						}
					}

					this.issign = true
					uni.hideLoading();

					if(this.issearch){
						this.userlog(2)
						
					}else{
						this.userlog(1)
					}
					

					setTimeout(() => {
						this.upH()
					}, 100)
				})
			},
			getUrl(e, u) {
				if (u == 1) {
					uni.redirectTo({
						url: e
					});
				} else {
					uni.navigateTo({
						url: e
					});
				}
			},

			getUrl(e, u) {
				if (this.$conFig.onUrl(e)) {
					if (u == 1) {
						uni.redirectTo({
							url: e
						});
					} else {
						uni.navigateTo({
							url: e
						});
					}
				}
			},
			userlog(e) {
				//this.$conFig.alert_Load()

				this.$reqUest.userLog({
					ClassType: this.expert.labels,
					ClassId: this.id,
					ClassTitle: this.expert.title,
					ClassAuthor: this.expert.expert,
					VideoPlayLong: this.timedate,
					OperationType: e,
					doctorId: this.expert.expert_id
				}).then(data => {
					var [error, res] = data;
					//uni.hideLoading();
				})
			},
			onpltxts() {

				this.frame = false
			},
			onpltxt() {
				this.frame = true
			},
			onuppl() {

			},
			onupdz() {
				
				if (this.expert.has_thumbup == '1') {
					this.$conFig.alert_Tips('您已经点过赞！')
					return
				}
				this.$reqUest.post('linkmed', '/linkmed2020/thumbup', {
					"nid": this.id
				}).then(data => {
					var [error, res] = data;
					this.expert.has_thumbup = 1
					this.expert.thumbup++
					if (res.data.code == 100000) {
						this.iszan = true
						this.userlog(3)
					}
				})
			},
			oncomReply(e, index) {
				this.comReply = e
				this.comIndex = index
				this.frame = true
			},
			Send() {
				
				console.log(this.AreaBlur)
				
				if (this.AreaBlur == "") {
					this.$conFig.alert_Tips('请先输入评语！')
					return
				}
				this.$conFig.alert_Load()
				var options = {
					nid: this.id,
					body: this.AreaBlur
				}
				if (this.comReply != "") {
					options.pid = this.comReply
				}


				this.$reqUest.post('linkmed', '/linkmed2020/comment', options).then(data => {
					var [error, res] = data;
					
					console.log(res)
					
					uni.hideLoading();
					this.userlog(4)

					if (res.data.code == 100000) {
						this.$conFig.alert_Tips('成功评论！')

						if (this.comReply != "") {
							this.commentIofo[this.comIndex].children.push({
								avatar: uni.getStorageSync('userInfo').avatar,
								content: this.AreaBlur,
								name: uni.getStorageSync('userInfo').name,
								updated: Date.parse(new Date()) / 1000,
								pid: res.data.result.pid,
								cid: res.data.result.cid
							})
						} else {
							this.commentIofo.unshift({
								avatar: uni.getStorageSync('userInfo').avatar,
								content: this.AreaBlur,
								name: uni.getStorageSync('userInfo').name,
								updated: this.getTimer(Date.parse(new Date()) / 1000),
								children: [],
								count: 0,
								open: false,
								pid: res.data.result.pid,
								cid: res.data.result.cid
							})
						}

						this.expert.comment_count++

						this.comReply = ""
						this.comIndex = ""
						this.onpltxts()
					} else {
						this.$conFig.alert_Tips('系统异常请稍后再试！')
						this.onpltxts()
					}


				})
			},
			onshare() {

				if (this.share) {
					this.share = false
				} else {
					this.share = true
				}


			},
			onCollection() {
				this.$conFig.alert_Load()

				this.$reqUest.post('linkmed', '/linkmed2020/fav', {
					"nid": this.id
				}).then(data => {
					var [error, res] = data;
					uni.hideLoading();
					if (res.data.code == 100000) {
						if (this.expert.has_flag == 1) {
							this.expert.has_flag = 0
							this.$conFig.alert_Tips('已取消收藏！')
						} else {
							this.expert.has_flag = 1
							this.$conFig.alert_Tips('收藏成功！')
						}
						this.userlog(5)
					} else {
						this.$conFig.alert_Tips('系统异常请稍后再试！')
					}

				})


			},
			dianzan(cid, index, state) {
				if (state == 1) {
					this.$conFig.alert_Tips('您已经点赞！')
					return
				}

				this.$conFig.alert_Load()
				this.$reqUest.get('inmedia', '/linyimaiapi.ashx?Method=commentlike', {
					cid: cid
				}).then(data => {
					var [error, res] = data;
					uni.hideLoading();
					if (res.data.result == "true") {
						this.$conFig.alert_Tips('点赞成功！')

						this.commentIofo[index].state = 1
						this.commentIofo[index].count = this.commentIofo[index].count + 1

					} else {
						this.$conFig.alert_Tips('系统异常请稍后再试！')
					}
				})
			},
			onopen(index) {
				if (this.commentIofo[index].open) {
					this.commentIofo[index].open = false
				} else {
					this.commentIofo[index].open = true
				}
			},
			shareImg() {
				
				this.fxtsrc = ""
				this.userlog(7)
				this.$conFig.alert_Load()
				var self = this;
				self.qrShow=true
				self.share=false
				var imgUrl = this.expert.thumbnail.src
				
				uni.downloadFile({
					url: imgUrl, //仅为示例，并非真实的资源
					success: (res) => {
						if (res.statusCode === 200) {
							
							var newimg = res.tempFilePath;
							uni.getImageInfo({
								src: imgUrl,
								success: (res) => {
									var width_ = res.width; //获取图片真实宽度
									var height_ = res.height;
									var ratio = width_ / height_; //图片的真实宽高比例		
									var viewWidth = 722 ; //设置图片显示宽度，            
									var viewHeight = 722 / ratio;
				
									this.cH = viewHeight + 470
				
				
									this.$reqUest.get('inmedia', '/linyimaiapi.ashx?Method=unlimited', {
										"scene": "share=true&id=" + this.id,
										page: "pages/details/details"
									}).then(data => {
										var [error, res] = data;
										
										
										
										var bs64 = res.data.message
										
										uni.downloadFile({
											url: bs64, 
											success: (res) => {
												
												if (res.statusCode === 200) {
													
													var newimgs = res.tempFilePath;
				
													let ctx = uni.createCanvasContext('testCanvas');
				
													ctx.fillStyle = "#fff";
													ctx.fillRect(0, 0, 722, 928);
				
													ctx.save();
													self.drawRoundRect(ctx, 0, 0, 722, 900, 0);
													ctx.clip();
				
													
													ctx.drawImage(newimg, 0, 0, viewWidth, viewHeight);
													
													ctx.drawImage('../../static/ewmbgs.png', 0, viewHeight-130, 722, 570);
													
													ctx.drawImage(newimgs, 240, viewHeight+20, 240, 240);
													ctx.restore();
													ctx.draw();
													
													setTimeout(()=>{
														uni.canvasToTempFilePath({
															x: 0,
															y: 0,
															width: 722,
															height: self.cH,
															canvasId: 'testCanvas',
															success: function(res) {
																self.fxtsrc = res.tempFilePath
																self.qrShowan=true
																uni.hideLoading();
																setTimeout(function(){
																	self.$conFig.alert_Tips('请长按保存至本地！')
																},500)
															},
															fail: function(res) {
														
															}
														})
													},200)
												}
											}
										});
				
									})
								}
							})
						}
					}
				});
			},
			
			drawRoundRect: function(cxt, x, y, width, height, radius) {
				cxt.beginPath();
				cxt.arc(x + radius, y + radius, radius, Math.PI, Math.PI * 3 / 2);
				cxt.lineTo(width - radius + x, y);
				cxt.arc(width - radius + x, radius + y, radius, Math.PI * 3 / 2, Math.PI * 2);
				cxt.lineTo(width + x, height + y - radius);
				cxt.arc(width - radius + x, height - radius + y, radius, 0, Math.PI * 1 / 2);
				cxt.lineTo(radius + x, height + y);
				cxt.arc(radius + x, height - radius + y, radius, Math.PI * 1 / 2, Math.PI);
				cxt.closePath();
			},
			save: function() {
				
				
				var that = this;
				//获取相册授权
				wx.getSetting({
					success(res) {
						if (!res.authSetting['scope.writePhotosAlbum']) {
							wx.authorize({
								scope: 'scope.writePhotosAlbum',
								success() {
									
									that.savaImageToPhoto();
								}
							})
						} else {
							that.savaImageToPhoto();
						}
					}
				})
			},
			
			savaImageToPhoto: function() {
				let that = this;
				wx.showLoading({
					title: '努力生成中...'
				})
			
				wx.hideLoading()
				wx.saveImageToPhotosAlbum({
					filePath: that.fxtsrc,
					success(res) {
						wx.showModal({
							content: '图片已保存到相册了',
							showCancel: false,
							success: function(res) {
								if (res.confirm) {
									
									
									that.qrShowan=false
									that.qrShow=false
								}
							}
						})
					}
				})
			},
			quxiao(){
				this.qrShowan=false
				this.qrShow=false
			}
		},
	};
</script>

<style>
	.video {
		width: 100%;
		height: 422upx;
	}

	.derbq {
		width: calc(100% - 70px);
	}

	.detnav {
		padding: 0px 17px;
	}

	.detnav>view {
		width: 50%;
		color: #9B9B9B;
		border-bottom: rgba(255, 255, 255, 0) solid 3px;
		padding: 19px 0px 8px 0px;
	}

	.detnav>view .detnavimgn2 {
		display: none;
	}

	.detnav>view.on.dh1 {
		color: #B92727;
		border-bottom: #B92727 solid 3px;
	}

	.detnav>view.on.dh2 {
		color: #BB2695;
		border-bottom: #BB2695 solid 3px;
	}

	.detnav>view.on.dh3 {
		color: #020193;
		border-bottom: #020193 solid 3px;
	}

	.detnav>view.on .detnavimgn1 {
		display: none;
	}

	.detnav>view.on .detnavimgn2 {
		display: block;
	}

	.detgr {}

	.detgrview {
		padding: 24px 17px 17px 17px;
	}

	.detgrimg {
		width: 51px;
		height: 51px;
		border-radius: 100px;
	}

	.detgrxx {
		margin-left: 10px;
		width: calc(100% - 61px);
		margin-top: -5px;
	}


	.lyk {
		bottom: 0px;
		left: 0px;
		width: calc(100% - 34px);
		padding: 20px 17px 30px 17px;
		z-index: 3;
		box-shadow: 0px -5px 20px 0px rgba(0, 0, 0, 0.09);
	}

	.lykinput {
		width: calc(100% - 170px);
		padding: 7px 10px;
		background: #F1F0F5;
		border-radius: 4px;
	}

	.lyksj {
		margin-top: 7px;
	}

	.zblist>view {
		margin-bottom: 25px;
	}

	.zblistimg {
		width: 45px;
		height: 45px;
		border-radius: 100px;
	}

	.zblistnr {
		width: calc(100% - 60px);
	}

	.zblist>view.on .fl {
		float: right;
	}

	.zblist>view.on .fr {
		float: left;
	}

	.zblist>view.on .zblistnrname {
		color: #B92727;
	}

	.zblist>view.on .zblistnrpl {}

	.jrzbj {
		padding: 2px 10px;
		background: #F1F0F5;
		border-radius: 10px;
	}

	.lylistnrname {
		width: calc(100% - 50px);
		
	}

	.lylistnrdz {
		width: 50px;
	}

	.lylistjg {
		height: 5px;
		background: #F9FBFE;
		width: calc(100% + 34px);
		margin-left: -17px;
	}

	.sharenr {
		bottom: 0px;
		left: 0px;
		width: 100%;
		background: #e8e8e8;
	}

	.shareqx {
		background: #f6f6f6;
		border-top: #dfdfdf solid 1px;
		padding: 15px 0px 20px 0px;
	}

	.fxbut {
		width: 60px;
		height: 60px;
		border-radius: 10px;
		background: #fff;
		margin: 0px auto;
		margin-bottom: 10px;
	}


	
	
	.testCanvas {
		width: 722px; opacity: 0;
	}
</style>
