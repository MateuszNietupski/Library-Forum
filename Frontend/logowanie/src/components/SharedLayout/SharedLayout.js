import { Outlet } from "react-router-dom";
import  Header  from "../navbar";
import Footer from "../footer";
import './SharedLayout.css';
import {Paper} from "@mui/material";
import {useTheme} from "../../context/ThemeProvider";

const SharedLayout = () => {
    
    return (
        <Paper elevation={3}  className="shared-layout">
            <Header />
            <Outlet />
            <Footer />
        </Paper>
    );
};

export default SharedLayout;

