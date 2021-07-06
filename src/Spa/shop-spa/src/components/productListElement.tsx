import axios from 'axios';
import { Spinner } from 'grommet';
import React from 'react';
import { FaWindowClose } from 'react-icons/fa';
import { API_GATEWAY } from '../configuration/url';
import ProductImage from './productImage';

interface IProduct {
    Id: string,
    Name: string,
    Description: string,
    Specification: string,
    ProducerId: string,
    CategoryId: string,
    NetPrice: string,
    Tax: string,
    GrossPrice: string
}

export default class ProductListItem extends React.Component<{productId: string, onLoaded:(grossPrice: number) => void, onRemove:(productId: string) => void}, 
    {images: string[], isLoading: boolean, product: IProduct}> 
{
    state = {
        product: {
            Id: '',
            Name: '',
            Description: '',
            Specification: '',
            ProducerId: '',
            CategoryId: '',
            NetPrice: '',
            Tax: '',
            GrossPrice: '',
        },
        images: [],
        isLoading: true
    }

    async componentDidMount() {
        let res = await axios.get(`${API_GATEWAY}/images/Products/Images?productIds=${this.props.productId}`);
        if (res.data.length > 0) {
            this.setState({ images: res.data[0].Urls });
        }

        res = await axios.get(`${API_GATEWAY}/products/api/Products/${this.props.productId}`);
        this.setState({ product: res.data, isLoading: false });

        this.props.onLoaded(res.data.GrossPrice);
    }

    render () {
        if (this.state.isLoading) {
            return (<Spinner />);
        }

        return (<div>
            <div style={{display:'flex'}}>
                <div>
                    <FaWindowClose size={24} 
                    color={'#9C3848'} 
                    style={{cursor:'pointer'}} 
                    onClick={() => { this.props.onRemove(this.props.productId) }}/>
                </div>
                <div style={{minWidth: '168px', maxWidth:'168px'}}>
                    <ProductImage images={this.state.images} />
                </div>
                <div style={{marginLeft: '0.5em'}}>
                    <h3>{this.state.product.Name}</h3>
                    <p style={{fontSize: '12px'}}>{this.state.product.Description}</p>
                    <p style={{letterSpacing:'1px', color:'#1E3888'}}>{this.state.product.GrossPrice} PLN</p>
                </div>
            </div>
        </div>);
    }
}