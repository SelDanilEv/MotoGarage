import { Variant } from "@mui/material/styles/createTypography";
import { ReactNode } from "react";

export interface SectionHeaderProps {
  data: {
    title?: string;
    paragraph?: string;
  };
  children?: ReactNode;
  tag?: Variant;
  className?: string;
}