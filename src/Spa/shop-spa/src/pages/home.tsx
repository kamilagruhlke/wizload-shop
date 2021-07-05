import React from 'react';
import { API_GATEWAY } from '../configuration/url';
import axios from 'axios';
import Categories from '../components/categories';
import ProductBoxs from '../components/productBoxs';
import Footer from '../components/footer';

export default class Home extends React.Component<{}, {products: any, homePageIsLoading: boolean}> {
    state = {
      products: [],
      homePageIsLoading: true,
    };
  
    componentDidMount() {
      axios.get(`${API_GATEWAY}/products/api/Products/Last/20`).then(res => {
        const products = res.data;
        this.setState({ products, homePageIsLoading: false });
      });
    }
  
    render () {
      return (
        <div>
          <div style={{
            background:'url("/img/carousel/2.jpg")', 
            minHeight:'820px', 
            backgroundAttachment:'fixed', 
            backgroundPosition:'center', 
            backgroundRepeat:'no-repeat', 
            backgroundSize:'cover' }}>
          </div>

          <h1 style={{textAlign: 'center', marginTop:'2em'}}>Categories</h1>
          <Categories />

          <hr style={{ border: '1px solid #fafafa' }} />

          <h1 style={{textAlign: 'center', marginTop: '2em'}}>Featured products</h1>
          <ProductBoxs products={this.state.products} isLoading={this.state.homePageIsLoading} />

          <Footer />
        </div>
      );
    }
}