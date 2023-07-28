import $reqUest from 'common/request.js'
import $base64 from 'common/base64.min.js'
import $md5 from 'common/md5.js'
import jweixin from 'common/h5jssdk.js'
import vconsole from 'common/vconsole.min.js'


export default {
	//userInfo 服务端个人信息
	//wxuserInfo 微信个人信息
	//timeDepartment  临时科室
	//https://linkmed.jnjadc.com/
	//领医迈  https://linkmed-test.jnjadc.com   https://www.linkmed.cn
	linkmedApi: 'https://www.linkmed.cn',


	headsculpture: 'https://www.linkmed.cn/sites/default/files/linkmed2020_avatar/wxe05d7eb8b5fcf7ec.o6zAJs_rWJrfsEydFM0fanF68TiM.LvVaDsOBmIURe7c6378a2c12dc140bad2f91936f41f3.png',
	//英蕾
	inmediaApi: 'https://w.linkmed.cn/linkmed-Applet-API/api',
	inmediaInfo: 'https://w.linkmed.cn/wx/interFaceInfo/linkmed/',
	inmediaunion: 'https://w.linkmed.cn/wx/userInfo/',

	inmediaSso: "https://apps.atline.cn/linyima/api/sso.html?v=" + Date.parse(new Date()),

	bokecc: "https://p.bokecc.com/servlet/getvideofile",

	issign(share, url, oid) {

		uni.setStorage({
			key: 'timeUrl',
			data: ""
		});

		if (uni.getStorageSync('timeDepartment').category == undefined||uni.getStorageSync('timeDepartment').category=="所有科室"||uni.getStorageSync('timeDepartment').category=="预览组"||uni.getStorageSync('timeDepartment').category=="未分组"||uni.getStorageSync('timeDepartment').category=="") {
			uni.setStorage({
				key: 'timeUrl',
				data: url
			});
			uni.reLaunch({
				url: "../interest/interest"
			});
			return
		}
		
		if (uni.getStorageSync('oid').length > 10) {
			this.oninfo(1)
		} else {

			var arr = ['predetails/predetails', 'details/details', 'upload/upload','login/login']
			var num = 0

			for (var i = 0; i < arr.length; i++) {
				if (url.indexOf(arr[i]) > 0) {
					num++
				}
			}
			if (num > 0) {
				if (uni.getStorageSync('oid') == "") {
					if (oid != undefined && oid != '') {
						uni.setStorage({
							key: 'oid',
							data: oid
						});
						this.oninfo(0)
					} else {
						var fh = '?'
						if (url.indexOf('id') > 0) {
							fh = '&'
						}
						var back = $base64.Base64.encode('https://w.linkmed.cn/linkmed-Applet-API/miniprogram/#/' + url)
						var sign = $md5('https://w.linkmed.cn/linkmed-Applet-API/miniprogram/#/' + url + fh +
							'')
						var weburl = "https://w.linkmed.cn/wx/outMembers/?backurl=" + back + "&sign=" + sign
						window.location.href = weburl
						return
					}
				}
			}
		}

		return true
	},
	onWeixin(_title, _desc, _link, _imgUrl) {

		var _link = "https://w.linkmed.cn/linkmed-Applet-API/miniprogram/#" + _link
		var _imgUrl = "https://w.linkmed.cn/linkmed-Applet-API/miniprogram/static/" + _imgUrl

		var surl = window.location.href; //据说可以解决URL中带参数的问题，前台用的js编码，后台php解码

		uni.request({
			url: 'https://w.linkmed.cn/wx/gettoken60.aspx?callback=?',
			header: {
				'content-type': "application/x-www-form-urlencoded", //post一定要带这个header，被这里坑了半天  
			},
			data: {
				'rurl': surl
			},
			success: res => {
				var res = JSON.parse(res.data.substring(1, res.data.length - 1));
				jweixin.config({
					debug: false, // 开启调试模式,调用的所有api的返回值会在客户端alert出来，若要查看传入的参数，可以在pc端打开，参数信息会通过log打出，仅在pc端时才会打印。  
					appId: res.appid, // 必填，公众号的唯一标识  
					timestamp: res.timestamp, // 必填，生成签名的时间戳  
					nonceStr: res.nonceStr, // 必填，生成签名的随机串  
					signature: res.signature, // 必填，签名，见附录1  
					surl: surl, //自己添加的，debug为true的时候可以网页打印出对应的URL是否正确  
					jsApiList: ["updateAppMessageShareData", "updateTimelineShareData"]
				});

				jweixin.ready(function() {
					//自定义“分享给朋友”及“分享到QQ”按钮的分享内容（1.4.0）  
					jweixin.updateAppMessageShareData({
						title: _title, // 分享标题  
						desc: _desc, // 分享描述  
						link: _link, // 分享链接  
						imgUrl: _imgUrl, // 分享图标                                
						success: function() {
							// 用户确认分享后执行的回调函数  
						},
						cancel: function() {
							// 用户取消分享后执行的回调函数  
						}
					});
					//自定义“分享到朋友圈”及“分享到QQ空间”按钮的分享内容（1.4.0）  
					jweixin.updateTimelineShareData({
						title: _title, // 分享标题                                  
						link: _link, // 分享链接  
						imgUrl: _imgUrl, // 分享图标                                
						success: function() {
							// 用户确认分享后执行的回调函数  
						},
						cancel: function() {
							// 用户取消分享后执行的回调函数  
						}
					});

				});
			}
		});
	},
	oninfo(e) {

		uni.request({
			url: 'https://w.linkmed.cn/wx/userInfo_linkmed/?unionId=' + uni.getStorageSync('oid'),
			header: {
				'content-type': "application/x-www-form-urlencoded",
			},
			success: (res) => {
				var info = res.data
				console.log("//////")
				console.log(info)
				
				uni.setStorage({
					key: 'userInfo',
					data: {
						"uid": info.uid,
						"type": info.type,
						"mail": info.email,
						"mobile": info.phone,
						"name": info.trueName,
						"category": info.department,
						"hospital": info.hospital,
						"position": info.zhichen,
						"openid": info.openId,
						"unionid": info.unionId,
						"avatar": info.headImg
					}
				});
				uni.setStorage({
					key: 'sign',
					data: uni.getStorageSync('sign') + "/true"
				});
				if(e==0){
					uni.setStorage({
						key: 'timeDepartment',
						data: {
							"category": info.department==''?uni.getStorageSync('timeDepartment').category:info.department,
						}
					});
				}
				// uni.reLaunch({
				// 	url: uni.getStorageSync('timeUrl')
				// });

				console.log(res)
			},
		})
	},
	onUrl(url) {
		return true
	},
	newList(e) {
		var ListData = []
		var newListData = []

		Object.keys(e).forEach((key) => {
			ListData.push(e[key])
		})

		for (var i = 0; i < ListData.length - 1; i++) {
			newListData.push(ListData[i])
			if (ListData[i].thumbnail != undefined) {

				var imgSrc = ListData[i].thumbnail.src

				//newListData[i].thumbnail.src="https://www.linkmed.cn"+imgSrc.split("https://linkmed-test.jnjadc.com")[1]

			}
		}
		return newListData
	},
	newListLength(e) {
		var ListData = []

		Object.keys(e).forEach((key) => {
			ListData.push(e[key])
		})

		return ListData.length
	},
	ClassType(e) {
		var _txt = ["课程直播", "直播回放", "强生微课", "手术解析", "专题课程"]
		uni.setStorage({
			key: 'timeClassType',
			data: {
				Type: e,
				Class: _txt[e]
			}
		});
	},
	ChangeDateFormat(timestamp) {
		var date = new Date(parseInt(timestamp.replace("/Date(", "").replace(")/", ""), 10));
		Y = date.getFullYear() + '-';
		M = (date.getMonth() + 1 < 10 ? '0' + (date.getMonth() + 1) : date.getMonth() + 1) + '-';
		D = (date.getDate() < 10 ? '0' + (date.getDate()) : date.getDate()) + ' ';
		h = (date.getHours() < 10 ? '0' + (date.getHours()) : date.getHours()) + ':';
		m = (date.getMinutes() < 10 ? '0' + (date.getMinutes()) : date.getMinutes()) + ':';
		s = (date.getSeconds() < 10 ? '0' + (date.getSeconds()) : date.getSeconds());
		return Y + M + D + h + m + s;
	},
	getUrl(e) {
		uni.navigateTo({
			url: e
		});
	},
	alert_Tips(e) {
		uni.showToast({
			icon: "none",
			title: e,
			duration: 1500,
			mask: true
		})
	},
	alert_Load(e) {

		var _e = e
		if (e == undefined) {
			_e = "加载中"
		}
		uni.showLoading({
			title: _e,
			mask: true
		})
		//uni.showLoading({title: _e})
	}
}
