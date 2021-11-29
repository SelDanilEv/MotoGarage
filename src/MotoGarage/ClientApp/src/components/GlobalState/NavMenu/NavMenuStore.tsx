import React, { createContext, useReducer } from "react";
import NavMenuReducer from "./NavMenuReducer";

const initialState: any = {
  CurrentUser: undefined,
};

const NavMenuStore = ({ children }: any) => {
  const [state, dispatch] = useReducer(NavMenuReducer, initialState);
  return (
    <NavMenuContext.Provider value={[state, dispatch]}>
      {children}
    </NavMenuContext.Provider>
  );
};

export const NavMenuContext = createContext(initialState);
export default NavMenuStore;
