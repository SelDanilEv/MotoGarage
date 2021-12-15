import * as React from "react";
import { Link as RouterLink, useNavigate, useSearchParams } from "react-router-dom";
import ErrorResponse from "../../Interfaces/ErrorResponse";
import { useContext, useState } from "react";
import { LoadingContext } from "../GlobalState/LoadingState/LoadingStore";
import FormField from "../Fields/FormField";
import wrapAPICall from "../GlobalState/LoadingState/wrapAPICall";
import LockedButton from "../Fields/LockedButton";
import { Box, Grid, Typography } from "@mui/material";

const ResetPassword = () => {
  const navigate = useNavigate();

  const [errorState, setErrorState] = useState({
    passwordError: "",
    passwordConfirmError: "",
  });

  const [searchParams, setSearchParams] = useSearchParams();
  const [loadingState, setLoadingState]: any = useContext(LoadingContext);

  const replaceAll = (source: string, str1: string, str2: string, ignore: any) => {
    return source.replace(new RegExp(str1.replace(/([\/\,\!\\\^\$\{\}\[\]\(\)\.\*\+\?\|\<\>\-\&])/g, "\\$&"), (ignore ? "gi" : "g")), (typeof (str2) == "string") ? str2.replace(/\$/g, "$$$$") : str2);
  }

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    wrapAPICall(async () => {
      event.preventDefault();
      const data = new FormData(event.currentTarget);

      if (data.get("password") !== data.get("passwordConfirm")) {
        setErrorState(() => ({
          passwordError: "",
          passwordConfirmError: "Password mismatch",
        }));
        return;
      }

      const requestData = {
        Code: replaceAll(searchParams.get("code") || "", " ", "+", ""),
        Email: searchParams.get("userEmail"),
        Password: data.get("password"),
      };

      const response = await fetch("api/Account/ResetPassword", {
        method: "POST",
        body: JSON.stringify(requestData),
        headers: {
          "Content-Type": "application/json",
        },
      });

      switch (response.status) {
        case 200:
          navigate("/Account/Login");
          break;
        case 400:
        default:
          const result = await response.json();

          setErrorState(() => ({
            passwordError: "",
            passwordConfirmError: "",
          }));

          let error: ErrorResponse = result;
          error.errors = new Map<string, Array<string>>(
            Object.entries(result.errors)
          );

          validateFields(error);
      }
    }, setLoadingState);
  };

  const validateFields = (error: ErrorResponse) => {
    error.errors.forEach((value: string[], key: string) => {
      let field = key;
      let errorArray = value;

      let errorMessage: string = errorArray ? errorArray[0] : "";

      if (errorMessage) {
        field = field.toLowerCase();
        setErrorState((prevState) => ({
          ...prevState,
          [field + "Error"]: errorMessage,
        }));
      }
    });
  };

  return (
    <Box
      sx={{
        my: 16,
        mx: 4,
        display: "flex",
        flexDirection: "column",
        alignItems: "center",
      }}
    >
      <Typography component="h1" variant="h5">
        Reset password
      </Typography>
      <Box component="form" onSubmit={handleSubmit} sx={{ mt: 1 }}>
        <FormField
          name="password"
          type="password"
          label="Password"
          margin="normal"
          required
          fullWidth
          error={errorState.passwordError}
        />
        <FormField
          name="passwordConfirm"
          type="password"
          label="Confirm password"
          margin="normal"
          required
          fullWidth
          error={errorState.passwordConfirmError}
        />
        <LockedButton
          type="submit"
          fullWidth
          variant="contained"
          sx={{ mt: 3, mb: 2 }}
          isLoading={loadingState.Loading}
        >
          Save new password
        </LockedButton>
        <Grid container>
          <Grid item xs>
            <RouterLink to="/Account/Login">Back to login page</RouterLink>
          </Grid>
        </Grid>
      </Box>
    </Box>
  );
};

export default ResetPassword;
