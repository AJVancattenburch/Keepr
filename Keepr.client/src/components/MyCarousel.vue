<template>

  <Carousel ref="homeCarousel" :itemsToShow="3.95" :wrapAround="true" :transition="500" >
    <Slide v-for="k in keeps" :key="k.id">
      <div class="container">
        <p> {{ k.name }} </p>
        <img :src="k.img" alt="Keep Image">
      </div>
    </Slide>
  </Carousel>

</template>


<script>
import { computed, ref } from "vue"
import { Carousel, Pagination, Slide } from 'vue3-carousel'
import 'vue3-carousel/dist/carousel.css'
import { AppState } from "../AppState.js"

export default {
  
  defineComponent: {
    Carousel,
    Slide,
    // Pagination,
  },
  
  setup(){

    const myCarousel = ref(null)

    return {
      myCarousel,
      keeps: computed(()=>AppState.keeps)
    }
  }
}



</script>


<style scoped lang="scss">

.keep-card {
 
}



</style>