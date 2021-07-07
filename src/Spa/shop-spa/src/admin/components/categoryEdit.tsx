import axios from 'axios';
import { Button, Box, FormField, Form, TextInput, CheckBox } from 'grommet';
import React from 'react';
import { API_GATEWAY } from '../../configuration/url';

export default class CategoryEdit extends React.Component<{id: string, name: string, onSubmit: () => void}, {id: string, name: string, isDeleted: boolean}> {
    state = {
        id: this.props.id,
        name: this.props.name,
        isDeleted: false
    }

    componentDidUpdate(prev: {id: string, name: string}) {
        if (prev.id !== this.props.id) {
            this.setState({ id: this.props.id, name: this.props.name });
        }
    }

    render () {
        return (<div style={{margin:'2em', maxWidth:'1024px', minWidth: '360px'}}>
            <Form
                onSubmit={async () => {
                    let accessToken = localStorage.getItem('access_token');
                    
                    if (this.props.id === '') {
                        await axios.post(`${API_GATEWAY}/categories/api/Categories`, {
                            "Id": this.state.id,
                            "Name": this.state.name,
                            "IsDeleted": this.state.isDeleted
                        }, {
                            headers: {
                                'Authorization': `Bearer ${accessToken}` 
                            }
                        });
                    }
                    else {
                        await axios.put(`${API_GATEWAY}/categories/api/Categories`, {
                            "Id": this.state.id,
                            "Name": this.state.name,
                            "ParentId": null,
                            "IsDeleted": this.state.isDeleted
                        }, {
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
                <div style={{marginTop: '1em', marginBottom: '1em'}}>
                    <CheckBox
                        checked={this.state.isDeleted}
                        label="Is deleted"
                        onChange={(ev: any) => this.setState({ isDeleted: ev.target.checked })} />
                </div>
                <Box direction="row" gap="medium">
                    <Button type="submit" primary label="Save" />
                </Box>
            </Form>
        </div>);
    }
}