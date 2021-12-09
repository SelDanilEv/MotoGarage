import React from "react";
import PropTypes from "prop-types";
import { Typography } from "@material-ui/core";
import classNames from "classnames";

import { SectionHeaderInitProps } from "../../interfaces/SectionInitProps";
import SectionHeaderProps from "../../interfaces/SectionHeaderProps";

const SectionHeader = (inProps: SectionHeaderProps) => {
  const { className, tag, data, children, ...restProps } = {
    ...SectionHeaderInitProps,
    ...inProps,
  };

  const classes = classNames("section-header", className);

  return (
    <>
      {(data.title || data.paragraph) && (
        <div {...restProps} className={classes}>
          <div className="container-xs">
            {children}
            {data.title && (
              <Typography
                variant={tag}
                className={classNames(
                  "mt-0",
                  data.paragraph ? "mb-16" : "mb-0"
                )}
              >
                {data.title}
              </Typography>
            )}
            {data.paragraph && <p className="m-0">{data.paragraph}</p>}
          </div>
        </div>
      )}
    </>
  );
};

export default SectionHeader;
