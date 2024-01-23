export const baseUrl = "http://localhost:5004"
export const frontUrl = "http://localhost:3000"

export const ENDPOINTS = {
    login : `${baseUrl}/api/auth/Login`,
    register : `${baseUrl}/api/auth/Register`,
    refreshToken : `${baseUrl}/api/auth/RefreshToken`,
    GETGALLERY: `${baseUrl}/api/getGallery`,
    updateGallerySequence: `${baseUrl}/api/updateGallerySequence`,
    getForumCategories: `${baseUrl}/api/getCategories`,
    getPost: `${baseUrl}/api/getPost`,
    getForumPosts: `${baseUrl}/api/getPosts`,
    getBooks: `${baseUrl}/api/getBooks`,
    confirmationMail: `${baseUrl}/api/confirmationMail`,
    addComment: `${baseUrl}/api/addComment`
}

export const PATHS = {
    home: '/',
    login: '/login',
    register: '/register',
    addNews: '/addNews',
    forum: '/forum',
    category: '/forum/:categoryId',
    subcategory: '/forum/:categoryId/subcategory/:subcategoryId',
    post: '/forum/:categoryId/subcategory/:subcategoryId/post/:postId',
    adminPanel: '/adminPanel',
    books: '/books'
}

export const LOCAL_STORAGE = {
    accessToken: 'access_token',
    refreshToken: 'refresh_token',
    role: 'role'
};