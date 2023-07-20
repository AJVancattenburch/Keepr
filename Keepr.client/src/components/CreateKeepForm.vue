<template>
    
  <form v-if="keeps" class="form__contact scrollable" @submit.prevent="createKeep">
    <fieldset style="overflow-y: auto; overflow-x: hidden;">
      <div class="row justify-content-center align-items-center pt-5 scrollable" style="overflow-y: auto; overflow-x: hidden; padding-bottom: 5rem;">
          <p class="canvas-header pt-5">Create a New Keep!</p>
          <!-- <div class="col-9">
            <label for="category" class="mt-5">Select a Category</label>
            <select name="category" id="category" class="form-control" required v-model="editable.category">
              <option disabled selected class="text-center fw-bold">** OPTIONS **</option>
              <option value="Specialty Coffee">Specialty Coffee</option>
              <option value="Starters">Starters</option>
              <option value="Mexican">Mexican</option>
              <option value="Italian">Italian</option>
              <option value="American">American</option>
              <option value="Chinese">Chinese</option>
              <option value="Soup">Soup</option>
              <option value="Cheese">Cheese</option>
              <option value="Dessert">Dessert</option>
              <option value="Other">Other</option>
            </select>
          </div> -->
          <div class="col-9">
            <label for="name" class="mt-4">Name your Keep</label>
            <input type="text" id="name" name="name" class="form-control" minlength="2" maxlength="100" required
              v-model="editable.name">
          </div>

          <div class="col-9">
            <label for="img" class="mt-2">Upload an Image</label>
            <input type="url" id="img" name="img" class="form-control" required minlength="5" maxlength="500" v-model="editable.img" @input="showImg()">
            <div class="d-flex flex-column justify-content-center">
              <p class="text-muted text mb-0 recipe-text">Image Preview</p>
              <div v-if="previewKeepImg">
                <img :src="previewKeepImg" class="recipe-img" :alt="`Preview Image for ${editable.name} failed to load...`">
              </div>
              <div v-else>
                <img src="https://developers.elementor.com/docs/assets/img/elementor-placeholder-image.png" class="recipe-img" :alt="`Placeholder Image for ${editable.name} failed to load...`">
              </div>
            </div>
          </div>
        
          <div class="col-9">
            <label for="description" class="mt-3">Description</label>
            <textarea type="text" id="description" name="description" rows="3" class="form-control" required minlength="3"
              maxlength="5000" v-model="editable.description"></textarea>
          </div>

          <!-- <div class="col-9">
            <div>
              <ul>
                <li class="ingredient"><span class="d-flex justify-content-center">
                    {{ editableIngredient.quantity }} {{ editableIngredient.name }}<span><i class="mdi mdi-minus text-danger selectable"
                        title="Delete Ingredient"
                        @click="deleteIngredient(i.id)"></i></span>
                  </span>
                </li>
              </ul>
              <div class="row">
                <div class="col-3 justify-content-center align-items-center">
                  <label for="ingredient" class="">Ingredients Required:</label>
                  <input type="text" id="ingredient" minlength="3" maxlength="25" name="ingredient"
                    class="form-control name-input" v-model="editableIngredient.name" required>
                </div>
                <div class="col-1 justify-content-center align-items-center pt-4">
                  <label for="quantity">Qty:</label>
                  <input type="text" id="quantity" minlength="3" maxlength="15" name="quantity" class="form-control qty-input" v-model="editableIngredient.quantity" required>
                </div>
                <div class="col-1 justify-content-center align-items-center">
                  <input type="checkbox" id="optional" name="optional" class="form-check-input" @change="createIngredient()">
                </div>
              </div>
            </div>
          </div> -->
        
        <button type="submit" class="button close-button fs-1" tabindex="4">Create Keep &#187;</button>

      </div>
    </fieldset>
  </form>
  
  <svg version="1.1" id="Layer_1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" xml:space="preserve">
    <defs>
      <filter id="blur0">
        <feGaussianBlur in="SourceGraphic" stdDeviation="0 0" />
      </filter>
      <filter id="blur1">
        <feGaussianBlur in="SourceGraphic" stdDeviation="0 5" />
      </filter>
      <filter id="blur2">
        <feGaussianBlur in="SourceGraphic" stdDeviation="0 10" />
      </filter>
      <filter id="blur3">
        <feGaussianBlur in="SourceGraphic" stdDeviation="0 15" />
      </filter>
      <filter id="blur4">
        <feGaussianBlur in="SourceGraphic" stdDeviation="0 20" />
      </filter>
    </defs>
  </svg>

</template>


<script>
import { watchEffect, computed, ref } from "vue";
import { AppState } from "../AppState.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
import { keepsService } from "../services/KeepsService.js";
import { Keep } from "../models/Keep.js";
import { Offcanvas } from "bootstrap";


export default {

  props: {
    keep: {
      type: Keep,
      required: true
    }
  },

  setup(props) {

    const editable = ref({})
    const previewKeepImg = ref(null)

    watchEffect(() => {
      if(props.keep) {
        previewKeepImg.value = { ...props.keep.img }
      }
    })

    return {
      editable,
      previewKeepImg,

      keeps: computed(() => AppState.keeps),

      async createKeep() {
        try {
          logger.log(editable.value)
          const newKeep = editable.value
          await keepsService.createKeep(newKeep)
          editable.value = {}
          Offcanvas.getOrCreateInstance('#createKeepForm').hide()
        } catch (error) {
          logger.error(error)
          Pop.toast(error, 'Failed to Create Keep', 'error', 'top-center', 3000)
        }
      },

      showImg() {
        previewKeepImg.value = editable.value.img
      },
    }
  }
}

</script>


<style scoped lang="scss">

@import url('https://fonts.googleapis.com/css?family=Roboto+Slab:400');
@import url('https://fonts.googleapis.com/css?family=Shadows+Into+Light');

$line-height: 40px;

::selection { 
  color: #fff;
  background: #9FBE5A;  
}

body {
  width: 100vw;
  height: 100%; 
  padding-bottom: 150px;
  overflow-x: hidden; 
  display: flex;
  align-items: center;
  justify-content: center;
  background: linear-gradient(to bottom, #16a085, #f4d03f) center / cover no-repeat;
}

.canvas-header {
  margin-bottom: 75px;
  font: 400 56px 'Roboto Slab', serif;
  font-size: 40px;
  text-align: center;
  text-shadow: 1px 1px 3px #ff000084;
  animation: fadeIn 2s ease-in-out forwards;
}



//NOTE - <form> HOLDS THE 'TORN PAPER' BORDER. CREATE CSS INSTANCE FOR THE FOLLOWING: 
//  form {
//   /* place styling attributes here */
// }

form {
  height: 100%;

}

.form__contact {
  max-width: 1000px;
  margin: 0 auto;
  border-left: 30px solid white; 
  border-image: url(data:image/svg+xml;base64,PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0idXRmLTgiPz48IURPQ1RZUEUgc3ZnIFBVQkxJQyAiLS8vVzNDLy9EVEQgU1ZHIDEuMS8vRU4iICJodHRwOi8vd3d3LnczLm9yZy9HcmFwaGljcy9TVkcvMS4xL0RURC9zdmcxMS5kdGQiPjxzdmcgdmVyc2lvbj0iMS4xIiBpZD0iTGF5ZXJfMSIgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIiB4bWxuczp4bGluaz0iaHR0cDovL3d3dy53My5vcmcvMTk5OS94bGluayIgeD0iMHB4IiB5PSIwcHgiIHdpZHRoPSI0MS44NnB4IiBoZWlnaHQ9IjUyLjMyNnB4IiB2aWV3Qm94PSIwIDAgNDEuODYgNTIuMzI2IiBlbmFibGUtYmFja2dyb3VuZD0ibmV3IDAgMCA0MS44NiA1Mi4zMjYiIHhtbDpzcGFjZT0icHJlc2VydmUiPjxwYXRoIGZpbGw9IiNGRkZGRkYiIGQ9Ik0wLDB2MjUuMTYzaDcuMDk3YzAuNTAxLTQuOTg5LDQuNzEyLTguODg0LDkuODMzLTguODg0YzUuNDU4LDAsOS44ODQsNC40MjUsOS44ODQsOS44ODRzLTQuNDI1LDkuODg0LTkuODg0LDkuODg0Yy01LjEyMSwwLTkuMzMyLTMuODk1LTkuODMzLTguODg0SDB2MjUuMTYzaDQxLjg2VjBIMHoiLz48L3N2Zz4=) 5% 100% repeat;
  border-image-width: 0px 0px 0px 30px;
  transform: translateY(100%);
  animation: init 1s ease-in-out forwards;
  
  fieldset {
    position: relative;
    height: 105vh;
    margin: 0;
    padding: 5px 5px $line-height 80px;
    border: none;
    border-radius: 0 20px 20px 0;
    font: 24px 'Shadows Into Light', cursive;
    background: #fff linear-gradient(rgba(0,0,0,.1) 1px, transparent 0) 0 20px / 100% $line-height;
    
    &:after {
      position: absolute; 
      top: 0;
      left: 50px;
      content: '';
      height: 100%;
      width: 1px;
      border-left: double #E08183;
    }
  } 
  
	p {
    margin: 0 0 $line-height 0;
    line-height: $line-height;
    color: #333;
	}

	span {
		position: relative;
		line-height: $line-height;
	}

	.close-button {
		margin-top: $line-height;
    float: right;
    border: none;
    font-family: 'Shadows Into Light', cursive;
    color: #E08183;
    background: transparent;
    cursor: pointer;
    transition: transform 0.25s ease;
    
    &:hover {
      transform: translateX(10px)
    }
	}
}

.form__field {
  display: inline;
  color: #7DB665;
  outline: none;

  &:empty {
    display: inline-block;
    color: #ff0000;
  }
  
  // Use a data-attr to replicate a placeholder
  &:empty,
  &:empty:focus {
    &:before {
      content: attr(data-placeholder);
    }
  }
}

// Hide blur defs
svg {
  display: none;
}

@keyframes init {
  75% {
    transform: translateY(-20px);
  }
  100% {
    transform: translateY(0);
  }
}

@keyframes do-blur {
  0% {
    filter: url(#blur4);
  }
  
  25% {
    filter: url(#blur3);
  }
  
  50% {
    filter: url(#blur2);
  }
  
  75% {
    filter: url(#blur1);
  }
  
  100% {
    filter: url(#blur0); 
  }
}



@import url('https://fonts.googleapis.com/css2?family=Inter:wght@100..900&display=swap');

body {
	background: #170F1E;
	display: grid;
	height: 105vh;
	place-items: center;
	-webkit-font-smoothing: antialiased;
	width: 100vw;
}

.offcanvas-button {
  animation: 1.5s ease infinite alternate running shimmer;
  background: linear-gradient(90deg, #FFE27D 0%, #ffb05b 30%, #ff916c 85%);
  background-size: 200% 100%;
  border: none;
	border-radius: 6px;
  box-shadow: -2px -2px 10px rgba(255, 227, 126, 0.5), 2px 2px 10px rgba(255, 98, 0, 0.5);
  color: #170F1E;
  cursor: pointer;
	font-family: 'Inter', sans-serif;
  font-size: 16px;
	font-weight: 670;
  line-height: 24px;
  overflow: hidden;
  padding: 12px 24px;
  position: absolute;
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

.recipe-text {
  position: relative;
  top: 1.5rem;
  left: 40%;
  right: 40%;
}

.recipe-img {
  position: relative;
  top: 0;
  left: 25%;
  height: 250px;
  width: 300px;
  border-radius: 6px;
  box-shadow: -2px -2px 10px #ffe37e80, 2px 2px 10px #ff620080;
  margin-top: 2rem;
}

.qty-input {
  position: relative;
  width: 100%;
  height: 40px;
  top: .8rem;
}

.form-check-input {
  position: relative;
  width: 100%;
  height: 25px;
  width: 25px;
  top: 4.7rem;
}

@keyframes shimmer {
  to {
    background-size: 100% 100%;
    box-shadow: -2px -2px 6px rgba(255, 227, 126, 0.5), 2px 2px 6px rgba(144, 148, 255, 0.5);
  }
}

</style>