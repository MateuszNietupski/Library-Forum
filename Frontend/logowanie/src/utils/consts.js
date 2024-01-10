export const baseUrl = "http://localhost:5004"
export const frontUrl = "http://localhost:3000"

export const ENDPOINTS = {
    login : `${baseUrl}/api/auth/Login`,
    register : `${baseUrl}/api/auth/Register`,
    refreshToken : `${baseUrl}/api/auth/RefreshToken`,
    GETGALLERY: `${baseUrl}/api/getGallery`,
    getForumCategories: `${baseUrl}/api/getCategories`
}

export const PATHS = {
    home: '/',
    login: '/login',
    register: '/register',
    addNews: '/addNews',
    forum: '/forum',
    adminPanel: '/adminPanel'
}

export const LOCAL_STORAGE = {
    accessToken: 'access_token',
    refreshToken: 'refresh_token',
    role: 'role'
};