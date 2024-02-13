<template>

  <section class="container-fluid pb-5">
    <div class="row">
      <div class="col-12 pt-5 d-flex">
        <button v-if="account.id == vault?.creator.id" title="Delete Vault?" type="button" class="btn btn-danger" style="opacity:.8;" @click="deleteVault(vault.id)">Delete Vault</button>
      </div>
    </div>
    <div class="row">
      <div v-if="vault" class="col-12 pt-5 mt-5">
        <div class="col-12 d-flex justify-content-center align-items-center">
          <h1 class="vault-name py-3"> {{ vault.name }} </h1>
        </div>
        <div class="col-12 d-flex justify-content-center align-items-center">
          <img :src="vault.img" class="vault-img img-fluid rounded-4" :alt="`Vault Image for ${vault.name}`" />
        </div>
        <div class="col-12 d-flex justify-content-center align-items-center">
          <p class="vault-description p-5 fs-5"> {{ vault.description }} </p>
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

    <section class="">
      <div class="masonry-columns" id="masonry-columns">
        <div v-for="k in vaultKeep" :key="k.id">
          <KeepCard :keep="k"  class="pb-2"/>
        </div>
      </div>
    </section>
    
  </section>

<!-- ** VAULTKEEP DATA **
  ** description: "Before colleges, quilts were only quills. However, some nosey salts are thought of simply as clubs. Some morose cabinets are thought of simply as swisses. As far as we can estimate, some swingeing chiefs are thought of simply as kicks."
  ** id: 1
  ** img: "https://images.unsplash.com/photo-1556711905-4bd1b6603275?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=410&h=860&q=60"
  ** kept: 0
  ** name: "Mindful"
  ** createdAt: "2023-07-15T06:02:59"
  ** updatedAt: "2023-07-20T02:14:04"
  ** vaultKeepId: 1
  ** views: 9

** VAULTKEEP'S USER DATA **
  * CREATOR'S ID: "647fe77f07eaa2e6662ac239",
    - name: "Unicorn Sparkle",…
    - bio: "Likes bees!"
    - coverImg: "https://media.istockphoto.com/id/1432570871/photo/honey-bees-flying-into-wooden-beehives.jpg?s=2048x2048&w=is&k=20&c=IqJxuzR80SwgHpNYw-1lQSgof3qzm8aBbc_a5ekchIM="
    - id: "647fe77f07eaa2e6662ac239"
    - name: "Unicorn Sparkle"
    - picture: "https://media.istockphoto.com/id/1396407304/photo/insects-of-europe-bees-side-view-macro-of-western-honey-bee-isolated-on-white-background-with.jpg?s=2048x2048&w=is&k=20&c=eo77Wnv7b9qe6yh_mDhsJ3aFKE7QjHIEclu1W0mgHnc="
    - creatorId: "647fe77f07eaa2e6662ac239" -->



</template>


<script>
import { AppState } from "../AppState.js";
import { computed, onMounted } from "vue";
import { vaultsService } from "../services/VaultsService.js";
import { vaultKeepsService } from "../services/VaultKeepsService.js"
import { useRoute, useRouter } from "vue-router";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
import KeepCard from "../components/KeepCard.vue";


export default {

  setup() {

    const route = useRoute()
    const router = useRouter()

    onMounted(async() => {
      try {
        await vaultsService.getVaultById(route.params.vaultId)
        await vaultKeepsService.getKeepsByVaultId(route.params.vaultId)
        // Pop.toast(`Welcome to ${AppState.account.name}'s vault!`, 'success', 'top-end', 3000)
        AppState.vaultKeeps.forEach(k => logger.log(k))
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

      async deleteVault(vaultId) {
        try {
          if (await Pop.confirm('⚠️ Delete this vault? ⚠️', '🛑 THIS CANNOT BE UNDONE 🛑', 'Yes, delete it!')) {
          await vaultsService.deleteVault(vaultId)
          Pop.toast(`Vault Deleted!`, 'success', 'top-end', 2000)
          router.push({ name: 'Account' })
          }
        } catch (error) {
          router.push({ name: 'Home' })
          Pop.toast(`[RESTRICTED] UNAUTHORIZED!`, 'error', 'top-end', 3000)
        }
      },
    }
  },

  components: {
    KeepCard
  }

}

</script>


<style scoped lang="scss">

.container-fluid {
  height: 100%;
  background-color: #0482ffc9;
  box-shadow: 0 0 10px 10px #271f1841 inset;
  padding-top: 2rem;
  padding-left: 1rem;
  padding-bottom: 5rem !important;
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

article {
  display: flex;
  flex-direction: column;
  background-color: #eaa865ea;
  box-shadow: -5px 5px 10px 10px #eaa865f9, 
              0 0 10px 10px #eaa765c4 inset;
  border-top-left-radius: .5rem;
  padding-top: 2rem;
  padding-left: 1rem;
  padding-bottom: 0rem;
  padding-right: 1rem;
}

.vault-name {
  text-shadow: 1px 1px 2px white;
}

.vault-description {
  color: rgb(255, 255, 255);
  text-shadow: 1.5px 1.5px 0px #eaa865f9;
  font-size: 1.25rem;
  font-weight: 550;
  line-height: 1.5rem;
  padding: 4rem;
  margin: 1rem auto;
  background: linear-gradient(to bottom, #eaa865b8, #0d3c65b5) center / cover no-repeat;
  border-radius: .5rem;
  box-shadow:inset  0px 1px 10px 1px #1660a0;
  border: 1px solid #0c323f5c;
  opacity: .95;
  filter: blur(0.25px) brightness(1.1);
}

.vault-creator-info {
  position: absolute;
  top: 3.5rem;
  right: .75rem;
  line-height: .25rem;
  font-size: small;
  font-weight: 550;
  color: #040a1a;
  text-shadow: 1.5px 1.5px 1px #bfcdef5c;
}

.masonry-columns {
  columns: 3 200px;
  column-gap: 3rem;
  padding-left: 5em;
  padding-right: 5em;
  padding-top: 1em;
}

@media screen and (max-width: 768px) {
  .masonry-columns {
    columns: 2 100px;
    padding-left: 1em;
    padding-right: 1em;
    margin-bottom: 3em;
  }
  
  article {
    display: none;
  }
}

</style>