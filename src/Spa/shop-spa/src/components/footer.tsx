import React from 'react';
import { Link } from 'react-router-dom';
import { FaFacebook, FaLinkedin, FaGoogle, FaInstagram } from "react-icons/fa";
import './styles/footer.css';

export default class Footer extends React.Component {

    render () {
        return (
            <div>
                <div className="footer-container">
                    <div className="footer-top-left">
                    <div style={{margin: '0 auto', textAlign: 'left'}}>
                        <h1>VISIT US</h1>
                        <p>Mon - Fri: 8am - 8pm</p>
                        <p>Saturday: 9am - 7pm</p>
                    </div>
                    </div>
                    <div className="footer-right">
                    <div style={{margin: '0 auto'}}>
                        <h1 style={{textAlign:'center', marginBottom:'0.5em'}}>MEDIA</h1>
                        <div>
                        <Link to="/">
                            <FaFacebook size={32} style={{border:'5px solid #fff', padding:'1em', margin:'1em' }}/>
                        </Link>
                        <Link to="/">
                            <FaLinkedin size={32} style={{border:'5px solid #fff', padding:'1em', margin:'1em' }}/>
                        </Link>
                        <Link to="/">
                            <FaGoogle size={32} style={{border:'5px solid #fff', padding:'1em', margin:'1em' }}/>
                        </Link>
                        <Link to="/">
                            <FaInstagram size={32} style={{border:'5px solid #fff', padding:'1em', margin:'1em' }}/>
                        </Link>
                        </div>
                    </div>
                    </div>
                    <div className="footer-bottom-left">
                    <div style={{margin: '0 auto', textAlign: 'left'}}>
                        <h1>CONTACT</h1>
                        <p>Email: contact@wizload.com</p>
                        <p>Tel: 123-456-7890</p>
                        <p>Adresse: 500 Terry Francois Street SF, CA 94158</p>
                    </div>
                    </div>
                </div>
                <div style={{background:'#151616', textAlign:'center', padding: '1em', marginTop: '2px', color: '#fff'}}>©2021-WizLoad by Kamila Gruhlke & Patryk Pasek</div>
            </div>
        );
    }
}