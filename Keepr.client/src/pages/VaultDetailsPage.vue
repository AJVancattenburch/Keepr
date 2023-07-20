<template>

  <section class="container-fluid">
    <div class="row">
      <div v-if="vault" class="col-12 pt-5 mt-5">
        <div class="col-12 d-flex justify-content-center align-items-center">
          <h1 class="vault-name py-3"> {{ vault.name }} </h1>
        </div>
        <div class="col-12 d-flex justify-content-center align-items-center">
          <img :src="vault.img" class="vault-img img-fluid rounded-4" :alt="`Vault Image for ${vault.name}`" />
        </div>
        <div class="col-12 d-flex justify-content-center align-items-center">
          <p class="vault-description pt-5 py-3 fs-5"> {{ vault.description }} </p>
        </div>
        <article class="row vault-creator-info">
          <p class="created-at"> Published: {{ new Date(vault.createdAt).toLocaleDateString() }} 
                                          @ {{ new Date(vault.createdAt).toLocaleTimeString() }}  </p>
          <p class="updated-at"> Updated: {{ new Date(vault.updatedAt).toLocaleDateString() }} 
                                        @ {{ new Date(vault.updatedAt).toLocaleTimeString() }} </p>
          <p class="creator-name"> Creator: {{ vault.creator.name }} </p>
        </article>
      </div>
    </div>
    <div class="row">
      <div v-for="k in vaultKeep" :key="k?.id" class="col-4">
        <p> {{ keep }} </p>
      </div>
    </div>
  </section>

</template>


<script>
import { AppState } from "../AppState.js";
import { computed, onMounted } from "vue";
import { vaultsService } from "../services/VaultsService.js";
import { vaultKeepsService } from "../services/VaultKeepsService.js"
import { useRoute, useRouter } from "vue-router";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
// import KeepCard from "../components/KeepCard.vue";


export default {

  setup() {

    const route = useRoute()
    const router = useRouter()

    onMounted(async() => {
            try {
              await vaultsService.getVaultById(route.params.vaultId)
              await vaultKeepsService.getKeepsByVaultId(route.params.vaultId)
              // Pop.toast(`Welcome to ${AppState.account.name}'s vault!`, 'success', 'top-end', 3000)
            } catch (error) {
              router.push({ name: 'Home' })
              Pop.toast("[RESTRICTED] UNAUTHORIZED!", 'error', 'top-end', 3000)
              // router.push({ name: 'VaultDetails', params: { vaultId: route.params.id }})
            }
        })

    return {

      route,

      account: computed(() => AppState.account),
      keep: computed(() => AppState.keeps),
      vault: computed(() => AppState.activeVault),
      vaultKeep: computed(() => AppState.vaultHasKeeps),

    }
  },
  components: {
    // KeepCard,
  }
}

</script>



<style scoped lang="scss">

.container-fluid {
  height: 100%;
  background-color: #0482ffc5;
  box-shadow: 0 0 10px 10px #271f1841 inset;
  padding-top: 2rem;
  padding-left: 1rem;
  padding-bottom: 2rem;
  padding-right: 2rem;
}

.vault-img {
  width: 50vh;
  aspect-ratio: 1/1;
  object-fit: cover;
  object-position: center;
  box-shadow: 3px 3px 10px 3px #425890ea;
  border: 1px solid #0c1b3f5c;
}

.vault-description {
}

article {
  display: flex;
  flex-direction: column;
  background-color: #fab46ee5;
  box-shadow: 0 0 10px 10px #eaa865ea, 0 0 10px 10px #eaa765c4 inset;
  border-top-left-radius: .5rem;
  padding-top: 2rem;
  padding-left: 1rem;
  padding-bottom: 1rem;
  padding-right: 1rem;
}

.vault-creator-info {
  position: absolute;
  bottom: 0;
  right: .75rem;
  line-height: .25rem;
  font-size: small;
  font-weight: 550;
  color: #040a1a;
  text-shadow: 1.5px 1.5px 1px #bfcdef5c;
}

</style>