import * as React from "react";
import { CurrentUserContext } from "../GlobalState/CurrentUser/CurrentUserStore";
import { useContext, useState } from "react";
import { Box, IconButton } from '@mui/material';
import ManageAccountsIcon from '@mui/icons-material/ManageAccounts';
import UpdateUserInfoPopup from "../Popup/Templates/UpdateUserInfoPopup";

const CurrentUserInfo = () => {
  const [currentUserState, setCurrentUserState]: any = useContext(CurrentUserContext);
  const [showUpdateUserInfoPopup, setShowUpdateUserInfoPopup] = useState(false);

  const renderUserSection = () => {
    console.log("InfoSection")
    console.log(currentUserState.CurrentUser)
    if (currentUserState.CurrentUser) {
      return (
        <>
          {
            showUpdateUserInfoPopup ?
              <UpdateUserInfoPopup setShowPopup={setShowUpdateUserInfoPopup} />
              : null
          }
          <Box>
            {currentUserState.CurrentUser.name}({currentUserState.CurrentUser.email})
            <IconButton
              color="default"
              onClick={() => { setShowUpdateUserInfoPopup(true) }} >
              <ManageAccountsIcon />
            </IconButton>
          </Box>
        </>
      )
    }
    else {
      return ""
    }
  }

  return (
    <>
      {renderUserSection()}
    </>
  );
}

export default CurrentUserInfo;