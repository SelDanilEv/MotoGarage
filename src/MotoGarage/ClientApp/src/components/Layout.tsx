import * as React from "react";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import NavMenu from "./NavMenu";
import Home from "./../components/Home";
import Counter from "./../components/Counter";
import Login from "./Login";

interface LayoutProps {}

const Layout = ({}: LayoutProps) => {
  return (
    <>
      <Router>
        <NavMenu></NavMenu>
        <Switch>
          <Route exact path={["/", "/Home/Index"]} component={Home} />
          <Route path="/Account/Login" component={Login} />
          <Route
            path="/counter"
            component={() => <Counter multiplicator={5} />}
          />
        </Switch>
      </Router>
    </>
  );
};

export default Layout;
