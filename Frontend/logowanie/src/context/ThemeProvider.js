import React,{ createContext,useState,useContext} from "react";
import {createTheme, ThemeProvider as MuiThemeProvider} from "@mui/material/styles";
import {blue, red} from "@mui/material/colors";


const ThemeContext = createContext();

export const ThemeProvider = ({ children }) => {
  const [isDarkMode, setIdDarkMode] = useState(false);
    
  const toggleDarkMode = () => {
      setIdDarkMode(!isDarkMode);
  }
  
  const theme = createTheme({
      palette: {
          mode: isDarkMode ? 'dark' : 'light',
          primary: {
              main: isDarkMode ? '#64b5f6' : '#1976d2',
          }
      },
  });
  return(
    <ThemeContext.Provider value={{isDarkMode,toggleDarkMode}}>
        <MuiThemeProvider theme={theme}>
          {children}
        </MuiThemeProvider>
    </ThemeContext.Provider>  
  );
};

export const useTheme = () => {
  return useContext(ThemeContext);  
};
