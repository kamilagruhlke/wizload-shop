import axios from 'axios';
import { Box } from 'grommet';
import React from 'react';
import Categories from '../components/categories';
import ProductBoxs from '../components/productBoxs';
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
                <ProductBoxs products={this.state.products} isLoading={false}/>
            </Box>
        );
    }
}