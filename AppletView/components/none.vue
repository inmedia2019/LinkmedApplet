<template>
	<view class="body center " style="height: 300px;">
		<template v-if="tab==1">
			<view class="animated fadeIn fastpf">
				<view class="center">
					<image style="width: 115px; height: 116px;" src="../static/404.png" mode="widthFix"></image>
				</view>
				<view class="h30"></view>
				<view class="tec f20 c000">无内容</view>
				<view class="h5"></view>
				<view class="tec c999">没有找到你要的内容</view>
			</view>
		</template>
		<template v-if="tab==2">
			<view class="animated fadeIn fastpf">
				<view class="center">
					<image style="width: 115px; height: 116px;" src="../static/kong2.png" mode="widthFix"></image>
				</view>
				<view class="h30"></view>
				<view class="tec f20 c000">无收藏</view>
				<view class="h5"></view>
				<view class="tec c999">快去收藏你感兴趣的课程吧</view>
			</view>
		</template>
		<template v-if="tab==3">
			<view class="animated fadeIn fastpf">
				<view class="center">
					<image style="width: 115px; height: 116px;" src="../static/kong6.png" mode="widthFix"></image>
				</view>
				<view class="h30"></view>
				<view class="tec f20 c000">无预约</view>
				<view class="h5"></view>
				<view class="tec c999">快去预约你喜欢的讲师直播吧</view>
			</view>
		</template>
		<template v-if="tab==4">
			<view class="animated fadeIn fastpf" style="margin-top: -50px;">
				<view class="center">
					<image style="width: 115px; height: 116px;" src="../static/kong3.png" mode="widthFix"></image>
				</view>
				<view class="h30"></view>
				<view class="tec f20 c000">无课程</view>
				<view class="h5"></view>
				<view class="tec c999">啊哦，去别的科室看看吧</view>
			</view>
		</template>
		<template v-if="tab==5">
			<view class="animated fadeIn fastpf">
				<view class="center">
					<image style="width: 115px; height: 116px;" src="../static/kong4.png" mode="widthFix"></image>
				</view>
				<view class="h30"></view>
				<view class="tec f20 c000">无直播</view>
				<view class="h5"></view>
				<view class="tec c999">直播即将上线，先看看其他课程吧</view>
			</view>
		</template>
		<template v-if="tab==6">
			<view class="animated fadeIn fastpf">
				<view class="center">
					<image style="width: 115px; height: 116px;" src="../static/kong5.png" mode="widthFix"></image>
				</view>
				<view class="h30"></view>
				<view class="tec f20 c000">无课程</view>
				<view class="h5"></view>
				<view class="tec c999">更多精品课程即将来袭</view>
			</view>
		</template>
		<template v-if="tab==7">
			<view class="animated fadeIn fastpf">
				<view class="center">
					<image style="width: 115px; height: 116px;" src="../static/kong7.png" mode="widthFix"></image>
				</view>
				<view class="h30"></view>
				<view class="tec f20 c000">无消息</view>
				<view class="h5"></view>
				<view class="tec c999">暂无消息动态</view>
			</view>
		</template>
		<template v-if="tab==8">
			<view class="animated fadeIn fastpf">
				<view class="center">
					<image style="width: 115px; height: 116px;" src="../static/kong4.png" mode="widthFix"></image>
				</view>
				<view class="h30"></view>
				<view class="tec f20 c000">无活动</view>
				<view class="h5"></view>
				<view class="tec c999">活动即将上线，先看看其他课程吧</view>
			</view>
		</template>
		
	</view>
</template>

<script>
	export default {
		props: ['tab'],
		data() {
			return {
				
			}
		},
		methods: {
			
		},
		created() {
			
		}
	}
</script>

<style>
</style>
