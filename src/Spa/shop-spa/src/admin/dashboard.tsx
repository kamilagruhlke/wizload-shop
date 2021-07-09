import { Box, Tab, Tabs } from 'grommet';
import React from 'react';
import Products from './products';
import Categories from './categories';
import Producers from './producers';
import Orders from './orders';
import { Authorization } from '../utils/authorization';
import Lottie from 'react-lottie';
import animationData from '../lottie/403.json'

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
        const defaultOptions = {
            loop: true,
            autoplay: true,
            animationData: animationData
        };

        if (this.state.accessAllowed === false) {
            return  <Box pad="medium">
                <Lottie options={defaultOptions} height={512} />
            </Box>
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