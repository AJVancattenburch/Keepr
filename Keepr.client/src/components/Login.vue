<template>
  <span class="navbar-text">
    <button class="btn selectable text-success lighten-30 text-uppercase my-2 my-lg-0" @click="login"
      v-if="!user.isAuthenticated">
      <i title="Log In..." class="mdi mdi-login" style="filter: brightness(1.3)"></i>
    </button>
    <div v-else>
      <div class="dropdown my-lg-0">
        <div type="button" class="border-0 selectable no-select rounded-circle" data-bs-toggle="dropdown" aria-expanded="false">
          <div v-if="account.name || user.name">
            <i :title="account.name || user.name" height="40" class="mdi mdi-account-badge fs-1" style="color: #227f2b !important; text-shadow: 1.5px 1.5px 1px #160101; filter: brightness(1);" ></i>
          </div>
        </div>
        <div class="dropdown-menu dropdown-menu-lg-end dropdown-menu-start p-0" aria-labelledby="authDropdown">
          <div class="list-group">
            <router-link :to="{ name: 'Account' }">
              <div class="list-group-item dropdown-item list-group-item-action">
                Manage Account
              </div>
            </router-link>
            <div class="list-group-item dropdown-item list-group-item-action text-danger selectable" style="z-index: 1000;" @click="logout">
              <i class="mdi mdi-logout"></i>
              logout
            </div>
          </div>
        </div>
      </div>
    </div>
  </span>
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

<style lang="scss" scoped>

.mdi-account-badge:hover {
  filter: brightness(1.3);
}

</style>






<!-- <template>
  
    
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

<style lang="scss" scoped></style> -->
