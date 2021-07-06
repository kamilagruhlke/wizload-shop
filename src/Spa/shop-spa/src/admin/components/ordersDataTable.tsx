import axios from 'axios';
import { Button, DataTable, Spinner, Text, Box } from 'grommet';
import React from 'react';
import { API_GATEWAY } from '../../configuration/url';

interface IOrderedProduct {
    Id: string,
    ProductId: string
}

interface IOrder {
    Id: string,
    OrderedProducts: IOrderedProduct[],
    Status: string,
    ValueNet: number,
    ValueTax: number,
    Address: string,
    City: string,
    PostalCode: string,
    ClientFullName: string,
    Email: string,
    PhoneNumber: string,
    CreatedBy: string,
    CreatedAt: string,
    UpdatedAt: string,
    UpdatedBy: string
}


export default class OrdersDataTable extends React.Component<{status: string}, {orders: IOrder[], isLoading: boolean}> {
    state = {
        orders: [],
        isLoading: true
    }

    componentDidMount() {
        this.loadOrders();
    }

    componentDidUpdate(prev: {status: string}) {
        if (prev.status !== this.props.status) {
            this.loadOrders();
        }
    }

    loadOrders = () => {
        //PENDING //IN_PROGRESS //FINISHED
        this.setState({isLoading: true});

        let accessToken = localStorage.getItem('access_token');
        axios.get(`${API_GATEWAY}/orders/api/Orders/Status/${this.props.status}`, {
            headers: {
              'Authorization': `Bearer ${accessToken}` 
            }
        }).then(res => {
            const orders = res.data;
            this.setState({orders: orders, isLoading: false});
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
                property: 'Status',
                header: <Text>Status</Text>,
              },
              {
                property: 'ValueNet',
                header: <Text>Net</Text>,
              },
              {
                property: 'ValueTax',
                header: <Text>Tax</Text>,
              },
              {
                property: 'ClientFullName',
                header: <Text>Full name</Text>,
              },
              {
                property: 'Email',
                header: <Text>Email</Text>,
              },
              {
                property: 'CreatedAt',
                header: <Text>Created</Text>,
              },
              {
                property: 'Edit',
                header: <Text>Edit</Text>,
                render: data => (
                    <Button label={'Edit'} size={'small'} />
                ),
              }
            ]}
            data={this.state.orders}
        />);
    }
}