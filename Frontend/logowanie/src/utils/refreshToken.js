import axiosDefault from './defaultInstance';
import { ENDPOINTS, LOCAL_STORAGE } from './consts';

export const refreshToken = async () => {
  try {
    const response = await axiosDefault.post(ENDPOINTS.refreshToken, {
      refresh: localStorage.getItem(LOCAL_STORAGE.refreshToken)
    });

    localStorage.setItem(LOCAL_STORAGE.accessToken, response.data.accessToken);

    return { succeed: true };
  } catch (error) {
    return { succeed: false };
  }
};