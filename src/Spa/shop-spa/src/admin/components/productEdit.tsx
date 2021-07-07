import axios from 'axios';
import { Button, Box, FormField, Form, TextInput, TextArea, Select } from 'grommet';
import React from 'react';
import { API_GATEWAY } from '../../configuration/url';

interface IData {
    id: string, 
    name: string, 
    description: string,
    specification: string,
    producerId: string,
    producerCode: string,
    categoryId: string,
    netPrice: string,
    tax: string,
    grossPrice: string
}

interface IProps extends IData {
    onSubmit: () => void
}

interface IOption {
    label: string,
    value: string
}

interface IState extends IData {
    categories: IOption[],
    producers: IOption[]
}

export default class ProductEdit extends React.Component<IProps, IState> {
    state = {
        id: this.props.id,
        name: this.props.name,
        description: this.props.description,
        specification: this.props.specification,
        producerId: this.props.producerId,
        producerCode: this.props.producerCode,
        categoryId: this.props.categoryId,
        netPrice: this.props.netPrice,
        tax: this.props.tax,
        grossPrice: this.props.grossPrice,
        categories: [],
        producers: []
    }

    componentDidMount() {
        axios.get(`${API_GATEWAY}/categories/api/Categories/Active`).then(res => {
            const categories = res.data;
            let data : IOption[] = [];
            for(let category of categories) {
                data.push({label: category.Name, value: category.Id});
            }

            this.setState({ categories: data });
        });

        axios.get(`${API_GATEWAY}/products/api/Producers`).then(res => {
            const producers = res.data;
            let data : IOption[] = [];
            for(let producer of producers) {
                data.push({label: producer.Name, value: producer.Id});
            }

            this.setState({ producers: data });
        });
    }

    componentDidUpdate(prev: {id: string, name: string}) {
        if (prev.id !== this.props.id) {
            this.setState({ id: this.props.id, name: this.props.name, description: this.props.description });
        }
    }

    render () {
        return (<div style={{margin:'2em', maxWidth:'1024px', minWidth: '360px'}}>
            <Form
                onSubmit={async () => {
                    let accessToken = localStorage.getItem('access_token');

                    if (this.props.id === '') {
                        await axios.post(`${API_GATEWAY}/products/api/Products?Name=${this.state.name}&Description=${this.state.description}&Specification=${this.state.specification}&ProducerId=${this.state.producerId}&ProducerCode=${this.state.producerCode}&CategoryId=${this.state.categoryId}&NetPrice=${this.state.netPrice}&Tax=${this.state.tax}&GrossPrice=${this.state.grossPrice}`, null, {
                            headers: {
                                'Authorization': `Bearer ${accessToken}` 
                            }
                        });
                    }
                    else {
                        await axios.put(`${API_GATEWAY}/products/api/Products?Id=${this.state.id}&Name=${this.state.name}&Description=${this.state.description}&Specification=${this.state.specification}&ProducerId=${this.state.producerId}&ProducerCode=${this.state.producerCode}&CategoryId=${this.state.categoryId}&NetPrice=${this.state.netPrice}&Tax=${this.state.tax}&GrossPrice=${this.state.grossPrice}`, null, {
                            headers: {
                                'Authorization': `Bearer ${accessToken}` 
                            }
                        });
                    }

                    this.props.onSubmit();
                }}>
                <FormField name="id" htmlFor="id" label="Id">
                    <TextInput id="id" name="id" onChange={(ev) => { this.setState({ id: ev.target.value }) }} value={this.state.id} readOnly/>
                </FormField>
                <FormField name="name" htmlFor="name" label="Name">
                    <TextInput id="name" name="name" onChange={(ev) => { this.setState({ name: ev.target.value }) }} value={this.state.name}/>
                </FormField>
                <FormField name="description" htmlFor="description" label="Description">
                    <TextArea id="description" name="description" onChange={(ev) => { this.setState({ description: ev.target.value }) }} value={this.state.description}/>
                </FormField>
                <FormField name="specification" htmlFor="specification" label="Specification">
                    <TextArea id="specification" name="specification" onChange={(ev) => { this.setState({ specification: ev.target.value }) }} value={this.state.specification}/>
                </FormField>
                <FormField name="producerId" htmlFor="producerId" label="Producer Id">
                    <TextInput id="producerId" name="producerId" onChange={(ev) => { this.setState({ producerId: ev.target.value }) }} value={this.state.producerId} readOnly/>
                    <Select
                        options={this.state.producers}
                        labelKey="label"
                        valueKey="value"
                        emptySearchMessage={"No Producers Available"}
                        onChange={({ option }) => {
                            this.setState({producerId: option.value})
                        }} />
                </FormField>
                <FormField name="producerCode" htmlFor="producerCode" label="Producer Code">
                    <TextInput id="producerCode" name="producerCode" onChange={(ev) => { this.setState({ producerCode: ev.target.value }) }} value={this.state.producerCode}/>
                </FormField>
                <FormField name="categoryId" htmlFor="categoryId" label="Category Id">
                    <TextInput id="categoryId" name="categoryId" onChange={(ev) => { this.setState({ categoryId: ev.target.value }) }} value={this.state.categoryId} readOnly/>
                    <Select
                        options={this.state.categories}
                        labelKey="label"
                        valueKey="value"
                        emptySearchMessage={"No Categories Available"}
                        onChange={({ option }) => {
                            this.setState({categoryId: option.value})
                        }} />
                </FormField>
                <FormField name="netPrice" htmlFor="netPrice" label="Net">
                    <TextInput id="netPrice" name="netPrice" onChange={(ev) => { this.setState({ netPrice: ev.target.value, grossPrice: this.calculateGross(ev.target.value, this.state.tax) }) }} value={this.state.netPrice}/>
                </FormField>
                <FormField name="tax" htmlFor="tax" label="Tax">
                    <TextInput id="tax" name="tax" onChange={(ev) => { this.setState({ tax: ev.target.value, grossPrice: this.calculateGross(this.state.netPrice, ev.target.value) }) }} value={this.state.tax}/>
                </FormField>
                <FormField name="grossPrice" htmlFor="grossPrice" label="Gross">
                    <TextInput id="grossPrice" name="grossPrice" value={this.state.grossPrice} readOnly/>
                </FormField>
                <Box direction="row" gap="medium">
                    <Button type="submit" primary label="Save" />
                </Box>
            </Form>
        </div>);
    }

    calculateGross = (netPrice: string, tax: string) : string => {
        return (parseInt(netPrice) + (parseInt(netPrice) * (parseInt(tax)/100))).toString();
    }
}