import { Box } from 'grommet';
import React from 'react';
import Lottie from 'react-lottie';
import animationData from '../lottie/404.json'

export default class Product extends React.Component {
    render () {
        const defaultOptions = {
            loop: true,
            autoplay: true,
            animationData: animationData
        };

        return (
            <div>
                <Box pad="medium">
                    <Lottie options={defaultOptions}
                        height={512} />
                </Box>
            </div>
        );
    }
}