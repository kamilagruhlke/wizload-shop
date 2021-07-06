import axios from 'axios';
import { Button, DataTable, Text, Box, Spinner } from 'grommet';
import React from 'react';
import { API_GATEWAY } from '../configuration/url';

interface ICategory {
    Id: string,
    Name: string
}

export default class Categories extends React.Component<{}, {categories: ICategory[], isLoading: boolean}> {
    state = {
        categories: [],
        isLoading: true
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

        return (<DataTable
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
                render: data => (
                    <Button label={'Edit'} size={'small'} />
                  ),
              }
            ]}
            data={this.state.categories}
          />);
    }
}