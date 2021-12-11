import * as React from "react";
import LoadingStore from "./LoadingState/LoadingStore";
import CurrentUserStore from "./NavMenu/CurrentUserStore";

const UseGlobalState = ({ children }: any) => {
  return (
    <>
      <CurrentUserStore>
        <LoadingStore>
          {children}
        </LoadingStore>
      </CurrentUserStore>
    </>
  );
};

export default UseGlobalState;
