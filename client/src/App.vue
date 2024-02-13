<template>
  <header>
    <Navbar />
  </header>
  <main>
    
    <router-view />

    <SmModal id="detailsModal">
      <KeepDetailsCard />
    </SmModal>
    
    <LgScrollingOffcanvas id="createKeepForm">
      <CreateKeepForm />
    </LgScrollingOffcanvas>

    <LgScrollingOffcanvas id="createVaultForm">
      <CreateVaultForm />
    </LgScrollingOffcanvas>

    <LgScrollingModal id="editAccountForm">
        <EditAccountForm />
    </LgScrollingModal>
  
  </main>


</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import Navbar from './components/Navbar.vue'
import LgScrollingModal from './components/LgScrollingModal.vue'
import SmModal from './components/SmModal.vue'
import LgScrollingOffcanvas from './components/LgScrollingOffcanvas.vue'
import KeepDetailsCard from './components/KeepDetailsCard.vue'
import CreateKeepForm from "./components/CreateKeepForm.vue"
import CreateVaultForm from "./components/CreateVaultForm.vue"
import EditAccountForm from "./components/EditAccountForm.vue"
// import gsap from 'gsap'

export default {
  setup() {

    // onMounted(() => {

    // const Line = ($el) => {
    //   const $paths = $el.querySelectorAll(".path");
    //   const total = gsap.timeline();
    //   const duration = gsap.utils.random(150, 300);
    //   const y = gsap.utils.random(-250, 250);
    //   const rotate = gsap.utils.random(-20, 20);
    //   const scaleXFrom = gsap.utils.random(2, 2.5);
    //   const scaleXTo = gsap.utils.random(1.5, 1.75);
    //   const scaleYFrom = gsap.utils.random(1.5, 2);
    //   const scaleYTo = gsap.utils.random(0.6, 0.7);
    //   const opacityFrom = gsap.utils.random(0.75, 0.8);
    //   const opacityTo = gsap.utils.random(0.85, 1);
    //   const ease = gsap.utils.random([
    //     "power2.inOut",
    //     "power3.inOut",
    //     "power4.inOut",
    //     "sine.inOut"
    //   ]);
    //   total.to($paths, {
    //     xPercent: -100,
    //     duration: duration,
    //     ease: "none",
    //     repeat: -1
    //   });

    //   total.fromTo(
    //     $el,
    //     {
    //       y,
    //       opacity: opacityFrom,
    //       rotate,
    //       scaleY: scaleYFrom,
    //       scaleX: scaleXFrom,
    //       transformOrigin: "50% 50%"
    //     },
    //     {
    //       y: y * -1,
    //       opacity: opacityTo,
    //       rotate: rotate * -1,
    //       scaleY: scaleYTo,
    //       scaleX: scaleXTo,
    //       repeat: -1,
    //       yoyo: true,
    //       yoyoEase: ease,
    //       duration: duration * 0.25,
    //       ease: ease,
    //       transformOrigin: "50% 50%"
    //     },
    //     0
    //   );

    //   total.seek(gsap.utils.random(10, 20));
    // };

    // gsap.utils.toArray(".g").forEach(($el) => Line($el));
    // gsap.to("svg", { opacity: 1, duration: 1 });
    // })

    return {
      appState: computed(() => AppState)
    }
  },
  components: { 
                Navbar,
                SmModal, 
                LgScrollingModal,
                LgScrollingOffcanvas, 
                CreateKeepForm, 
                CreateVaultForm, 
                EditAccountForm, 
                KeepDetailsCard, 
              }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

:root{
  --main-height: calc(100vh - 32px - 64px);
}

.editAccountForm {
  width: 75vw;
}

footer {
  display: grid;
  place-content: center;
  height: 32px;
}

::-webkit-scrollbar {
  width: .9rem;
  height: .9rem;
  border-radius: 0.5rem;
  background-color: #205bff;
  border-style: groove;
}

::-webkit-scrollbar-thumb {
  background-color: #8b8b8b5f;
  border-radius: 0.25rem;
}

::-webkit-scrollbar-track {
  background-color: rgba(32, 20, 12, 0.512);
  border-radius: 0.25rem;
  border-style: ridge;
}

::-webkit-scrollbar-corner {
  background-color: #2d288c;
  border-radius: 0.5rem;
  border-style: groove;
}

</style>
