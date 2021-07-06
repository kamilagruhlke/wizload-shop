import axios from 'axios';
import { Box } from 'grommet';
import React from 'react';
import { Link } from 'react-router-dom';
import Categories from '../components/categories';
import ProductListItem from '../components/productListElement';
import { API_GATEWAY } from '../configuration/url';

export default class Basket extends React.Component<{basketId: string}, {productIds: string[], grossPrice: number}> {
    state = {
        productIds: [],
        grossPrice: 0
    }

    componentDidMount() {
        let accessToken = localStorage.getItem('access_token');
        axios.get(`${API_GATEWAY}/basket/api/Basket/${this.props.basketId}`, {
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
                <h1 style={{textAlign: 'center', marginTop:'2em'}}>Your basket contains: '{this.state.productIds?.length ?? 0}' elements.</h1>
                <h4 style={{textAlign: 'center'}}>Total: <b style={{letterSpacing:'1px', color:'#1E3888'}}>{this.state.grossPrice.toFixed(2)} PLN</b></h4>

                {this.state.grossPrice > 0 ? <Link to={`/basket/order/${localStorage.getItem('basket')}`} className={'zoom'}>
                    <div style={{border:'3px solid #f7f7f7', minWidth: '100px', maxWidth: '120px', margin:'0 auto'}}>
                        <h2 style={{fontSize:'18px', textAlign:'center', letterSpacing:'4px'}}>Order</h2>
                    </div>
                </Link> : null}

                <div style={{maxWidth: '1024px', margin: '0 auto'}}>
                    {this.renderProducts()}
                </div>
            </Box>
        );
    }

    renderProducts = () => {
        if (this.state.productIds === undefined || this.state.productIds === null) {
            return null;
        }

        let i = 0;
        return this.state.productIds.map((productId: string) => {
            return (<div key={`${productId}-${++i}`}>
                <ProductListItem productId={productId} 
                    onLoaded={(grossPrice: number) => { 
                        this.setState({ grossPrice: this.state.grossPrice + grossPrice }) 
                    }}
                    onRemove={async (idToRemove: string) => {
                        let basketId = localStorage.getItem('basket');
                        let accessToken = localStorage.getItem('access_token');
                
                        let basket = await axios.get(`${API_GATEWAY}/basket/api/Basket/${basketId}`, {
                            headers: {
                              'Authorization': `Bearer ${accessToken}` 
                            }
                        });
                
                        let data = basket.data;
                        let productIds = data.productIds ?? [];

                        productIds = productIds.filter((e: string) => e !== idToRemove);
                
                        await axios.post(`${API_GATEWAY}/basket/api/Basket`, {
                            "basketId": basketId,
                            "productIds": productIds
                        }, {
                            headers: {
                                'Authorization': `Bearer ${accessToken}` 
                            }
                        });

                        this.setState({productIds, grossPrice: 0});
                    }}/>
                <hr style={{borderTop:'1px solid #fafafa'}}/>
            </div>);
        });
    }
}