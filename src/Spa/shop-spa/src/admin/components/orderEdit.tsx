import axios from 'axios';
import { Button, Box, FormField, Form, TextInput, CheckBox, Select } from 'grommet';
import React from 'react';
import ProductListItem from '../../components/productListElement';
import { API_GATEWAY } from '../../configuration/url';

interface IData {
    id: string,
    orderedProducts: string[],
    status: string,
    valueNet: number,
    valueTax: number,
    address: string,
    city: string,
    postalCode: string
}

interface IProps extends IData {
    onSubmit: () => void
}

interface IState extends IData {}

export default class OrderEdit extends React.Component<IProps, IState> {
    state = {
        id: this.props.id,
        orderedProducts: this.props.orderedProducts,
        status: this.props.status,
        valueNet: this.props.valueNet,
        valueTax: this.props.valueTax,
        address: this.props.address,
        city: this.props.city,
        postalCode: this.props.postalCode
    }

    componentDidUpdate(prev: IProps) {
        if (prev.id !== this.props.id) {
            this.setState({ 
                id: this.props.id, 
                orderedProducts: this.props.orderedProducts, 
                status: this.props.status, 
                valueNet: this.props.valueNet, 
                valueTax: this.props.valueTax, 
                address: this.props.address, 
                city: this.props.city,
                postalCode: this.props.postalCode
            });
        }
    }

    render () {
        return (<div style={{margin:'2em', maxWidth:'1024px', minWidth: '360px'}}>
            <Form
                onSubmit={async () => {
                    let accessToken = localStorage.getItem('access_token');

                    await axios.put(`${API_GATEWAY}/orders/api/Orders`, {
                        "Id": this.state.id,
                        "OrderedProducts": this.state.orderedProducts,
                        "Status": this.state.status,
                        "ValueNet": this.state.valueNet,
                        "ValueTax": this.state.valueTax,
                        "Address": this.state.address,
                        "City": this.state.city,
                        "PostalCode": this.state.postalCode,
                    }, {
                        headers: {
                            'Authorization': `Bearer ${accessToken}` 
                        }
                    });
                    
                    this.props.onSubmit();
                }}>

                <FormField name="id" htmlFor="id" label="Id">
                    <TextInput id="id" name="id" onChange={(ev) => { this.setState({ id: ev.target.value }) }} value={this.state.id} readOnly/>
                </FormField>
                <FormField name="valueNet" htmlFor="valueNet" label="Net">
                    <TextInput id="valueNet" name="valueNet" onChange={(ev) => { this.setState({ valueNet: ev.target.valueAsNumber }) }} value={this.state.valueNet} />
                </FormField>
                <FormField name="valueTax" htmlFor="valueTax" label="Tax">
                    <TextInput id="valueTax" name="valueTax" onChange={(ev) => { this.setState({ valueTax: ev.target.valueAsNumber }) }} value={this.state.valueTax} />
                </FormField>
                <FormField name="address" htmlFor="address" label="Address">
                    <TextInput id="address" name="address" onChange={(ev) => { this.setState({ address: ev.target.value }) }} value={this.state.address}/>
                </FormField>
                <FormField name="city" htmlFor="city" label="City">
                    <TextInput id="city" name="city" onChange={(ev) => { this.setState({ city: ev.target.value }) }} value={this.state.city}/>
                </FormField>
                <FormField name="postalCode" htmlFor="postalCode" label="Postal code">
                    <TextInput id="postalCode" name="postalCode" onChange={(ev) => { this.setState({ postalCode: ev.target.value }) }} value={this.state.postalCode}/>
                </FormField>
                <FormField name="status" htmlFor="status" label="Status">
                    <Select
                        options={["PENDING", "IN_PROGRESS", "FINISHED"]}
                        onChange={({ option }) => {
                            this.setState({status: option})
                        }} />
                </FormField>
                <Box direction="row" gap="medium">
                    <Button type="submit" primary label="Save" />
                </Box>
            </Form>

            {this.renderProducts()}
        </div>);
    }

    renderProducts = () => {
        let i = 0;
        return this.state.orderedProducts.map((productId: string) => {
            return (<div key={`${productId}-${++i}`}>
                <ProductListItem productId={productId} onLoaded={(grossPrice:number) => {}} onRemove={() => {}} readOnly/>
                <hr style={{borderTop:'1px solid #fafafa'}}/>
            </div>);
        });
    }
}