import React, { useContext, useEffect, useState } from "react";
import classNames from "classnames";
import SectionHeader from "./partials/SectionHeader";
import { SectionTilesInitProps } from "../interfaces/SectionInitProps";
import SectionTilesProps from "../interfaces/SectionTilesProps";
import OneReview from "./partials/OneReview";
import wrapAPICall from "../../GlobalState/LoadingState/wrapAPICall";
import { LoadingContext } from "../../GlobalState/LoadingState/LoadingStore";

const HomeReview = (inProps: SectionTilesProps) => {
  const props = {
    ...SectionTilesInitProps,
    ...inProps,
  };

  const [reviews, setReviews]: any = useState([]);
  const [loadingState, setLoadingState]: any = useContext(LoadingContext);

  useEffect(() => {
    wrapAPICall(async () => {
      const response = await fetch("api/ServiceRequest/GetReviews", {
        method: "GET",
      });

      const result = await response.json();

      switch (response.status) {
        case 200:
          setReviews(result);
          break;
        case 400:
        default:
      }
    }, setLoadingState);
  }, [])

  const outerClasses = classNames(
    "testimonial section",
    props.topOuterDivider && "has-top-divider",
    props.bottomOuterDivider && "has-bottom-divider",
    props.hasBgColor && "has-bg-color",
    props.invertColor && "invert-color",
    props.className
  );

  const innerClasses = classNames(
    "testimonial-inner section-inner",
    props.topDivider && "has-top-divider",
    props.bottomDivider && "has-bottom-divider"
  );

  const tilesClasses = classNames("tiles-wrap", props.pushLeft && "push-left");

  const sectionHeader = {
    title: "Our reviews",
    paragraph:
      "Vitae aliquet nec ullamcorper sit amet risus nullam eget felis semper quis lectus nulla at volutpat diam ut venenatis tellus—in ornare.",
  };

  return (
    <section {...props} className={outerClasses}>
      <div className="container">
        <div className={innerClasses}>
          <SectionHeader data={sectionHeader} className="center-content" />
          <div className={tilesClasses}>
            {reviews &&
             reviews.map((elem: any) => {
              return <OneReview
                Rate={elem.rate}
                ClientName={elem.clientName}
                ReviewText={elem.reviewText} />
            })}
          </div>
        </div>
      </div>
    </section>
  );
};

export default HomeReview;
