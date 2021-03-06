import * as React from "react";
import { Link as RouterLink, useNavigate } from "react-router-dom";
import ErrorResponse from "./../../Interfaces/ErrorResponse";
import { useContext, useState } from "react";
import { CurrentUserContext } from "../GlobalState/CurrentUser/CurrentUserStore";
import { LoadingContext } from "../GlobalState/LoadingState/LoadingStore";
import FormField from "../Fields/FormField";
import wrapAPICall from "../GlobalState/LoadingState/wrapAPICall";
import LockedButton from "../Fields/LockedButton";
import { Box, Grid, Typography } from "@mui/material";
// import { Box, Grid, Typography } from "@material-ui/core";

const Login = () => {
  const navigate = useNavigate();

  const [getState, setState] = useState({
    emailError: "",
    passwordError: "",
  });

  const [currentUserState, setCurrentUserState]: any = useContext(CurrentUserContext);
  const [loadingState, setLoadingState]: any = useContext(LoadingContext);

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    wrapAPICall(async () => {
      event.preventDefault();
      const data = new FormData(event.currentTarget);

      const requestData = {
        Email: data.get("email"),
        Password: data.get("password"),
      };

      const response = await fetch("api/Account/Login", {
        method: "POST",
        body: JSON.stringify(requestData),
        headers: {
          "Content-Type": "application/json",
        },
      });

      const result = await response.json();

      switch (response.status) {
        case 200:
          setCurrentUserState({ type: "SET_USER", payload: result });

          navigate("/");
          break;
        case 400:
        default:
          setState(() => ({
            emailError: "",
            passwordError: "",
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
        setState((prevState) => ({
          ...prevState,
          [field + "Error"]: errorMessage,
        }));
      }
    });
  };

  return (
    <Box
      sx={{
        my: 15,
        mx: 4,
        display: "flex",
        flexDirection: "column",
        alignItems: "center",
      }}
    >
      <Typography component="h1" variant="h5">
        Sign in
      </Typography>
      <Box component="form"
        // noValidate 
        onSubmit={handleSubmit} sx={{ mt: 1 }}>
        <FormField
          name="email"
          type="email"
          autoComplete="email"
          label="Email Address"
          margin="normal"
          required
          autoFocus
          fullWidth
          error={getState.emailError}
        />
        <FormField
          name="password"
          type="password"
          autoComplete="current-password"
          label="Password"
          margin="normal"
          required
          fullWidth
          error={getState.passwordError}
        />
        <LockedButton
          type="submit"
          fullWidth
          variant="contained"
          sx={{ mt: 3, mb: 2 }}
          isLoading={loadingState.Loading}
        >
          Sign In
        </LockedButton>
        <Grid container>
          <Grid item xs>
            <RouterLink to="/Account/ForgotPassword">Forgot password?</RouterLink>
          </Grid>
          <Grid item xs>
            <RouterLink to="/Account/Registration">
              Don't have an account?
            </RouterLink>
          </Grid>
        </Grid>
      </Box>
    </Box>
  );
};

export default Login;
