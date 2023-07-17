<template>

  <div class="offcanvas-body row">
    <div> {{ keep }} </div>
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



    return {



    }
  }
}

</script>