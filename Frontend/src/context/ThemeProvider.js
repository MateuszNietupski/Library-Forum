import React,{ createContext,useState,useContext} from "react";
import {createTheme, ThemeProvider as MuiThemeProvider} from "@mui/material/styles";
import {ChromePicker} from "react-color";
import useLocalStorge from "../hooks/useLocalStorge";

const ThemeContext = createContext();

export const ThemeProvider = ({ children }) => {
    const [isDarkMode,setIsDarkMode] = useLocalStorge("thheme");
    const [primaryColor, setPrimaryColor] = useState('#1976d2');
    const toggleDarkMode = () => {
        setIsDarkMode(!isDarkMode);
    }
    const theme = createTheme({
        palette: {
            mode: isDarkMode ? 'dark' : 'light',
            primary: {
                main: isDarkMode ? "#64b5f6" : primaryColor,
            }
        },
    });
    return (
        <ThemeContext.Provider value={{isDarkMode, toggleDarkMode, primaryColor, setPrimaryColor}}>
            <MuiThemeProvider theme={theme}>

                {children}
            </MuiThemeProvider>
        </ThemeContext.Provider>
    );
};

export const useTheme = () => {
  return useContext(ThemeContext);  
};
