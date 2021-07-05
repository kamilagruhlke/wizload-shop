import React from 'react';

export default class ProductImage extends React.Component <{images: string[]}> {
    render () {
        if (this.props.images === undefined || this.props.images.length <= 0) {
            return (<img style={{height: '100%', width: '100%', objectFit: 'contain'}} src="/img/no-image.png" alt="Not found"/>);
        }

        const randomImageIndex = Math.floor(Math.random() * this.props.images.length);
        return (<img style={{height: '100%', width: '100%', objectFit: 'contain'}} src={this.props.images[randomImageIndex]} alt={this.props.images[randomImageIndex]}/>);
    }
}