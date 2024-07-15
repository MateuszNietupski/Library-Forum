import { useContext } from "react";
import { Navigate,Outlet } from "react-router-dom";
import { PATHS } from "../utils/consts";
import { AuthContext } from "../context/AuthProvider";

const ProtectedRoute = ({allowedRoles}) => {
    const {user} = useContext(AuthContext);
    return (
        user.roles.find(role => allowedRoles?.includes(role))
            ? <Outlet />
            : user
                ? <Navigate to={PATHS.unauthorized} />
                : <Navigate to={PATHS.login} />
    )
};

export default ProtectedRoute;