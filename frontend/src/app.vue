<template>
<div id="app">
  <root-header/>
  <router-view></router-view>
  <root-footer/>
</div>
</template>

<script>
import Header from './components/header'
import Footer from './components/footer'

export default {
  components:{
    "root-header" : Header,
    "root-footer" : Footer
  }
}
</script>

<style lang="scss">
@import '@/assets/theme.scss';
</style>
