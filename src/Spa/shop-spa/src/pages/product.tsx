import axios from 'axios';
import { Box, Image, Card, Button, FormField, TextInput} from 'grommet';
import React from 'react';
import { API_GATEWAY } from '../configuration/url';
import '../styles/style.css';
import '../styles/box.css';
import Categories from '../components/categories';

interface ProductParameter {
    id: number;
}

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
            Specification: "",
            ProducerId:"",
            CategoryId: "",
            NetPrice: "",
            Tax:"",
            GrossPrice:""
        } as IProduct,
        images: [] as IImage[]
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

    render () {
        return (
            <Box>
                <Categories />
            <div className="container">
                <div className="name">
                    <div>
                        <div className="boxText">
                            <Box>
                                <h1>{this.state.product.Name}</h1>
                                <h4>{this.state.product.ProducerId}</h4>
                            </Box>
                        </div>
                    </div>
                </div>
                <div className="description">
                    <div>
                        <div className="boxDescription">
                            <Box>
                                <h4>{this.state.product.Description}</h4>
                            </Box> 
                        </div>
                    </div>
                </div>
                <div className="image">
                    <div className="boxItem">
                        <Box height="500px" width="500px">
                            <Card height='100%'>
                                <Image src={this.getImage()} fit="cover"/>
                            </Card>
                        </Box>
                    </div>
                </div>
                <div className="specification">
                    <div>
                        <div className="boxDescription">
                            <h4>{this.state.product.Specification}</h4>
                        </div>
                    </div>
                </div>
                <div className="price">
                    <div>
                        <div className="boxText">
                                <h1>{this.state.product.GrossPrice + ' PLN/szt.'}</h1>
                        </div>
                    </div>
                </div>
                <div className="basket">
                    <div>
                        <div className="boxText">
                            <Box direction='row'>
                                <Box>
                                    <FormField label="Number of products">
                                        <TextInput placeholder="Number of products" type='number' defaultValue={1} min="1" max="10" />
                                    </FormField>
                                </Box>
                                <Button primary label="Add to basket" 
                                    alignSelf="center"
                                    size='medium'
                                    style={{marginLeft: 50}}/>
                            </Box>
                        </div>
                    </div>
                </div>
            </div>
            </Box>
        );
    }

    getImage() : string {
        if (this.state.images.length <= 0) {
            return "/img/no-image.png";
        }

        const image = this.state.images[0];
        const randomImageIndex = Math.floor(Math.random() * image.Urls.length);

        if (image.Urls[randomImageIndex].length <= 0) {
            return "/img/no-image.png";
        }

        return image.Urls[randomImageIndex];
    }
}