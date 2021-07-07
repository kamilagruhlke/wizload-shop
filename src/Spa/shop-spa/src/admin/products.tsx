import axios from 'axios';
import { Box, Button, DataTable, Layer, Spinner, Text } from 'grommet';
import React from 'react';
import { API_GATEWAY } from '../configuration/url';
import ProductEdit from './components/productEdit';

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


export default class Products extends React.Component<{}, {products: IProduct[], isLoading: boolean, selectedProduct: IProduct, layerVisible: boolean}> {
    state = {
        products: [],
        isLoading: true,
        selectedProduct: {
            Id: '',
            Name: '',
            Description: '',
            Specification: '',
            ProducerId: '',
            ProducerCode: '',
            CategoryId: '',
            NetPrice: '',
            Tax: '',
            GrossPrice: ''
        },
        layerVisible: false
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

        return (<div>
            <div style={{textAlign: 'center', margin:'1em'}}>
                <Button label="New product" onClick={() => {
                    this.setState({ selectedProduct: {
                      Id: '',
                      Name: '',
                      Description: '',
                      Specification: '',
                      ProducerId: '',
                      ProducerCode: '',
                      CategoryId: '',
                      NetPrice: '',
                      Tax: '',
                      GrossPrice: ''
                    }, layerVisible: true })}} />
            </div>

        <DataTable
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
                render: (data : IProduct) => (<Button label={'Edit'} size={'small'} onClick={() => {
                    this.setState({ selectedProduct: data, layerVisible: true })
                }}/>),
              }
            ]}
            data={this.state.products}
          />

          {this.state.layerVisible ?
            <Layer modal={true} onEsc={() => this.setState({layerVisible: false})} onClickOutside={() => this.setState({layerVisible: false})}>
                <ProductEdit id={this.state.selectedProduct.Id} 
                  name={this.state.selectedProduct.Name} 
                  description={this.state.selectedProduct.Description} 
                  specification={this.state.selectedProduct.Specification} 
                  producerId={this.state.selectedProduct.ProducerId} 
                  producerCode={this.state.selectedProduct.ProducerCode} 
                  categoryId={this.state.selectedProduct.CategoryId} 
                  netPrice={this.state.selectedProduct.NetPrice} 
                  tax={this.state.selectedProduct.Tax} 
                  grossPrice={this.state.selectedProduct.GrossPrice} 
                  onSubmit={() => {
                     this.setState({layerVisible: false});
                     this.loadProducts();
                }} />
                <div style={{background:'#9C3848', color:'#fff', letterSpacing:'2px', textAlign:'center', padding:'1em', cursor:'pointer'}}
                    onClick={() => this.setState({layerVisible: false})}>
                    <b>CLOSE</b>
                </div>
            </Layer> : null}
      </div>);
    }
}