<template>
  <div v-if="keep" :key="keep?.id" class="container-fluid">
    <div class="modal-content" style="">


      <div class="card mb-3" style="">
        <div class="row g-0">
          <div class="col-md-5">
            <img :src="keep.img" class="img-fluid rounded-5 keep-img p-3 pb-0" alt="Keep Image">
          </div>
          <div class="col-md-7">
            <div class="card-body">
              <h5 class="card-title"> {{ keep.name }} </h5>
              <div class="d-flex justify-content-start">
                <i title="View Count" class="col-1 mdi mdi-eye fs-3"></i>
                <p class="col-2 view-count fs-5">: {{ keep?.views }} </p>
                <div class="col-8 d-flex justify-content-end pt-1">
                  <i 
                    :title="`'${keep.name}' is currently stored in ${keep.kept} vaults`" 
                    class="mdi mdi-database text-dark fs-4">
                  </i>
                  <p class="kept-count text-dark fs-5">: {{ keep.kept }} </p>
                </div>
              </div>
              <p class="keep-description card-text"> {{ keep.description }} </p>
              <div class="row">
                <!-- <i :title="`Add '${keep.name}' to a new Vault?'`" class="mdi mdi-plus-circle dropdown-toggle fs-2 text-primary" style="">
                </i> -->
                <div class="drop-up">
                  <form v-if="route.name != 'VaultDetails'" action="" @submit.prevent="addKeepToVault()">
                    <div class="row">
                      <div class="col-6 d-flex h-75 pt-4">
                        <select v-model="selectedOption">
                          <option class="text-center" selected disabled>Choose a Vault</option>
                          <option class="text-center" v-for="v in vaults" :key="v?.id" :value="v?.id" :style="{ backgroundImage: `url(${v.img})` }"> {{ v.name }}</option>
                        </select>
                      </div>
                      <div class="col-6 d-flex justify-content-end">
                        <button type="submit" class="btn btn-primary mt-3" data-bs-dismiss="modal">Add to Vault</button> 
                      </div>
                    </div>
                    </form>
                    <div v-if="vaultKeep && route.name == 'VaultDetails'" class="col-12 d-flex justify-content-end" style="position: relative; bottom: 2rem;">
                      <div class="">
                        <i title="Remove Keep From Vault?" class="delete-icon mdi mdi-file-document-remove text-danger fs-2" style="" @click="removeKeepFromVault()"></i>
                      </div>
                    </div>
                    <div v-else-if="route.name != 'VaultDetails'"></div>
                  </div>
              <p class="card-text"><small class="text-body-secondary"> Created At: {{ new Date(keep.createdAt).toLocaleDateString() }} 
                @ {{ new Date(keep.createdAt).toLocaleTimeString() }} </small></p>
                <router-link :to="{name: 'Profile', params: { profileId: keep.creator.id }}" class="selected">
                  <a @click="goToProfilePage()" href=""> {{ keep.creator.name }} </a>
                  <img :src="keep.creator.picture" class="img-fluid rounded-circle creator-img ms-2" style="aspect-ratio: 1/1; border: 2px double black; height: 35px;" alt="Keep Image">
                </router-link>
            </div>
          </div>
        </div>
      </div>

    </div>





      <!-- <button type="button" class="btn-close p-1 ms-1 mt-1" data-bs-dismiss="modal" aria-label="Close"></button>

      <div class="keep-header mb-4" style="border: none;">
        <h1 class="col-12 keep-title text-center" id="exampleModalLabel"> {{ keep.name }} </h1>
      </div>
      <div class="modal-body row d-flex pt-3">
        <div class="justify-content-between ps-2" style="color: aliceblue; text-shadow: 1px 1px 2px black;">
          <div class="d-flex justify-content-start">
            <i title="View Count" class="col-1 mdi mdi-eye fs-3"></i>
            <p class="col-2 view-count fs-5">: {{ keep?.views }} </p>
            <div class="col-8 d-flex justify-content-end pt-1">
              <i 
                :title="`'${keep.name}' is currently stored in ${keep.kept} vaults`" 
                class="mdi mdi-database text-light fs-4">
              </i>
              <p class="kept-count fs-5">: {{ keep.kept }} </p>
            </div>
            <img class="col-1 d-flex creator-img offset-8 align-items-end img-fluid" :src="keep.creator.picture" :alt="`Image of ${keep.creator.name} failed to load`">
          </div>
        </div>
        <img :src="keep.img" class="keep-img img-fluid" :alt="`Image by: ${keep.creator.name} failed to load`">
        <p class="keep-description pt-3"> {{ keep.description }} </p>
        <div class="modal-footer">
          <div class="col-9 pt-4 me-4">
            <p class="text-start">Published on: {{ keep.createdAt }} </p>
          </div>
        </div> -->
        <!-- <div class="row"> -->
          <!-- <i :title="`Add '${keep.name}' to a new Vault?'`" class="mdi mdi-plus-circle dropdown-toggle fs-2 text-primary" style="">
          </i>  -->
          <!-- <div class="drop-up">
            <form v-if="route.name != 'VaultDetails'" action="" @submit.prevent="addKeepToVault()">
              <div class="row">
                <div class="col-6 d-flex h-75 pt-4">
                  <select v-model="selectedOption">
                    <option class="text-center" selected disabled>Choose a Vault</option>
                    <option class="text-center" v-for="v in vaults" :key="v?.id" :value="v?.id" :style="{ backgroundImage: `url(${v.img})` }"> {{ v.name }}</option>
                  </select>
                </div>
                <div class="col-6 d-flex justify-content-end">
                  <button type="submit" class="btn btn-primary mt-3" data-bs-dismiss="modal">Add to Vault</button> 
                </div>
              </div>
              </form>
              <div v-if="vaultKeep && route.name == 'VaultDetails'" class="col-12 d-flex justify-content-end" style="position: relative; bottom: 2rem;">
                <div class="">
                  <i title="Remove Keep From Vault?" class="delete-icon mdi mdi-file-document-remove text-danger fs-2" style="" @click="removeKeepFromVault()"></i>
                </div>
              </div>
              <div v-else-if="route.name != 'VaultDetails'"></div>
            </div>
          </div> -->
        <!-- </div> -->
      
    </div>
  </div>
</template>

<script>
import { AppState } from "../AppState.js";
import { computed, ref } from 'vue';
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
import { vaultKeepsService } from "../services/VaultKeepsService.js";
import { keepsService } from "../services/KeepsService.js";
import { useRoute, useRouter } from "vue-router";
import { router } from "../router.js";
import { Modal } from "bootstrap";
import { Keep } from "../models/Keep.js";

export default {

  props: {
    kept: {
      type: Keep,
      required: true
    }
  },

  setup() {

    const route = useRoute()
    const router = useRouter()

    const selectedOption = ref({})
    

    return {
      
      route,
      router,
      selectedOption,

      keep: computed(() => AppState.activeKeep),
      vaults: computed(() => AppState.myVaults),
      account: computed(() => AppState.account),
      vault: computed(() => AppState.activeVault),
      vaultKeep: computed(() => AppState.vaultHasKeeps),
      
      async addKeepToVault() {
        try {
          await vaultKeepsService.addKeepToVault(selectedOption.value, AppState.activeKeep.id)
        } catch (error) {
          logger.error(error)
          Pop.error(error.message, 'Error')
        }
      },

      async removeKeepFromVault() {
        try {
          logger.log('debugger working')
          if (await Pop.confirm("Are you sure you want to remove this keep from your vault?", "You can always add it again later if you change your mind.", "Yes, I'm sure", "warning")) {
          const vaultKeep = AppState.vaultHasKeeps.find(vk => vk.id == AppState.activeKeep.id)
          const vaultKeepId = vaultKeep.vaultKeepId
          await vaultKeepsService.removeKeepFromVault(vaultKeepId)
          Pop.toast(`Keep has been removed`, "success", "top")
          window.location.reload()
          // Modal.getOrCreateInstance('#detailsModal').hide()
          }
        } catch (error) {
          logger.error(error)
          Pop.error(error.message)
        }
      },

      async goToProfilePage() {
        Modal.getOrCreateInstance('#detailsModal').hide()
      }
    }
  }
}

</script>

<style scoped lang="scss">

@import url(https://fonts.googleapis.com/css?family=Ubuntu:400,700);
@import "vue-select/dist/vue-select.css";

$line-height: 40px;

::selection { 
  color: #fff;
  background: #9FBE5A;
}

.v-select {
  padding-bottom: 3em;
}

.vs__selected-options {
  background-color: #0297fade;
  border-radius: 1rem;
  padding: 1rem;
  box-shadow: 0 -25px 5px 5px #0297fade, 0 15px 5px 5px #0297fade;
}

.vs__actions {

}

.vs__selected-indicator {
  padding-left: 1em;
}

body {
  width: 100vw;
  height: 100%; 
  padding-bottom: 50px;
  overflow-x: hidden; 
  display: flex;
  align-items: center;
  justify-content: center;
  background: linear-gradient(to bottom, #16a085, #f4d03f) center / cover no-repeat;
  font-family: 'Ubuntu', sans-serif;
}

.btn-close {
  background-color: aliceblue;
}

.keep-header {
  background-color: #0297fade;
  box-shadow: 0 -25px 5px 5px #0297fade, 0 15px 5px 5px #0297fade;
}

.keep-title {
  color: aliceblue;
  font-size: 2.5rem;
  font-family: 'Ubuntu', sans-serif;
  font-weight: 550;
  text-shadow: 2px 2px 3px black;
}

.modal-body {
  background-color: slategray;
  overflow-x: hidden;
  width: 100%;
  --bs-gutter-x: 0;
}

.view-count {
  padding-top: .3rem;
}

.kept-count {
  padding-top: .175rem;
}

.creator-img {
  height: 2rem;
}

.keep-img {
  height: 50vh;
  aspect-ratio: 1/1;
  object-fit: cover;
  border-radius: .8rem;
}

.card-title {
  color: rgb(0, 18, 110);
  text-shadow: 1.5px 1.5px 1px aliceblue;
  font-family: 'Ubuntu', sans-serif;
  padding-bottom: 1.5rem;
}

.keep-description {
  color: rgb(0, 18, 110);
  font-size: 1rem;
  font-family: 'Ubuntu', sans-serif;
  font-weight: 550;
  text-shadow: 1.5px 1.5px 1px aliceblue;
  padding-bottom: 5rem;
}

p.text-secondary {
  padding-top: 3rem;
  color: rgb(0, 18, 110) !important;
  text-shadow: 1.5px 1.5px 1px aliceblue;
  font-family: 'Ubuntu', sans-serif;
}

//.mdi-plus-circle {
//  position: absolute; 
//  bottom: 1rem; 
//  left: 1.1rem; 
//  background: #ffb451dd;
//  width: 25px;
//  height: 25px;
//  border-radius: 50%;
//  text-align: center;
//  line-height: 0px;
//  display: flex;
//  align-items: center;
//  justify-content: center;
//  vertical-align: sub;
//  padding: 17px;
//  transition: all .5s ease-in-out;
//}

//.mdi-plus-circle:hover {
//  animation: growAndBrighten 1s ease-in-out forwards;
//  transition: all .5s ease-in-out;
//}

@keyframes growAndBrighten {
  0% {
    transform: scale(1);
    filter: brightness(1);
  }
  50% {
    transform: scale(1.1);
    filter: brightness(1.1);
  }
  100% {
    transform: scale(1.05);
    filter: brightness(1);
  }
}


@media screen and (max-width: 768px) {
  .view-count {
    padding-top: .1rem;
  }
}

</style>