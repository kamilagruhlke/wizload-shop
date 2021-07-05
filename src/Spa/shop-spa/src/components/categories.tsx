import axios from 'axios';
import { Anchor, Nav } from 'grommet';
import React from 'react';
import { API_GATEWAY } from '../configuration/url';

export default class Categories extends React.Component <{}, {categories: any}> {
    state = {
        categories: []
      };

    componentDidMount() {
        axios.get(`${API_GATEWAY}/categories/api/Categories/Active`).then(res => {
            const categories = res.data;
            this.setState({ categories });
        });
    }
    
    render () {
        return (
            <Nav direction="row-responsive" background="brand" pad="medium">
                <Anchor label={'Home'} href='/' color="#cbbde2" />
                {this.renderCategories()}
            </Nav>
        );
    }

    renderCategories = () => {
        return this.state.categories.map((category: any) => {
          return <Anchor key={category.Id} label={category.Name} href={`/products/${category.Id}`} color="#cbbde2" />;
        });
    }
}