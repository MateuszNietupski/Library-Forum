import { Route,Routes } from 'react-router-dom';
import { PATHS } from './utils/consts';
import './App.css';
import { LoginPage } from './pages/LoginPage';
import Register from './pages/Register';
import SharedLayout from './components/SharedLayout/SharedLayout';
import ProtectedRoute from './components/ProtectedRoute';
import Home from './pages/Home/Home';
import AddNews from './pages/AddNews';
import Forum from "./pages/Forum";
import PostList from "./pages/PostList";
import Post from "./pages/Post";
import BooksPage from "./pages/BooksPage";
import AdminPage from "./pages/AdminPage";

function App() {
  return (
    <div className="App">
      <Routes>
        <Route element={<SharedLayout />} >
          <Route path={PATHS.login} element={<LoginPage />} />
          <Route path={PATHS.register} element={<Register />} />
          <Route element={<ProtectedRoute />}>
            <Route path={PATHS.home} element={<Home />} />
            <Route path={PATHS.forum} element={<Forum />} />
            <Route path={PATHS.subcategory} element={<PostList/>}/>
            <Route path={PATHS.post} element={<Post/>}/>
            <Route path={PATHS.books} element={<BooksPage/>}/>
            <Route path={PATHS.adminPanel} element={<AdminPage/>}/>
          </Route>
        </Route>
      </Routes>
    </div>
  );
}

export default App;
