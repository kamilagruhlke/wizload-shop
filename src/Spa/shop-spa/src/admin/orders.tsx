import { Tab, Tabs } from 'grommet';
import React from 'react';
import OrdersDataTable from './components/ordersDataTable';

export default class Orders extends React.Component {
    render () {
        return (<Tabs>
            <Tab title="Pending orders">
                <OrdersDataTable status={'PENDING'} />
            </Tab>
            <Tab title="In progress orders">
                <OrdersDataTable status={'IN_PROGRESS'} />
            </Tab>
            <Tab title="Finished orders">
                <OrdersDataTable status={'FINISHED'} />
            </Tab>
        </Tabs>)
    }
}