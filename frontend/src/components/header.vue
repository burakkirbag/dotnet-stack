<script>
import { mapState, mapActions } from 'vuex'

export default {
  name: 'Header',
}
</script>

<template>
  <header id="header">
    <div id="nav">
      <a href="/" class="router-link-active router-link-exact-active" aria-current="page">Home</a>
      <span>&nbsp;|&nbsp;</span>
      <a href="/about" class="">About</a>
    </div>
  </header>
</template>

<style scoped>
</style>
