import * as React from "react";
import ErrorResponse from "./../../Interfaces/ErrorResponse";
import { useContext, useState } from "react";
import { LoadingContext } from "../GlobalState/LoadingState/LoadingStore";
import FormField from "../Fields/FormField";
import wrapAPICall from "../GlobalState/LoadingState/wrapAPICall";
import LockedButton from "../Fields/LockedButton";
import { Box, Typography } from "@mui/material";

const CreateRequestReview = (props: any) => {
  const [loadingState, setLoadingState]: any = useContext(LoadingContext);

  const [errorState, setErrorState]: any = useState({});

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    wrapAPICall(async () => {
      event.preventDefault();
      const data = new FormData(event.currentTarget);

      const requestData = {
        Id: data.get("Title"),
        Message: data.get("Message"),
      };

      const response = await fetch("api/ServiceRequest/AddRequestReview", {
        method: "POST",
        body: JSON.stringify(requestData),
        headers: {
          "Content-Type": "application/json",
        },
      });

      const result = await response.json();

      switch (response.status) {
        case 200:
          props.setShowPopup(false)
          break;
        case 400:
        default:

          let error: ErrorResponse = result;
          error.errors = new Map<string, Array<string>>(
            Object.entries(result.errors)
          );
      }
    }, setLoadingState);
  };

  return (
    <Box
      sx={{
        my: 1,
        mx: 4,
        display: "flex",
        flexDirection: "column",
        alignItems: "center",
      }}
    >
      <Typography component="h1" variant="h5">
        Create new request
      </Typography>
      <Box component="form"
        // noValidate 
        onSubmit={handleSubmit} sx={{ mt: 1 }}>
        <FormField
          name="Title"
          label="Title"
          margin="normal"
          fullWidth
          required
          autoFocus
          error={errorState.titleError}
        />
        <FormField
          name="Message"
          label="Message"
          margin="normal"
          fullWidth
          required
          multiline
          rows={7}
          maxRows={7}
          error={errorState.messageError}
        />
        <LockedButton
          type="submit"
          variant="contained"
          isLoading={loadingState.Loading}
          style={{
            left: '50%',
            transform: 'translate(-50%, -50%)'
          }}
        >
          Send review
        </LockedButton>
      </Box>
    </Box>
  );
};

export default CreateRequestReview;
