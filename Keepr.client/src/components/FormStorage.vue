<template>

  <div class=" container-fluid section-container section-container-bg shadow elevation-5">
    <div class="container">
      <div class="row">
        <div class="col-md-12 section-description wow fadeIn">
          <div class="divider-1 wow fadeInUp"><span></span></div>
            <p></p>
          </div>
        </div>
        <div class="row">
          <div class="col-md-12 form-3-box text-center wow fadeInUp">

            <form action="" method="post">
              <!-- Current Occupation  -->
              <fieldset class="form-group p-3">
                <legend class="d-flex justify-content-center bg-dark px-2">Name</legend>
                <div class="form-group text-left">
                  <label for="job-title">Name</label>
                  <input type="text" class="form-control job-title" id="job-title" placeholder="Job title..." name="job-title">
                </div>
                <div class="form-group text-left">
                  <label for="job-description">Job description:</label>
                  <textarea class="form-control job-description" id="job-description" placeholder="Job description..." name="job-description"></textarea>
                </div>
                <div class="form-group text-left">
                  <label for="salary">Salary:</label>
                  <input type="text" class="form-control salary" id="salary" placeholder="Salary..." name="salary">
                </div>
              </fieldset>
              <!-- Your Address  -->
              <fieldset class="form-group p-3">
                <legend class="w-auto px-2">Your Address</legend>
                <div class="form-group text-left">
                  <label for="address">Address:</label>
                  <input type="text" class="form-control address" id="address" placeholder="Address..." name="address">
                </div>
                <div class="form-group text-left">
                  <label for="zip-code">ZIP code:</label>
                  <input type="text" class="form-control zip-code" id="zip-code" placeholder="ZIP code..." name="zip-code">
                </div>
                <div class="form-group text-left">
                  <label for="city">City:</label>
                  <input type="text" class="form-control city" id="city" placeholder="City..." name="city">
                </div>
                <div class="form-group text-left">
                  <label for="state">State:</label>
                  <input type="text" class="form-control state" id="state" placeholder="State..." name="state">
                </div>
              </fieldset>
              <!-- Job Preferences  -->
              <fieldset class="form-group p-3">
                <legend class="w-auto px-2">Job Preferences</legend>
                <div class="form-group text-left">
                  <label for="preferences">Preferences:</label>
                  <textarea class="form-control preferences" id="preferences" placeholder="Preferences..." name="preferences"></textarea>
                </div>
                <div class="form-group text-left">
                  <label for="new-salary">New salary:</label>
                  <input type="text" class="form-control new-salary" id="new-salary" placeholder="New salary..." name="new-salary">
                </div>
              </fieldset>
              <!-- Submit Button  -->
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