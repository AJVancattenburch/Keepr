<template>
  <div v-if="account" :key="account.id" class="about text-center">
    <h1>Welcome {{ account.name }}</h1>
    <img class="rounded" :src="account.picture" alt="" />
    <p>{{ account.email }}</p>
    <p>{{ account.bio }}</p>
    <p>{{ account.coverImg }}</p>
  </div>


  <div v-if="account" class=" container-fluid section-container section-container-bg shadow elevation-5">
    <div class="container">
      <div class="row">
        <div class="col-md-12 section-description">
          <div class="divider-1 wow fadeInUp"><span></span></div>
            <p></p>
          </div>
        </div>
        <div class="row">
          <div class="col-md-12 form-3-box text-center wow">

            <!-- NOTE - This form enables the currently logged in Account Holder to edit their profile  -->
            <form @submit.prevent="updateAccount">
              <fieldset class="form-group p-3">
                <legend class="d-flex justify-content-center p-5 mb-5">Edit Account</legend>
                <div class="form-group text-left">
                  <label for="name">Name</label>
                  <input v-model="editable.name" type="text" class="form-control job-title" id="name" placeholder="Name..." name="name">
                </div>
                <div class="form-group text-left">
                  <label for="bio">Bio:</label>
                  <textarea v-model="editable.bio" class="form-control bio" id="bio" placeholder="About Me..." name="bio"></textarea>
                </div>
                <div class="form-group text-left">
                  <label for="picture">My Profile Image:</label>
                  <input v-model="editable.picture" type="url" class="form-control salary" id="picture" placeholder="Place Image URL to preview..." name="picture">
                </div>
                <!-- NOTE - Previews Profile Image if there is a URL inputted -->
                <div v-if="previewProfilePic" class="form-group text-left">
                  <label for="previewProfilePic">Preview Profile Image:</label>
                  <img @input="showProfilePic()" :src="previewProfilePic" :alt="`Sorry ${account.name}, your image preview failed to load.`" />
                </div>
                <!-- NOTE - Else, Previews Current Profile Image if there is no URL inputted -->
                <div v-else class="form-group text-left my-4">
                  <label for="previewProfilePic">Preview Profile Image:</label>
                  <img :src="account.picture" class="rounded-circle" alt="previewProfilePic" />
                </div>
                <div class="form-group text-left">
                  <label for="coverImg">My Cover Image:</label>
                  <input v-model="editable.coverImg" type="url" class="form-control salary" id="coverImg" placeholder="Place Cover URL to preview..." name="coverImg">
                </div>
                <!-- NOTE - Previews Profile Image if there is a URL inputted -->
                <div v-if="previewCoverImg" class="form-group text-left">
                  <label for="previewCoverImg">Preview Cover Image:</label>
                  <img @input="showCoverImg()" :src="previewCoverImg" :alt="`Sorry ${account.name}, your cover image preview failed to load.`" />
                </div>
                <!-- NOTE - Else, Previews Current Profile Image if there is no URL inputted -->
                <div v-else class="form-group text-left my-4">
                  <label for="previewCoverImg">Preview Cover Image:</label>
                  <img :src="account.coverImg" class="rounded" alt="previewCoverImg" />
                </div>
              </fieldset>
              <!-- NOTE - Button that Submits Updated Profile Information -->
              <div class="form-group row">
                <div class="col">
                    <button type="submit" class="btn btn-primary submit-btn">Update Account</button>
                </div>
              </div>
            </form>
 
        </div>
      </div>
    </div>
  </div>

</template>

<script>

import { computed, ref, watchEffect } from 'vue';
import { AppState } from '../AppState';
import { logger } from "../utils/Logger.js";
import { accountService } from "../services/AccountService.js";
import Pop from "../utils/Pop.js";
export default {
  setup() {

    const editable = ref({})
    const previewProfilePic = ref(null)
    const previewCoverImg = ref(null)
    
    watchEffect(() => {
      if(AppState.account.id) {
        editable.value = { ...AppState.account }
        previewProfilePic.value = AppState.account.picture
      }
      if(AppState.account.id) {
        editable.value = { ...AppState.account }
        previewCoverImg.value = AppState.account.coverImg
      }
    })

    return {
      editable,
      previewProfilePic,
      previewCoverImg,

      account: computed(() => AppState.account),

      showProfilePic() {
        previewProfilePic.value = editable.value.picture
      },
      showCoverImg() {
        previewCoverImg.value = editable.value.coverImg
      },
 



      async updateAccount() {
        try {
          logger.log(editable.value)
          const accountData = editable.value
          await accountService.updateAccount(accountData)
          editable.value = {}
          // STUB - *** This will be replaced with a modal or offcanvas if there is time. Remember to getOrCreateInstance! ***
        } catch (error) {
          Pop.toast(error.message, 'error')
          logger.log(error)
        }
      },
    }
  }
}
</script>

<style scoped lang="scss">

@import url(https://fonts.googleapis.com/css?family=Ubuntu:400,700);

img {
  max-width: 100px;
}

.section-container {
  width: 90%;
  font-family: 'Ubuntu', sans-serif;
  border-radius: .5rem;
  margin-bottom: 3rem;
  padding: 2rem;
}

.section-container-bg {
  background: radial-gradient(ellipse at center, #51fec184 0%, 
                                                 #1c8ffab9 100%);
  box-shadow: inset 3px 3px 10px 3px #0482ffc5, 
              inset -3px -3px 10px 3px #00a6ffc9, 
                     3px 3px 10px 3px #0482ffc5, 
                    -3px -3px 10px 3px #00a6ffc9;
  opacity: .9;
}

legend {
  text-shadow: 1px 1px 1px aliceblue;
  background: linear-gradient(145deg, #5cc0fed0, 
                                      #35f3fda9);
  box-shadow: inset 3px 3px 10px 3px #0482ffc5, 
              inset -3px -3px 10px 3px #00a6ffc9, 
                    3px 3px 10px 3px #0482ffc5, 
                    -3px -3px 10px 3px #00a6ffc9;
  filter: brightness(.7);
  border-radius: .25rem;
}

label {

  font-weight: bold;
  text-shadow: .5px .5px 1px aliceblue;
  margin: 10px;
  filter: brightness(.7);
}

input, textarea {
  background: #dddddd;
  width: 100%;
  padding: 10px;
  padding-left: 20px;
  font-size: 1rem;
  font-weight: 450;
  border-radius: .25rem;
  box-shadow: inset 6px 6px 6px #cbced1, 
              inset -6px -6px 6px white;
}

.submit-btn {
  background: linear-gradient(145deg, #ffc66f, 
                                      #fffb00df);
  color: #000;
  text-shadow: 1px 1px 1px #fff;
  box-shadow: inset 3px 3px 3px #a1d7f9, 
              inset -3px -3px 3px #6fb4c7;
  border: none;
}

.submit-btn:active {
  color: #000;
  text-shadow: 1px 1px 1px #898989;
  background: linear-gradient(145deg, #e2b063, 
                                      #e7e300df);
  box-shadow: inset 3px 3px 3px #78613c, 
              inset -3px -3px 3px #615c39;
}


</style>
