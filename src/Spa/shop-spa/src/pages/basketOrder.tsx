import React from 'react';

export default class BasketOrder extends React.Component<{basketId: string}> {
    render () {
        return (
            <div>
                {this.props.basketId}
            </div>
        );
    }
}