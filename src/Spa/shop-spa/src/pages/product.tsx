import axios from 'axios';
import { Box, Image, Card} from 'grommet';
import React from 'react';
import { API_GATEWAY } from '../configuration/url';

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

export default class Product extends React.Component<ProductParameter, {product : IProduct}> {
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
        } as IProduct
    }

    componentDidMount() {
        axios.get(`${API_GATEWAY}/products/api/Products/${this.props.id}`).then(res => {
            const product = res.data;
            this.setState({product});
        });
    }

    render () {
        return (
            <div>
                <Box pad="medium">
                    {JSON.stringify(this.state.product)}
                    <Box direction='column'border={{ color: 'transpartent', size:'large'}}
                    width='100%' background='transparent'>
                        <Box 
                        height="small" 
                        width="small">
                            <Card height='300px'>
                                <Image src="/img/carousel/2.jpg" fit="cover"/>
                            </Card>
                        </Box>
                    </Box>
                </Box>
            </div>
        );
    }
}