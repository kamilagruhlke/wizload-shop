import axios from 'axios';
import React from 'react';
import { Link } from 'react-router-dom';
import { API_GATEWAY } from '../configuration/url';

export default class Categories extends React.Component <{}, {categories: any, isLoading: boolean}> {
    state = {
        categories: [],
        isLoading: true
    };

    componentDidMount() {
        axios.get(`${API_GATEWAY}/categories/api/Categories/Active`).then(res => {
            const categories = res.data;
            this.setState({ categories, isLoading: false });
        });
    }
    
    render () {
        if (this.state.isLoading) {
            return (<h2 style={{textAlign:'center'}}>Loading...</h2>);
        }

        return (
            <div>
                <div style={{display:'flex', justifyContent:'center'}}>
                    <Link to={`/`} className={'zoom'}>
                        <div style={{border:'3px solid #f7f7f7', minWidth: '100px', maxWidth: '320px', margin:'1em', paddingLeft: '1em', paddingRight: '1em'}}>
                            <h2 style={{fontSize:'18px', textAlign:'center', letterSpacing:'4px'}}>Home</h2>
                        </div>
                    </Link>
                    {this.renderCategories()}
                </div>
            </div>
        );
    }

    renderCategories = () => {
        return this.state.categories.map((category: any) => {
          return (<Link key={category.Id} to={`/products/${category.Id}`} className={'zoom'}>
            <div style={{border:'3px solid #f7f7f7', minWidth: '100px', maxWidth: '320px', margin:'1em', paddingLeft: '1em', paddingRight: '1em'}}>
                <h2 style={{fontSize:'18px', textAlign:'center', letterSpacing:'4px'}}>{category.Name}</h2>
            </div>
          </Link>);
        });
    }
}