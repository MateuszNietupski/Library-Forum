import axios from 'axios';
import createAuthRefreshInterceptor from 'axios-auth-refresh';
import { refreshToken } from './refreshToken';
import { baseUrl,LOCAL_STORAGE,PATHS } from './consts';

const axiosAuth = axios.create({
    baseURL: baseUrl
});

axiosAuth.interceptors.request.use(
    (config) => {
      const accessToken = localStorage.getItem(LOCAL_STORAGE.accessToken);
      if (accessToken && typeof accessToken !== 'undefined') {
        config.headers.Authorization = `Bearer ${accessToken}`;
      }
      return config;
    },
    (error) => {
      return Promise.reject(error);
    }
  );

  const refreshAuthLogic = async (failedRequest) => {
    const response = await refreshToken();
    if (response.succeed) {
      const accessToken = localStorage.getItem(LOCAL_STORAGE.accessToken);
      failedRequest.response.config.headers.Authorization = `Bearer ${accessToken}`;
      return Promise.resolve();
    } else {
      localStorage.removeItem(LOCAL_STORAGE.accessToken);
      localStorage.removeItem(LOCAL_STORAGE.refreshToken);
      window.location.href = PATHS.login;
  
      return Promise.reject(failedRequest);
    }
  };
  
  createAuthRefreshInterceptor(axiosAuth, refreshAuthLogic);

  export default axiosAuth;
