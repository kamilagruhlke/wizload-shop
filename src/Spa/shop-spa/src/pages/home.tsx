import { Box, Card, CardBody, Carousel, Image, Tab, Tabs, CardFooter, Spinner, Nav, Anchor } from 'grommet';
import React from 'react';
import { Link } from 'react-router-dom';
import { API_GATEWAY } from '../configuration/url';
import axios from 'axios';

export default class Home extends React.Component<{}, {products: any, homePageIsLoading: boolean, categories: any}> {
    state = {
      products: [],
      homePageIsLoading: true,
      categories: []
    };
  
    componentDidMount() {
      axios.get(`${API_GATEWAY}/products/api/Products/Last/20`).then(res => {
        const products = res.data;
        this.setState({ products, homePageIsLoading: false });
      });

      axios.get(`${API_GATEWAY}/categories/api/Categories/Active`).then(res => {
        const categories = res.data;
        this.setState({ categories });
      });
    }
  
    render () {
      return (
        <div>
          <Box height="medium" width="100%" overflow="hidden" background="light-6">
            <Carousel fill controls="arrows">
              <Image fit="cover" src="img/carousel/1.jpg" />
              <Image fit="cover" src="img/carousel/2.jpg" />
              <Image fit="cover" src="img/carousel/3.jpg" />
            </Carousel>
          </Box>
          <Nav direction="row-responsive" background="brand" pad="medium">
            {this.renderCategories()}
          </Nav>
          <Box pad="medium">
            <Box pad="medium" direction="row" gap="medium" wrap={true}>
              {this.renderProducts()}
            </Box>
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
        return <Link key={product.Id} to={`product/${product.Id}`} style={{ color: 'inherit', textDecoration: 'inherit'}}>
          <Box pad='medium' margin='small'>
            <Card height="300px" width="300px">
              <CardBody>
                <Image src="img/carousel/1.jpg" fit="cover"/>
              </CardBody>
              <CardFooter pad="small">Nazwa produktu</CardFooter>
            </Card>
          </Box>
        </Link>;
      });
    };

    renderCategories = () => {
      return this.state.categories.map((category: any) => {
        return <Anchor key={category.Id} label={category.Name} href={`products/${category.Id}`} />;
      });
    }
}