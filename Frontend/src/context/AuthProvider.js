import React, { createContext,useState,useEffect,useCallback } from "react";
import { LOCAL_STORAGE } from "../utils/consts";
import axiosAuth from "../utils/authInstance";

const AuthContext = createContext({
    isLoggedIn: false,
    login: () => {},
    logout: () => {},
});

const AuthProvider = ({ children }) => {
    const [isLoggedIn, setIsLoggedIn] = useState(!!localStorage.getItem(LOCAL_STORAGE.accessToken));
    const [user, setUser] = useState(() => {
        const userInfo = localStorage.getItem(LOCAL_STORAGE.userInfo);
        try {
            return userInfo ? JSON.parse(userInfo) : null;
        } catch (e) {
            console.error("Error parsing user info from localStorage:", e);
            return null;
        }
    });
    const login = useCallback((userInfo) => {
        setIsLoggedIn(true);
        setUser(userInfo);
      }, [user]);
    
    const logout = useCallback(() => {
        localStorage.removeItem(LOCAL_STORAGE.accessToken);
        localStorage.removeItem(LOCAL_STORAGE.refreshToken);
        localStorage.removeItem(LOCAL_STORAGE.userInfo);
        setIsLoggedIn(false);
      }, []);
    
    useEffect(() => {
        const onStorageChange = () => {
            const newAccessToken = localStorage.getItem(LOCAL_STORAGE.accessToken);
            axiosAuth()
            if (!newAccessToken) setIsLoggedIn(false);
          };
          window.addEventListener('storage', onStorageChange);
          return () => {
            window.removeEventListener('storage', onStorageChange);
          };
    }, []);

    return (
        <AuthContext.Provider value={{isLoggedIn, login, logout, user}}>
            {children}
        </AuthContext.Provider>
    )
};

export { AuthContext , AuthProvider};