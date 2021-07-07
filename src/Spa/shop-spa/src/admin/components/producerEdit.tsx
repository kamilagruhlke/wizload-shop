import axios from 'axios';
import { Button, Box, FormField, Form, TextInput, CheckBox } from 'grommet';
import React from 'react';
import { API_GATEWAY } from '../../configuration/url';

export default class ProducerEdit extends React.Component<{id: string, name: string, description: string, onSubmit: () => void}, {id: string, name: string, description: string}> {
    state = {
        id: this.props.id,
        name: this.props.name,
        description: this.props.description
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
                        await axios.post(`${API_GATEWAY}/products/api/Producers?Name=${this.state.name}&Description=${this.state.description}`, null, {
                            headers: {
                                'Authorization': `Bearer ${accessToken}` 
                            }
                        });
                    }
                    else {
                        await axios.put(`${API_GATEWAY}/products/api/Producers?Id=${this.state.id}&Name=${this.state.name}&Description=${this.state.description}`, null, {
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
                    <TextInput id="description" name="description" onChange={(ev) => { this.setState({ description: ev.target.value }) }} value={this.state.description}/>
                </FormField>
                <Box direction="row" gap="medium">
                    <Button type="submit" primary label="Save" />
                </Box>
            </Form>
        </div>);
    }
}