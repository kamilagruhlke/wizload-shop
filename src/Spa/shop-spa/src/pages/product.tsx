import axios from 'axios';
import { Box, Image, Card, Button, Select} from 'grommet';
import React from 'react';
import { API_GATEWAY } from '../configuration/url';
import '../styles/style.css';

interface ProductParameter {
    id: number;
}

interface IProduct {
    Id: string,
    Name: string,
    Description: string,
    Specyfication: string,
    ProducerId: string,
    CategoryId: string,
    NetPrice: string,
    Tax: string,
    GrossPrice: string
}

interface IImage {
    ProductId: string,
    Urls: string[]
}

export default class Product extends React.Component<ProductParameter, {product : IProduct, images: IImage[]}> {
    state = {
        product : {
            Id: "",
            Name: "",
            Description: "",
            Specyfication: "",
            ProducerId:"",
            CategoryId: "",
            NetPrice: "",
            Tax:"",
            GrossPrice:""
        } as IProduct,
        images: []
    }

    componentDidMount() {
        axios.get(`${API_GATEWAY}/products/api/Products/${this.props.id}`).then(res => {
            const product = res.data;
            this.setState({product});
        });

        axios.get(`${API_GATEWAY}/images/Products/Images?productIds=${this.props.id}`).then(res => {
            const images = res.data;
            this.setState({images});
        });
    }

    Select() {
        return (
          <Select
            options={['small', 'medium', 'large']}
          />
        );
      }

    render () {
        return (
            <div className="container">
                <div className="description">
                    <div>
                    {JSON.stringify(this.state.product)}
                    </div>
                </div>
                <div className="specyfication">
                    <div>
                    {JSON.stringify(this.state.product)}
                    </div>
                </div>
                <div className="image">
                            <Box
                            pad='large'
                            height="large" 
                            width="large">
                                <Card height='300px'>
                                    <Image src="/img/carousel/2.jpg" fit="cover"/>
                                </Card>
                            </Box>
                </div>
                <div className="name">
                    <div>
                    {JSON.stringify(this.state.product)}
                    </div>
                </div>
                <div className="price">
                    <div>
                    {JSON.stringify(this.state.product)}
                    </div>
                </div>
                <div className="basket">
                    <div>
                        <Box>
                            <Button primary label="label" 
                            alignSelf="end"/>
                            {this.Select()}    
                        </Box>
                    </div>  
                </div>
            </div>
        );
    }
}