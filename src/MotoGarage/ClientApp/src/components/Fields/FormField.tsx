import { TextField } from "@mui/material";
import * as React from "react";
import { FormFieldProps } from "./FormFieldProps";

const FormField = ({ name, error, ...restProps }: FormFieldProps) => {
  return (
    <TextField
      name={name}
      id={name}
      error={Boolean(error)}
      helperText={error || " "}
      {...restProps}
    />
  );
};

export default FormField;
