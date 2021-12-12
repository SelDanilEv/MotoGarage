import React, { useContext, useState } from "react";
import classNames from "classnames";
import ButtonGroup from "../elements/ButtonGroup";
import Button from "../elements/Button";
import { Link as RouterLink } from "react-router-dom";

import { SectionInitProps } from "../interfaces/SectionInitProps";
import SectionShared from "../interfaces/SectionShared";
import { CurrentUserContext } from "../../GlobalState/CurrentUser/CurrentUserStore";
import CreateRequestPopup from "../../Popup/Templates/CreateRequestPopup";

const HomeHeader = (inProps: SectionShared) => {
  const [currentUserState, setCurrentUserState]: any = useContext(CurrentUserContext);
  const [showCreateRequestPopup, setShowCreateRequestPopup] = useState(false);

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
    <section
      {...restProps}
      className={outerClasses}
      style={{
        marginTop: "40px"
      }}>
      <div className="container-sm">
        <div className={innerClasses}>
          <div className="hero-content">
            <h1
              className="mt-0 mb-16 reveal-from-bottom"
              data-reveal-delay="200"
            >
              Welcome to {" "}
              <span className="text-color-primary">Moto Garage</span>
              {" "} service
            </h1>
            <div className="container-xs">
              <p
                className="m-0 mb-32 reveal-from-bottom"
                data-reveal-delay="400"
              >
                We are a modern service with new equipment and a wide range
                of work performed from selection to engine replacement
              </p>
              <div className="reveal-from-bottom" data-reveal-delay="600">
                {
                  showCreateRequestPopup ?
                    <CreateRequestPopup setShowPopup={setShowCreateRequestPopup} />
                    : null
                }
                <ButtonGroup className="">
                  {
                    currentUserState.CurrentUser?.role != "Employee" &&
                    <Button
                    tag={RouterLink}
                    color="primary"
                    wideMobile
                    to={currentUserState.CurrentUser ?
                      "/" : "Account/Login"}
                    onClick={() => { setShowCreateRequestPopup(true) }}
                    className={undefined}
                    size="30px"
                    loading={undefined}
                    wide={undefined}
                    disabled={undefined}
                  >
                    Create a request
                  </Button>
                  }
                  {
                    !currentUserState.CurrentUser &&
                    <Button
                      tag={RouterLink}
                      color="dark"
                      wideMobile
                      to="Account/Registration"
                      className={undefined}
                      size={undefined}
                      loading={undefined}
                      wide={undefined}
                      disabled={undefined}
                    >
                      I don't have account
                    </Button>}
                </ButtonGroup>
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>
  );
};

export default HomeHeader;
