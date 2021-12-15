import React, { createContext, useReducer } from "react";
import CurrentUserReducer from "./CurrentUserReducer";

const initialState: any = {
  CurrentUser: undefined,
};

const CurrentUserStore = ({ children }: any) => {
  const [state, dispatch] = useReducer(CurrentUserReducer, initialState);
  return (
    <CurrentUserContext.Provider value={[state, dispatch]}>
      {children}
    </CurrentUserContext.Provider>
  );
};

export const CurrentUserStorageName = 'CurrentUser';
export const CurrentUserContext = createContext(initialState);
export default CurrentUserStore;

