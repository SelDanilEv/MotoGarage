import * as React from "react";
import LoadingStore from "./LoadingState/LoadingStore";
import NavMenuStore from "./NavMenu/NavMenuStore";

const UseGlobalState = ({ children }: any) => {
  return (
    <>
      <NavMenuStore>
        <LoadingStore>{children}</LoadingStore>
      </NavMenuStore>
    </>
  );
};

export default UseGlobalState;
