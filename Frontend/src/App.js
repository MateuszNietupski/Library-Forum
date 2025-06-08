import { Route,Routes } from 'react-router-dom';
import {PATHS, ROLES} from './utils/consts';
import './App.css';
import { LoginPage } from './pages/LoginPage';
import Register from './pages/Register';
import SharedLayout from './components/SharedLayout/SharedLayout';
import ProtectedRoute from './components/ProtectedRoute';
import Home from './pages/Home/Home';
import Forum from "./pages/Forum";
import PostList from "./pages/PostList";
import Post from "./pages/Post";
import BooksPage from "./pages/BooksPage";
import AdminPage from "./pages/AdminPage";
import Unauthorized from "./pages/Unauthorized";
import NotFound from "./pages/NotFound";
import BookDetailsPage from "./pages/BookDetailsPage";
import LoansPage from "./pages/LoansPage";
import LoanSummary from "./pages/LoanSummary";
import UserPage from "./pages/UserPage";

function App() {
  return (
    <div className="App">
      <Routes>
        <Route element={<SharedLayout />} >
          <Route path={PATHS.unauthorized} element={<Unauthorized/>}/>
          <Route path={PATHS.login} element={<LoginPage/>} />
          <Route path={PATHS.register} element={<Register/>} />

          <Route path={PATHS.books} element={<BooksPage/>}/>
          <Route path={PATHS.book} element={<BookDetailsPage/>}/>
          
          <Route element={<ProtectedRoute allowedRoles={[ROLES.user,ROLES.admin,ROLES.worker]}/>}>
            <Route path={PATHS.home} element={<Home />} />
            <Route path={PATHS.forum} element={<Forum />} />
            <Route path={PATHS.subcategory} element={<PostList/>}/>
            <Route path={PATHS.post} element={<Post/>}/>
            <Route path={PATHS.loanConfirm} element={<LoansPage/>}/>
            <Route path={PATHS.loanConfirmSuccess} element={<LoanSummary/>}/>
            <Route path={PATHS.userPanel} element={<UserPage/>}/>
          </Route>
          
          <Route element={<ProtectedRoute allowedRoles={[ROLES.admin]}/>}>
            <Route path={PATHS.adminPanel} element={<AdminPage/>}/>
          </Route>
          
          <Route path={"/*"} element={<NotFound/>}/>
        </Route>
      </Routes>
    </div>
  );
}

export default App;
