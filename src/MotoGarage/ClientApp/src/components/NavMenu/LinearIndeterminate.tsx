import * as React from "react";
import Box from "@mui/material/Box";
import LinearProgress from "@mui/material/LinearProgress";
import { LoadingContext } from "../GlobalState/LoadingState/LoadingStore";
import { useContext } from "react";

export default function LinearIndeterminate() {
  const [loadingState, setLoadingState]: any = useContext(LoadingContext);

  return (
    <Box
      sx={{
        position: "absolute",
        top: 0,
        zIndex: 10000,
        width: "100%",
        height: "5px",
      }}
    >
      {loadingState.Loading && <LinearProgress />}
    </Box>
  );
}
