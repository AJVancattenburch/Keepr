<template>

  <section v-if="vault" :key="vault.id" class="container-fluid">
    <div class="row">
      <div class="col-md-4">
        <router-link :to="{ name:  'VaultDetails', params: { vaultId: vault.id }}">
          <h1> {{ vault.name }} </h1>
          <img :src="vault.img" class="img-fluid" :alt="`Vault Image for ${vault.name}`" />
          <p> {{ vault.description }} </p>
          <small>Published: {{ vault.createdAt }} </small>l
        </router-link>
      </div>
    </div>
  </section>

</template>


<script>

import { Vault } from "../models/Vault.js";

export default {

  props: {
    vault: {
      type: Vault,
      required: true
    }
  },

  setup() {

    return {

    }
  }
}

</script>