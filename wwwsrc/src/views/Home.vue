<template>
  <div class="home container-fluid">
    <div class="row">
      <div class="col-6">
        <h1>Home</h1>
        <form @submit.prevent="addKeep">
          <div v-if="show" class="form-row m-3">
            <div class="col">
              <input v-model="newKeep.name" type="text" class="form-control" placeholder="name" />
            </div>
            <div class="col">
              <input
                v-model="newKeep.description"
                type="text"
                class="form-control"
                placeholder="description"
              />
            </div>
            <div class="col">
              <input v-model="newKeep.img" type="url" class="form-control" placeholder="Img" />
            </div>
            <div class="col">
              <input v-model="newKeep.isPrivate" type="checkbox" class="form-check-iput" />
              <a style="font-size: 7px;">private</a>
            </div>
            <div class="col">
              <button type="submit" class="btn btn-success">+</button>
            </div>
          </div>
        </form>
      </div>
      <div class="col-6">
        <h5>Public Keeps</h5>
        <button @click="show = !show" class="btn btn-warning m-2">
          <i class="fa fa-ban"></i>
        </button>
      </div>
      <div>
        <keep />
      </div>
    </div>
  </div>
</template>

<script>
import keep from "@/components/KeepComponent.vue";
export default {
  name: "home",
  data() {
    return {
      newKeep: {
        name: "",
        description: "",
        img: "",
        isPrivate: false,
        keeps: 0,
        shares: 0,
        views: 0,
      },
      newVaultKeep: {
        vaultId: "",
        keepId: "",
      },
      show: false,
    };
  },
  mounted() {
    this.$store.dispatch("getPublicKeeps");
  },
  computed: {
    user() {
      return this.$store.state.user;
    },
    publicKeeps() {
      return this.$store.state.publicKeeps;
    },
  },
  methods: {
    logout() {
      this.$store.dispatch("logout");
    },
    addKeep() {
      let keep = { ...this.newKeep };

      console.log("add keep in home.vue", keep);
      this.$store.dispatch("addKeep", keep);
      this.newKeep = {
        name: "",
        description: "",
        img: "",
        isPrivate: false,
        keeps: 0,
        shares: 0,
        views: 0,
      };
    },
  },
  components: {
    keep,
  },
};
</script>
