export const baseUrl = "http://localhost:5004"

export const ENDPOINTS = {
    userInfo : `${baseUrl}/api/auth/UserInfo`,
    login : `${baseUrl}/api/auth/Login`,
    register : `${baseUrl}/api/auth/Register`,
    refreshToken : `${baseUrl}/api/auth/RefreshToken`,
    GETGALLERY: `${baseUrl}/api/getGallery`,
    updateGallerySequence: `${baseUrl}/api/updateGallerySequence`,
    getForumCategories: `${baseUrl}/api/getCategories`,
    getPost: `${baseUrl}/api/getPost`,
    getForumPosts: `${baseUrl}/api/getPosts`,
    getBooks: `${baseUrl}/api/Books`,
    getBook: (id) => `${baseUrl}/api/Books/${id}`,
    addReview: (id) => `${baseUrl}/api/Review/${id}`,
    deleteReview: (id) => `${baseUrl}/api/Review/${id}`,
    updateReview: (id) => `${baseUrl}/api/Review/${id}`,
    confirmationMail: `${baseUrl}/api/confirmationMail`,
    addComment: `${baseUrl}/api/addComment`,
    addPost: `${baseUrl}/api/addPost`,
    addLoan: `${baseUrl}/api/Loans`,
    loanConfirm: `${baseUrl}/api/loanConfirm`,
    getUserLoans: (id) => `${baseUrl}/api/Loans/users/${id}`,
}

export const PATHS = {
    home: '/',
    login: '/login',
    register: '/register',
    unauthorized: '/unauthorized',
    forum: '/forum',
    category: '/forum/:categoryId',
    subcategory: '/forum/:categoryId/subcategory/:subcategoryId',
    post: '/forum/:categoryId/subcategory/:subcategoryId/post/:postId',
    adminPanel: '/adminPanel',
    books: '/books',
    book: `/books/:id`,
    loanConfirm: '/loanConfirm',
    loanConfirmSuccess: '/loanConfirm/success',
    userPanel: '/userPanel',
}

export const LOCAL_STORAGE = {
    accessToken: 'access_token',
    refreshToken: 'refresh_token',
    userInfo: 'userInfo'
};

export const ROLES = {
    user : "User",
    admin : "Admin",
    worker : "Worker"
}