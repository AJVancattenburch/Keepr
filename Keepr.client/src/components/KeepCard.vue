<template>
  
  <div class="keep-container">
    <div class="title-container mt-5">
      <h5 class="keep-title">{{ keep.name }}</h5>
      <router-link :to="{name: 'Profile', params: { profileId: keep.creator.id }}" class="selectable">
      <img :src="keep.creator.picture"
        :alt="keep.creator.name" :title="keep.creator.name">
      </router-link>
    </div>
    <div @click="getKeepById(keep?.id)" data-bs-target="#detailsModal" data-bs-toggle="modal">
      <img :src="keep.img" :alt="keep.name" class="keep-img">
    </div>
    <div v-if="keep.creator.id == account.id">
      <div class="d-flex justify-content-between">
        <!-- <button class="btn btn-success" style="position: relative; bottom: 3rem; left: .5rem;" @click="editKeep(keep.id)">Edit</button> -->
        <i v-if="keep" :key="keep.id" title="Delete Keep?" class="delete-icon mdi mdi-file-document-remove fs-2" style="" @click="deleteKeep(keep.id)"></i>
      </div>
    </div>
  </div>

</template>


<script>

import { Modal } from "bootstrap";
import { Keep } from "../models/Keep.js";
import { keepsService } from "../services/KeepsService.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
import { computed } from 'vue';
import { AppState } from '../AppState.js';

export default {

  props: {
    keep: {
      type: Keep,
      required: true
    }
  },

  setup(props) {

    // const route = useRoute()

    return {

      account: computed(() => AppState.account),
      
      getKeepById(keepId) {
        try {
          logger.log('[KEEPCARD] => getKeepById() keepId: ', keepId)
          keepsService.getKeepById(keepId)
          Modal.getOrCreateInstance('#detailsModal').show()
        } catch (error) {
          Pop.error(error.message, 'Error')
          logger.log(error)
        }
      },

      async deleteKeep(keepId) {
        try {
          if (await Pop.confirm(`Are you sure you want to delete ${props.keep.name}?`) && keepId == props.keep.id) {
             keepsService.deleteKeep(keepId)
          }
          Pop.toast(`Deleted ${props.keep.name}`, 'success', 'top-end', 1000)
        } catch (error) {
          Pop.error(error.message, 'Error')
          logger.log(error)
        }
      }
    }
  }
}


</script>


<style scoped lang="scss">

@import url(https://fonts.googleapis.com/css?family=Ubuntu:400,700);
@import url(https://weloveiconfonts.com/api/?family=entypo|fontawesome|zocial);

.keep-container {
  position: relative;
  width: 24vw;
  height: 100%;
  margin: 0 auto;
  margin-bottom: -1rem;
  border-radius: .5rem;
  overflow: hidden;
  transition: all .3s ease-in-out;
  animation: fadeAndDropIn 1s ease-in-out forwards;
}

@keyframes fadeAndDropIn {
  0% {
    opacity: .3;
    height: 6vh;
  }
  50% {
    opacity: .5;
    height: 50vh;
  }
  100% {
    opacity: 1;
    height: auto;
  }
}

.keep-img {
  width: 90%;
  background-size: cover;
  background-position: center;
  border-right: 3px solid #ffb451;
  border-left: 3px solid #ffb451;
  border-bottom: 3px solid #ffb451;
  border-radius: 0 0 .5rem .5rem;
  transition: all .3s ease-in-out;
}

.keep-img:hover {
  filter: brightness(1.1);
  cursor: pointer;
  transition: all .3s ease-in-out;
}

.title-container {
  position: relative;
  width: 90%;
  background: radial-gradient(circle, #ffac40b0 0%, #517df4 100%);
  display: flex;
  justify-content: space-between;
  padding: .5em;
  align-items: center;
  border-top-left-radius: .5rem;
  border-top: 3px solid #ffb451;
  border-right: 3px solid #ffb451;
  border-left: 3px solid #ffb451;
}

.title-container img {
  width: 2rem;
  height: 2rem;
  border-radius: 100%;
  object-fit: cover;
  object-position: center;
}

.keep-title {
  font-family: 'Ubuntu', sans-serif;
  margin-bottom: 0px;
  text-shadow: 2px 2px 2px black;
  color: aliceblue;
}

.delete-icon {
  height: 50px;
  position: absolute;
  bottom: 1rem; 
  right: 3.5rem; 
  color: #ff2f00;
  filter: brightness(1.3) 
          drop-shadow(1.5px 1.5px .5px #ffae00);
}

.delete-icon:hover {
  animation: shake .5s ease-in-out forwards;
}

@keyframes shake {
  0% { 
    transform: translate(1px, 1px) rotate(0deg); 
  }
  10% { 
    transform: translate(-1px, -2px) rotate(-1deg); 
    color: #ffa600;
  }
  20% { 
    transform: translate(-3px, 0px) rotate(1deg); 
    color: #ff7400;
  }
  30% { 
    transform: translate(3px, 2px) rotate(0deg); 
    color: #ff4d00;
  }
  40% { 
    transform: translate(1px, -1px) rotate(1deg); 
    color: #ff1a00;
  }
  50% { 
    transform: translate(-1px, 2px) rotate(-1deg); 
    color: #ff0000;
  }
  60% { 
    transform: translate(-3px, 1px) rotate(0deg); 
    color: #ff1a00;
  }
  70% { 
    transform: translate(3px, 1px) rotate(-1deg); 
    color: #ff4d00;

  }
  80% { 
    transform: translate(-1px, -1px) rotate(1deg); 
    color: #ff7400;
  }
  90% { 
    transform: translate(1px, 2px) rotate(0deg); 
    color: #ffa400;
  }
  100% { 
    transform: translate(1px, -2px) rotate(-1deg);
    color: #ff4d00;
    filter: brightness(1.5);
  }
}

@media screen and (max-width: 768px) {

  .keep-container {
    width: 45vw;
  }
  .title-container {
    padding-bottom: .4em;
    font-size: xx-small;
    box-sizing: border-box;
    padding-bottom: 1em;
  }
  .keep-img {
    width: relative;
  }

}

</style>