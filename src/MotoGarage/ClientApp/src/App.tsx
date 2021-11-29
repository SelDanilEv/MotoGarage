import * as React from "react";
import UseGlobalState from "./components/GlobalState/UseGLobalState";
import Layout from "./components/Layout";

import "./custom.css";

const App = () => {
  return (
    <>
      <UseGlobalState>
        <Layout />
      </UseGlobalState>
    </>
  );
};

export default App;
