import { TextFieldProps } from "@mui/material";

export default interface FormFieldProps
  extends Omit<TextFieldProps, "id" | "error" | "helperText"> {
  error: string;
}
