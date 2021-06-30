import React from 'react';

export default class SignInOidc extends React.Component<{}, { isLoaded: boolean }> {
    state = {
        isLoaded: false
    }

    componentDidMount() {
        let query = window.location.href.split('#')[1].split("&");
        
        for (let q of query) {
            if (q.indexOf("=") !== -1) {
                let data = q.split("=");
                let key = data[0];
                let value = data[1];

                localStorage.setItem(key, value);
            }
        }

        this.setState({isLoaded: true});
    }

    render () {
        if (this.state.isLoaded) {
            window.location.href = "/";
        }

        return (
            <div>
                Loading...
            </div>
        );
    }
}