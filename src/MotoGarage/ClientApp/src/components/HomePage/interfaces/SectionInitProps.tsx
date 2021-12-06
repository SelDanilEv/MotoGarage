export const SectionInitProps = {
  topOuterDivider: false,
  bottomOuterDivider: false,
  topDivider: false,
  bottomDivider: false,
  hasBgColor: false,
  invertColor: false,
};

export const SectionTilesInitProps = {
  ...SectionInitProps,
  pushLeft: false,
};

export const SectionSplitInitProps = {
  ...SectionInitProps,
  invertMobile: false,
  invertDesktop: false,
  alignTop: false,
  imageFill: false,
};

export const SectionHeaderInitProps = {
  children: null,
  tag: "h2" as import("@material-ui/core/styles/createTypography").Variant,
};
