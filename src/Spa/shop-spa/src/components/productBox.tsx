import React from 'react';
import Lottie from 'react-lottie';
import { Link } from 'react-router-dom';
import animationData from '../lottie/loading-image.json';
import ProductImage from './productImage';

export default class ProductBox extends React.Component <{id: string, name: string, grossPrice: string, images: string[], imagesLoaded: boolean}, {images: string[], imagesLoaded: boolean}> {
    constructor(props: {id: string, name: string, grossPrice: string, images: string[], imagesLoaded: boolean}) {
        super(props);

        this.state = {
            images: [],
            imagesLoaded: false
        };
    }

    componentDidUpdate(prevProps: {id: string, name: string, images: string[], imagesLoaded: boolean}) {
        if (this.props.imagesLoaded !== false && this.props.imagesLoaded !== prevProps.imagesLoaded && this.props.images.length !== prevProps.images.length) {
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
                <div style={{width: '277px', height: '350px', background: '#f7f7f7'}}>
                    {this.state.imagesLoaded === false ? <Lottie options={defaultOptions} /> : <ProductImage images={this.state.images} />}
                </div>
                <div style={{width: '277px'}}>
                    <p style={{fontSize: '12px', textAlign:'center'}}>{(this.props.name === undefined || this.props.name.length <= 0) ? '???' : this.props.name}</p>
                    <hr style={{borderTop: '0.5px dashed #bbb'}} />
                    <p style={{textAlign:'center', letterSpacing:'1px', color:'#1E3888'}}>{this.props.grossPrice} PLN</p>
                    <h2 style={{fontSize:'18px', textAlign:'center', letterSpacing:'4px'}} className={'zoom'}>OPEN</h2>
                </div>
            </Link>
        );
    }
}