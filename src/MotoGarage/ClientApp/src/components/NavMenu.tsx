import {
  AppBar,
  Toolbar,
  Typography,
  makeStyles,
  Button,
} from "@material-ui/core";
import React, { useState, useEffect } from "react";
import { Link as RouterLink } from "react-router-dom";
import { withStyles } from "@material-ui/core/styles";

const useStyles = makeStyles((theme) => ({
  header: {
    backgroundColor: "#400CCC",
    paddingRight: "79px",
    paddingLeft: "5%",
    "@media (max-width: 900px)": {
      paddingLeft: 0,
      paddingRight: 0,
    },
  },
  logo: {
    fontFamily: "Work Sans, sans-serif",
    fontWeight: 600,
    color: "#FFFEFE",
    textAlign: "left",
  },
  menuButton: {
    fontFamily: "Open Sans, sans-serif",
    fontWeight: 700,
    size: "18px",
    marginLeft: "38px",
  },
  toolbar: {
    display: "flex",
    justifyContent: "space-between",
  },
}));

const NavMenu = () => {
  const { header, logo, menuButton, toolbar } = useStyles();

  const [resultState, setResultState] = useState({
    error: "",
    loading: true,
    menuItems: [],
  });

  useEffect(() => {
    console.log("start fetch");
    fetch("NavMenu/GetMenuItems")
      .then((res) => res.json())
      .then(
        (result) => {
          console.log("fetch result");
          console.log(result);
          setResultState((prevState) => ({
            ...prevState,
            loading: false,
            menuItems: result,
          }));
        },
        (error) => {
          console.log("fetch error");
          setResultState((prevState) => ({
            ...prevState,
            loading: false,
            error: error.message,
          }));
        }
      );
  }, []);

  const displayDesktop = () => {
    return (
      <Toolbar className={toolbar}>
        {femmecubatorLogo}
        <div>{getMenuButtons()}</div>
      </Toolbar>
    );
  };

  const femmecubatorLogo = (
    <Typography variant="h6" component="h1" className={logo}>
      Moto Garage
    </Typography>
  );

  const getMenuButtons = () => {
    console.log("GetMenuItem");
    if (resultState.error) {
      return <div>Please try reload the page</div>;
    } else if (resultState.loading) {
      return <div>Loading ...</div>;
    } else {
      console.log("Try get menu");
      console.log(resultState.menuItems);
      return resultState.menuItems.map(({ displayName, href, id }) => {
        return (
          <Button
            {...{
              key: id,
              color: "inherit",
              to: href,
              component: RouterLink,
              className: menuButton,
            }}
          >
            {displayName}
          </Button>
        );
      });
    }
  };

  return (
    <header>
      <AppBar className={header}>{displayDesktop()}</AppBar>
    </header>
  );
};

export default withStyles(() => ({}), { withTheme: true })(NavMenu);
