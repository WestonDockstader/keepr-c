<template>
  <div class="home container-fluid">
    <nav class="navbar navbar-dark bg-dark">
      <h2 class="navbar-heading">KEEPR</h2>
      <div>
        <div v-if="!user.username">
          <button class="btn btn-outline-info" data-toggle="modal" data-target="#registerModal">Register</button>
          <button class="btn btn-outline-info" data-toggle="modal" data-target="#loginModal">Login</button>
        </div>
        <div v-if="user.username">
          <h3>Hello {{user.username}}</h3>
          <button class="btn btn-outline-info" @click="logout">Logout</button>
          <button v-if="home" class="btn btn-outline-info" @click="home=false, profile=true">My Profile</button>
          <button v-if="profile" class="btn btn-outline-info" @click="home=true, profile=false">Home</button>
          <button class="btn btn-outline-info" data-toggle="modal" data-target="#addKeepModal">+</button>
        </div>
      </div>
    </nav>

    <register></register>
    <login></login>
    <createKeep :user="user"></createKeep>
    <viewKeep :viewKeep="viewKeep" :vaults="vaults" :user="user"></viewKeep>
    <createVault :user="user"></createVault>

    <!-- display all public keeps -->
    <div class="row d-flex justify-content-around pt-3" v-if="home">
      <div v-for="keep in keeps" v-if="!keep.shareable" class="keep">
        <h3>{{keep.name}}</h3>
        <img :src="keep.address" class="keep-img" alt="">
        <div class="place-btn text-center">
          <button class="btn btn-outline-warning btn-view" @click="setViewKeep(keep)">View</button>
          <div class="dropdown place-btn-dropdown">
            <!-- this is the dropdown button -->
            <button class="btn btn-outline-warning dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true"
              aria-expanded="false">
              Add To Vault
            </button>
            <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
              <button class="dropdown-item" v-for="vault in vaults" @click="addToVault(keep),selected=vault.id">{{vault.name}}</button>
            </div>
          </div>
          <!-- <button class="btn btn-outline-warning" disabled>Share</button> -->
        </div>
      </div>
    </div>
    <!-- display profile -->
    <div class="row" v-if="profile">
      <div class="col-6 offset-3 mt-2 d-flex justify-content-around">
        <button class="btn btn-outline-success" data-toggle="modal" data-target="#addVaultModal">Create Vault</button>
        <button class="btn btn-outline-success" @click="profileView=0">My Vaults</button>
        <button class="btn btn-outline-success" @click="profileView=1">My Keeps</button>
      </div>
      <div class="col-12">
        <hr>
      </div>
      <div class="col" v-if="profileView==0">
        <!-- view a list of vaults -->
        <h3 v-if="viewvaults">Your Vaults</h3>
        <div v-if="viewvaults" v-for="vault in vaults" class="vault d-flex flex-row justify-content-around" @click="getVaultKeeps(vault.id),viewvaults=false,vaultname=vault.name">
          <h4>{{vault.name}}</h4>
          <img src="../../../vault-door-clipart-1.jpg" class="vault-img" alt="">
          <button class="btn btn-outline-danger" @click="deleteVault(vault)">X</button>
        </div>
        <!-- View vault after selected -->
        <div v-if="!viewvaults">
          {{vaultname}}
          <button class="btn btn-outline-success" @click="viewvaults=true,vaultname=''">Back</button>
        </div>
        <div class="keep" v-if="!viewvaults" v-for="keep in vaultkeeps">
          <h3>{{keep.name}}</h3>
          <img :src="keep.address" class="keep-img" alt="">
          <div class="place-btn text-center">
            <button class="btn btn-outline-warning btn-view" @click="setViewKeep(keep)">View</button>
            <div v-if="keep.shareable" class="btn-group-toggle" data-toggle="buttons">
              <label class="btn btn-secondary active" for="checkbox" @click="toggleSetting">
                <input type="checkbox" id="checkbox" checked autocomplete="off"> {{pSetting}}
              </label>
              <button @click="editKeep(keep)" class="btn btn-outline-success">Submit</button>
            </div>
          </div>
        </div>
      </div>
      <!-- view profile keeps -->
      <div class="col d-flex d-inline" v-if="profileView==1">
        <div class="keep" v-for="keep in keeps" v-if="keep.userId==user.id">
          <h3>{{keep.name}}</h3>
          <img :src="keep.address" class="keep-img" alt="">
          <div class="place-btn">
            <button class="btn btn-outline-warning btn-view" @click="setViewKeep(keep)">View</button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
  import register from './Register'
  import login from './Login'
  import createKeep from './CreateKeep'
  import viewKeep from './ViewKeep'
  import createVault from './CreateVault'
  export default {
    name: 'Home',
    components: {
      register,
      login,
      createKeep,
      viewKeep,
      createVault
    },
    data() {
      return {
        //pSetting - Privacy Setting is passed to Create Keep
        pSetting: "Private",
        home: true,
        profile: false,
        show: 0,
        viewKeep: {},
        profileView: 0,
        viewvaults: true,
        vaultname: ''
      }
    },
    mounted() {
      this.$store.dispatch('getKeeps')
    },
    computed: {
      user() {
        return this.$store.state.user
      },
      keeps() {
        return this.$store.state.keeps
      },
      vaults() {
        return this.$store.state.vaults
      },
      vaultkeeps() {
        return this.$store.state.vaultkeeps
      }
    },
    watch: {
      user: function () {
        this.$store.dispatch('getVaults', this.user.id)
      }
    },
    methods: {
      logout() {
        this.home = true
        this.profile = false
        this.$store.dispatch('logout')
      },
      editKeep(payload) {
        payload.shareable = this.show
        this.$store.dispatch('editKeep', payload)
        this.pSetting = "Private"
      },
      setViewKeep(payload) {
        payload.views++
        payload.shareable = payload.shareable ? 1 : 0
        this.$store.dispatch('editKeep', payload)
        this.viewKeep = payload
        $('#viewKeepModal').modal('show')
      },
      deleteVault(vault) {
        this.$store.dispatch('deleteVault', vault.id)
      },
      getVaultKeeps(payload) {
        this.$store.dispatch('getVaultKeeps', payload)
      }
    }
  }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  .navbar {
    background-color: darkslategray;
    color: rgb(87, 236, 241);
    height: 100%;
  }

  .navbar-heading {
    padding: 3px;
  }

  .vault {
    background-color: rgba(100, 100, 100, .5);
    border-radius: 10px;
    padding: 0.5rem;
  }

  .keep {
    border-radius: 20px;
    padding: 1rem 1rem;
    margin: 0.5rem;
    position: relative;
  }

  .keep:hover {
    background-color: rgba(49, 79, 79, 0.5);
  }

  .keep-img {
    height: 300px;
    width: 300px;
    border-radius: 10px;
  }

  .place-btn {
    position: absolute;
    top: 85%;
    width: 300px;
    /* visibility: hidden; */
  }

  .btn-view {
    float: left;
  }

  .place-btn-dropdown {
    float: right;
    width: auto;
  }

  .place-btn:hover {
    visibility: visible;
  }

  .view-img {
    display: inline-block;
  }
  .vault-img {
    height: 250px;
    width: 250px;
  }
</style>