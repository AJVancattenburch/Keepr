<template>
  
  <div class="keep-container">
    <div class="title-container">
      <h5 class="keep-title">{{ keep.name }}</h5>
      <img :src="keep.creator.picture"
        :alt="keep.creator.name" :title="keep.creator.name">
    </div>
    <img :src="keep.img" :alt="keep.name" class="keep-img">
  </div>



</template>


<script>

import { Keep } from "../models/Keep.js";

export default {

  props: {
    keep: {
      type: Keep,
      required: true
    }
  },

  setup() {

    // const route = useRoute()

    return {
      // route
    }
  }
}

</script>


<style scoped lang="scss">

@import url(https://fonts.googleapis.com/css?family=Ubuntu:400,700);
@import url(https://weloveiconfonts.com/api/?family=entypo|fontawesome|zocial);

.keep-container {
  width: 24vw;
  height: 100%;
  margin: 0 auto;
  margin-bottom: 1.5rem;
  border-radius: .5rem;
  overflow: hidden;
  transition: all .3s ease-in-out;
  animation: fadeAndDropIn 1s ease-in-out forwards;
}

@keyframes fadeAndDropIn {
  0% {
    opacity: .3;
    height: 6vh;
  }
  50% {
    opacity: .5;
    height: 50vh;
  }
  100% {
    opacity: 1;
    height: auto;
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
}

.title-container {
  position: relative;
  width: 90%;
  background: radial-gradient(circle, #ffac40b0 0%, #517df4 100%);
  display: flex;
  justify-content: space-between;
  padding: .5em;
  align-items: center;
  border-top-left-radius: .5rem;
  border-top: 3px solid #ffb451;
  border-right: 3px solid #ffb451;
  border-left: 3px solid #ffb451;
}

.title-container img {
  width: 2rem;
  height: 2rem;
  border-radius: 100%;
  object-fit: cover;
  object-position: center;
}

.keep-title {
  font-family: 'Ubuntu', sans-serif;
  margin-bottom: 0px;
  text-shadow: 2px 2px 2px black;
  color: aliceblue;

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

</style>