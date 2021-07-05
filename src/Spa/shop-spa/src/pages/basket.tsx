import axios from 'axios';
import { Box } from 'grommet';
import React from 'react';
import Categories from '../components/categories';
import { API_GATEWAY } from '../configuration/url';

export default class Basket extends React.Component<{}, {productIds: string[]}> {
    state = {
        productIds: []
    }

    componentDidMount() {
        let basketId = localStorage.getItem('basket');
        let accessToken = localStorage.getItem('access_token');
        axios.get(`${API_GATEWAY}/basket/api/Basket/${basketId}`, {
            headers: {
              'Authorization': `Bearer ${accessToken}` 
            }
        }).then(res => {
            const data = res.data;
            this.setState({productIds: data.productIds})
        });
    }

    render () {
        return (
            <Box>
                <Categories />
                {this.state.productIds}
            </Box>
        );
    }
}