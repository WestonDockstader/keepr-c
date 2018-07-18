<template>
  <div class="modal fade" id="viewKeepModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered" role="document">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" id="exampleModalCenterTitle">{{viewKeep.name}}</h5>
          <button type="button" class="close" data-dismiss="modal" aria-label="Close">
            <span aria-hidden="true">&times;</span>
          </button>
        </div>
        <div class="modal-body text-center">
          <img class="view-img" :src="viewKeep.address" alt="viewKeep.description">
          <p>{{viewKeep.description}}</p>
          <p>Views: {{viewKeep.views}}</p>
          <p>Keeps: {{viewKeep.keeps}}</p>
          <div v-if="viewKeep.shareable==1 || viewKeep.userId==user.id">
            <button v-if="viewKeep.userId==user.id" class="btn btn-outline-warning" @click="deleteKeep(viewKeep)" data-dismiss="modal">X</button>
          </div>
        </div>
        <div class="modal-footer d-flex justify-content-around">
          <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
          <form v-on:submit.prevent="">
            <div class="form-group">
              <!-- <input type="text" name="name" v-model="keep.name" class="form-control" id="formGroupExampleInput" placeholder="Title" required> -->
              <!-- This is my dropdown for vaults -->
              <select v-model="selected">
                <option disabled value="">Select Vault</option>
                <option v-for="vault in vaults" v-bind:value="vault.id">{{vault.name}}</option>
              </select>

            </div>
          </form>
          <button type="submit" class="btn btn-primary" data-dismiss="modal" @click="addToVault(viewKeep)">Save to Vault</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
  export default {
    name: 'ViewKeep',
    props: {
      viewKeep: {
        type: Object,
        required: true
      },
      vaults: {
        type: Array,
        required: true
      },
      user: {
        type: Object,
        required: true
      }
    },
    data() {
      return {
        selected: "",
        vaultkeep: {
          vaultId: '',
          keepId: '',
          userId: ''
        }
      }
    },
    methods: {
      deleteKeep(keep) {
        this.$store.dispatch('deleteKeep', keep.id)
      },
      addToVault(keep) {
        keep.keeps++
        this.$store.dispatch('editKeep', keep)
        this.vaultkeep.vaultId = this.selected
        this.vaultkeep.keepId = keep.id
        this.vaultkeep.userId = this.user.id
        console.log(this.vaultkeep)
        this.$store.dispatch('addToVaultKeep', this.vaultkeep)
        this.selected = ''
      }
    }
  }
</script>

<style>
  .view-img {
    display: inline-block;
  }
  .modal-dialog{text-align:center;}
  .modal-content{display:inline-block;}
</style>