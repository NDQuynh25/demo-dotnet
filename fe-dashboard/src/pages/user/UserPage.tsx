import { useState } from 'react';
import { UserPlus, Search, Trash2, Edit } from 'lucide-react';


export interface User {
    id: number;
    name: string;
    dob: string;
    email: string;
    phone: string;
    address: string;
}

export default function UserPage() {
    const [users, setUsers] = useState<User[]>([
    { id: 1, name: 'Nguyễn Văn A', dob: '1995-05-15', email: 'anv@gmail.com', phone: '0901234567', address: 'Quận 1, TP.HCM' },
    { id: 2, name: 'Trần Thị B', dob: '1998-10-20', email: 'btran@gmail.com', phone: '0988777666', address: 'Cầu Giấy, Hà Nội' },
  ]);

  const [searchTerm, setSearchTerm] = useState<string>('');

  const deleteUser = (id: number): void => {
    if(window.confirm('Bạn có chắc chắn muốn xóa?')) {
      setUsers(users.filter(user => user.id !== id));
    }
  };
  return (
    <div className="max-w-6xl mx-auto">
          
          {/* Header */}
          <div className="flex flex-col md:flex-row md:items-center justify-between gap-4 mb-8">
            <div>
              <h1 className="text-3xl font-bold text-slate-900">Quản lý Nhân sự</h1>
              <p className="text-slate-500 text-sm mt-1">Hệ thống lưu trữ và quản lý thông tin người dùng</p>
            </div>
            <button className="bg-blue-600 hover:bg-blue-700 text-white px-6 py-2.5 rounded-xl flex items-center justify-center gap-2 shadow-lg shadow-blue-200 transition-all active:scale-95">
              <UserPlus size={18} /> 
              <span className="font-semibold">Thêm thành viên</span>
            </button>
          </div>

          {/* Search Bar */}
          <div className="relative mb-8 bg-white rounded-2xl shadow-sm border border-slate-200 overflow-hidden">
            <Search className="absolute left-4 top-1/2 -translate-y-1/2 text-slate-400" size={20} />
            <input
              type="text"
              placeholder="Tìm kiếm theo tên hoặc email nhân viên..."
              className="w-full pl-12 pr-4 py-4 outline-none text-slate-700 placeholder:text-slate-400"
              onChange={(e: React.ChangeEvent<HTMLInputElement>) => setSearchTerm(e.target.value)}
            />
          </div>

          {/* Table Container */}
          <div className="bg-white rounded-2xl shadow-sm border border-slate-200 overflow-hidden">
            <div className="overflow-x-auto">
              <table className="w-full text-left border-collapse">
                <thead className="bg-slate-50/50 border-b border-slate-200">
                  <tr>
                    <th className="p-5 font-semibold text-slate-600 text-sm uppercase">Họ tên</th>
                    <th className="p-5 font-semibold text-slate-600 text-sm uppercase">Ngày sinh</th>
                    <th className="p-5 font-semibold text-slate-600 text-sm uppercase">Thông tin liên lạc</th>
                    <th className="p-5 font-semibold text-slate-600 text-sm uppercase">Địa chỉ</th>
                    <th className="p-5 font-semibold text-slate-600 text-sm uppercase text-right">Thao tác</th>
                  </tr>
                </thead>
                <tbody className="divide-y divide-slate-100">
                  {users
                    .filter(u => u.name.toLowerCase().includes(searchTerm.toLowerCase()) || u.email.includes(searchTerm))
                    .map((user) => (
                    <tr key={user.id} className="hover:bg-slate-50/50 transition-colors group">
                      <td className="p-5">
                        <div className="font-bold text-slate-800 group-hover:text-blue-600 transition-colors">{user.name}</div>
                      </td>
                      <td className="p-5 text-slate-600">{user.dob}</td>
                      <td className="p-5">
                        <div className="text-sm font-semibold text-slate-700">{user.email}</div>
                        <div className="text-xs text-slate-500 mt-1">{user.phone}</div>
                      </td>
                      <td className="p-5 text-sm text-slate-600 max-w-[200px] truncate">
                        {user.address}
                      </td>
                      <td className="p-5 text-right">
                        <div className="flex justify-end gap-3">
                          <button className="p-2 bg-slate-100 text-slate-600 rounded-lg hover:bg-blue-100 hover:text-blue-600 transition-all">
                            <Edit size={16} />
                          </button>
                          <button 
                            onClick={() => deleteUser(user.id)}
                            className="p-2 bg-slate-100 text-slate-600 rounded-lg hover:bg-red-100 hover:text-red-600 transition-all"
                          >
                            <Trash2 size={16} />
                          </button>
                        </div>
                      </td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
          </div>

        </div>
  );
}