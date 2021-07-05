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

    componentDidUpdate(prevProps: {products: IProduct[], isLoading: boolean}) : void {
        if(this.props.isLoading === false && this.props.products.length > 0 && prevProps.products.length !== this.props.products.length) {
            axios.get(`${API_GATEWAY}/images/Products/Images?${this.queryString('productIds', this.props.products.map(e => e.Id))}`).then(res => {
                const images = res.data;
                this.setState({ images, imagesLoaded: true });
            });
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
            return <div style={{width: '100%', textAlign: 'center'}}>
              <Spinner size="medium" />
            </div>;
        }

        return (
            <Box pad="large" direction="row-responsive" gap="medium" wrap={true} justify='center'>
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
                <Lottie options={defaultOptions} height={512} />
                <h2 style={{textAlign:'center'}}>Empty...</h2>
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

            return <ProductBox key={product.Id} id={product.Id} name={product.Name} images={images} imagesLoaded={this.state.imagesLoaded} />;
        });
    };
}