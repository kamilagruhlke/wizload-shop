import axios from 'axios';
import React from 'react';
import { FaShoppingBasket } from "react-icons/fa";
import { API_GATEWAY } from '../configuration/url';
import './styles/footer.css';

export default class BasketIcon extends React.Component<{}, {productsCount: number}> {
    state = {
        productsCount: 0
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
            if (data.productIds !== null) {
                this.setState({productsCount: data.productIds.length});
            }
        });
    }

    render () {
        return (
            <div style={{position:'relative'}}>
                {this.state.productsCount > 0 ?
                <div style={{position: 'absolute', background:'#1E3888', left: '-18px', borderRadius:'25px', paddingLeft:'5px', paddingRight:'5px', color:'#fff', fontSize: 12}}>
                    {this.state.productsCount}
                </div> : null}
                <FaShoppingBasket size={20}/>
            </div>
        );
    }
}