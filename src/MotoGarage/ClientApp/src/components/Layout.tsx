import * as React from "react";
import { Route, Routes } from "react-router-dom";
import NavMenu from "./NavMenu/NavMenu";
import Home from "./../components/Home";
import LoginAndRegistration from "./LoginAndRegistration/LoginAndRegistration";
import Logout from "./LoginAndRegistration/Logout";
import Registration from "./LoginAndRegistration/Registration";
import Login from "./LoginAndRegistration/Login";
import LinearIndeterminate from "./NavMenu/LinearIndeterminate";
import LayoutDefault from "./contentLayout/LayoutDefault";
import AdminPage from "./AdminPage/AdminPage";
import RequestsPage from "./Requests/RequestsPage";
import ForgotPassword from "./LoginAndRegistration/ForgotPassword";
import ResetPassword from "./LoginAndRegistration/ResetPassword";
import { useContext, useEffect } from "react";
import { CurrentUserContext, CurrentUserStorageName } from "../components/GlobalState/CurrentUser/CurrentUserStore";

const Layout = () => {
  const [currentUserState, setCurrentUserState]: any = useContext(CurrentUserContext);

  useEffect(() => {
    let userFromStorage = localStorage.getItem(CurrentUserStorageName);
    if (userFromStorage != null) {
      let currentUser = JSON.parse(userFromStorage);
      setCurrentUserState({ type: "SET_USER", payload: currentUser });
    }
  }, []);

  return (
    <>
      <LayoutDefault>
      <NavMenu />
      <LinearIndeterminate />
        <Routes>
          <Route path={"/"} element={<Home />} />
          <Route path={"/Admin/ManageUsers"} element={<AdminPage />} />
          <Route path={"/ServiceRequests/Manage"} element={<RequestsPage />} />
          <Route path="/Account" element={<LoginAndRegistration />}>
            <Route path="Login" element={<Login />} />
            <Route path="Registration" element={<Registration />} />
            <Route path="ForgotPassword" element={<ForgotPassword />} />
            <Route path="ResetPassword" element={<ResetPassword />} />
          </Route>
          <Route path="/Account/Logout" element={<Logout />} />
        </Routes>
      </LayoutDefault>
    </>
  );
};

export default Layout;
