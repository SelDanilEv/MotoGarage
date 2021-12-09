import { Button } from "@mui/material";
import * as React from "react";
import LockedButtonProps from "./LockedButtonProps";

const FormField = ({ isLoading, ...restProps }: LockedButtonProps) => {
  return <Button disabled={isLoading} {...restProps} />;
};

export default FormField;
