import * as React from "react";
import ErrorResponse from "../../Interfaces/ErrorResponse";
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
      <Typography component="h1" variant="h5">
        Create new request
      </Typography>
      <Box component="form"
        onSubmit={handleSubmit} sx={{ mt: 1 }}>
        <FormField
          name="ReviewText"
          label="Review text"
          margin="normal"
          defaultValue= {props.item?.review? props.item.review.reviewText : ""}
          fullWidth
          required
          autoFocus
          error={errorState.reviewTextError}
        />
        <FormField
          name="Rate"
          type="number"
          label="Rate (from 1 to 5)"
          defaultValue= {props.item?.review? props.item.review.rate : 5}
          InputProps={{ inputProps: 
            { min: 1, max: 5,  } 
          }}
          margin="normal"
          fullWidth
          required
          error={errorState.rateError}
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
          Create review
        </LockedButton>
      </Box>
    </Box>
  );
};

export default CreateRequestReview;
