import { Outlet } from "react-router-dom";
import  Header  from "../navbar";
import Footer from "../footer";
import './SharedLayout.css';

const SharedLayout = () => {
    return (
        <div className="shared-layout">
            <Header />
            <Outlet />
            <Footer />
        </div>
    );
};

export default SharedLayout;