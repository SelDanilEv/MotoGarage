import * as React from "react";
import { Grid, CssBaseline, Paper, Box } from "@mui/material";
import { Outlet } from "react-router-dom";

const LoginAndRegistration = () => {
  return (
    <Grid
      container
      //  component="main"
      sx={{ height: "100vh" }}
    >
      <CssBaseline />
      <Grid
        item
        xs={false}
        sm={4}
        md={7}
        sx={{
          backgroundImage: "url(images/motorcycle-wallpapers-small.jpg)",
          backgroundRepeat: "no-repeat",
          backgroundColor: (t) =>
            t.palette.mode === "light"
              ? t.palette.grey[50]
              : t.palette.grey[900],
          backgroundSize: "cover",
          backgroundPosition: "center",
        }}
      />
      <Grid
        item
        xs={12}
        sm={8}
        md={5}
        component={Paper}
        elevation={6}
        square
        id="LoginAndRegistration"
        // bgcolor="#151719"
        // className="container-sm"
      >
        <Outlet />
      </Grid>
    </Grid>
  );
};

export default LoginAndRegistration;
