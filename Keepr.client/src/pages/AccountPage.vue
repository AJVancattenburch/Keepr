<template>
  <div class="col-12">
    <img class="cover-img" style="width: 100%; height: 500px;" :src="account.coverImg" :alt="`${account.name}'s Cover Image`">
  </div>
  <section v-if="account" :key="account?.id" class="container-fluid">
    <i @click="showEditAccountForm()" role="button" class="mdi mdi-dots-horizontal fs-2"></i>

    <div class="">
      <div class="col-12 m-auto" style="width: 300px; position: relative; bottom: 5rem;">
        <img class="account-picture rounded img-fluid" :src="account.picture" alt="" />
      </div>
      <h1 class="my-name text-center p-3">Welcome {{ account.name }}</h1>
      <div class="row flex-row justify-content-center">
        <div class="col-6">
          <p class="bio-container">{{ account.bio }}</p>
        </div>
      </div>
    </div>

    <!-- <div class="row">
      <div class="col-12">
        <h1 class="text-center">Vaults</h1>
      </div>
      <div v-for="v in vaults" :key="v.id" class="col-4">
        <VaultCard :vault="v" />
      </div>
    </div> -->

    <section class="row" style="overflow-x: hidden;">
      <h2 class="carousel__title mt-3 text-center text-uppercase"> My Vaults </h2>
      <div class="col-12 m-auto bg-dark justify-content-center align-items-center shadow-lg elevation-5">
        <Carousel ref="myCarousel" :itemsToShow="3.95" :wrapAround="true" :transition="500" class="mt-3">
          <Slide v-for="slide in carouselVaults" :key="slide" class=" ">
              <div class="carousel__card bg-transparent" style="">
                <div class="carousel__item">
                  <router-link :to="{ name: 'VaultDetails', params: { vaultId: slide.id }}">
                  <img v-if="carouselVaults" @click="getVaultById(slide.id)" :src="slide.img" class="carousel__img card-img-top selectable pt-1" :alt="slide.name">
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

    <!-- <div v-if="keeps" :key="keeps?.id" class="row">
      <div class="col-12">
        <h1 class="text-center">Keeps</h1>
      </div> -->
      <div v-for="k in keeps" :key="k.id" class="col-4">
        <KeepCard :keep="k" />
      </div>


  </section>

</template>

<script>

import { computed, onMounted, ref } from 'vue';
import { AppState } from '../AppState';
import { logger } from "../utils/Logger.js";
import { vaultsService } from "../services/VaultsService.js";
import Pop from "../utils/Pop.js";
import { Modal } from "bootstrap";
// import VaultCard from '../components/VaultCard.vue';
import { Slide, Carousel } from "vue3-carousel";
import { keepsService } from "../services/KeepsService.js";
import KeepCard from '../components/KeepCard.vue';

export default {

  components: {
    // VaultCard,
    KeepCard,
    Slide,
    Carousel,
  },

  setup() {

    const myCarousel = ref(null)

    async function getMyVaults() {
      try {
        const accountId = AppState.account.id
        await vaultsService.getMyVaults(accountId)
      } catch (error) {
        Pop.error(error.message, 'Error')
        logger.log(error)
      }
    }

    async function getVaultById(vaultId) {
      try {
        if (AppState.account.id == AppState.myVaults.id) {
          AppState.myVaults = AppState.myVaults.filter(
            v => v.id == vaultId
          ).sort(
            (a, b) => b.createdAt - a.createdAt
          )
        }
        logger.log('[ACCOUNT PAGE] => getVaultById() vaultId: ', vaultId)
        await vaultsService.getVaultById(vaultId)
      } catch (error) {
        Pop.error(error.message, 'Error')
        logger.log(error)
      }
    }

    async function getMyKeeps() {
      try {
        const accountId = AppState.account.id
        await keepsService.getMyKeeps(accountId)
      } catch (error) {
        Pop.error(error.message, 'Error')
        logger.log(error)
      }
    }

    onMounted(() => {
      getMyVaults()
      getMyKeeps()
    })


    return {

      myCarousel,

      getVaultById,

      account: computed(() => AppState.account),
      vault: computed(() => AppState.myVaults),
      user: computed(() => AppState.user),

      carouselVaults: computed(() => AppState.myVaults.sort(
        (a, b) => a.createdAt - b.createdAt
      )),
      keeps: computed(() => AppState.myKeeps),

      async showEditAccountForm() {
        try {
          Modal.getOrCreateInstance('#editAccountForm').show()
        } catch (error) {
          Pop.error(error.message, 'Error')
          logger.log(error)
        }
      },
    }
  }
}
</script>

<style scoped lang="scss">

@import url(https://fonts.googleapis.com/css?family=Ubuntu:400,700);


.cover-img {
  object-fit: cover;
  object-position: center;
}

.account-picture {
  width: 100%;
  height: 200px;
  object-fit: cover;
  border-radius: 50%;
  border: 2px solid #205bff4f;
  box-shadow: 3px 3px 3px #cbced1, 
             -3px -3px 3px white;
  display: flex;
  margin: 0 auto;
  animation: fadeIn 1s ease-in-out 3s forwards;
  opacity: 0;
}

@keyframes fadeIn {
  0% {
    opacity: 0;
  }
  100% {
    opacity: 1;
  }
}

.my-name {
  font-family: 'Ubuntu', sans-serif;
  font-size: 3rem;
  font-weight: 700;
  color: #205bff;
  text-shadow: 1px 1px 4px #fe7c02;
  animation: slideThenFadeIn 3s ease-in-out;
}

@keyframes slideThenFadeIn {
  0% {
    transform: translateX(-100%);
    opacity: 0;
  }
  50% {
    transform: translateX(0);
    opacity: .1;
  }
  100% {
    transform: translateX(0);
    opacity: 1;
  }
}

.bio-container {
  font-family: 'Ubuntu', sans-serif;
  font-size: 1.05rem;
  font-weight: 500;
  color: #000000;
  text-shadow: 1px 1px 1px aliceblue;
  min-width: 100%;
  height: 200px;
  padding: 2rem;
  margin-left: -1rem;
  margin-bottom: 5rem;
  background: radial-gradient(ellipse at center, #51fec14d 0%, 
                                                 #1c8ffa3e 100%);
  box-shadow: inset 3px 3px 10px 3px #222629d7, 
              inset -3px -3px 10px 3px #222629c9, 
                     3px 3px 10px 3px #bef5f9c9, 
                    -3px -3px 10px 3px #bef5f9c9;
  border-radius: .5rem;
}

.mdi-dots-horizontal {
  color: aliceblue;
  text-shadow: 1px 1px .5px #000;
  position: absolute;
  right: 1rem;
  top: 5rem;
}


.carousel * {
  overflow: hidden;
  text-align: center;
}

.carousel__card {
  width: 100%;
  height: 75%;
  padding: 1rem;
  margin-left: 1rem;
  margin-right: 1rem;
  background-color: #000;
  border-radius: 2rem;
  box-shadow: 0 0 5px 5px #2260777c;
}

.carousel__item {
  height: 65vh;
  margin-bottom: 1rem;
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

.card-title {
  padding-top: 1rem;
  padding-bottom: 1rem;
}

.carousel__img {
  border-radius: 1rem;
}

.content__box {
  background: linear-gradient(145deg, #111927, 
  #1f2f49 100%);
}

</style>
