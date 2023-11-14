import React, { createContext,useState,useEffect,useCallback } from "react";
import { LOCAL_STORAGE } from "../utils/consts";


const AuthContext = createContext({
    isLoggedIn: false,
    login: () => {},
    logout: () => {}
});

const AuthProvider = ({ children }) => {
    const [isLoggedIn, setIsLoggedIn] = useState(!!localStorage.getItem(LOCAL_STORAGE.accessToken));

    const login = useCallback(() => {
        setIsLoggedIn(true);
      }, []);

    const logout = useCallback(() => {
        localStorage.removeItem(LOCAL_STORAGE.accessToken);
        localStorage.removeItem(LOCAL_STORAGE.refreshToken);
        localStorage.removeItem(LOCAL_STORAGE.role);
        setIsLoggedIn(false);
      }, []);
    
    useEffect(() => {
        const onStorageChange = () => {
            const newAccessToken = localStorage.getItem(LOCAL_STORAGE.accessToken);
            if (!newAccessToken) setIsLoggedIn(false);
          };
          window.addEventListener('storage', onStorageChange);
          return () => {
            window.removeEventListener('storage', onStorageChange);
          };
    }, []);

    return (
        <AuthContext.Provider value={{ isLoggedIn, login, logout }}>
            {children}
        </AuthContext.Provider>
    )
};

export { AuthContext , AuthProvider};