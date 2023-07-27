<script>
	export default {
		globalData: {  
			isshow: true,
			isLaunch:false,
		},  
		onLaunch: function() {
			this.globalData.isLaunch = true
		},
		onShow: function(e) {
			
		},
		onHide: function(e) {
			
		},
		methods: {
			
		},
	}
</script>

<style>
	@import url("./common/uni.css");
	@import url("./common/icon.css");
	@import url("./common/animate.min.css");
	
	uni-toast .uni-toast__content{ font-size: 12px !important; }
	uni-toast .uni-toast{ width: 5em !important; }
	
	page{ font-size: 14px; line-height: 1.5; color: #000; background: #F5F6FA; }
	view{-webkit-tap-highlight-color: rgba(0, 0, 0, 0)}
	
	.fl{ float: left;  }
	.fr{ float: right;}

	.w5{ width: 5px; }
	.w10{ width: 10px; }
	.w15{ width: 15px; }
	.w20{ width: 20px; }

	.h1{ height: 1px;}
	.h2{ height: 2px;}
	.h3{ height: 3px;}
	.h5{ height: 5px;}
	.h10{ height: 10px;}
	.h15{ height: 15px;}
	.h18{ height: 18px;}
	.h20{ height: 20px;}
	.h25{ height: 25px;}
	.h30{ height: 30px;}
	.h35{ height: 35px;}
	.h40{ height: 40px;}
	.h45{ height: 45px;}
	.h50{ height: 50px;}
	.h100{ height: 100%; }

	.f8{ font-size: 8px; }
	.f10{ font-size: 10px; }
	.f12{ font-size: 12px; }
	.f13{ font-size: 13px; }
	.f15{ font-size: 15px; }
	.f16{ font-size: 16px; }
	.f18{ font-size: 18px; }
	.f20{ font-size: 20px; }
	.f25{ font-size: 25px; }
	.fb{ font-weight: bold; }
	
	.c000{ color: #000; }
	.cfff{ color: #fff; }
	.c949597{ color: #949597; }
	.c9AA8B6{ color: #9AA8B6; }
	.c999{ color: #999; }
	.cB92727{ color: #B92727; }
	.c4A4A4A{ color: #4A4A4A; }
	.c9A9EAA{ color: #9A9EAA; }
	.cD0021B{ color: #D0021B; }
	.cBBC0CE{ color: #BBC0CE; }
	.cdadadc{ color: #dadadc; }
	.cFCCF0A{ color: #FCCF0A; }
	.c9B9B9B{ color: #9B9B9B; }
	.cC1C9D6{ color: #C1C9D6; }
	.c768196{ color: #768196; }
	.c9DA4B3{ color: #9DA4B3; }
	.cA2A3AB{ color: #A2A3AB; }
	.cDEDDDE{ color: #DEDDDE; }
	
	.cCA021B{ color: #CA021B; }
	.bfff{ background: #fff; }
	.bEAECF1{ background: #EAECF1; }
	.bD43D4C{ background: #D43D4C; }
	.bca021b{ background: #ca021b; }
	.bF9FBFE{ background: #F9FBFE; }
	.bECF3FC{ background: #ECF3FC; }
	.bDEDDDE{ background: #DEDDDE; }
	.bF3F6F8{ background: #F3F6F8; }
	.b979797{ background: #979797; }
	.bB92727{ background: #B92727; }
	.bAAB2B7{ background: #AAB2B7; }
	.bF5F6FA{background: #F5F6FA;}
	
	.mt4{ margin-top: 4px; }
	
	.bbs{background:linear-gradient(left,rgba(185,39,39,1) 0%,rgba(1,0,145,1) 100%);}
	
	.op6{ opacity: 0.6; }
	.op7{ opacity: 0.7; }
	.op2{ opacity: 0.2; }
	
	.w100{ width: 100%; }
	.whtl{ width: 100%; height: 100%; top: 0px; left: 0px; }

	.pf{ position: fixed; }
	.pa{ position: absolute;}
	.pr{ position: relative;}
	.dn{ display: none;}
	.db{ display: block;}
	.clear{ clear: both;}
	.ovh{ overflow: hidden; }
	.tec{ text-align: center; }
	.ter{ text-align: right; }
	.m0auto{ margin: 0px auto; }
	
	.ellipsis{
		overflow: hidden;
	    text-overflow: ellipsis; 
	    -o-text-overflow: ellipsis;
	    white-space:nowrap;
	}
	
	.center{display: -webkit-box;
					display: -ms-flexbox;
					display: -webkit-flex;
					display: flex;
					-webkit-box-pack: center;
					-ms-flex-pack: center;
					-webkit-justify-content: center;
					justify-content: center;
					-webkit-box-align: center;
					-ms-flex-align: center;
					-webkit-align-items: center;
					align-items: center;}
					
	.trall1{transition: all .1s;}
	.trall3{transition: all .3s;}

	.tk{z-index: 10; background: rgba(0, 0, 0, 0.7); }
	.z3{ z-index: 3; }
	
	
	
	.body{ padding: 17px; }
	.bodys{ padding: 10px 17px; }
	.shadow{ box-shadow: 0px 3px 5px rgba(0,0,0,0.2);}
	.shadow3{ box-shadow: 0px 5px 10px rgba(0,0,0,0.4);}
	.shadow2{ box-shadow: 0px 5px 10px rgba(202,2,27,0.4);}
	
	.triangle-down{ width: 0; height: 0; border-left: 5px solid transparent; border-right: 5px solid transparent; border-top: 8px solid #fff;}
	.triangle-right{ width: 0; height: 0; border-top: 4px solid transparent; border-left: 6px solid #999999; border-bottom: 4px solid transparent;}
	
	.noticetxt{ width: 15px; height: 15px; border-radius: 100px; top: -4px; right: -4px; }
	.topfr{ margin-top: -5px; width: calc(100% - 103px); }
	.topinput{ padding: 5px 14px; background:rgba(255,255,255,0.5); border-radius: 5px; }
	
	.more{ margin-top: 4px; }
	
	.navbottom{ height: 133px; bottom: 0px; left: 0px; width: 100%; color: #888888; }
	.navbg{ width: 528px; top: 0px; left: 50%; -webkit-transform: translateX(-264px); pointer-events: none; }
	.navbottomnr1,.navbottomnr3{ width: calc(50% - 45px); margin-top: 45px; }
	.navbottomnr1>view,.navbottomnr3>view{ width: 50%; }
	.navzi{ height: 23px; }
	.navzii2{ display: none; }
	.navbottomnr1>view.on .navzii1,.navbottomnr3>view.on .navzii1{ display: none; }
	.navbottomnr1>view.on .navzii2,.navbottomnr3>view.on .navzii2{ display: block; }
	.navbottomnr2{ width: 90px; }
	.navbottomnr2>view{ width: 56px; height: 56px; background: #ca021b; border-radius: 56px; margin:-8px auto 0px auto; }

	.yz{width:4px;height:20px;border-radius:2px;background:rgba(185,39,39,1);}
	
	.list{}
	.list>view{ margin-bottom: 12px; }
	.listimg{ margin-left: 3px; width: 130px; height: 70px; border-radius: 5px; border: #fff solid 2px; overflow: hidden; }
	.listimg view{ background: rgba(0,0,0,0.6); }
	.imgnew{ top: 0px; left: 0px; }
	.listimgtime{ bottom: 2px; right: 5px; }
	.listwb{ width: calc(100% - 147px); margin-left: 10px; }
	.listwbtitle{ height: 39px; margin-bottom: 20px; }
	
	.shuru{ width: calc(100% - 34px); bottom: 0px; left: 0px; }
	.shuru textarea{ padding: 10px; background: #F1F0F5; width: calc(100% - 20px); border-radius: 5px; height: 80px; }
	.lybut{ width: 76px; height: 28px; border-radius: 5px;  }
	
	.videolisttitle{ width: calc(100% + 20px); }
	.videolisttitle .fl{ margin-left: 3px; border-radius: 5px; padding: 5px 10px; margin-right: 10px; background:rgba(185,39,39,0.2); }
	.videolisttitle .fl.on{ background:rgba(185,39,39,1); box-shadow: 0px 3px 5px rgba(0,0,0,0.2); }
	
	.collect{ width: calc(100% + 20px); height: 28px; }
	.collect .fl{ margin-left: 3px; border-radius: 5px; padding: 5px 10px; margin-right: 10px; background:rgba(255,255,255,0.2); }
	.collect .fl.on{ background:rgba(255,255,255,1); color: #B92727; }
	
	.caleswnav{ width: 80px; height: 28px; right: -12px; bottom: 10px; border-radius: 5px; }
	.caleswnav.on{ background: #9B9B9B; }
	.listli{ height: 310rpx; border-radius: 5px; border: #fff solid 2px; margin-bottom: 20px; }
	
	.videolist{ border: #DEDDDE solid 1px; background: #fff; border-radius: 5px; padding: 15px; margin-bottom: 15px; }
	.videolistimg{ width: 125px; height: 70px; border-radius: 5px; border: #fff solid 2px; }
	.videolistimg>view{ background: rgba(0,0,0,0.4); }
	.videolistimg text{ bottom: 2px; right: 7px; }
	
	.videolistname{ width: calc(100% - 140px); }
	.videolistnametxt{ height: 38px; }
	.videolistnametx{ width: 28px; height: 28px; border-radius: 100px; }
	
	.yd{ width: 90px; bottom: 0px; right: 0px; }
	
	
	.dlk{ width: 80%; border-radius: 5px; }
	
	.input{width: 580rpx;}
	.input input{ border-bottom: #BBC0CE solid 1px;  }
	.input picker{ border-bottom: #BBC0CE solid 1px;  }
	
	.inputtxt1{ width: calc(100% - 40px); padding:8px 5px 8px 35px ; }
	.inputtxt2{ width: calc(100% - 140px); padding:8px 100px 8px 35px ; }
	.inputtxt3{ width: calc(100% - 85px); padding:8px 5px 8px 80px ; }
	.inputtxt4{ width: calc(100% - 10px); padding:8px 5px 8px 5px; }
	.inputtxt4_view{ padding-left: 130px; width: calc(100% - 130px); }
	.inputtxt5{ width: calc(100% - 140px); padding:8px 5px 8px 135px ; }
	
	.inputwb{ top: 0px; left: 0px; height: calc(100% - 2px); padding-left: 5px; }
	
	.signicon{ top: 0px; left: 0px; width: 30px; height: calc(100% - 2px); }
	
	.yzm{ top: 0px; right: 5px; height: 100%; }
	.yzm view{ border-radius: 5px; width: 70px; padding: 5px 10px; }
	
	.netbut{ width: 80%; padding: 10px 0px; text-align: center; border-radius: 100px; }
	
	.modifytel{ width: 75%; border-radius: 5px;}
	.modifytelinput input{ width: calc(100% - 30px); padding: 10px 15px; border-radius: 5px; box-shadow:0px 1px 3px 0px rgba(0,0,0,0.14) inset; }
	
	.modifytelbut{ border-top: #D6D6D6 solid 1px; }
	.modifytelbut>view{ height: 45px;}
	
	.maxh{  max-height: 53px; }
	
	
	.bannebtbg {
		padding: 7px 10px;
		width: calc(100% - 20px);
		bottom: 0px;
		left: 0px;
	}
</style>
