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

function App() {
  return (
    <div className="App">
      <Routes>
        <Route path={PATHS.login} element={<LoginPage />} />
        <Route path={PATHS.register} element={<Register />} />
        
        <Route element={<SharedLayout />} >
          <Route element={<ProtectedRoute />}>
            <Route path={PATHS.addNews} element={<AddNews />} />
            <Route path={PATHS.home} element={<Home />} />
            <Route path={PATHS.forum} element={<Forum />} />
          </Route>
        </Route>
      </Routes>
    </div>
  );
}

export default App;
