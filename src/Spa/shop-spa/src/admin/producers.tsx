import axios from 'axios';
import { Box, Button, DataTable, Layer, Spinner, Text } from 'grommet';
import React from 'react';
import { API_GATEWAY } from '../configuration/url';
import ProducerEdit from './components/producerEdit';

interface IProducer {
    Id: string,
    Name: string,
    Description: string
}


export default class Producers extends React.Component<{}, {producers: IProducer[], isLoading: boolean, selectedProducer: IProducer, layerVisible: boolean}> {
    state = {
        producers: [],
        isLoading: true,
        selectedProducer: {
            Id: '',
            Name: '',
            Description: ''
        },
        layerVisible: false
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

        return (<div>
            <div style={{textAlign: 'center', margin:'1em'}}>
                <Button label="New producer" onClick={() => {
                    this.setState({ selectedProducer: { Id: '', Name: '', Description: '' }, layerVisible: true })
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
                    render: (data : IProducer) => (<Button label={'Edit'} size={'small'} onClick={() => {
                        this.setState({ selectedProducer: data, layerVisible: true })
                    }}/>),
                }
                ]}
                data={this.state.producers}
            />);

          {this.state.layerVisible ?
            <Layer modal={true} onEsc={() => this.setState({layerVisible: false})} onClickOutside={() => this.setState({layerVisible: false})}>
                <ProducerEdit id={this.state.selectedProducer.Id} name={this.state.selectedProducer.Name} description={this.state.selectedProducer.Description} onSubmit={() => {
                     this.setState({layerVisible: false});
                     this.loadProducers();
                }} />
                <div style={{background:'#9C3848', color:'#fff', letterSpacing:'2px', textAlign:'center', padding:'1em', cursor:'pointer'}}
                    onClick={() => this.setState({layerVisible: false})}>
                    <b>CLOSE</b>
                </div>
            </Layer> : null}
        </div>);
    }
}