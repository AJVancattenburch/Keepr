<template>
  
  <div class="keep-container">
    <div class="title-container">
      <h5 class="col-10 keep-title text-center pe-1">{{ keep.name }}</h5>
      <div class="d-flex align-items-center col-2">
        <router-link :to="{name: 'Profile', params: { profileId: keep.creator.id }}" class="selected">
        <img :src="keep.creator.picture"
          :alt="`Profile page for ${keep.creator.name}`" 
          :title="`Profile page for ${keep.creator.name}`"
          class="">
        </router-link>
      </div>
    </div>
    <div @click="getKeepById(keep?.id)" data-bs-target="#detailsModal" data-bs-toggle="modal">
      <img :src="keep.img" :alt="keep.name" class="keep-img">
    </div>
    <div v-if="keep.creator.id == account.id">
      <div class="d-flex justify-content-between">
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

.selected {
  text-decoration: none;
  color: #ffffff;
  font-weight: 700;
  font-size: 1.25rem;
  transition: all .3s ease-in-out;
}

.keep-container {
  position: relative;
  top: 3rem;
  padding-top: 2rem;
  width: 24.5vw;
  height: 100%;
  margin: 0 auto;
  margin-bottom: 1rem;
  border-radius: .5rem;
  overflow: hidden;
  transition: all .3s ease-in-out;
  animation: fadeIn 2s ease-in-out forwards;
}

@keyframes fadeIn {
  0% {
    opacity: 0;
  }
  50% {
    opacity: .5;
  }
  100% {
    opacity: 1;
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
  position: absolute;
  width: 90%;
  height: 20%;
  background: radial-gradient(circle, 
                            #ffac401b 0%, 
                            #517cf434 100%);
  display: flex;
  padding: .5em;
  align-items: center;
  border-top-left-radius: .5rem;
  border-top: 3px solid #ffb451;
  border-right: 3px solid #ffb451;
  border-left: 3px solid #ffb451;
  white-space: nowrap;
  z-index: 1;
}

.title-container img {
  margin-top: 3.25rem;
  width: 2rem;
  height: 2rem;
  border-radius: 100%;
  object-fit: cover;
  object-position: start;
  border: 2px solid #ffb451;
}

.keep-title {
  box-sizing: border-box;
  display: inline-block;
  font-family: 'Ubuntu', sans-serif;
  margin-top: 25px;
  text-shadow: 3px 3px 2px #000000;
  word-wrap: break-word;
  color: #ffffffff;
  font-size: 1.25rem;
  font-weight: 700;
  display: flex;
  justify-content: center;
  align-items: center;
}

.delete-icon {
  height: 50px;
  position: absolute;
  bottom: 1rem; 
  right: 4.5rem; 
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

@media screen and (min-width: 1200px) {
  .keep-title {
    padding-bottom: .4em;
    font-size: 2rem;
    box-sizing: border-box;
  }

  .title-container img {
    margin-bottom: 3rem;
    width: 3rem;
    height: 3rem;
  }
}

</style>