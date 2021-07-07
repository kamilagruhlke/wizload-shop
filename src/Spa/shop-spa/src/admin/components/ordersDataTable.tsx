import axios from 'axios';
import { Button, DataTable, Spinner, Text, Box, Layer } from 'grommet';
import React from 'react';
import { API_GATEWAY } from '../../configuration/url';
import OrderEdit from './orderEdit';

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


export default class OrdersDataTable extends React.Component<{status: string}, {orders: IOrder[], isLoading: boolean, selectedOrder: IOrder, layerVisible: boolean}> {
    state = {
        orders: [],
        isLoading: true,
        selectedOrder: {
          Id: '',
          OrderedProducts: [],
          Status: '',
          ValueNet: 0,
          ValueTax: 0,
          Address: '',
          City: '',
          PostalCode: '',
          ClientFullName: '',
          Email: '',
          PhoneNumber: '',
          CreatedBy: '',
          CreatedAt: '',
          UpdatedAt: '',
          UpdatedBy: ''
        },
        layerVisible: false
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

        return (<div>
          <DataTable
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
                  render: (data : IOrder) => (<Button label={'Edit'} size={'small'} onClick={() => {
                      this.setState({ selectedOrder: data, layerVisible: true })
                  }}/>),
                }
              ]}
              data={this.state.orders}
          />

          {this.state.layerVisible ?
            <Layer modal={true} onEsc={() => this.setState({layerVisible: false})} onClickOutside={() => this.setState({layerVisible: false})}>
                <OrderEdit id={this.state.selectedOrder.Id} 
                  status={this.props.status}
                  valueNet={this.state.selectedOrder.ValueNet}
                  valueTax={this.state.selectedOrder.ValueTax}
                  address={this.state.selectedOrder.Address}
                  city={this.state.selectedOrder.City}
                  postalCode={this.state.selectedOrder.PostalCode}
                  orderedProducts={this.state.selectedOrder.OrderedProducts.map((e:IOrderedProduct) => e.ProductId)}
                  onSubmit={() => {
                     this.setState({layerVisible: false});
                     this.loadOrders();
                }} />
                <div style={{background:'#9C3848', color:'#fff', letterSpacing:'2px', textAlign:'center', padding:'1em', cursor:'pointer'}}
                    onClick={() => this.setState({layerVisible: false})}>
                    <b>CLOSE</b>
                </div>
            </Layer> : null}

        </div>);
    }
}