import React from "react";

const OneReview = (props: any) => {

  return (
    <>
      <div
        className="tiles-item reveal-from-left"
        data-reveal-delay="200"
      >
        <div className="tiles-item-inner">
          <div className="testimonial-item-content">
            <p className="text-sm mb-0">
              {props.ReviewText}
            </p>
          </div>
          <div className="testimonial-item-footer text-xs mt-32 mb-0 has-top-divider">
            <span className="testimonial-item-name text-color-high">
              {`${props.Rate} from 5 `}
            </span>
            <span className="text-color-low"> / </span>
            <span className="testimonial-item-link">
              {props.ClientName}
            </span>
          </div>
        </div>
      </div>
    </>
  );
};

export default OneReview;
