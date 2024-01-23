import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import reportWebVitals from './reportWebVitals';
import {Route,Routes,BrowserRouter} from 'react-router-dom';
import  { AuthProvider }  from './context/AuthProvider';
import {ThemeProvider} from "./context/ThemeProvider";
import {ItemCartProvider} from "./context/IterCartProvider";

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
    <BrowserRouter>
        <ThemeProvider>
          <AuthProvider>
              <ItemCartProvider>
                <Routes>
                  <Route path='/*' element={<App />} />
                </Routes>
              </ItemCartProvider>
          </AuthProvider>
        </ThemeProvider>
    </BrowserRouter>
  
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
