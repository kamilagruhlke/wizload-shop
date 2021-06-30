import { Box, Card, CardBody, Carousel, Image, CardFooter, Spinner } from 'grommet';
import React from 'react';
import { Link } from 'react-router-dom';
import { API_GATEWAY } from '../configuration/url';
import axios from 'axios';
import Categories from '../components/categories';

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
          <Box height="512px" width="100%" overflow="hidden" background="light-6">
            <Carousel fill controls="arrows">
              <Image fit="cover" src="img/carousel/1.jpg" />
              <Image fit="cover" src="img/carousel/2.jpg" />
              <Image fit="cover" src="img/carousel/3.jpg" />
            </Carousel>
          </Box>
          <Categories />
          <Box pad="medium" direction="row" gap="medium" wrap justify='center'>
            {this.renderProducts()}
          </Box>
        </div>
      );
    }

    renderProducts = () => {
      if (this.state.homePageIsLoading) {
        return <div style={{margin:"0 auto"}}>
          <Spinner size="medium" />
        </div>;
      }
      
      return this.state.products.map((product: any) => {
        return <Link key={product.Id} to={`product/${product.Id}`} style={{ color: 'inherit', textDecoration: 'inherit', margin:'1em'}}>
          <Card height="300px" width="300px">
            <CardBody>
              <Image src="img/carousel/1.jpg" fit="cover"/>
            </CardBody>
            <CardFooter pad="small">Nazwa produktu</CardFooter>
          </Card>
        </Link>;
      });
    };
}