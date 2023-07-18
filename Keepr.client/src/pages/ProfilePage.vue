<template>
<section v-if="profile" :key="profile?.id" class="container-fluid">

  <div>
    <div class="row cover-img" :style="{ backgroundImage: `url(${profile.picture})` }">
      <div class="card d-flex flex-row mx-auto" style="width: 70%; height: 70%; opacity: .92;">
        <div class="col-4">
          <img class="profile-img m-0"
               :src="profile.picture" 
               :alt="profile.name">
        </div>
        <div class="col-8 card-right d-flex flex-column justify-content-center">
          <h5 class="ps-4 ms-5"> {{ profile.name }} </h5>
          <div class="row my-5">
            <div class="col-10 bio-container" style="">
              <p class="profile-bio"> {{ profile.bio }} </p>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>












  <section class="row" style="overflow-x: hidden;">
    <h2 class="carousel__title mt-3 text-center text-uppercase"> {{ profile.name }}'s keeps </h2>
    <div class="col-12 m-auto bg-dark justify-content-center align-items-center shadow-lg elevation-5">
      <Carousel ref="myCarousel" :itemsToShow="3.95" :wrapAround="true" :transition="500" class="mt-5">
        <Slide v-for="slide in carouselKeeps" :key="slide" class="">
            <div class="carousel__card bg-transparent" style="">
              <div class="carousel__item">
                <img v-if="carouselKeeps" @click="getKeepById(slide.id)" data-bs-target="#detailsModal" :src="slide.img" class="card-img-top selectable pt-5" :alt="slide.name">
                <h3 class="card-title"> {{ slide.name }} </h3>
                <div class="card-body mb-4">
                  <p class="card-text"> {{ slide.description.split(' ').splice(0, 9).join(' ') }}... </p>
                </div>
              </div>
            </div>
        </Slide>
      </Carousel>
    </div>
    <!-- NOTE - Accent Row between Carousel & Previous / Next Slide Buttons -->
    <div class="col-12 accent-row d-flex mb-5 pb-2 pt-2" style="height: 4rem;">
    <!-- NOTE - Previous / Next Slide Buttons -->
      <div class="col-6 col-md-6 d-flex justify-content-center">
        <p role="button" class="carousel__btn me-5" style="width: 10vw;" @click="myCarousel.prev()">
          <i title="Previous" class="mdi mdi-arrow-left-circle fs-1"></i>
        </p>
      </div>
      <div class="col-6 col-md-6 d-flex justify-content-center">
        <p role="button" class="carousel__btn ms-5" style="width: 10vw;" @click="myCarousel.next()">
          <i title="Next" class="mdi mdi-arrow-right-circle fs-1"></i>
        </p>
      </div>
    </div>
  </section>

</section>

</template>


<script>

import { computed, onMounted, ref } from 'vue'
import { AppState } from '../AppState'
import { useRoute } from 'vue-router'
import { logger } from "../utils/Logger.js"
import Pop from "../utils/Pop.js"
import { profilesService } from "../services/ProfilesService.js"
import { keepsService } from "../services/KeepsService.js"
import { Modal } from "bootstrap"
import { Carousel, Slide } from 'vue3-carousel'

export default {

  setup() {

    const myCarousel = ref(null)
    const route = useRoute()

    async function getProfileById() {
      try {
        const profileId = route.params.profileId
        await profilesService.getProfileById(profileId)
      } catch (error) {
        logger.log('{ProfilePage} getProfileById() error: ', error)
        Pop.error(error.message, 'Error')
      }
    }
    

    onMounted(() => {
      getProfileById()
    })

    return {
      
      // route,
      myCarousel,

      keep: computed(() => AppState.activeKeep),
      profile: computed(() => AppState.activeProfile),
      carouselKeeps: computed(() => AppState.keeps.sort(
        (a, b) => b.views - a.views)
        ),

      async getKeepById(keepId) {
        try {
          logger.log('[KEEPCARD] => getKeepById() keepId: ', keepId)
          await keepsService.getKeepById(keepId)
          Modal.getOrCreateInstance('#detailsModal').show()
        } catch (error) {
          Pop.error(error.message, 'Error')
          logger.log(error)
        }
      },

    }
  },
  components: {
    Carousel,
    Slide
  },
}

</script>

<style scoped lang="scss">

.cover-img {
  --bs-gutter-x: 0;
  width: 100%;
  height: 100%;
  background-position: center;
  background-size: cover;
  background-repeat: no-repeat;
  padding: 5rem;
}

.card {
  --bs-gutter-x: 0;
  box-shadow: 0 0 10px 10px #2260777c;
}


.profile-img {
  width: 100%;
  height: 100%;
  object-fit: cover;
  object-position: center;
}

.card-right {
  background-color: aliceblue;
  border-radius: .25rem;
}

.bio-container {
  margin: auto;
  height: 10rem;
  background: linear-gradient(-45deg, 
                              transparent, 
                              #f5d0bb97);
  background-color: #f0f8ff47;
  box-shadow: 0 0 10px 10px #2260777c inset;
  border-radius: .25rem;
}

.profile-bio {
}

.carousel * {
  
  overflow: hidden;
  text-align: center;
}

.carousel__title {
  color: #fff;
  text-shadow: 1.5px 1.5px 1px #000;
  background: linear-gradient(145deg, #5cc0fed0, 
                                      #1c8ffab9 100%);
  box-shadow: inset 3px 3px 10px 3px #0482ffc5, 
              inset -3px -3px 10px 3px #00a6ffc9, 
                     3px 3px 10px 3px #0482ffc5, 
                    -3px -3px 10px 3px #00a6ffc9;
  opacity: .9;
}

.carousel__card {
  width: 100%;
  height: 75%;
  background-color: #000;
  border-radius: .25rem;
  box-shadow: 0 0 10px 10px #2260777c;
}

img.card-img-top {
  width: 100%;
  aspect-ratio: 1/1;
  object-fit: cover;
  object-position: center;
  border-radius: .25rem;
}

.card-text {
  color: #fff;
  padding: 1rem;
  margin: auto;
  width: 90%;
  height: 20vh;
  text-shadow: 1.5px 1.5px 1px #000;
  background: linear-gradient(145deg, #5cc0fe5c, 
                                      #1c8ffa53 100%);
  box-shadow: inset 3px 3px 10px 3px #0482ffc5, 
              inset -3px -3px 10px 3px #00a6ffc9, 
                     3px 3px 10px 3px #0482ffc5, 
                    -3px -3px 10px 3px #00a6ffc9;
  opacity: .9;
}

.carousel__button {
  position: absolute;
  top: 50%;
  transform: translateY(-50%);
  width: 2rem;
  height: 2rem;
  border-radius: 50%;
  background-color: #fff;
  color: #000;
  font-size: 1.5rem;
  line-height: 2rem;
  text-align: center;
  cursor: pointer;
  box-shadow: 0 0 10px 10px #2260777c;
}

.card-title {
  position: relative;
  top: 0;
  left: 0;
  width: 100%;
  padding-top: .5rem;
  padding-bottom: .5rem;
  color: #ffc04a;
  font-size: 1.5rem;
  text-shadow: 1.5px 1.5px 1px #000;
}

.carousel__slide {
  padding: 5px;
}

.carousel__viewport {
  perspective: 2000px;
}

.carousel__track {
  transform-style: preserve-3d;
}

.carousel__slide--sliding {
  transition: 0.5s;
}

.carousel__slide {
  opacity: 0.9;
  transform: rotateY(-20deg) scale(0.9);
}

.carousel__slide--active ~ .carousel__slide {
  transform: rotateY(20deg) scale(0.9);
}

.carousel__slide--prev {
  opacity: 1;
  transform: rotateY(-10deg) scale(0.95);
}

.carousel__slide--next {
  opacity: 1;
  transform: rotateY(10deg) scale(0.95);
}

.carousel__slide--active {
  opacity: 1;
  transform: rotateY(0) scale(1.1);
}

.accent-row {
  background: linear-gradient(145deg, #5cc0fed0, 
                                      #1c8ffab9 100%);
  box-shadow: inset 3px 3px 10px 3px #0482ffc5, 
              inset -3px -3px 10px 3px #00a6ffc9, 
                     3px 3px 10px 3px #0482ffc5, 
                    -3px -3px 10px 3px #00a6ffc9;
  opacity: .9;
}

</style>