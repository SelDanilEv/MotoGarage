import { useNavigate } from "react-router-dom";
import { useContext, useEffect } from "react";
import { NavMenuContext } from "../GlobalState/NavMenu/NavMenuStore";
import { LoadingContext } from "../GlobalState/LoadingState/LoadingStore";
import wrapAPICall from "../GlobalState/LoadingState/wrapAPICall";

const Logout = () => {
  const navigate = useNavigate();

  const [navMenuState, setNavMenuState] = useContext(NavMenuContext);
  const [loadingState, setLoadingState]: any = useContext(LoadingContext);

  const logout = async () => {
    wrapAPICall(async () => {
      const response = await fetch("api/Account/Logout");

      console.log(response);

      switch (response.status) {
        case 200:
          setNavMenuState({ type: "CLEAN_USER", payload: undefined });

          console.log("History push");
          navigate("/");
          break;
        case 400:
          console.log("Validation error");
        default:
          console.log("Default");
      }
    }, setLoadingState);
  };

  useEffect(() => {
    logout();
  }, [logout]);
  return null;
};

export default Logout;
