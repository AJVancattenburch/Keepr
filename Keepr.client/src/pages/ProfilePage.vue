<template>

  <div v-if="profile" container-fluid>
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












  <section class="row">
    <h2 class="text-white mt-3 text-center">FEATURED TOURNAMENTS</h2>
    <div class="col-12 bg-dark justify-content-center align-items-center shadow-lg elevation-5">
      <Carousel ref="homeCarousel" :itemsToShow="3.95" :wrapAround="true" :transition="500" class="mt-5">
        <Slide v-for="slide in carouselTournaments" :key="slide" class="">
            <div class="card bg-transparent" style="">
              <div class="carousel__item">
                <img :src="slide.img" class="card-img-top pt-5" :alt="slide.name">
                <h3 class="card-title"> {{ slide.name }} </h3>
                <div class="card-body mb-4">
                  <p class="card-text text-light pb-3"> {{ slide.description.split(' ').splice(0, 19).join(' ') }}... </p>
                </div>
              </div>
            </div>
        </Slide>
      </Carousel>
    </div>
    <!-- NOTE - ACCENT ROW BETWEEN CAROUSEL AND PREVIOUS / NEXT SLIDE BUTTONS -->
    <div class="row accent-row d-flex mb-5 pb-2 pt-2" style="height: 4rem;">
    <!-- NOTE - PREVIOUS / NEXT SLIDE BUTTONS -->
    <div class="col-6 col-md-6 d-flex justify-content-center" style="position: absolute; top: -1.5rem; left: 0;">
      <button type="button" class="btn btn-info neon-button me-5" style="width: 10vw;" @click="homeCarousel.prev()">Previous Slide</button>
    </div>
    <div class="col-6 col-md-6 d-flex justify-content-center" style="position: absolute; top: -1.5rem; right: 0;">
      <button type="button" class="btn btn-info neon-button ms-5" style="width: 10vw;" @click="homeCarousel.next()">Next Slide</button>
    </div>
  </div>
</section>

</template>


<script>

import { computed, onMounted } from 'vue'
import { AppState } from '../AppState'
import { useRoute } from 'vue-router'
import { logger } from "../utils/Logger.js"
import Pop from "../utils/Pop.js"
import { profilesService } from "../services/ProfilesService.js"

export default {

  setup() {

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
      
      keep: computed(() => AppState.activeKeep),
      profile: computed(() => AppState.activeProfile),

    }
  }
}

</script>

<style scoped lang="scss">

.cover-img {
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

</style>