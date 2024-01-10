import { useContext, useState } from 'react';
import { AuthContext } from '../context/AuthProvider';
import { useNavigate } from 'react-router-dom';
import axiosDefault from '../utils/defaultInstance';
import { PATHS,ENDPOINTS,LOCAL_STORAGE } from '../utils/consts';
import {jwtDecode} from "jwt-decode";
const useAuth = () => {
  const { login: loginContext } = useContext(AuthContext);
  const navigate = useNavigate();
  const [error, setError] = useState('');

  const login = (data) => {
    axiosDefault
      .post(ENDPOINTS.login, data)
      .then((response) => {
        localStorage.setItem(LOCAL_STORAGE.accessToken, response.data.accessToken);
        localStorage.setItem(LOCAL_STORAGE.refreshToken, response.data.refreshToken);
        localStorage.setItem(LOCAL_STORAGE.role, jwtDecode(response.data.accessToken));
        loginContext();
        navigate(PATHS.home);
      })
      .catch((error) => {
        setError(error.message);
      });
  };

  return {
    login,
    error
  };
};

export default useAuth;