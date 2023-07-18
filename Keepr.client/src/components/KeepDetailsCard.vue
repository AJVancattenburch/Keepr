<template>
  <div v-if="keep" :key="keep.id" class="container-fluid">
    <div class="modal-content">
      <button type="button" class="btn-close p-1 ms-1 mt-1" data-bs-dismiss="modal" aria-label="Close"></button>
      <div class="keep-header mb-4" style="border: none;">
        <h1 class="col-12 keep-title text-center" id="exampleModalLabel"> {{ keep.name }} </h1>
      </div>
      <div class="modal-body row d-flex pt-3">
        <div class="justify-content-between ps-2" style="color: aliceblue; text-shadow: 1px 1px 2px black;">
          <div class="d-flex justify-content-start">
            <i title="View Count" class="col-1 mdi mdi-eye fs-3"> </i>
            <p class="col-2 view-count fs-5">: {{ keep.views }} </p>
            <img class="col-1 d-flex creator-img offset-8 align-items-end img-fluid" :src="keep.creator.picture" :alt="`Image of ${keep.creator.name} failed to load`">
          </div>
        </div>
        <img :src="keep.img" class="keep-img img-fluid" :alt="`Image by: ${keep.creator.name} failed to load`">
        <p class="keep-description pt-3"> {{ keep.description }} </p>
        <div class="modal-footer">
          <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
          <button type="button" class="btn btn-primary">Save changes</button>
        </div>
        
      </div>
    </div>
  </div>
</template>

<script>
import { AppState } from "../AppState.js";
import { Keep } from "../models/Keep.js";
import { computed } from 'vue'


export default {

  // props: {
  //   keep: {
  //     type: Keep,
  //     required: true
  //   }
  // },

  setup() {



    return {

      keep: computed(() => AppState.activeKeep)

    }
  }
}

</script>

<style scoped lang="scss">

@import url(https://fonts.googleapis.com/css?family=Ubuntu:400,700);

.btn-close {
  background-color: aliceblue;
}

.keep-header {
  background-color: #0297fade;
  box-shadow: 0 -25px 5px 5px #0297fade, 0 15px 5px 5px #0297fade;
}

.keep-title {
  color: aliceblue;
  font-size: 2.5rem;
  font-family: 'Ubuntu', sans-serif;
  font-weight: 550;
  text-shadow: 2px 2px 3px black;
}

.modal-body {
  background-color: slategray;
  overflow-x: hidden;
  width: 100%;
  --bs-gutter-x: 0;
}

.view-count {
  padding-top: .3rem;
}

.creator-img {
  height: 2rem;
}

.keep-img {
  height: 100%;
  aspect-ratio: 1/1;
  object-fit: cover;
  border-radius: .8rem;
}

.keep-description {
  color: aliceblue;
  font-size: 1rem;
  font-family: 'Ubuntu', sans-serif;
  font-weight: 550;
  text-shadow: 1.5px 1.5px 1px black;
}

@media screen and (max-width: 768px) {
  .view-count {
    padding-top: .1rem;
  }
}

</style>