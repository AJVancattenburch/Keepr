<template>

  <div v-if="profile">
    <p> {{ keep.creator }} </p>
  </div>

</template>


<script>

// import { computed, onMounted } from 'vue'
// import { AppState } from '../AppState'
// import { useRoute } from 'vue-router'
// import { logger } from "../utils/Logger.js"
// import Pop from "../utils/Pop.js"
// import { profilesService } from "../services/ProfilesService.js"

export default {

  setup() {

    // const route = useRoute()

    // async function getProfileById() {
    //   try {
    //     const profileId = route.params.profileId
    //     await profilesService.getProfileById(profileId)
    //   } catch (error) {
    //     logger.log('{ProfilePage} getProfileById() error: ', error)
    //     Pop.error(error.message, 'Error')
    //   }
    // }
    

    // onMounted(() => {
      // getProfileById()
    // })

    return {
      
      // route,
      
      // keep: computed(() => AppState.activeKeep),
      // profile: computed(() => AppState.activeProfile),

    }
  }
}

</script>