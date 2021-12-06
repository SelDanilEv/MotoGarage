import React, { createContext, useReducer } from "react";
import LoadingReducer from "./LoadingReducer";

const initialState: any = {
  Loading: false,
  CallsCounter: Number(0),
};

const LoadingStore = ({ children }: any) => {
  const [state, dispatch] = useReducer(LoadingReducer, initialState);
  return (
    <LoadingContext.Provider value={[state, dispatch]}>
      {children}
    </LoadingContext.Provider>
  );
};

export const LoadingContext = createContext(initialState);
export default LoadingStore;
