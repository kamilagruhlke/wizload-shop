import axios from 'axios';
import { Button, DataTable, Text, Box, Spinner, Layer } from 'grommet';
import React from 'react';
import { API_GATEWAY } from '../configuration/url';
import CategoryEdit from './components/categoryEdit';

interface ICategory {
    Id: string,
    Name: string
}

export default class Categories extends React.Component<{}, {categories: ICategory[], isLoading: boolean, selectedCategory: ICategory, layerVisible: boolean}> {
    state = {
        categories: [],
        isLoading: true,
        selectedCategory: {
            Id: '',
            Name: ''
        },
        layerVisible: false
    }

    componentDidMount() {
        this.loadCategories();
    }

    loadCategories = () => {
        this.setState({isLoading: true})

        axios.get(`${API_GATEWAY}/categories/api/Categories/Active`).then(res => {
            const categories = res.data;
            this.setState({ categories, isLoading: false });
        });
    }

    render () {
        if (this.state.isLoading) {
            return (<Box justify='center' direction='row' gap='small' pad='small'>
                <Spinner size='medium' />
                <Text alignSelf='center'>Loading...</Text>
            </Box>);
        }

        return (<div>
            <div style={{textAlign: 'center', margin:'1em'}}>
                <Button label="New category" onClick={() => {
                    this.setState({ selectedCategory: { Id: '', Name: '' }, layerVisible: true })
                }} />
            </div>

            <DataTable
                style={{margin: '0 auto', marginTop: '1em'}}
                columns={[
                {
                    property: 'Id',
                    header: <Text>Id</Text>,
                    primary: true,
                },
                {
                    property: 'Name',
                    header: <Text>Name</Text>
                },
                {
                    property: 'Edit',
                    header: <Text>Edit</Text>,
                    render: (data : ICategory) => (<Button label={'Edit'} size={'small'} onClick={() => {
                        this.setState({ selectedCategory: data, layerVisible: true })
                    }}/>),
                }
                ]}
                data={this.state.categories} />

            {this.state.layerVisible ?
                <Layer modal={true} onEsc={() => this.setState({layerVisible: false})} onClickOutside={() => this.setState({layerVisible: false})}>
                    <CategoryEdit id={this.state.selectedCategory.Id} name={this.state.selectedCategory.Name} onSubmit={() => {
                         this.setState({layerVisible: false});
                         this.loadCategories();
                    }} />
                    <div style={{background:'#9C3848', color:'#fff', letterSpacing:'2px', textAlign:'center', padding:'1em', cursor:'pointer'}}
                        onClick={() => this.setState({layerVisible: false})}>
                        <b>CLOSE</b>
                    </div>
                </Layer> : null}
        </div>);
    }
}