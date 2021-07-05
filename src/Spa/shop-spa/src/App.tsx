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
import Basket from './pages/basket';
import SignInOidc from './pages/signInOidc';
import { Authorization } from './utils/authorization';
import { UUID } from './utils/uuid';
import BasketIcon from './components/basketIcon';
 
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
              <Box direction="row" pad="small">
                {this.authorization.isAuthorized() ? <Link to="/basket" style={{marginRight: "1em"}}>
                  <BasketIcon />
                </Link> : null}
                <DarkModeToggle
                  onChange={() => {
                    localStorage.setItem('template_mode', !this.state.darkMode ? 'dark' : 'light');
                    this.setState({ darkMode: !this.state.darkMode });
                  }}
                  checked={this.state.darkMode}
                  size={48} />
              </Box>
            </Box>
          </Header>
  
          <Switch>
            <Route exact path="/product/:id" render={({match}: any) => (<Product id={match.params.id} /> )} />
            <Route exact path="/products/:categoryId" render={({match}: any) => (<Products categoryId={match.params.categoryId} /> )} />
            <Route exact path="/basket" component={Basket} />
            <Route exact path="/signin-oidc" component={SignInOidc} />
            <Route exact path="/" component={Home} />
            <Route component={NotFound} />
          </Switch>
        </Grommet>
      </Router>
    );
  } 
}