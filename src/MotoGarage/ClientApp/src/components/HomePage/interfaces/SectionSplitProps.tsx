import SectionShared from "./SectionShared";

export default interface SectionSplitProps extends SectionShared {
  invertMobile?: Boolean;
  invertDesktop?: Boolean;
  alignTop?: Boolean;
  imageFill?: Boolean;
};