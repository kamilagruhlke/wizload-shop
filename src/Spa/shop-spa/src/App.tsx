import React from 'react';
import './App.css';
import {
  BrowserRouter as Router,
  Switch,
  Route,
  Link
} from "react-router-dom";
import { Box, Grommet, Header, Button } from 'grommet';
import DarkModeToggle from "react-dark-mode-toggle";
import { theme } from './theme'
import Home from './pages/home';
import Product from './pages/product';
import NotFound from './pages/notFound';
import Products from './pages/products';
import SignInOidc from './pages/signInOidc';
import { Authorization } from './utils/authorization';
import { UUID } from './utils/uuid';
import { FaFacebook, FaLinkedin, FaGoogle, FaInstagram } from "react-icons/fa";
 
export default class App extends React.Component<{}, {darkMode: boolean, isAuthorized: false, user: { name: string | undefined }}> {
  authorization = new Authorization();

  constructor(props: any) {
    super(props);

    if (localStorage.getItem('basket') == null) {
      localStorage.setItem('basket', UUID.v4());
    }

    if (localStorage.getItem('template_mode') == null) {
      localStorage.setItem('template_mode', 'light');
    }

    this.authorization.getDetails().then(e => {
      this.setState({ user: { name: e?.name }});
    });

    this.state = {
      darkMode: localStorage.getItem('template_mode') === 'dark' ? true : false,
      isAuthorized: false,
      user: {
        name: "Loading..."
      }
    };
  }

  render () {
  
    return (
      <Router>
        <Grommet full theme={theme as any} themeMode={this.state.darkMode ? "dark" : "light"}>
          <Header>
            <Box pad="small">
              {this.authorization.isAuthorized() 
                ? <Box direction="row"> 
                    <Button label="Logout" onClick={() => { localStorage.clear(); window.location.href = "/"; }} /> 
                    <Box justify="center" style={{ marginLeft: "1em" }}> 
                      {this.state.user.name} 
                    </Box>
                  </Box>
                : <Button label="Login" onClick={() => { window.location.href = this.authorization.loginUrl(); }} />}
            </Box>
            <Box pad="small">
              <Box direction="row">
                <DarkModeToggle
                  onChange={() => {
                    localStorage.setItem('template_mode', !this.state.darkMode ? 'dark' : 'light');
                    this.setState({ darkMode: !this.state.darkMode });
                  }}
                  checked={this.state.darkMode}
                  size={48} />
                <div style={{marginLeft: "1em"}}>
                  Basket
                </div>
              </Box>
            </Box>
          </Header>
  
          <Switch>
            <Route exact path="/product/:id" render={({match}: any) => (<Product id={match.params.id} /> )} />
            <Route exact path="/products/:categoryId" render={({match}: any) => (<Products categoryId={match.params.categoryId} /> )} />
            <Route exact path="/signin-oidc" component={SignInOidc} />
            <Route exact path="/" component={Home} />
            <Route component={NotFound} />
          </Switch>
          
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
                <h1 style={{textAlign:'center', marginBottom:'1.5em'}}>MEDIA</h1>
                <div style={{margin:'1em'}}>
                  <Link to="#">
                    <FaFacebook size={32} style={{border:'5px solid #fff', padding:'1em', margin:'1em' }}/>
                  </Link>
                  <Link to="#">
                    <FaLinkedin size={32} style={{border:'5px solid #fff', padding:'1em', margin:'1em' }}/>
                  </Link>
                  <Link to="#">
                    <FaGoogle size={32} style={{border:'5px solid #fff', padding:'1em', margin:'1em' }}/>
                  </Link>
                  <Link to="#">
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
        </Grommet>
      </Router>
    );
  } 
}