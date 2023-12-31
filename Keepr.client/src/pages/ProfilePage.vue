<template>
<section v-if="profile" :key="profile?.id" class="container-fluid">

  <div>
    <div class="row cover-img" :style="{ backgroundImage: `url(${profile.coverImg})` }">
      <div class="card d-flex flex-row mx-auto" style="width: 70%; height: 70%; opacity: .92;">
        <div class="col-4">
          <img class="profile-img m-0"
               :src="profile.picture" 
               :alt="profile.name">
        </div>
        <div class="col-8 card-right d-flex flex-column justify-content-center">
          <div class="col-3 profile-name-box">
            <h5 class="text-center my-3"> {{ profile.name }} </h5>
          </div>
          <div class="row my-5">
            <div class="col-10 bio-container" style="">
              <p class="profile-bio"> {{ profile.bio }} </p>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>

  <section v-if="profile" :key="profile?.id" class="row" style="overflow-x: hidden;">
    <h2 class="carousel__title mt-3 text-center text-uppercase"> {{ profile.name }}'s Vaults </h2>
    <div class="col-12 m-auto bg-dark justify-content-center align-items-center shadow-lg elevation-5">
      <Carousel 
        ref="myCarousel" 
        :itemsToShow="3.95" 
        :wrapAround="true" 
        :transition="500" 
        class="mt-3">
          <Slide 
            v-for="slide in carouselVaults" 
            :key="slide" 
            class=" ">
                <div v-if="!slide.isPrivate" class="carousel__card bg-transparent" style="">
                  <div class="carousel__item">
                    <router-link :to="{ name: 'VaultDetails', params: { vaultId: slide.id }}">
                      <img 
                        v-if="carouselVaults" 
                        @click="getVaultById(slide.id)" 
                        :src="slide.img" 
                        class="carousel__img card-img-top selectable pt-1" 
                        :alt="slide.name">
                    </router-link>
                    <div class="my-3 mb-4 rounded-5" style="">
                      <div class="content__box p-3">
                        <h3 class="card-title"> {{ slide.name }} </h3>
                        <div class="card-body mb-4">
                          <p class="card-text"> {{ slide.description.split(' ').splice(0, 9).join(' ') }}... </p>
                      </div>
                    </div>
                    </div>
                  </div>
                </div>
                <div v-else class="carousel__card bg-transparent" style="">
                  <div class="carousel__item">
                      <img 
                        v-if="carouselVaults" 
                        @click="getVaultById(slide.id)" 
                        :src="slide.img" 
                        class="carousel__img card-img-top selectable pt-1" 
                        :alt="slide.name">
                    <div class="my-5 mb-4 rounded-5" style="">
                      <div class="content__box pt-3" style="height: 1px !important;">
                        <h3 class="card-title"> {{ slide.name }} </h3>
                        <div class="card-body">
                          <p class="card-text"> {{ slide.description.split(' ').splice(0, 20).join(' ') }}... </p>
                      </div>
                    </div>
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
        <p 
          role="button" 
          class="carousel__btn me-5" 
          style="width: 10vw;" 
          @click="myCarousel.prev()">
          <i title="Previous Slide" class="mdi mdi-arrow-left-circle fs-1" style="text-shadow: 1px 1px 1px black; opacity: .8"></i>
        </p>
      </div>
      <div class="col-6 col-md-6 d-flex justify-content-center">
        <p 
          role="button" 
          class="carousel__btn ms-5" 
          style="width: 10vw;" 
          @click="myCarousel.next()">
          <i title="Next Slide" class="mdi mdi-arrow-right-circle fs-1" style="text-shadow: 1px 1px 1px black; opacity: .8"></i>
        </p>
      </div>
    </div>
  </section>
  <h2 class="profile-name text-center text-uppercase position-relative"> {{ profile.name }}'s Keeps </h2>

  <div class="masonry-columns">
    <div v-for="k in keeps" :key="k.id" class="">
      <KeepCard :keep="k" />
    </div>
  </div>

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
import KeepCard from '../components/KeepCard.vue'
import { vaultsService } from "../services/VaultsService.js"

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

    async function getKeepsByProfileId() {
      try {
        const profileId = route.params.profileId
        await keepsService.getKeepsByProfileId(profileId)
      } catch (error) {
        logger.log('{ProfilePage} getKeepsByProfileId() error: ', error)
        Pop.error(error.message, 'Error')
      }
    }

    async function getVaultsByProfileId() {
      try {
        const profileId = route.params.profileId
        await vaultsService.getVaultsByProfileId(profileId)
      } catch (error) {
        logger.log('{ProfilePage} getVaultsByProfileId() error: ', error)
        Pop.error(error.message, 'Error')
      }
    }

    async function getVaultById() {
      try {
        const vaultId = route.params.vaultId
        logger.log('[ACCOUNT PAGE] => getVaultById() vaultId: ', vaultId)
        await vaultsService.getVaultById(vaultId)
      } catch (error) {
        Pop.error(error.message, 'Error')
        logger.log(error)
      }
    }

    onMounted(() => {
      getProfileById()
      getKeepsByProfileId()
      getVaultsByProfileId()
    })

    return {
      
      getVaultById,
      route,
      myCarousel,
      account: computed(() => AppState.account),
      user: computed(() => AppState.user),
      keep: computed(() => AppState.activeKeep),
      keeps: computed(() => AppState.userKeeps),
      vault: computed(() => AppState.userVaults),
      profile: computed(() => AppState.activeProfile),

      carouselVaults: computed(() => AppState.userVaults.sort(
        (a, b) => b.views - a.views
        )),

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
    Slide,
    KeepCard,
  },
}

</script>

<style scoped lang="scss">

.container-fluid {

  background: linear-gradient(145deg, #5cc0fed0, 
                                      #1c8ffab9 100%);

}

.cover-img {
  --bs-gutter-x: 0;
  width: 100%;
  height: 100%;
  background-position: center;
  background-size: cover;
  background-repeat: no-repeat;
  padding: 10rem;
}

.card {
  --bs-gutter-x: 0;
  box-shadow: 0 0 10px 10px #2260777c;
}

.profile-name {
  top: -2.5rem;
  padding-top: 2rem;
  padding-bottom: 2rem;
  width: 100%;
  opacity: 1;
  text-shadow: 1.5px 1.5px 1px #000;
  color: #fff;
  font-size: 2.5rem;
  text-align: center;
  font-weight: bold;
  letter-spacing: .1rem;
  text-transform: uppercase;
  font-family: 'Roboto', sans-serif;
  font-family: 'Roboto Condensed', sans-serif;
  font-family: 'Roboto Slab', serif;
  font-family: 'Roboto Mono', monospace;
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

.profile-name-box {
  background: linear-gradient(145deg, #5cc0fed0, 
                                      #1c8ffab9 100%);
  box-shadow: 3px 0px 10px 0px #0482ffc5, 
              3px 0px 10px 0px #00a6ffc9;
  opacity: 1;
  text-shadow: 1.5px 1.5px 1px #000;
  color: #fff;
  font-size: 1.5rem;
  text-align: center;
  font-weight: bold;
  letter-spacing: .1rem;
  text-transform: uppercase;
  font-family: 'Roboto', sans-serif;
  font-family: 'Roboto Condensed', sans-serif;
  font-family: 'Roboto Slab', serif;
  font-family: 'Roboto Mono', monospace;
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
  display: flex;
  justify-content: center;
  padding-top: 1rem;
  padding-bottom: 1rem;
}

.carousel * {
  overflow: auto;
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
  border-radius: .5rem;
  box-shadow: 0 0 10px 10px #2260777c;
}

img.card-img-top {
  width: 100%;
  aspect-ratio: 1/1;
  object-fit: cover;
  object-position: center;
  border-radius: .5rem;
}

.carousel__img__private {
  opacity: .5;
  filter: grayscale(100%) blur(1px);
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

.masonry-columns {
  columns: 4 200px;
  column-gap: 2rem;
  padding-left: 3em;
  padding-right: 3em;
  padding-top: 1em;
  background-color: transparent;
}

@media screen and (max-width: 768px) {
  .masonry-columns {
    width: 40vw;
    columns: 2 100px;
    padding-left: 1em;
    padding-right: 1em;
    margin-bottom: 3em;
  }

  .profile-name-box {
    font-size: 1.25rem;
    flex-direction: column;
  }
}

</style>