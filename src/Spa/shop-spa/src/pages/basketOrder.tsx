import axios from 'axios';
import { Box, Button, Form, FormField, TextInput } from 'grommet';
import React from 'react';
import { toast, ToastContainer } from 'react-toastify';
import { API_GATEWAY } from '../configuration/url';

interface IState { 
    netValue: number, 
    taxValue: number, 
    productIds: string[],
    fullName: string,
    email: string,
    phoneNumber: string,
    address: string,
    city: string,
    postalCode: string
}

export default class BasketOrder extends React.Component<{basketId: string}, IState> {
    state = {
        netValue: 0,
        taxValue: 0,
        productIds: [],
        fullName: '',
        email: '',
        phoneNumber: '',
        address: '',
        city: '',
        postalCode: ''
    }

    async componentDidMount() {
        let accessToken = localStorage.getItem('access_token');
        let res = await axios.get(`${API_GATEWAY}/basket/api/Basket/${this.props.basketId}`, {
            headers: {
              'Authorization': `Bearer ${accessToken}` 
            }
        });

        var netValue = 0;
        var taxValue = 0;

        for(let productId of res.data.productIds) {
            let res = await axios.get(`${API_GATEWAY}/products/api/Products/${productId}`);
            let product = res.data;

            netValue += product.NetPrice;
            taxValue += product.Tax;
        }

        this.setState({ netValue, taxValue, productIds: res.data.productIds });
    }

    render () {
        return (
            <div>
                <ToastContainer />
                <div style={{maxWidth: '1024px', margin: '0 auto'}}>
                    <Form
                        onReset={() => this.setState({fullName: '', email: '', phoneNumber: '', address: '', city: '', postalCode: ''})}
                        onSubmit={async () => {
                            let accessToken = localStorage.getItem('access_token');
                            await axios.delete(`${API_GATEWAY}/basket/api/Basket`, {
                                headers: {
                                    'Authorization': `Bearer ${accessToken}` 
                                },
                                data: {
                                    "basketId": this.props.basketId
                                }
                            });

                            await axios.post(`${API_GATEWAY}/orders/api/Orders`, {
                                "OrderedProducts": this.state.productIds,
                                "ValueNet": this.state.netValue,
                                "ValueTax": this.state.taxValue,
                                "Address": this.state.address,
                                "City": this.state.city,
                                "PostalCode": this.state.postalCode,
                                "ClientFullName": this.state.fullName,
                                "Email": this.state.email,
                                "PhoneNumber": this.state.phoneNumber
                            }, {
                                headers: {
                                    'Authorization': `Bearer ${accessToken}` 
                                }
                            });

                            toast.dark('🦄  product(s) ordered!', {
                                position: "top-right",
                                autoClose: 5000,
                                hideProgressBar: false,
                                closeOnClick: true,
                                pauseOnHover: true,
                                draggable: true,
                                progress: undefined,
                            });

                            window.location.href = "/";
                        }}>
                        <FormField name="fullName" htmlFor="fullName" label="Full name">
                            <TextInput id="fullName" name="fullName" onChange={(ev) => { this.setState({ fullName: ev.target.value }) }}/>
                        </FormField>
                        <FormField name="email" htmlFor="email" label="Email">
                            <TextInput id="email" name="email" type='email' onChange={(ev) => { this.setState({ email: ev.target.value }) }}/>
                        </FormField>
                        <FormField name="phoneNumber" htmlFor="phoneNumber" label="Phone number">
                            <TextInput id="phoneNumber" name="phoneNumber" type='phone' onChange={(ev) => { this.setState({ phoneNumber: ev.target.value }) }}/>
                        </FormField>
                        <FormField name="address" htmlFor="address" label="Address">
                            <TextInput id="address" name="address" onChange={(ev) => { this.setState({ address: ev.target.value }) }}/>
                        </FormField>
                        <FormField name="city" htmlFor="city" label="City">
                            <TextInput id="city" name="city" onChange={(ev) => { this.setState({ city: ev.target.value }) }}/>
                        </FormField>
                        <FormField name="postalCode" htmlFor="postalCode" label="Postal code">
                            <TextInput id="postalCode" name="postalCode" onChange={(ev) => { this.setState({ postalCode: ev.target.value }) }}/>
                        </FormField>
                        <Box direction="row" gap="medium">
                            <Button type="submit" primary label="Submit" />
                            <Button type="reset" label="Reset" />
                        </Box>
                    </Form>
                </div>
            </div>
        );
    }
}