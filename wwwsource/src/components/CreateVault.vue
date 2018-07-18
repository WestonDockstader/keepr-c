<template>
  <div class="modal fade" id="addVaultModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered" role="document">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" id="exampleModalCenterTitle">Add Vault</h5>
          <button type="button" class="close" data-dismiss="modal" aria-label="Close">
            <span aria-hidden="true">&times;</span>
          </button>
        </div>
        <div class="modal-body">
          <form v-on:submit.prevent="addVault">
            <div class="form-group">
              <input type="text" name="name" v-model="vault.name" class="form-control" id="formGroupExampleInput" placeholder="Title" required>
            </div>
            <div class="form-group">
              <input type="text" name="description" v-model="vault.description" class="form-control" id="formGroupExampleInput" placeholder="Description">
            </div>
          </form>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
          <button type="submit" class="btn btn-primary" @click="addVault" data-dismiss="modal">Submit</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
  export default {
    name: 'CreateVault',
    props: {
      user: {
        type: Object,
        required: true
      }
    },
    data() {
      return {
        vault: {
          name: "",
          description: ""
        }
      }
    },
    methods: {
      addVault() {
        this.vault.userId = this.user.id
        this.$store.dispatch('addVault', this.vault)
      }
    }
  }
</script>

<style>
</style>