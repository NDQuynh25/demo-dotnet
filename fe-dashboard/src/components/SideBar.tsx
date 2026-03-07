import { ShieldCheck, LayoutDashboard, Users, Bell, Settings, LogOut } from 'lucide-react';
import { authService } from '../apis/AuthApi';

interface SidebarProps {
  isCollapsed: boolean;
}

export const SideBar: React.FC<SidebarProps> = ({ isCollapsed }) => {

  const handleLogout = async() => {
    try {
      const res = await authService.logout();
      if (res.success) {
        localStorage.removeItem('user');
        window.location.href = '/';
      }
    } catch (error) {
      console.error('Logout failed:', error);
    }
  }
  return (
    <aside className={`${isCollapsed ? 'w-20' : 'w-64'} bg-slate-900 text-slate-300 flex flex-col sticky top-0 h-screen transition-all duration-300 overflow-hidden`}>
      {/* Logo Section */}
      <div className={`p-6 flex items-center ${isCollapsed ? 'justify-center' : 'gap-3'} text-white`}>
        <div className="bg-blue-600 p-2 rounded-lg text-white shrink-0">
          <ShieldCheck size={24} />
        </div>
        {!isCollapsed && <span className="text-xl font-bold tracking-tight whitespace-nowrap">AdminPro</span>}
      </div>

      {/* Navigation */}
      <nav className="flex-1 px-4 space-y-2 mt-4">
        {[
          { icon: <LayoutDashboard size={20} />, label: 'Tổng quan', active: false },
          { icon: <Users size={20} />, label: 'Người dùng', active: true },
        
          { icon: <Settings size={20} />, label: 'Cài đặt', active: false },
        ].map((item, idx) => (
          <a
            key={idx}
            href="#"
            className={`flex items-center ${isCollapsed ? 'justify-center' : 'gap-3'} px-4 py-3 rounded-xl transition-colors ${
              item.active ? 'bg-blue-600/10 text-blue-400 border border-blue-600/20' : 'hover:bg-slate-800'
            }`}
            title={isCollapsed ? item.label : ''} 
          >
            <div className="shrink-0">{item.icon}</div>
            {!isCollapsed && <span className="font-medium whitespace-nowrap">{item.label}</span>}
          </a>
        ))}
      </nav>

      {/* Logout */}
      <div className="p-4 border-t border-slate-800">
        <button className={`flex items-center ${isCollapsed ? 'justify-center' : 'gap-3'} w-full px-4 py-3 rounded-xl text-red-400 hover:bg-red-400/10 transition-colors`} onClick={handleLogout}>
          <LogOut size={20} className="shrink-0" />
          {!isCollapsed && <span className="font-medium">Đăng xuất</span>}
        </button>
      </div>
    </aside>
  );
};