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
    <!-- This is the register modal -->
    <div class="modal fade" id="registerModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
      <div class="modal-dialog modal-dialog-centered" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="exampleModalCenterTitle">Register</h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body">
            <form v-on:submit.prevent="userLogin">
              <div class="form-group">
                <input type="text" name="username" v-model="register.username" class="form-control" id="formGroupExampleInput" placeholder="Username"
                  required>
              </div>
              <div class="form-group">
                <input type="text" name="email" v-model="register.email" class="form-control" id="formGroupExampleInput" placeholder="email"
                  autocomplete="email">
              </div>
              <div class="form-group">
                <input type="text" name="password" v-model="register.password" class="form-control" id="formGroupExampleInput" placeholder="Password">
              </div>
            </form>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
            <button type="submits" class="btn btn-primary" @click="userRegister" data-dismiss="modal">Register</button>
          </div>
        </div>
      </div>
    </div>
    <!-- This is the login modal -->
    <div class="modal fade" id="loginModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
      <div class="modal-dialog modal-dialog-centered" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="exampleModalCenterTitle">Login</h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body">
            <form v-on:submit.prevent="userLogin">
              <div class="form-group">
                <input type="text" name="email" v-model="login.email" class="form-control" id="formGroupExampleInput" placeholder="Email"
                  autocomplete="email" required>
              </div>
              <div class="form-group">
                <input type="text" name="password" v-model="login.password" class="form-control" id="formGroupExampleInput" placeholder="Password">
              </div>
            </form>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
            <button type="submit" class="btn btn-primary" @click="userLogin" data-dismiss="modal">Login</button>
          </div>
        </div>
      </div>
    </div>
    <!-- Add Keep Modal -->
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
    <!-- View Keep Modal -->
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
              <img :src="viewKeep.address" alt="viewKeep.description">
              <p>{{viewKeep.description}}</p>
              <p>Views: {{viewKeep.views}}</p>
              <p>Keeps: {{viewKeep.keeps}}</p>
              <button v-if="viewKeep.userId==user.id" class="btn btn-outline-warning" @click="deleteKeep(viewKeep)" data-dismiss="modal">X</button>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
            <button type="submit" class="btn btn-primary" @click="" data-dismiss="modal">Submit</button>
          </div>
        </div>
      </div>
    </div>
    <!-- add vault modal -->
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
    <!-- display all public keeps -->
    <div class="row d-flex justify-content-around pt-3" v-if="home">
      <div v-for="keep in keeps" class="keep" @click="setViewKeep(keep)">
        <h3>{{keep.name}}</h3>
        <img :src="keep.address" alt="">
      </div>
    </div>
    <div class="row" v-if="profile">
      <button class="btn btn-outline-primary" data-toggle="modal" data-target="#addVaultModal">Create Vault</button>
      <div v-for="vault in vaults">
        {{vault.name}}
      </div>
    </div>
  </div>
</template>

<script>
  export default {
    name: 'Home',
    data() {
      return {
        register: {
          username: "",
          email: "",
          password: ""
        },
        login: {
          email: "",
          password: ""
        },
        keep: {
          name: "",
          description: "",
          address: "",
          userId: "",
          private: 0,
          views: 0,
          keeps: 0
        },
        pSetting: "Public",
        home: true,
        profile: false,
        show: 0,
        viewKeep: {},
        vault:{
          name: "",
          description: ""
        }
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
      }
    },
    watch: {
      user: {
        handler(user){
          this.$store.dispatch('getVaults', user.id)
        }
      }
    },
    methods: {
      userLogin() {
        this.$store.dispatch('login', this.login)
      },
      userRegister() {
        this.$store.dispatch('register', this.register)
      },
      logout() {
        this.home=true
        this.profile=false
        this.$store.dispatch('logout')
      },
      addKeep() {
        this.keep.userId = this.user.id
        this.keep.private = this.show
        console.log(this.keep)
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
        console.log(this.show)
      },
      deleteKeep(post) {
        this.$store.dispatch('deleteKeep', post.id)
      },
      editKeep(keep) {
        this.$store.dispatch('editKeep', keep)
      },
      setViewKeep(payload) {
        payload.views++
        payload.private = 0
        this.$store.dispatch('editKeep', payload)
        this.viewKeep = payload
        $('#viewKeepModal').modal('show')
      },
      addVault(){
        this.vault.userId=this.user.id
        this.$store.dispatch('addVault', this.vault)
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

  .keep {
    background-color: rgba(100, 100, 100, 0.5);
    border-radius: 20px;
    padding: 1rem;
  }
</style>