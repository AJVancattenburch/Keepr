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
          <h2 class="bio-title text-center pt-4" style="">My Bio:</h2>
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
      <h4 class="carousel__title py-3 mt-3 text-center text-uppercase"> My Vaults: {{ vaultCount}} </h4>
      <div class="col-12 m-auto bg-dark justify-content-center align-items-center shadow-lg elevation-5">
        <Carousel ref="myCarousel" :itemsToShow="3.95" :wrapAround="true" :transition="500" class="carousel-items-to-show mt-3">
          <Slide v-for="slide in carouselVaults" :key="slide" class=" ">
              <div class="carousel__card bg-transparent" style="height: 100%;">
                <div class="carousel__item">
                  <div v-if="account.id == slide.creatorId" class="d-flex justify-content-end">
                    <i v-if="slide" :key="slide.id" title="Delete Vault?" class="delete-icon mdi mdi-file-document-remove selectable fs-2" style="" @click="deleteVault(slide.id)"></i>
                  </div>
                  <router-link :to="{ name: 'VaultDetails', params: { vaultId: slide.id }}">
                    <img :title="`Go to the Vault Page for '${slide.name}'`" v-if="carouselVaults" @click="getVaultById(slide.id)" :src="slide.img" class="carousel__img card-img-top selectable pt-1" :alt="slide.name">
                  </router-link>
                  <div class="my-3 mb-4 rounded-5" style="">
                    <div class="content__box pb-3">
                      <h3 class="card-title"> {{ slide.name }} </h3>
                      <div class="card-body">
                        <p class="card-text"> {{ slide.description.split(' ').splice(0, 3).join(' ') }}... </p>
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

    <div class="row">
      <h2 class="keep-count mt-3 text-center text-uppercase py-4"> My Keeps: {{ keepCount}} </h2>
    </div>
    <div class="masonry-columns">
      <div v-for="k in keeps" :key="k.id" class="">
        <KeepCard :keep="k" class=""/>
      </div>
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
        await vaultsService.getMyVaults()
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

    async function deleteVault(vaultId) {
      try {
        if (await Pop.confirm(`Are you sure you want to delete this vault?`)) {
          await vaultsService.deleteVault(vaultId)
        }
          Pop.toast(`${AppState.myVaults.name} has been deleted!`, 'Vault Deleted', 'bg-danger', 'text-light', 'top-end', 2000)
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

      deleteVault,
      getVaultById,

      account: computed(() => AppState.account),
      user: computed(() => AppState.user),
      vault: computed(() => AppState.myVaults),
      keepCount: computed(() => AppState.myKeeps.length),
      vaultCount: computed(() => AppState.myVaults.length),

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


.container-fluid {
  height: 100%;
  background-color: #0482ffc9;
  box-shadow: 0 0 10px 10px #271f1841 inset;
  padding-top: 2rem;
  padding-left: 1rem;
  padding-bottom: 5rem !important;
  padding-right: 2rem;
}

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
  animation: fadeIn 1s ease-in-out 2s forwards;
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
  color: #002fb2;
  text-shadow: 2px 2px 2px #ffaa5a;
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

.bio-title {
  color: #002fb2;
  text-shadow: 1.5px 1.5px 2px #ffaa5a;
  padding-bottom: 2rem;
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
  text-align: center;
  background: radial-gradient(ellipse at center, #f7fbffd5 0%, 
                                                 #95ffda63 100%);
  box-shadow: inset 3px 3px 10px 3px #222629d7, 
              inset -3px -3px 10px 3px #222629c9, 
                     3px 3px 10px 3px #bef5f9e6, 
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
  height: 45vh;
  margin-bottom: 1rem;
}

.carousel__title {
  color: #fff;
  font-size: 4rem;
  text-shadow: 2.5px 2.5px 1px #000;
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
  font-size: 1.25rem;
  padding-bottom: 1rem;
  height: 75px;
}

.carousel__img {
  border-radius: 1rem;
  height: 25vh;
  object-fit: cover;
  object-position: center;
}

.content__box {
  background: linear-gradient(145deg, #111927, 
  #1f2f49 100%);
}

.keep-count {
  color: #fff;
  font-size: 4rem;
  width: 100%;
  text-shadow: 2.5px 2.5px 1px #000;
  background: linear-gradient(145deg, #5cc0fe71, 
                                      #faca1c13 100%);
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
}

@media screen and (max-width: 768px) {
  .masonry-columns {
    width: 40vw;
    columns: 2 100px;
    padding-left: 1em;
    padding-right: 1em;
    margin-bottom: 3em;
  }

  .carousel-items-to-show {
    width: 225vw;
    object-fit: contain;
  }

  .carousel__title, .keep-count {
    font-size: 2rem;
  }

  .my-name {
    font-size: 2rem;
  }

  .bio-container {
    width: 75vw;
    height: 20vh;
    margin-right: auto;
    margin-left: -3.5rem;
  }
  
}

</style>
