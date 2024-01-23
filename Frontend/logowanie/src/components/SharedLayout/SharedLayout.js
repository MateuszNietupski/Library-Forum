import { Outlet } from "react-router-dom";
import  Header  from "../navbar";
import Footer from "../footer";
import './SharedLayout.css';
import {Paper} from "@mui/material";
import {useTheme} from "../../context/ThemeProvider";

const SharedLayout = () => {
    const { isDarkMode } = useTheme();
    
    const sharedStyles = {
        backgroundColor: isDarkMode ? '#1a1a1a' : '#ffffff',
        color: isDarkMode ? '#ffffff' : '#000000'
    }
    return (
        <Paper elevation={3} style={sharedStyles} className="shared-layout">
            <Header />
            <Outlet />
            <Footer />
        </Paper>
    );
};

export default SharedLayout;

