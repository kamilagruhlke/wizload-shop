import axios from 'axios';
import { Box, Spinner } from 'grommet';
import React from 'react';
import { API_GATEWAY } from '../configuration/url';
import ProductBox from './productBox';
import Lottie from 'react-lottie';
import animationData from '../lottie/empty.json'

interface IProduct {
    Id: string,
    Name: string
}

interface IImage {
    ProductId: string,
    Urls: string[]
}

export default class ProductBoxs extends React.Component <{products: IProduct[], isLoading: boolean}, {images: IImage[], imagesLoaded: boolean}> {
    constructor(props : {products: IProduct[], isLoading: boolean}) {
        super(props);

        this.state = {
            images: [],
            imagesLoaded: false
        }
    }

    async componentDidUpdate(prevProps: {products: IProduct[], isLoading: boolean}) {
        if(this.props.isLoading === false && this.props.products.length > 0 && prevProps.products.length !== this.props.products.length) {
            this.setState({ images: [], imagesLoaded: false });
            let res = await axios.get(`${API_GATEWAY}/images/Products/Images?${this.queryString('productIds', this.props.products.map(e => e.Id))}`);
            this.setState({ images: res.data, imagesLoaded: true });
        }
    }

    queryString(key: string, values: string[]) : string {
        let result : string[] = [];
        for(let value of values) {
            result.push(`${key}=${value}`);
        }

        return result.join('&');
    }

    render () {
        if (this.props.isLoading) {
            return <div style={{margin: '0 auto'}}>
              <Spinner size="medium" />
            </div>;
        }

        return (
            <Box pad="large" direction="row" gap="medium" wrap={true} justify='center' style={{maxWidth: '1460px', margin:'0 auto'}}>
                {this.renderProducts()}
            </Box>
        );
    }

    renderProducts = () => {
        if (this.props.products.length <= 0) {
            const defaultOptions = {
                loop: true,
                autoplay: true,
                animationData: animationData
            };

            return (<div>
                <h2 style={{textAlign:'center'}}>Nothing found...</h2>
                <Lottie options={defaultOptions} />
            </div>);
        }

        return this.props.products.map((product: any) => {
            let images : string[] = [];
            if (this.state.imagesLoaded) {
                let urls = this.state.images?.filter(e => e.ProductId === product.Id)[0]?.Urls;
                if (urls !== undefined && urls.length >= 0) {
                    images = urls;
                }
            }

            return <ProductBox key={product.Id} 
                id={product.Id}
                name={product.Name} 
                grossPrice={product.GrossPrice} 
                images={images} 
                imagesLoaded={this.state.imagesLoaded} />;
        });
    };
}