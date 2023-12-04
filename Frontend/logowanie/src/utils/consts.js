export const baseUrl = "http://localhost:5004"
export const frontUrl = "http://localhost:3000"

export const ENDPOINTS = {
    login : `${baseUrl}/api/auth/Login`,
    register : `${baseUrl}/api/auth/Register`,
    refreshToken : `${baseUrl}/api/auth/RefreshToken`,
    GETNEWSHORT: `${baseUrl}/api/News/getNewsShortcuts`,
    ADDNEWS: `${baseUrl}/api/newsAdd`,
    DELETENEWS: `${baseUrl}/api/News/deleteNews`,
    GETGALLERY: `${baseUrl}/api/getGallery`
}

export const PATHS = {
    home: '/',
    login: '/login',
    register: '/register',
    addNews: '/addNews'
}

export const LOCAL_STORAGE = {
    accessToken: 'access_token',
    refreshToken: 'refresh_token'
};