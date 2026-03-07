import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';


import MainLayout from './layouts/MainLayout';
import UserPage from './pages/user/UserPage';
import LoginPage from './pages/auth/LoginPage';
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';



const App: React.FC = () => {
  return (
    <BrowserRouter>
      <Routes>
        
        <Route path="/" >
          <Route path="" element={<LoginPage />} />
         
        </Route>

        
        <Route path="" element={<MainLayout />}>
          <Route path='/dashboard' index element={<div>Trang chủ Dashboard</div>} />
          <Route path="/users" element={<UserPage />} />
          <Route path="/settings" element={<div>Cài đặt</div>} />
        </Route>

        
        <Route path="*" element={<Navigate to="/" />} />
      </Routes>
      <ToastContainer position="top-right" autoClose={3000} style={{ zIndex: 99999 }}/>
    </BrowserRouter>
  );
};
export default App;