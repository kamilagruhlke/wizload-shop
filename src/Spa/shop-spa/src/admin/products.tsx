import axios from 'axios';
import { Box, Button, DataTable, Spinner, Text } from 'grommet';
import React from 'react';
import { API_GATEWAY } from '../configuration/url';

interface IProduct {
    Id: string,
    Name: string,
    Description: string,
    Specification: string,
    ProducerId: string,
    ProducerCode: string,
    CategoryId: string,
    NetPrice: string,
    Tax: string,
    GrossPrice: string
}


export default class Products extends React.Component<{}, {products: IProduct[], isLoading: boolean}> {
    state = {
        products: [],
        isLoading: true
    }

    componentDidMount() {
        this.loadProducts();
    }

    loadProducts = () => {
        this.setState({isLoading: true})

        axios.get(`${API_GATEWAY}/products/api/Products/Last/1000`).then(res => {
            const products = res.data;
            this.setState({products, isLoading: false});
        });
    }

    render () {
        if (this.state.isLoading) {
            return (<Box justify='center' direction='row' gap='small' pad='small'>
                <Spinner size='medium' />
                <Text alignSelf='center'>Loading...</Text>
            </Box>);
        }

        return (<DataTable
            style={{margin: '0 auto', marginTop: '1em'}}
            columns={[
              {
                property: 'Id',
                header: <Text>Id</Text>,
                primary: true,
              },
              {
                property: 'ProducerCode',
                header: <Text>ProducerCode</Text>
              },
              {
                property: 'Name',
                header: <Text>Name</Text>
              },
              {
                property: 'NetPrice',
                header: <Text>Net</Text>
              },
              {
                property: 'Tax',
                header: <Text>Tax</Text>
              },
              {
                property: 'GrossPrice',
                header: <Text>Gross</Text>
              },
              {
                property: 'Edit',
                header: <Text>Edit</Text>,
                render: data => (
                    <Button label={'Edit'} size={'small'} />
                  ),
              }
            ]}
            data={this.state.products}
          />);
    }
}