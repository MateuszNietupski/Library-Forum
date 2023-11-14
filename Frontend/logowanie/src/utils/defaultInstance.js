import axios from 'axios';
import { baseUrl } from './consts';

const axiosDefault = axios.create({
    baseURL: baseUrl
});

export default axiosDefault;
