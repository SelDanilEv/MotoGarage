import { ButtonProps } from "@mui/material";

export interface LockedButtonProps extends Omit<ButtonProps, "disabled"> {
  isLoading: boolean;
}
