<template>
	<view>
		{{id}}
		<template v-if="aa">
			aaa
		</template>
	</view>
</template>

<script>
	export default {
		data() {
			return {
				aa:false,
				id:""
			}
		},
		onLoad(e) {
			console.log("/////")
			console.log(e)
			this.id=e.id
			this.aa=true
		},
		onShow() {
			
		},
		methods: {
			
		}
	}
</script>

<style>

</style>
