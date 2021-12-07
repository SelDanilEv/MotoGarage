import { createTheme, CssBaseline, ThemeProvider } from "@mui/material";
import React, { ReactNode } from "react";

const themeDark = createTheme({
  palette: {
    background: {
      default: "#222222",
    },
    text: {
      primary: "#ffffff",
    },
    mode: "dark",
  },
});

const LayoutDefault = ({ children }: { children: ReactNode }) => (
  <ThemeProvider theme={themeDark}>
    {/* <CssBaseline /> */}
    {/* <Header navPosition="right" className="reveal-from-bottom" /> */}
    <main className="site-content">{children}</main>
    {/* <Footer /> */}
  </ThemeProvider>
);

export default LayoutDefault;
