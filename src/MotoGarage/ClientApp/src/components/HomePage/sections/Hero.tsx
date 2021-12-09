import React, { useState } from "react";
import classNames from "classnames";
import ButtonGroup from "../elements/ButtonGroup";
import Button from "../elements/Button";

import { SectionInitProps } from "../interfaces/SectionInitProps";
import SectionShared from "../interfaces/SectionShared";

const Hero = (inProps: SectionShared) => {
  const {
    className,
    topOuterDivider,
    bottomOuterDivider,
    topDivider,
    bottomDivider,
    hasBgColor,
    invertColor,
    ...restProps
  } = {
    ...SectionInitProps,
    ...inProps,
  };

  const [videoModalActive, setVideomodalactive] = useState(false);

  const openModal = (e: { preventDefault: () => void }) => {
    e.preventDefault();
    setVideomodalactive(true);
  };

  const closeModal = (e: { preventDefault: () => void }) => {
    e.preventDefault();
    setVideomodalactive(false);
  };

  const outerClasses = classNames(
    "hero section center-content",
    topOuterDivider && "has-top-divider",
    bottomOuterDivider && "has-bottom-divider",
    hasBgColor && "has-bg-color",
    invertColor && "invert-color",
    className
  );

  const innerClasses = classNames(
    "hero-inner section-inner",
    topDivider && "has-top-divider",
    bottomDivider && "has-bottom-divider"
  );

  return (
    <section {...restProps} className={outerClasses}>
      <div className="container-sm">
        <div className={innerClasses}>
          <div className="hero-content">
            <h1
              className="mt-0 mb-16 reveal-from-bottom"
              data-reveal-delay="200"
            >
              Landing template for{" "}
              <span className="text-color-primary">startups</span>
            </h1>
            <div className="container-xs">
              <p
                className="m-0 mb-32 reveal-from-bottom"
                data-reveal-delay="400"
              >
                Our landing page template works on all devices, so you only have
                to set it up once, and get beautiful results forever.
              </p>
              <div className="reveal-from-bottom" data-reveal-delay="600">
                <ButtonGroup className="">
                  <Button
                    tag="a"
                    color="primary"
                    wideMobile
                    href="https://cruip.com/"
                    className={undefined}
                    size={undefined}
                    loading={undefined}
                    wide={undefined}
                    disabled={undefined}
                  >
                    Get started
                  </Button>
                  <Button
                    tag="a"
                    color="dark"
                    wideMobile
                    href="https://github.com/cruip/open-react-template/"
                    className={undefined}
                    size={undefined}
                    loading={undefined}
                    wide={undefined}
                    disabled={undefined}
                  >
                    View on Github
                  </Button>
                </ButtonGroup>
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>
  );
};

export default Hero;
