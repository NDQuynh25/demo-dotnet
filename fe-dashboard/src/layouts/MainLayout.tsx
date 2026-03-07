import React, { useState } from 'react';
import { Navigate, Outlet } from 'react-router-dom';
import { SideBar } from '../components/SideBar';
import { Menu, X, Bell, UserCircle } from 'lucide-react';

const MainLayout: React.FC = () => {
  const [isCollapsed, setIsCollapsed] = useState(false);
  const isAuthenticated = !!localStorage.getItem('user');

  if (!isAuthenticated) {
    return <Navigate to="/" />;
  }

  return (
    <div className="flex min-h-screen bg-slate-50 font-sans">
     
      <SideBar isCollapsed={isCollapsed} />

      <div className="flex-1 flex flex-col h-screen overflow-hidden">
       
        <header className="h-16 bg-white border-b border-slate-200 flex items-center justify-between px-6 shrink-0 transition-all">
          <div className="flex items-center gap-4">
           
            <button 
              onClick={() => setIsCollapsed(!isCollapsed)}
              className="p-2 hover:bg-slate-100 rounded-lg text-slate-600 transition-colors"
            >
              {isCollapsed ? <Menu size={22} /> : <X size={22} />}
            </button>
            
            <div className="text-sm text-slate-400 font-medium hidden sm:block">
              Hệ thống / <span className="text-slate-900">Danh sách người dùng</span>
            </div>
          </div>

          <div className="flex items-center gap-3">
            <button className="p-2 text-slate-400 hover:text-blue-600 transition-all">
              <Bell size={20} />
            </button>
            <div className="flex items-center gap-2 ml-2 pl-4 border-l border-slate-100">
              <span className="text-sm font-bold text-slate-700">Admin</span>
              <UserCircle size={28} className="text-slate-300" />
            </div>
          </div>
        </header>

        <main className="flex-1 overflow-y-auto p-6 transition-all duration-300">
          <Outlet />
        </main>
      </div>
    </div>
  );
};

export default MainLayout;