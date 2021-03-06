import {
  AppBar,
  Toolbar,
  Typography,
  Button,
} from "@mui/material";
import { withTheme, makeStyles } from '@mui/styles';
import React, { useState, useEffect, useContext } from "react";
import { Link as RouterLink } from "react-router-dom";
import MenuItem from "../../Interfaces/MenuItem";
import { CurrentUserContext } from "../GlobalState/CurrentUser/CurrentUserStore";
import wrapAPICall from "../GlobalState/LoadingState/wrapAPICall";
import { LoadingContext } from "../GlobalState/LoadingState/LoadingStore";
import CurrentUserInfo from "./CurrentUserInfo";

const useStyles = makeStyles((theme: any) => ({
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

  const [currentUserState, setCurrentUserState]: any = useContext(CurrentUserContext);
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
  }, [currentUserState]);

  const displayDesktop = () => {
    return (
      <Toolbar className={toolbar}>
        {femmecubatorLogo}
        <CurrentUserInfo />
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
    if (resultState.error) {
      return <div>Please try reload the page</div>;
    } else if (resultState.loading) {
      return <div>Loading ...</div>;
    } else {
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
      if (currentUserState.CurrentUser) {
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

export default withTheme(NavMenu);
