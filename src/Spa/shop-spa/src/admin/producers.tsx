import axios from 'axios';
import { Box, Button, DataTable, Spinner, Text } from 'grommet';
import React from 'react';
import { API_GATEWAY } from '../configuration/url';

interface IProducer {
    Id: string,
    Name: string,
    Description: string
}


export default class Producers extends React.Component<{}, {producers: IProducer[], isLoading: boolean}> {
    state = {
        producers: [],
        isLoading: true
    }

    componentDidMount() {
        this.loadProducers();
    }

    loadProducers = () => {
        this.setState({isLoading: true})

        axios.get(`${API_GATEWAY}/products/api/Producers`).then(res => {
            const producers = res.data;
            this.setState({producers, isLoading: false});
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
            data={this.state.producers}
          />);
    }
}