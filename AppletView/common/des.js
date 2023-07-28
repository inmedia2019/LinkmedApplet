
// import WXBizDataCrypt from 'common/WXBizDataCrypt.js'

export default {
	getdes(sessionKey,encryptedData,iv){
		var appId = 'wx4f4bc4dec97d474b'
		var pc = new WXBizDataCrypt(appId, sessionKey)
		console.log("///////")
		return pc.decryptData(encryptedData , iv)
	}
}



