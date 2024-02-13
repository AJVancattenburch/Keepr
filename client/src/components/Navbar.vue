<template>

  <nav class="navbar navbar-expand-lg bg-nav" style="opacity: 1;">
    <div class="container-fluid">
      <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
        <div class="d-flex flex-column align-items-center">
          <img title="home" alt="logo" src="/src/assets/img/keepr_logo.png" class="keepr-logo" style="opacity: .9; filter: blur(.25px)" height="45" />
        </div>
      </router-link>

        <div v-if="user.isAuthenticated" class="col-2 justify-content-start">
          <button class="button" data-bs-target="#createKeepForm" data-bs-toggle="offcanvas" aria-controls="createKeepForm">Create Keep</button>
        </div>
        <div v-if="user.isAuthenticated" class="col-2 justify-content-end">
          <button class="button" data-bs-target="#createVaultForm" data-bs-toggle="offcanvas" aria-controls="createVaultForm">Create Vault</button>
        </div>

        <!-- ANCHOR - Offcanvas Button for opening the 'SearchKeepsOffcanvas.vue' Component -->
      <!-- <div class="d-flex justify-content-end pe-5">
        <div class="col-7 me-5">
          <img title="Search Keeps..." src="src/assets/img/search-posts-icon.png" class="search-icon justify-content-end align-items-center img-fluid" style="z-index: 1; height: 45px; background-size: cover; object-fit: cover;" role="button" data-bs-toggle="offcanvas" data-bs-target="#myOffcanvas" aria-controls="myOffcanvas">
        </div>
      </div> -->
      
      <!-- 
      <form class="d-flex">
        <input type="text" class="form-control me-2" placeholder="Search Keeps..." aria-label="Search" v-model="editable">
        <button @click="" class="btn btn-outline-light mx-2" style="color: aliceblue; text-shadow: 1px 1px 1px black; box-shadow: 1px 1px 3px black; background-color: #f1ff55b8;" type="submit">Search</button>
      </form> 
      -->
        <Login />
    </div>

  </nav>

</template>

<script>
import Login from './Login.vue';
import { logger } from '../utils/Logger'
import { ref, computed } from 'vue'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
import Pop from "../utils/Pop"

export default {
  setup() {

    const editable = ref({})

    return {

      editable,
      user: computed(() => AppState.user),

      async searchKeeps() {
        try {
          const searchTerm = editable.value
            .split(' ')
            .join('+')
          logger.log("[SEARCH TERM] =>",searchTerm)
          AppState.query = searchTerm
          await keepsService.searchKeeps(searchTerm)
          editable.value = ''
          // router.push(
          //   { name: 'Search' , params: { query: searchString }}
          // )
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      }
    }
  },
  components: { Login }
}
</script>

<style scoped lang="scss">

.keepr-logo {
  filter: brightness(1.1) !important;
  transition: .5s ease-in-out !important;}

.keepr-logo:hover {
  filter: brightness(1.3) !important;
  transition: .5s ease-in-out !important;
}

.navbar-expand-lg {
  position: fixed;
  width: 100%;
  z-index: 1;
}

.search-icon {
  position: absolute;
  background-color: transparent;
  top: 8px;
  right: 150px; 
  box-shadow: 0px 0px 10px #f8db4a;
  opacity: .7;
  filter: blur(.1px);
  z-index: 2;
  border-radius: 50%;
  aspect-ratio: 1/1;
  transition: .5s;
}

.search-burger:hover {
  opacity: 1;
  filter: brightness(1.4);
  transform: scale(1.05);
  transition: .5s;
}

a:hover {
  text-decoration: none;
}

.nav-link {
  color: aliceblue;
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  color: aliceblue;
  position: relative;
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

.bg-nav {
  background: linear-gradient(90deg, #1b79f3 0%,
                                      #a9f1db 30%,
                                      #ffb05b 85%);
  box-shadow: 0px 0px 10px #517df4;
}

.button {
  animation: 1.5s ease infinite alternate running shimmer;
  background: linear-gradient(90deg, #FFE27D 0%,
                                     #ffb05b 30%,
                                     #ff916c 85%);
  background-size: 200% 100%;
  border: none;
	border-radius: 6px;
  box-shadow: -2px -2px 10px #ffe37e80, 
              2px 2px 10px #ff620080;
  color: #170F1E;
  cursor: pointer;
	font-family: 'Inter', sans-serif;
  font-size: 16px;
	font-weight: 670;
  line-height: 24px;
  height: 45px;
  overflow: hidden;
  padding: 12px 24px;
  display: flex;
  align-items: center;
  text-decoration: none;
  transition: 0.2s;
  
  svg {
    left: -20px;
    opacity: 0.5;
    position: absolute;
    top: -2px;
    transition: 0.5s cubic-bezier(.5,-0.5,.5,1.5);
  }
  
  &:hover svg {
    opacity: 0.8;
    transform: translateX(50px) scale(1.5);
  }
  
  &:hover {
    transform: rotate(-3deg);
  }
  
  &:active {
    transform: scale(0.95) rotate(-3deg);
  }
}

@keyframes shimmer {
  0% {
    background-size: 100% 100%;
    box-shadow: -2px -2px 6px #ffe37e80, 
                2px 2px 6px #9094ff80;
  }
  50% {
    background-size: 200% 100%;
    box-shadow: -2px -2px 10px #ffe37e80, 
                2px 2px 10px #ff620080;
  }
  100% {
    background-size: 100% 100%;
    box-shadow: -2px -2px 6px #ffe37e80, 
                2px 2px 6px #9094ff80;
  }
}

.button:hover {
  animation: 1.5s ease infinite alternate running shimmerAndTilt;
}

@keyframes shimmerAndTilt {
  0% {
    background-size: 100% 100%;
    box-shadow: -2px -2px 6px #ffe37e80, 
                2px 2px 6px #9094ff80;
  }
  50% {
    background-size: 200% 100%;
    box-shadow: -2px -2px 10px #ffe37e80, 
                2px 2px 10px #ff620080;
  }
  100% {
    background-size: 100% 100%;
    box-shadow: -2px -2px 6px #ffe37e80, 
                2px 2px 6px #9094ff80;
  }
}


@media screen and (min-width: 768px) {
  nav {
    height: 64px;
  }
}
</style>





<!-- <template>
  <div class="d-flex justify-content-center align-items-end m-2">
    <button class="btn btn-primary" type="button" data-bs-toggle="offcanvas" data-bs-target="#offcanvasScrolling" aria-controls="offcanvasScrolling">Menu</button>
  </div>

  <div class="offcanvas offcanvas-end" data-bs-scroll="true" data-bs-backdrop="false" tabindex="-1" id="offcanvasScrolling" aria-labelledby="offcanvasScrollingLabel" style="background-color: transparent; border: none;">
    <div class="offcanvas-header">
     
    </div>
    <div class="offcanvas-body">
      
      <nav class="navbar container-fluid">
    <button type="button" class="btn-close" style="position: absolute; top: 0; right: 0;" data-bs-dismiss="offcanvas" aria-label="Close"></button>

  <div class="my-links">
    <span title="Menu" class="bubble"><i class="mdi mdi-menu"></i></span>
    <ul class="menu">
      <li v-if="!user.isAuthenticated">
        <a @click="login" href="" class="mdi mdi-login"></a>
      </li>
      <div v-else-if="user.isAuthenticated">
        <li>
          <a title="" href=""><Login /></a>
        </li>
        <li>
          <a @click="logout" title="Logout" href="" class="mdi mdi-logout"></a>
        </li>
        <li>
          <a title="Manage Account" href="/#/account" class="mdi mdi-account"></a>
        </li>
        <li>
          <a title="Private Vaults" href="" class="mdi mdi-lock"></a>
        </li>
        <li>
          <a title="Home" href="/#" class="mdi mdi-home"></a>
        </li>
        <li>
          <a title="Trending Keeps" href="" class="mdi mdi-ear-hearing"></a>
        </li>
      </div>
    </ul>

  </div>
  </nav>

</div>
</div>

</template>

<script>
import Pop from "../utils/Pop.js";
import { AppState } from "../AppState.js";
import { computed } from "vue";
import { AuthService } from "../services/AuthService.js";
import Login from './Login.vue';
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
  },
  components: { Login }
}
</script>

<style scoped>

nav {
  background-color: #06096765;
  height: 40% !important;
  width: 25%;
  box-shadow: 0 0 10px #000000;
  position: fixed;
  top: 0;
  right: 0rem;
  border-top-left-radius: 50%;
  border-bottom-left-radius: 50%;
  border-bottom-right-radius: 0;
  z-index: 1;
}

a:hover {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

@media screen and (min-width: 768px) {
  nav {
    height: 64px;
  }
}


span.bubble {
  position: relative;
  top: .75rem;
}

.my-links {
  width: 150px;
  height: 150px;
  line-height: 120px;
  border-radius: 50%;
  background: #060967c3;
  margin: 0 auto;
  position: absolute;
  top: 4rem;
  left: 0rem;
  right: 0;
  cursor: pointer;
  text-align: center;
  font-size: 3em;
  font-weight: bold;
  color: #fbc6a093;
  text-shadow: 3px 3px 2px #000000;
  transition: .24s .2s;
  z-index: 1;
}

.my-links::before {
  content: '';
  position: absolute;
  display: block;
  background: #052966;
  top: 75%;
  right: 30%;
  width: 50%;
  height: 100%;
  cursor: none;
  transform: perspective(1em) 
             rotateX(40deg) 
             scale(1, .35);
  filter: blur(1em);
  z-index: -1;
}

.my-links:hover {
  background: #172bdcbb;
  color: #dce315d7;
  text-shadow: 3px 3px 2px #000000;
  transition: .7s;
}

.my-links:hover::before {
  background: #172bdcbb;
  transition: .24s .2s;

}

.my-links .menu {
  list-style: none;
  padding: 0;
  margin: 0;
  position: absolute;
  top: -100px;
  left: -100px;
  border: 150px solid transparent;
  border-radius: 50%;
  transform: scale(0);
  transition: transform 1.4s .07s;
  z-index: -1;
}

.my-links:hover .menu {
  transition: transform .4s .08s,
  z-index 0s .5s;
  transform: scale(1);
  z-index: 1;
}

.my-links:hover .menu li {
  transition: all .7s;
}

.my-links .menu li a {
  width: 40px;
  height: 40px;
  line-height: 38px;
  border-radius: 50%;
  background: #172bdcbb;
  position: absolute;
  font-size: 59%;
  color: #dce315d7;
  transition: .7s;
}

.my-links:hover .menu li:nth-child(1) a {
  transform: rotate(90deg) translate(80px) rotate(-90deg);
}
.menu li:nth-child(1) a::after {
  content: '';
  position: absolute;
  display: block;
  background: #000f9abb;
  top: 75%;
  left: 5%;
  width: 90%;
  height: 100%;
  filter: blur(1em);
  transform: perspective(1em) 
             rotateX(40deg) 
             scale(1, .35);
} 

.my-links:hover .menu li:nth-child(2) a {
  transform: rotate(135deg) translate(80px) rotate(-135deg);
}
.menu li:nth-child(2) a::after {
  content: '';
  position: absolute;
  display: block;
  background: #000f9abb;
  top: 75%;
  left: 5%;
  width: 90%;
  height: 100%;
  filter: blur(1em);
  transform: perspective(1em) 
             rotateX(40deg) 
             scale(1, .35);
} 

.my-links:hover .menu li:nth-child(3) a {
  transform: rotate(180deg) translate(80px) rotate(-180deg);
}
.menu li:nth-child(3) a::after {
  content: '';
  position: absolute;
  display: block;
  background: #000f9abb;
  top: 75%;
  left: 5%;
  width: 90%;
  height: 100%;
  filter: blur(1em);
  transform: perspective(1em) 
             rotateX(40deg) 
             scale(1, .35);
} 

.my-links:hover .menu li:nth-child(4) a {
  transform: rotate(225deg) translate(80px) rotate(-225deg);
}
.menu li:nth-child(4) a::after {
  content: '';
  position: absolute;
  display: block;
  background: #000f9abb;
  top: 75%;
  left: 5%;
  width: 90%;
  height: 100%;
  filter: blur(1em);
  transform: perspective(1em) 
             rotateX(40deg) 
             scale(1, .35);
} 

.my-links:hover .menu li:nth-child(5) a {
  transform: rotate(270deg) translate(80px) rotate(-270deg);
}
.menu li:nth-child(5) a::after {
  content: '';
  position: absolute;
  display: block;
  background: #000f9abb;
  top: 75%;
  left: 5%;
  width: 90%;
  height: 100%;
  filter: blur(1em);
  transform: perspective(1em) 
             rotateX(40deg) 
             scale(1, .35);
} 

.my-links:hover .menu li:nth-child(6) a {
  transform: rotate(315deg) translate(80px) rotate(-315deg);
}
.menu li:nth-child(6) a::after {
  content: '';
  position: absolute;
  display: block;
  background: #000f9abb;
  top: 75%;
  left: 5%;
  width: 90%;
  height: 100%;
  filter: blur(1em);
  transform: perspective(1em) 
             rotateX(40deg) 
             scale(1, .35);
} 

.my-links:hover .menu li:nth-child(7) a {
  transform: rotate(380deg) translate(80px) rotate(-380deg);
}
.menu li:nth-child(7) a::after {
  content: '';
  position: absolute;
  display: block;
  background: #000f9abb;
  top: 75%;
  left: 5%;
  width: 90%;
  height: 100%;
  filter: blur(1em);
  transform: perspective(1em) 
             rotateX(40deg) 
             scale(1, .35);
} 

.my-links:hover .menu li:nth-child(8) a {
  transform: rotate(100deg) translate(0px) rotate(100deg);
}
.my-link .menu li:nth-child(8) a::before {
  content: '';
  position: absolute;
  display: block;
  background: #000f9abb;
  top: 75%;
  left: 5%;
  width: 90%;
  height: 100%;
  transform: perspective(1em) 
             rotateX(40deg) 
             scale(1, .35);
} 

.my-links:hover .menu li a:hover {
  background: #e37c15;
  color: black;
}

.my-links:hover .menu li a:hover i {
  color: black;
}

.my-links:hover .menu li a i {
  transition: .7s;
}



</style> -->
