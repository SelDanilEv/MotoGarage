import * as React from "react";
import { Route, Routes } from "react-router-dom";
import NavMenu from "./NavMenu";
import Home from "./../components/Home";
import LoginAndRegistration from "./LoginAndRegistration/LoginAndRegistration";
import Logout from "./LoginAndRegistration/Logout";
import Registration from "./LoginAndRegistration/Registration";
import Login from "./LoginAndRegistration/Login";
import LinearIndeterminate from "./LinearIndeterminate";

const Layout = () => {
  return (
    <>
      <NavMenu />
      <LinearIndeterminate/>
      <Routes>
        <Route path={"/"} element={<Home />} />
        <Route path="/Account" element={<LoginAndRegistration />}>
          <Route path="Login" element={<Login />} />
          <Route path="Registration" element={<Registration />} />
        </Route>
        <Route path="/Account/Logout" element={<Logout />} />
      </Routes>
    </>
  );
};

export default Layout;
