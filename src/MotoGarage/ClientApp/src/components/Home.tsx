import * as React from "react";
import HomeWorkflow from "./HomePage/sections/HomeWorkflow";
import HomeFeatures from "./HomePage/sections/HomeFeatures";
import HomeHeader from "./HomePage/sections/HomeHeader";
import HomeReview from "./HomePage/sections/HomeReview";

import "./../assets/scss/style.min.css";

const Home = () => {
  return (
    <>
      <HomeHeader className="illustration-section-01" />
      <HomeFeatures />
      <HomeWorkflow
        invertMobile
        topDivider
        imageFill
        className="illustration-section-02"
      />
      <HomeReview topDivider />
    </>
  );
};

export default Home;
