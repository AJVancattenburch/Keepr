<template>
  
    <!-- <button class="btn selectable text-success lighten-30 text-uppercase my-2 my-lg-0 rounded-circle" @click="login"
      v-if="!user.isAuthenticated">
      <i title="Login" class="mdi mdi-login"></i>
    </button> -->
    <div>
      <div class="dropdown my-2 my-lg-0">
        <div type="button" class="border-0 selectable no-select" data-bs-toggle="dropdown" aria-expanded="false">
          <div v-if="account.picture || user.picture">
            <img :src="account.picture || user.picture" :title="`Logged In as ${account.name}`" alt="account photo" height="40" class="rounded-circle mb-2" />
          </div>
        </div>
        <div class="dropdown-menu dropdown-menu-lg-end dropdown-menu-start p-0" aria-labelledby="authDropdown">
          <div class="list-group">
            <router-link :to="{ name: 'Account' }">
              <div class="list-group-item dropdown-item list-group-item-action" style="text-shadow: .5px .5px 1px black;">
                Manage Account
              </div>
            </router-link>
            <!-- <div class="list-group-item dropdown-item list-group-item-action text-danger selectable" style="text-shadow: .5px .5px 1px black;" @click="logout">
              <i class="mdi mdi-logout"></i>
              logout -->
            <!-- </div> -->
          </div>
        </div>
      </div>
    </div>
</template>

<script>
import { computed } from 'vue'
import { AppState } from '../AppState'
import { AuthService } from '../services/AuthService'
export default {
  setup() {
    return {

      user: computed(() => AppState.user),
      account: computed(() => AppState.account),

      async login() {
        AuthService.loginWithPopup()
      },
      async logout() {
        AuthService.logout({ returnTo: window.location.origin })
      }
      
    }
  }
}
</script>

<style lang="scss" scoped></style>
