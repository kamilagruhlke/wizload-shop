import { Tab, Tabs } from 'grommet';
import React from 'react';
import Products from './products';
import Categories from './categories';
import Producers from './producers';
import Orders from './orders';
import { Authorization } from '../utils/authorization';

export default class Dashboard extends React.Component<{}, { accessAllowed: boolean }> {
    state = {
        accessAllowed: false
    }

    async componentDidMount() {
        let details = await new Authorization().getDetails();
        if (details?.role !== "Administrator") {
            this.setState({accessAllowed: false})
        } else {
            this.setState({accessAllowed: true})
        }
    }

    render () {
        if (this.state.accessAllowed === false) {
            return null;
        }

        return (<Tabs>
            <Tab title="Categories">
                <Categories />
            </Tab>
            <Tab title="Producers">
                <Producers />
            </Tab>
            <Tab title="Products">
                <Products />
            </Tab>
            <Tab title="Orders">
              <Orders />
            </Tab>
        </Tabs>)
    }
}