import * as React from "react";
import { Grid, CssBaseline, Paper, Box, Typography } from "@mui/material";
// import { Grid, CssBaseline, Paper, Box, Typography} from "@material-ui/core";
import ErrorResponse from "../../Interfaces/ErrorResponse";
import { useContext, useState } from "react";
import { Link as RouterLink, useNavigate } from "react-router-dom";
import FormField from "../Fields/FormField";
import wrapAPICall from "../GlobalState/LoadingState/wrapAPICall";
import { LoadingContext } from "../GlobalState/LoadingState/LoadingStore";
import LockedButton from "../Fields/LockedButton";

const Registration = () => {
  const navigate = useNavigate();

  const [loadingState, setLoadingState]: any = useContext(LoadingContext);

  const [getState, setState] = useState({
    nameError: "",
    emailError: "",
    passwordError: "",
  });

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    wrapAPICall(async () => {
      setState(() => ({
        emailError: "",
        nameError: "",
        passwordError: "",
      }));

      event.preventDefault();
      const data = new FormData(event.currentTarget);

      const requestData = {
        Name: data.get("name"),
        Email: data.get("email"),
        Password: data.get("password"),
      };


      const response = await fetch("api/AccountManager/CreateUser", {
        method: "POST",
        body: JSON.stringify(requestData),
        headers: {
          "Content-Type": "application/json",
        },
      });

      const result = await response.json();

      switch (response.status) {
        case 200:
          navigate("/Account/Login/");
          break;
        case 400:
        default:

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
        my: 10,
        mx: 4,
        display: "flex",
        flexDirection: "column",
        alignItems: "center",
      }}
    >
      <Typography component="h1" variant="h5">
        Create new account
      </Typography>
      <Box component="form" onSubmit={handleSubmit} sx={{ mt: 1 }}>
        <FormField
          name="name"
          autoComplete="name"
          label="How can we call you?"
          margin="normal"
          required
          fullWidth
          autoFocus
          error={getState.nameError}
        />
        <FormField
          name="email"
          type="email"
          autoComplete="email"
          label="Email Address"
          margin="normal"
          required
          fullWidth
          error={getState.emailError}
        />
        <FormField
          name="password"
          type="password"
          autoComplete="password"
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
          Create account
        </LockedButton>
        <Grid container>
          <Grid item xs>
            <RouterLink to="/Account/Login">I have an account!</RouterLink>
          </Grid>
        </Grid>
      </Box>
    </Box>
  );
};

export default Registration;
