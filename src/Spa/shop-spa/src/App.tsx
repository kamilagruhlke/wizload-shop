import React from 'react';
import './App.css';
import {
  BrowserRouter as Router,
  Switch,
  Route
} from "react-router-dom";
import { Box, Grommet, Header, Menu, Footer, Text, Avatar } from 'grommet';
import DarkModeToggle from "react-dark-mode-toggle";
import { theme } from './theme'
import Home from './pages/home';
import Product from './pages/product';
import NotFound from './pages/notFound';
import Products from './pages/products';
 
export default class App extends React.Component<{}, {darkMode: boolean}> {
  constructor(props: any) {
    super(props);

    if (localStorage.getItem('template_mode') == null) {
      localStorage.setItem('template_mode', 'light');
    }

    this.state = {
      darkMode: localStorage.getItem('template_mode') === 'dark' ? true : false
    };
  }

  render () {
  
    return (
      <Router>
        <Grommet full theme={theme as any} themeMode={this.state.darkMode ? "dark" : "light"}>
          <Header>
            <Box direction="row" gap="small" pad="small">
              <Avatar src="//s.gravatar.com/avatar/00000000000000000000000000000000?s=80" />   
              <Menu label="account" items={[{ label: 'logout' }]} />
            </Box>
            <Box pad="small">
              <DarkModeToggle
                onChange={() => {
                  localStorage.setItem('template_mode', !this.state.darkMode ? 'dark' : 'light');
                  this.setState({ darkMode: !this.state.darkMode });
                }}
                checked={this.state.darkMode}
                size={48} />
            </Box>
          </Header>
  
          <Switch>
            <Route exact path="/product/:id" render={({match}: any) => (<Product id={match.params.id} /> )} />
            <Route exact path="/products/:categoryId" render={({match}: any) => (<Products categoryId={match.params.categoryId} /> )} />
            <Route exact path="/" component={Home} />
            <Route component={NotFound} />
          </Switch>
          
          <Footer pad="small">
            <Text size="small">Copyright - WizLoad</Text>
          </Footer>
        </Grommet>
      </Router>
    );
  } 
}