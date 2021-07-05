import { Card, CardBody, CardFooter } from 'grommet';
import React from 'react';
import Lottie from 'react-lottie';
import { Link } from 'react-router-dom';
import animationData from '../lottie/loading-image.json';
import ProductImage from './productImage';

export default class ProductBox extends React.Component <{id: string, name: string, images: string[], imagesLoaded: boolean}, {images: string[], imagesLoaded: boolean}> {
    constructor(props: {id: string, name: string, images: string[], imagesLoaded: boolean}) {
        super(props);

        this.state = {
            images: [],
            imagesLoaded: false
        };
    }

    componentDidUpdate(prevProps: {id: string, name: string, images: string[], imagesLoaded: boolean}) {
        if (this.props.imagesLoaded !== prevProps.imagesLoaded) {
            this.setState({ images: this.props.images, imagesLoaded: this.props.imagesLoaded });
        }
    }

    render () {
        const defaultOptions = {
            loop: true,
            autoplay: true,
            animationData: animationData
        };

        return (
            <Link key={this.props.id} to={`/product/${this.props.id}`} style={{ color: 'inherit', textDecoration: 'inherit', margin:'1em'}}>
                <Card height="300px" width="300px">
                    <CardBody>
                        {this.state.imagesLoaded === false ? <Lottie options={defaultOptions} /> : <ProductImage images={this.state.images} />}
                    </CardBody>
                    <CardFooter pad="small" background="brand">{this.props.name}</CardFooter>
                </Card>
            </Link>
        );
    }
}