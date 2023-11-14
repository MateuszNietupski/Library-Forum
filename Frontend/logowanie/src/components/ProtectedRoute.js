import { useContext } from "react";
import { Navigate,Outlet } from "react-router-dom";
import { PATHS } from "../utils/consts";
import { AuthContext } from "../context/AuthProvider";

const ProtectedRoute = () => {
    const {isLoggedIn} = useContext(AuthContext);
    return isLoggedIn ? <Outlet /> : <Navigate to={PATHS.login} />;
};

export default ProtectedRoute;