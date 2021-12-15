import * as React from "react";
import ErrorResponse from "../../../../Interfaces/ErrorResponse";
import { useContext, useState } from "react";
import { LoadingContext } from "../../../GlobalState/LoadingState/LoadingStore";
import wrapAPICall from "../../../GlobalState/LoadingState/wrapAPICall";
import { Link as RouterLink } from "react-router-dom";
import { Box, Button, Typography } from "@mui/material";

const ShowResetMessageSend = (props: any) => {
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
