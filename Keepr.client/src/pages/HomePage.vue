<template>

  <div class="container-fluid">    
    <section class="row mt-3 overflow-hidden">
      <div class="masonry-columns" id="masonry-with-columns">
        <div v-for="(keep, index) in keeps" :key="index" class="">
          <KeepCard :keep="keep" class=""/>
        </div>
      </div>
    </section>
  </div>

</template>


<script>

import { AppState } from "../AppState.js";
import KeepCard from '../components/KeepCard.vue';
import { computed, onMounted } from 'vue';
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";
import { keepsService } from "../services/KeepsService.js";
// import { useRoute } from 'vue-router'
// import { profilesService } from "../services/ProfilesService.js";

export default {

  components: {
    KeepCard,
  },

  setup() {


    async function getAllKeeps() {
      try {
        await keepsService.getAllKeeps()
      } catch (error) {
          Pop.error(error.message, 'Error')
          logger.log(error)
      }
    }

    // async function getProfileById() {
    //   try {
    //     const profileId = route.params.profileId
    //     await profilesService.getProfileById(profileId)
    //   } catch (error) {
    //     logger.log('{ProfilePage} getProfileById() error: ', error)
    //     Pop.error(error.message, 'Error')
    //   }
    // }

    onMounted(() => {
      getAllKeeps()
    })

    return {

      // route,
      // getProfileById,

      keeps: computed(() => AppState.keeps)

    }
  }
}

</script>


<style scoped lang="scss">



.masonry-columns {
  columns: 4 200px;
  column-gap: 2rem;
  padding-left: 3em;
  padding-right: 3em;
  padding-top: 1em;

  .keeps-card {
    width: 150px;
    color: white;
    margin: 0 1rem 1rem 0;
    display: inline-block;
    width: 100%;
    text-align: center;
    font-family: system-ui;
    font-weight: 900;
    font-size: 2rem;
    border-radius: 8px;
  }
}

.keeps-card {
  position: relative;
}

.keeps-card:hover {
  cursor: pointer;
  transform: scale(1.025);

}


@media screen and (max-width: 768px) {
  .masonry-columns {
    columns: 2 100px;
    padding-left: 1em;
    padding-right: 1em;
    margin-bottom: 3em;
  }
}

</style>
