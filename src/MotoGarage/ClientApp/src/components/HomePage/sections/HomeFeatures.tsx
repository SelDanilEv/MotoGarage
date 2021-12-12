import React from "react";
import classNames from "classnames";
import SectionHeader from "./partials/SectionHeader";
import Image from "../elements/Image";

import { SectionTilesInitProps } from "../interfaces/SectionInitProps";
import SectionTilesProps from "../interfaces/SectionTilesProps";

import feature_tile_icon_01 from "./../../../assets/images/feature-tile-icon-01.svg";
import feature_tile_icon_02 from "./../../../assets/images/feature-tile-icon-02.svg";
import feature_tile_icon_03 from "./../../../assets/images/feature-tile-icon-03.svg";
import feature_tile_icon_04 from "./../../../assets/images/feature-tile-icon-04.svg";
import feature_tile_icon_05 from "./../../../assets/images/feature-tile-icon-05.svg";
import feature_tile_icon_06 from "./../../../assets/images/feature-tile-icon-06.svg";

const HomeFeatures = (inProps: SectionTilesProps) => {
  const {
    className,
    topOuterDivider,
    bottomOuterDivider,
    topDivider,
    bottomDivider,
    hasBgColor,
    invertColor,
    pushLeft,
    ...restProps
  } = {
    ...SectionTilesInitProps,
    ...inProps,
  };

  const outerClasses = classNames(
    "features-tiles section",
    topOuterDivider && "has-top-divider",
    bottomOuterDivider && "has-bottom-divider",
    hasBgColor && "has-bg-color",
    invertColor && "invert-color",
    className
  );

  const innerClasses = classNames(
    "features-tiles-inner section-inner pt-0",
    topDivider && "has-top-divider",
    bottomDivider && "has-bottom-divider"
  );

  const tilesClasses = classNames(
    "tiles-wrap center-content",
    pushLeft && "push-left"
  );

  const sectionHeader = {
    title: "Why choose us?",
    paragraph:
      "We have many advantages that our competitors do not have." +
      "We are also the first service with such a wide range of services.",
  };

  return (
    <section {...restProps} className={outerClasses}>
      <div className="container">
        <div className={innerClasses}>
          <SectionHeader data={sectionHeader} className="center-content" />
          <div className={tilesClasses}>
            <div className="tiles-item reveal-from-bottom">
              <div className="tiles-item-inner">
                <div className="features-tiles-item-header">
                  <div className="features-tiles-item-image mb-16">
                    <Image
                      src={feature_tile_icon_01}
                      alt="Features tile icon 01"
                      width={64}
                      height={64} className={undefined} />
                  </div>
                </div>
                <div className="features-tiles-item-content">
                  <h4 className="mt-0 mb-8">Customer care</h4>
                  <p className="m-0 text-sm">
                    The quality of performed work is the most important goal for us.
                    It is important for us client is satisfied with our service
                  </p>
                </div>
              </div>
            </div>

            <div
              className="tiles-item reveal-from-bottom"
              data-reveal-delay="200"
            >
              <div className="tiles-item-inner">
                <div className="features-tiles-item-header">
                  <div className="features-tiles-item-image mb-16">
                    <Image
                      src={feature_tile_icon_02}
                      alt="Features tile icon 02"
                      width={64}
                      height={64} className={undefined} />
                  </div>
                </div>
                <div className="features-tiles-item-content">
                  <h4 className="mt-0 mb-8">Request system</h4>
                  <p className="m-0 text-sm">
                    We have a special system that allows us to
                    quickly and efficiently process all incoming requests
                  </p>
                </div>
              </div>
            </div>

            <div
              className="tiles-item reveal-from-bottom"
              data-reveal-delay="400"
            >
              <div className="tiles-item-inner">
                <div className="features-tiles-item-header">
                  <div className="features-tiles-item-image mb-16">
                    <Image
                      src={feature_tile_icon_03}
                      alt="Features tile icon 03"
                      width={64}
                      height={64} className={undefined} />
                  </div>
                </div>
                <div className="features-tiles-item-content">
                  <h4 className="mt-0 mb-8">Services</h4>
                  <p className="m-0 text-sm">
                    We are engaged in a wide range of services, such as:
                    Motorcycle selection, maintenance, repair of faults, winter storage
                    services, travel preparation, consultations.
                  </p>
                </div>
              </div>
            </div>

            <div className="tiles-item reveal-from-bottom">
              <div className="tiles-item-inner">
                <div className="features-tiles-item-header">
                  <div className="features-tiles-item-image mb-16">
                    <Image
                      src={feature_tile_icon_04}
                      alt="Features tile icon 04"
                      width={64}
                      height={64} className={undefined} />
                  </div>
                </div>
                <div className="features-tiles-item-content">
                  <h4 className="mt-0 mb-8">Equipment</h4>
                  <p className="m-0 text-sm">
                    We use new equipment and only high-quality tools and components
                  </p>
                </div>
              </div>
            </div>

            <div
              className="tiles-item reveal-from-bottom"
              data-reveal-delay="200"
            >
              <div className="tiles-item-inner">
                <div className="features-tiles-item-header">
                  <div className="features-tiles-item-image mb-16">
                    <Image
                      src={feature_tile_icon_05}
                      alt="Features tile icon 05"
                      width={64}
                      height={64} className={undefined} />
                  </div>
                </div>
                <div className="features-tiles-item-content">
                  <h4 className="mt-0 mb-8">Reviews</h4>
                  <p className="m-0 text-sm">
                    The average review for our services is 4.5 / 5
                  </p>
                </div>
              </div>
            </div>

            <div
              className="tiles-item reveal-from-bottom"
              data-reveal-delay="400"
            >
              <div className="tiles-item-inner">
                <div className="features-tiles-item-header">
                  <div className="features-tiles-item-image mb-16">
                    <Image
                      src={feature_tile_icon_06}
                      alt="Features tile icon 06"
                      width={64}
                      height={64} className={undefined} />
                  </div>
                </div>
                <div className="features-tiles-item-content">
                  <h4 className="mt-0 mb-8">Do not worry</h4>
                  <p className="m-0 text-sm">
                    Using our service, we will not bother you over trifles
                  </p>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>
  );
};

export default HomeFeatures;
