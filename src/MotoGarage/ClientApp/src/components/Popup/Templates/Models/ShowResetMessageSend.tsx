import * as React from "react";
import ErrorResponse from "../../../../Interfaces/ErrorResponse";
import { useContext, useState } from "react";
import { LoadingContext } from "../../../GlobalState/LoadingState/LoadingStore";
import FormField from "../../../Fields/FormField";
import wrapAPICall from "../../../GlobalState/LoadingState/wrapAPICall";
import LockedButton from "../../../Fields/LockedButton";
import { Link as RouterLink } from "react-router-dom";
import { Box, Button, Typography } from "@mui/material";

const ShowResetMessageSend = (props: any) => {
  const [loadingState, setLoadingState]: any = useContext(LoadingContext);

  const [errorState, setErrorState]: any = useState({});

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    wrapAPICall(async () => {
      event.preventDefault();
      const data = new FormData(event.currentTarget);

      const requestData = {
        Id: props.item.id,
        Review: {
          Id: props.item.id,
          ReviewText: data.get("ReviewText"),
          Rate: data.get("Rate"),
        },
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
        my: 1, mx: 2, display: "flex",
        flexDirection: "column", alignItems: "center",
      }}>
      <Box className="center-content" sx={{ mt: 1, mb: 2, width: 1 / 2 }}>
        <Typography
          component="h1" variant="h5"
          display="inline">
          A message with instructions on how to recover your password has been sent to your email
        </Typography>
      </Box>
      <Button
          {...{
            color: "secondary",
            to: "/",
            component: RouterLink
          }}
        > OK </Button>
    </Box >
  );
};

export default ShowResetMessageSend;
