import { Box, Card, CardBody, Carousel, Image, Tab, Tabs, CardFooter } from 'grommet';
import React from 'react';
import { Link } from 'react-router-dom';

export default class Home extends React.Component {
    state = {
      count: 0
    };
  
    increment = () => {
      this.setState({
        count: (this.state.count + 1)
      });
    };
  
    decrement = () => {
      this.setState({
        count: (this.state.count - 1)
      });
    };
  
    render () {
      return (
        <div>
          <Box height="medium" width="100%" overflow="hidden" background="light-6">
            <Carousel fill controls="arrows">
              <Image fit="cover" src="img/carousel/1.jpg" />
              <Image fit="cover" src="img/carousel/2.jpg" />
              <Image fit="cover" src="img/carousel/3.jpg" />
            </Carousel>
          </Box>
          <Box pad="medium">
            <Tabs>
              <Tab title="Kategoria 1">
                <Box pad="medium" direction="row" gap="medium">
                  <Link to="product/1" style={{ color: 'inherit', textDecoration: 'inherit'}}>
                    <Card height="180px" width="180px">
                      <CardBody>
                        <Image src="img/carousel/1.jpg" fit="cover"/>
                      </CardBody>
                      <CardFooter pad="small">Nazwa produktu</CardFooter>
                    </Card>
                  </Link>

                  <Link to="product/2" style={{ color: 'inherit', textDecoration: 'inherit'}}>
                    <Card height="180px" width="180px">
                      <CardBody>
                        <Image src="img/carousel/2.jpg" fit="cover"/>
                      </CardBody>
                      <CardFooter pad="small">Nazwa produktu</CardFooter>
                    </Card>
                  </Link>

                  <Link to="product/3" style={{ color: 'inherit', textDecoration: 'inherit'}}>
                    <Card height="180px" width="180px">
                      <CardBody>
                        <Image src="img/carousel/3.jpg" fit="cover"/>
                      </CardBody>
                      <CardFooter pad="small">Nazwa produktu</CardFooter>
                    </Card>
                  </Link>
                </Box>
              </Tab>
              <Tab title="Kategoria 2">
                <Box pad="medium">Kategoria 2</Box>
              </Tab>
            </Tabs>
          </Box>
        </div>
      );
    }
  }