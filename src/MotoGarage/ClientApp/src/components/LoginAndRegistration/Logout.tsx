import { useNavigate } from "react-router-dom";
import { useContext, useEffect } from "react";
import { CurrentUserContext } from "../GlobalState/CurrentUser/CurrentUserStore";
import { LoadingContext } from "../GlobalState/LoadingState/LoadingStore";
import wrapAPICall from "../GlobalState/LoadingState/wrapAPICall";

const Logout = () => {
  const navigate = useNavigate();

  const [navMenuState, setNavMenuState] = useContext(CurrentUserContext);
  const [loadingState, setLoadingState]: any = useContext(LoadingContext);

  const logout = async () => {
    wrapAPICall(async () => {
      const response = await fetch("api/Account/Logout");

      switch (response.status) {
        case 200:
          setNavMenuState({ type: "CLEAN_USER", payload: undefined });

          navigate("/");
          break;
        case 400:
        default:
      }
    }, setLoadingState);
  };

  useEffect(() => {
    logout();
  }, []);
  return null;
};

export default Logout;
