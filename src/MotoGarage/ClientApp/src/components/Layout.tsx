import * as React from "react";
import { Route, Routes } from "react-router-dom";
import NavMenu from "./NavMenu";
import Home from "./../components/Home";
import LoginAndRegistration from "./LoginAndRegistration/LoginAndRegistration";
import Logout from "./LoginAndRegistration/Logout";
import Registration from "./LoginAndRegistration/Registration";
import Login from "./LoginAndRegistration/Login";
import LinearIndeterminate from "./LinearIndeterminate";
import LayoutDefault from "../contentLayout/LayoutDefault";
import AdminPage from "./AdminPage/AdminPage";

const Layout = () => {
  return (
    <>
      <NavMenu />
      <LinearIndeterminate />
      <LayoutDefault>
        <Routes>
          <Route path={"/"} element={<Home />} />
          <Route path={"/Admin/ManageUsers"} element={<AdminPage />} />
          <Route path="/Account" element={<LoginAndRegistration />}>
            <Route path="Login" element={<Login />} />
            <Route path="Registration" element={<Registration />} />
          </Route>
          <Route path="/Account/Logout" element={<Logout />} />
        </Routes>
      </LayoutDefault>
    </>
  );
};

export default Layout;
