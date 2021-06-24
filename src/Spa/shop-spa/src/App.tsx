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
 
function App() {
  const [darkMode, setDarkMode] = React.useState(false);
  return (
    <Router>
      <Grommet full theme={theme as any} themeMode={darkMode ? "dark" : "light"}>
        <Header>
          <Box direction="row" gap="small" pad="small">
            <Avatar src="//s.gravatar.com/avatar/00000000000000000000000000000000?s=80" />   
            <Menu label="account" items={[{ label: 'logout' }]} />
          </Box>
          <Box pad="small">
            <DarkModeToggle
              onChange={() => setDarkMode(!darkMode)}
              checked={darkMode}
              size={48} />
          </Box>
        </Header>

        <Switch>
          <Route exact path="/product/:id" render={({match}: any) => (<Product id={match.params.id} /> )} />
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

export default App;
