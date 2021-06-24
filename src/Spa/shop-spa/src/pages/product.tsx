import { Box } from 'grommet';
import React from 'react';

interface ProductParameter {
    id: number;
}

export default class Product extends React.Component<ProductParameter> {
    render () {
        return (
            <div>
                <Box pad="medium">
                    Produkt {this.props.id}
                </Box>
            </div>
        );
    }
}