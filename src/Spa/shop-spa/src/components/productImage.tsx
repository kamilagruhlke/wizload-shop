import React from 'react';
import { Image } from 'grommet';

export default class ProductImage extends React.Component <{images: string[]}> {
    render () {
        if (this.props.images === undefined || this.props.images.length <= 0) {
            return (<Image src="/img/no-image.png" fit="cover"/>);
        }

        const randomImageIndex = Math.floor(Math.random() * this.props.images.length);
        return (<Image src={this.props.images[randomImageIndex]} fit="cover"/>);
    }
}