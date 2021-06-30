import axios from 'axios';
import { Box, Card, Nav, Image, CardBody, CardFooter, Anchor } from 'grommet';
import React from 'react';
import { Link } from 'react-router-dom';
import Categories from '../components/categories';
import { API_GATEWAY } from '../configuration/url';

interface ProductsParameter {
    categoryId: string
}

export default class Products extends React.Component<ProductsParameter> {

    state = {
        products: []
    };

    componentDidMount() {
        axios.get(`${API_GATEWAY}/products/api/Products/ByCategoryId/${this.props.categoryId}`).then(res => {
            const products = res.data;
            this.setState({products});
        });
    }

    render () {
        return (
            <Box>
                <Categories />
                <Box pad="large" direction="row-responsive" gap="medium" wrap={true} justify='center'>
                    {this.renderProducts()}
                </Box>
            </Box>
        );
    }

    renderProducts = () => {
        return this.state.products.map((product: any) => {
          return <Link key={product.Id} to={`/product/${product.Id}`} style={{ color: 'inherit', textDecoration: 'inherit', margin:'1em'}}>
            <Card height="300px" width="300px">
                <CardBody>
                    <Image src="/img/carousel/2.jpg" fit="cover"/>
                </CardBody>
                <CardFooter pad="small">{product.Name}</CardFooter>
            </Card>    
          </Link>;
        });
    };
}