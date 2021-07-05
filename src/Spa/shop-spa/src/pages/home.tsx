import { Nav, Text } from 'grommet';
import React from 'react';
import { API_GATEWAY } from '../configuration/url';
import axios from 'axios';
import Categories from '../components/categories';
import ProductBoxs from '../components/productBoxs';

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
          <Nav direction="row-responsive" background="brand" pad="small">
            <Text style={{fontWeight:'bold'}}>Get 10% off with promo code #stayhome</Text>
          </Nav>
          <div style={
            {
              background:'url("/img/carousel/1.jpg")', 
              minHeight:'512px', 
              backgroundAttachment:'fixed', 
              backgroundPosition:'center', 
              backgroundRepeat:'no-repeat', 
              backgroundSize:'cover'
            }
          }></div>
          <Categories />
          <ProductBoxs products={this.state.products} isLoading={this.state.homePageIsLoading} />
        </div>
      );
    }
}