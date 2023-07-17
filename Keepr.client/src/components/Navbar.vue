<template>
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
      <div v-else>
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

  <!-- <nav class="navbar navbar-expand-lg navbar-dark bg-dark px-3">
    <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
      <div class="d-flex flex-column align-items-center">
        <img alt="logo" src="../assets/img/cw-logo.png" height="45" />
      </div>
    </router-link>
    <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarText"
      aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarText">
      <ul class="navbar-nav me-auto">
        <li>
          <router-link :to="{ name: 'About' }" class="btn text-success lighten-30 selectable text-uppercase">
            About
          </router-link>
        </li>
      </ul> -->
      <!-- LOGIN COMPONENT HERE -->
      
    <!-- </div>
  </nav> -->
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



</style>
