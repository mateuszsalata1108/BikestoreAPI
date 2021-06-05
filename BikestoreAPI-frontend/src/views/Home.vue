<template>
  <div class="home">
    <h1>Bikestore</h1>
      <div>
        <p>
        <b-button variant="success" size="sm" @click="$bvModal.show('add-modal')">Add a record</b-button>&nbsp;
        <b-button v-if="selected" variant="primary" size="sm" @click="onEditClick();$bvModal.show('edit-modal');">Edit selected record</b-button>&nbsp;
        <b-button v-if="selected" variant="danger" size="sm" @click="$bvModal.show('delete-modal')">Delete selected record</b-button> 
        </p>
      </div>
    <div>
      <b-table
        id="bikes"
        striped
        hover
        :fields="fields"
        :items="products"
        :sort-by.sync="sortBy"
        :sort-desc.sync="sortDesc"
        responsive
        selectable
        select-mode="single"
        @row-selected="onRowSelected"
        >
        <template v-slot:cell(availability)="availability">
          <b-form-group>
              <input id="checkbox" onclick="return false;" type="checkbox" v-model="availability.item.availability" />
          </b-form-group>
        </template>
        </b-table>
    </div>
    <b-modal id="add-modal" hide-footer>
      <div>
        <h3>Adding a product</h3>
        <div>
          <b-form @submit="productAdd()"><br>
            <b-form-group id="name" label="Product name" label-for="name">
              <b-form-input id="name" v-model="productPost.name" required></b-form-input>
            </b-form-group><br>
            <b-form-group id="type" label="Product type" label-for="type">
              <b-form-input id="type" v-model="productPost.type" required></b-form-input>
            </b-form-group><br>
            <b-form-group id="manufacturer" label="Manufacturer" label-for="manufacturer">
              <b-form-input id="manufacturer" v-model="productPost.manufacturer" required></b-form-input>
            </b-form-group><br>
            <b-form-group id="price" label="Price" label-for="price">
              <b-form-input id="price" v-model.number="productPost.price" required></b-form-input>
            </b-form-group><br>
            <b-form-checkbox id="availability" v-model="productPost.availability">&nbsp;Availability</b-form-checkbox><br>
            <b-button type="submit" variant="primary">Submit</b-button>&nbsp;
            <b-button @click="$bvModal.hide('add-modal')" variant="danger">Cancel</b-button>
          </b-form>
        </div>
      </div>
    </b-modal>
    <b-modal id="edit-modal" hide-footer>
      <div v-if="product">
        <h3>Editing product with id {{product.id}}</h3>
        <div>
          <b-form @submit="productPut(product.id)"><br>
            <b-form-group id="name" label="Product name" label-for="name">
              <b-form-input id="name" v-model="product.name" required></b-form-input>
            </b-form-group><br>
            <b-form-group id="type" label="Product type" label-for="type">
              <b-form-input id="type" v-model="product.type" required></b-form-input>
            </b-form-group><br>
            <b-form-group id="manufacturer" label="Manufacturer" label-for="manufacturer">
              <b-form-input id="manufacturer" v-model="product.manufacturer" required></b-form-input>
            </b-form-group><br>
            <b-form-group id="price" label="Price" label-for="price">
              <b-form-input id="price" v-model.number="product.price" required></b-form-input>
            </b-form-group><br>
            <b-form-checkbox id="availability" v-model="product.availability">&nbsp;Availability</b-form-checkbox><br>
            <b-button type="submit" variant="primary">Submit</b-button>&nbsp;
            <b-button @click="$bvModal.hide('edit-modal')" variant="danger">Cancel</b-button>
          </b-form>
        </div>
      </div>
    </b-modal>
    <b-modal id="delete-modal" hide-footer>
      <div>
        <h4 v-if="selected">Are you sure you want to delete the product with id {{selected[0].id}}?</h4>
      </div>
      <b-button variant="primary" class="mt-3" block @click="$bvModal.hide('delete-modal')">Cancel</b-button>&nbsp;
      <b-button variant="danger" class="mt-3" block @click="productDelete(selected[0].id)">Yes</b-button>
    </b-modal>
  </div>
</template>

<script>
import axios from 'axios'

export default {
  name: 'Home',
  data () {
    return {
      sortBy: 'id',
      sortDesc: false,
      products: null,
      product: null,
      productPost: {
        type: '',
        name: '',
        manufacturer: '',
        price: 0,
        availability: false
      },
      selected: null,
      fields: [
          'id',
          'type',
          'name',
          'manufacturer',
          'price',
          'availability']
    }
  },
  async mounted () {
    try {
      const res = await axios.get('https://localhost:44335/api/products')
      this.products = res.data

    } catch(e) {
      console.error(e)
    }
  },
  methods: {
    onRowSelected(product) {
      this.selected = product
      console.log(this.selected)
    },
    async getProduct(id) {
    try{
      const res = await axios.get('https://localhost:44335/api/products/'+id)
      this.product = res.data

    } catch(e) {
        console.error(e)
      }
    },
    onEditClick() {
      this.getProduct(this.selected[0].id)
    },
    async productAdd() {
      console.log(this.productPost)
      try{
        await axios.post('https://localhost:44335/api/products', this.productPost)
      } catch(e) {
        console.error(e)
      }
    },
    async productDelete(id) {
      try {
        await axios.delete('https://localhost:44335/api/products/'+id)
        this.$router.go()
      } catch(e) {
        console.error(e)
      }
    },
    async productPut(id) {
      console.log(this.product)
      try {
       await axios.put('https://localhost:44335/api/products/'+id, this.product)
      } catch(e) {
        console.error(e)
      }
    },
  }

  }
</script>
