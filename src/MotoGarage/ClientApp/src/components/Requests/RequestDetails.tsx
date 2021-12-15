import * as React from "react";
import ErrorResponse from "../../Interfaces/ErrorResponse";
import { useContext, useEffect, useState } from "react";
import { LoadingContext } from "../GlobalState/LoadingState/LoadingStore";
import FormField from "../Fields/FormField";
import wrapAPICall from "../GlobalState/LoadingState/wrapAPICall";
import LockedButton from "../Fields/LockedButton";
import { Box, InputLabel, MenuItem, Select } from "@mui/material";
import Guid from "../utils/Guid";
import { CurrentUserContext } from "../GlobalState/CurrentUser/CurrentUserStore";
import RequestReviewPopup from "../Popup/Templates/RequestReviewPopup";

const RequestDetails = (props: any) => {
  const [loadingState, setLoadingState]: any = useContext(LoadingContext);
  const [currentUserState, setCurrentUserState]: any = useContext(CurrentUserContext);

  const [errorState, setErrorState]: any = useState({});
  const [showReviewPopup, setShowReviewPopup]: any = useState(false);
  const [options, setOptions]: any = useState([]);

  useEffect(() => {
    wrapAPICall(async () => {
      const response = await fetch("api/AccountManager/GetAllEmployees", {
        method: "GET",
      });

      const result = await response.json();

      switch (response.status) {
        case 200:
          let employees = [{ key: Guid.empty, value: "Empty" }];
          result.forEach(
            (employee: any) => {
              employees.push({ key: employee.id, value: `${employee.name}(${employee.email})` })
            }
          )
          setOptions(employees);
          break;
        case 400:
        default:
      }
    }, setLoadingState);
  }, [])

  // useEffect(() => {
  //   if (showReviewPopup) {
  //     props.refreshData();
  //   }
  // }, [showReviewPopup])

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    wrapAPICall(async () => {
      event.preventDefault();
      const data = new FormData(event.currentTarget);

      const requestData = {
        Id: props.item.id,
        Title: props.item.title,
        Message: data.get("Message"),
        Status: +props.item.status,
        Review: props.item.review,
        ReporterId: props.item.reporter?.id,
        AssigneeId:
          currentUserState.CurrentUser?.role == 'Client' ?
            (props.item.assignee ?
              props.item.assignee.id :
              Guid.empty) :
            data.get("assignee"),
      };


      const response = await fetch("api/ServiceRequest/UpdateServiceRequest", {
        method: "POST",
        body: JSON.stringify(requestData),
        headers: {
          "Content-Type": "application/json",
        },
      });

      const result = await response.json();

      switch (response.status) {
        case 200:
          props.refreshData()
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

  const renderDetailsForm = () => {
    switch (currentUserState.CurrentUser?.role) {
      case "Admin":
        return <Box component="form"
          onSubmit={handleSubmit} sx={{ mt: 1 }}>
          <FormField
            name="Message"
            label="Message"
            margin="normal"
            fullWidth
            defaultValue={props.item.message}
            required
            multiline
            rows={7}
            maxRows={7}
            error={errorState.messageError}
          />
          <FormField
            name="Message"
            label="Message"
            margin="normal"
            fullWidth
            defaultValue={`${props.item.reporter?.name}(${props.item.reporter?.email})`}
            disabled
            error={errorState.messageError}
          />
          <InputLabel id="assignee-label">Assignee</InputLabel>
          <Select
            labelId="assignee-label"
            id="assignee"
            name="assignee"
            fullWidth
            margin="dense"
            defaultValue={
              props.item.assignee ?
                props.item.assignee.id :
                Guid.empty}
          >
            {
              options.map((option: any) => {
                return (
                  <MenuItem key={option.key} value={option.key}>
                    {option.value}
                  </MenuItem>
                );
              })
            }
          </Select>
          <LockedButton
            type="submit"
            variant="contained"
            isLoading={loadingState.Loading}
            fullWidth
            style={{
              margin: "40px 0px 20px",
              padding: "15px"
            }}
          >
            Update
          </LockedButton>
        </Box>
      case "Employee":
        return <Box component="form"
          onSubmit={handleSubmit} sx={{ mt: 1 }}>
          <FormField
            name="Message"
            label="Message"
            margin="normal"
            fullWidth
            defaultValue={props.item.message}
            disabled
            multiline
            rows={7}
            maxRows={7}
            error={errorState.messageError}
          />
          <FormField
            name="reporter"
            label="Reporter"
            margin="normal"
            fullWidth
            defaultValue={`${props.item.reporter?.name}(${props.item.reporter?.email})`}
            disabled
            error={errorState.messageError}
          />
          <FormField
            name="Assignee"
            label="Assignee"
            margin="normal"
            fullWidth
            defaultValue={`${props.item.assignee?.name}(${props.item.assignee?.email})`}
            disabled
            error={errorState.messageError}
          />
        </Box>
      case "Client":
        return <Box component="form"
          onSubmit={handleSubmit} sx={{ mt: 1 }}>
          <FormField
            name="Message"
            label="Message"
            margin="normal"
            fullWidth
            defaultValue={props.item.message}
            required
            multiline
            rows={7}
            maxRows={7}
            error={errorState.messageError}
          />
          <FormField
            name="Message"
            label="Message"
            margin="normal"
            fullWidth
            defaultValue={`${props.item.reporter.name}(${props.item.reporter.email})`}
            disabled
            error={errorState.messageError}
          />
          <FormField
            name="assignee"
            label="assignee"
            margin="normal"
            fullWidth
            defaultValue={
              props.item.assignee ?
                `${props.item.assignee.name}(${props.item.assignee.email})` :
                "Not assigned"}
            disabled
            error={errorState.messageError}
          />
          <LockedButton
            type="submit"
            variant="contained"
            isLoading={loadingState.Loading}
            fullWidth
            style={{
              margin: "10px 0px 20px",
              padding: "15px"
            }}
          >
            Update
          </LockedButton>
          {
            // props.item.review == null &&
            props.item.status == 6 ?
              <LockedButton
                variant="contained"
                isLoading={loadingState.Loading}
                fullWidth
                style={{
                  margin: "10px 0px 20px",
                  padding: "15px",
                  background: "rgb(50 131 253)",
                }}
                onClick={() => { setShowReviewPopup(true) }}
              >
                Left review
              </LockedButton>
              : null
          }
        </Box>
    }
  }

  return (
    <Box
      sx={{
        my: 1,
        mx: 4,
        display: "flex",
        flexDirection: "column",
        background: "black",
        padding: "20px",
        borderRadius: "5px"
      }}
    >
      {
        showReviewPopup ?
          <RequestReviewPopup
            setShowPopup={setShowReviewPopup}
            item={props.item} />
          : null
      }
      {renderDetailsForm()}
    </Box>
  );
};

export default RequestDetails;
