import React from "react";
import classNames from "classnames";
import Image from "../elements/Image";

import { SectionSplitInitProps, } from "../interfaces/SectionInitProps";
import SectionSplitProps from "../interfaces/SectionSplitProps";
import SectionHeader from "./partials/SectionHeader";

import features_split_image_01 from "./../../../assets/images/pict1.jpg";
import features_split_image_02 from "./../../../assets/images/pict2.jpg";
import features_split_image_03 from "./../../../assets/images/pict3.jpg";

const HomeWorkflow = (inProps: SectionSplitProps) => {
  const {
    className,
    topOuterDivider,
    bottomOuterDivider,
    topDivider,
    bottomDivider,
    hasBgColor,
    invertColor,
    invertMobile,
    invertDesktop,
    alignTop,
    imageFill,
    ...restProps
  } = {
    ...SectionSplitInitProps,
    ...inProps,
  };
  const outerClasses = classNames(
    "features-split section",
    topOuterDivider && "has-top-divider",
    bottomOuterDivider && "has-bottom-divider",
    hasBgColor && "has-bg-color",
    invertColor && "invert-color",
    className
  );

  const innerClasses = classNames(
    "features-split-inner section-inner",
    topDivider && "has-top-divider",
    bottomDivider && "has-bottom-divider"
  );

  const splitClasses = classNames(
    "split-wrap",
    invertMobile && "invert-mobile",
    invertDesktop && "invert-desktop",
    alignTop && "align-top"
  );

  const sectionHeader = {
    title: "Our workflow",
    paragraph:
      "All ingenious is simple.",
  };

  return (
    <section {...restProps} className={outerClasses}>
      <div className="container">
        <div className={innerClasses}>
          <SectionHeader data={sectionHeader} className="center-content" />
          <div className={splitClasses}>
            <div className="split-item">
              <div
                className="split-item-content center-content-mobile reveal-from-left"
                data-reveal-container=".split-item"
              >
                <div className="text-xxs text-color-primary fw-600 tt-u mb-8">
                  Step 1
                </div>
                <h3 className="mt-0 mb-12">Create a request</h3>
                <p className="m-0">
                  You create a request, indicate in it the type of service that you would like to receive.
                  We will contact you and make an appointment
                </p>
              </div>
              <div
                className={classNames(
                  "split-item-image center-content-mobile reveal-from-bottom",
                  imageFill && "split-item-image-fill"
                )}
                data-reveal-container=".split-item"
              >
                <Image
                  src={features_split_image_01}
                  alt="Features split 01"
                  width={528}
                  height={396}
                  className={undefined}
                />
              </div>
            </div>

            <div className="split-item">
              <div
                className="split-item-content center-content-mobile reveal-from-right"
                data-reveal-container=".split-item"
              >
                <div className="text-xxs text-color-primary fw-600 tt-u mb-8">
                  Step 2
                </div>
                <h3 className="mt-0 mb-12">Executing a request</h3>
                <p className="m-0">
                The request is considered completed, the service is considered completed
                 or rejected, you have the opportunity to leave a review
                </p>
              </div>
              <div
                className={classNames(
                  "split-item-image center-content-mobile reveal-from-bottom",
                  imageFill && "split-item-image-fill"
                )}
                data-reveal-container=".split-item"
              >
                <Image
                  src={features_split_image_02}
                  alt="Features split 02"
                  width={528}
                  height={396}
                  className={undefined}
                />
              </div>
            </div>

            <div className="split-item">
              <div
                className="split-item-content center-content-mobile reveal-from-left"
                data-reveal-container=".split-item"
              >
                <div className="text-xxs text-color-primary fw-600 tt-u mb-8">
                  Step 3
                </div>
                <h3 className="mt-0 mb-12">You get everything you want</h3>
                <p className="m-0">
                  Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed
                  do eiusmod tempor incididunt ut labore et dolore magna aliqua
                  — Ut enim ad minim veniam, quis nostrud exercitation ullamco
                  laboris nisi ut aliquip ex ea commodo consequat.
                </p>
              </div>
              <div
                className={classNames(
                  "split-item-image center-content-mobile reveal-from-bottom",
                  imageFill && "split-item-image-fill"
                )}
                data-reveal-container=".split-item"
              >
                <Image
                  src={features_split_image_03}
                  alt="Features split 03"
                  width={528}
                  height={396}
                  className={undefined}
                />
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>
  );
};

export default HomeWorkflow;
