import { TextFieldProps } from "@mui/material";

export interface FormFieldProps
  extends Omit<TextFieldProps, "id" | "error" | "helperText"> {
  error: string;
}
