import * as React from "react";
import ErrorResponse from "../../../../Interfaces/ErrorResponse";
import { useContext, useState } from "react";
import { CurrentUserContext } from "../../../GlobalState/CurrentUser/CurrentUserStore";
import { LoadingContext } from "../../../GlobalState/LoadingState/LoadingStore";
import FormField from "../../../Fields/FormField";
import wrapAPICall from "../../../GlobalState/LoadingState/wrapAPICall";
import LockedButton from "../../../Fields/LockedButton";
import { Box, Divider, Typography } from "@mui/material";

const AccountInfoPage = (props: any) => {
  const [loadingState, setLoadingState]: any = useContext(LoadingContext);
  const [currentUserState, setCurrentUserState]: any = useContext(CurrentUserContext);
  const [errorState, setErrorState]: any = useState({});

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    wrapAPICall(async () => {
      event.preventDefault();
      const data = new FormData(event.currentTarget);

      if (data.get("newPassword") !== data.get("confirmNewPassword")) {
        setErrorState(() => ({
          nameError: "",
          oldPasswordError: "",
          newPasswordError: "",
          confirmNewPasswordError: "Password mismatch",
        }));
        return;
      }

      const requestData = {
        Name: data.get("name"),
        OldPassword: data.get("oldPassword"),
        NewPassword: data.get("newPassword"),
      };

      const response = await fetch("api/AccountManager/UpdateUserInfo", {
        method: "POST",
        body: JSON.stringify(requestData),
        headers: {
          "Content-Type": "application/json",
        },
      });

      const result = await response.json();

      switch (response.status) {
        case 200:
          let newCurrentUser = currentUserState.CurrentUser;
          newCurrentUser.name = data.get("name");
          setCurrentUserState({ type: "SET_USER", payload: newCurrentUser })
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
        Account info
      </Typography>
      <Box component="form"
        className="center-content"
        onSubmit={handleSubmit} sx={{ mt: 1, mb: 2 }}>
        <FormField
          name="name"
          label="Review text"
          margin="normal"
          defaultValue={currentUserState.CurrentUser.name}
          fullWidth
          autoFocus
          error={errorState.nameError}
        />
        <Divider variant="middle" />
        <FormField
          name="oldPassword"
          label="Old password"
          margin="normal"
          fullWidth
          error={errorState.oldPasswordError}
        />
        <FormField
          name="newPassword"
          label="New password"
          margin="normal"
          fullWidth
          error={errorState.newPasswordError}
        />
        <FormField
          name="confirmNewPassword"
          label="Confirm new password"
          margin="normal"
          fullWidth
          error={errorState.confirmNewPasswordError}
        />
        <Divider variant="middle" sx={{ m: 2 }} />
        <LockedButton
          type="submit"
          variant="contained"
          isLoading={loadingState.Loading}
        >
          Update
        </LockedButton>
      </Box>
    </Box>
    // <Box
    //   sx={{
    //     my: 1, mx: 2, display: "flex",
    //     flexDirection: "column", alignItems: "center",
    //   }}>
    //   <Typography component="h1" variant="h5">
    //     Account info
    //   </Typography>
    //   <Box component="form"
    //     onSubmit={handleSubmit} sx={{ mt: 1 }}>
        // <FormField
        //   name="name"
        //   label="Review text"
        //   margin="normal"
        //   defaultValue={currentUserState.CurrentUser.name}
        //   fullWidth
        //   autoFocus
        //   error={errorState.nameError}
        // />
        // {/* <Divider variant="middle" /> */}
        // <FormField
        //   name="oldPassword"
        //   label="Old password"
        //   margin="normal"
        //   fullWidth
        //   error={errorState.oldPasswordError}
        // />
        // <FormField
        //   name="newPassword"
        //   label="New password"
        //   margin="normal"
        //   fullWidth
        //   error={errorState.newPasswordError}
        // />
        // <FormField
        //   name="confirmNewPassword"
        //   label="Confirm new password"
        //   margin="normal"
        //   fullWidth
        //   error={errorState.confirmNewPasswordError}
        // />
        // {/* <Divider variant="middle" /> */}
    //     <LockedButton
    //       type="submit"
    //       variant="contained"
    //       isLoading={loadingState.Loading}
    //       style={{
    //         left: '50%',
    //         transform: 'translate(-50%, -50%)'
    //       }}
    //     >
    //       Update
    //     </LockedButton>
    //   </Box>
    // </Box>
  );
};

export default AccountInfoPage;
