<template>
  <div class="modal fade" id="addKeepModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered" role="document">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" id="exampleModalCenterTitle">Add Keep</h5>
          <button type="button" class="close" data-dismiss="modal" aria-label="Close">
            <span aria-hidden="true">&times;</span>
          </button>
        </div>
        <div class="modal-body">
          <form v-on:submit.prevent="addKeep">
            <div class="form-group">
              <input type="text" name="name" v-model="keep.name" class="form-control" id="formGroupExampleInput" placeholder="Title" required>
            </div>
            <div class="form-group">
              <input type="text" name="description" v-model="keep.description" class="form-control" id="formGroupExampleInput" placeholder="Description">
            </div>
            <div class="form-group">
              <input type="text" name="address" v-model="keep.address" class="form-control" id="formGroupExampleInput" placeholder="Img Address"
                required>
            </div>
            <div class="btn-group-toggle" data-toggle="buttons">
              <label class="btn btn-secondary active" for="checkbox" @click="toggleSetting">
                <input type="checkbox" id="checkbox" checked autocomplete="off"> {{pSetting}}
              </label>
            </div>
          </form>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
          <button type="submit" class="btn btn-primary" @click="addKeep" data-dismiss="modal">Submit</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
  export default {
    name: 'CreateKeep',
    props: {
      user: {
        type: Object,
        required: true
      }
    },
    data() {
      return {
        keep: {
          name: "",
          description: "",
          address: "",
          userId: "",
          shareable: 0,
          views: 0,
          keeps: 0
        },
        pSetting: "Private"
      }
    },
    methods: {
      addKeep() {
        this.keep.userId = this.user.id
        this.keep.shareable = this.show
        this.$store.dispatch('createKeep', this.keep)
        this.pSetting = "Public"
      },
      toggleSetting() {
        if (this.pSetting == "Public") {
          this.pSetting = "Private"
          this.show = 1
        }
        else {
          this.pSetting = "Public"
          this.show = 0
        }
      }
    }
  }
</script>

<style>
</style>