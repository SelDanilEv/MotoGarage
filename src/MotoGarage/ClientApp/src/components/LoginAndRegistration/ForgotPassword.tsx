import * as React from "react";
import { Link as RouterLink, useNavigate } from "react-router-dom";
import ErrorResponse from "../../Interfaces/ErrorResponse";
import { useContext, useState } from "react";
import { CurrentUserContext } from "../GlobalState/CurrentUser/CurrentUserStore";
import { LoadingContext } from "../GlobalState/LoadingState/LoadingStore";
import FormField from "../Fields/FormField";
import wrapAPICall from "../GlobalState/LoadingState/wrapAPICall";
import LockedButton from "../Fields/LockedButton";
import { Box, Grid, Typography } from "@mui/material";
import ShowResetMessageSend from "../Popup/Templates/Models/ShowResetMessageSend";
import PasswordResetSentByEmail from "../Popup/Templates/PasswordResetSentByEmail";

const ForgotPassword = () => {
  const navigate = useNavigate();

  const [errorState, setErrorState] = useState({
    emailError: ""
  });

  const [showEmailSendPopup, setShowEmailSendPopup] = useState(false);
  const [loadingState, setLoadingState]: any = useContext(LoadingContext);

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    wrapAPICall(async () => {
      event.preventDefault();
      const data = new FormData(event.currentTarget);

      const requestData = {
        Email: data.get("email"),
      };

      const response = await fetch("api/Account/ForgotPassword", {
        method: "POST",
        body: JSON.stringify(requestData),
        headers: {
          "Content-Type": "application/json",
        },
      });

      switch (response.status) {
        case 200:
          setShowEmailSendPopup(true);
          break;
        case 400:
        default:
          const result = await response.json();

          setErrorState(() => ({
            emailError: ""
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
    <>
      {
        showEmailSendPopup ?
          <PasswordResetSentByEmail setShowPopup={setShowEmailSendPopup} />
          : null
      }
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
          Forgot password
        </Typography>
        <Box component="form"
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
            error={errorState.emailError}
          />
          <LockedButton
            type="submit"
            fullWidth
            variant="contained"
            sx={{ mt: 3, mb: 2 }}
            isLoading={loadingState.Loading}
          >
            Reset password
          </LockedButton>
          <Grid container>
            <Grid item xs>
              <RouterLink to="/Account/Login">Back to login</RouterLink>
            </Grid>
          </Grid>
        </Box>
      </Box>
    </>
  );
};

export default ForgotPassword;
