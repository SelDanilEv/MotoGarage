import {
  AppBar,
  Toolbar,
  Typography,
  makeStyles,
  Button,
  withStyles
} from "@material-ui/core";
import React, { useState, useEffect, useContext } from "react";
import { Link as RouterLink } from "react-router-dom";
import { MenuItem } from "../Interfaces/MenuItem";
import { CurrentUserContext } from "./GlobalState/NavMenu/CurrentUserStore";
import wrapAPICall from "./GlobalState/LoadingState/wrapAPICall";
import { LoadingContext } from "./GlobalState/LoadingState/LoadingStore";

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
    isLogin: false,
    menuItems: Array<MenuItem>(),
  });

  const [navManuState, setNavManuState]: any = useContext(CurrentUserContext);
  const [loadingState, setLoadingState]: any = useContext(LoadingContext);

  useEffect(() => {
    wrapAPICall(async () => {
      fetch("api/NavMenu/GetMenuItems")
        .then((res) => res.json())
        .then(
          (result) => {
            setResultState((prevState) => ({
              ...prevState,
              loading: false,
              menuItems: result,
            }));
          },
          (error) => {
            setResultState((prevState) => ({
              ...prevState,
              loading: false,
              error: error.message,
            }));
          }
        );
    }, setLoadingState);
  }, [navManuState]);

  const displayDesktop = () => {
    return (
      <Toolbar className={toolbar}>
        {femmecubatorLogo}
        {console.log(navManuState.CurrentUser)}
        {navManuState.CurrentUser ? navManuState.CurrentUser.email : ""}
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

      let menuItems: Array<MenuItem> = filterMenuItems(resultState.menuItems);

      return menuItems.map(({ displayName, href, id }) => {
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

  const filterMenuItems = (menuItems: Array<MenuItem>): Array<MenuItem> => {
    let filteredMenuItems: Array<MenuItem> = [];

    if (menuItems) {
      if (navManuState.CurrentUser) {
        filteredMenuItems = menuItems.filter(
          (item) => item.displayName.toUpperCase() != "LOGIN"
        );
      } else {
        filteredMenuItems = menuItems.filter(
          (item) => item.displayName.toUpperCase() != "LOGOUT"
        );
      }
    }

    return filteredMenuItems;
  };

  return (
    <header>
      <AppBar className={header}>{displayDesktop()}</AppBar>
    </header>
  );
};

export default withStyles(() => ({}), { withTheme: true })(NavMenu);
