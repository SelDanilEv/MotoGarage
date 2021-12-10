import * as React from "react";
import FeaturesSplit from "./HomePage/sections/FeaturesSplit";
import FeaturesTiles from "./HomePage/sections/FeaturesTiles";
import Hero from "./HomePage/sections/Hero";
import Testimonial from "./HomePage/sections/Testimonial";

import "./../assets/scss/style.min.css";

const Home = () => {
  return (
    <>
      <Hero className="illustration-section-01" />
      <FeaturesTiles />
      <FeaturesSplit
        invertMobile
        topDivider
        imageFill
        className="illustration-section-02"
      />
      <Testimonial topDivider />
    </>
  );
};

export default Home;
