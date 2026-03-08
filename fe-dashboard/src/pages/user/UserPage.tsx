import { useEffect, useState } from 'react';
import { UserPlus, Search, Mail, Phone, Calendar, Edit, Trash2, RotateCcw } from 'lucide-react';
import { UserForm } from './components/UserForm';
import React, { useRef,  } from 'react';
import {  AlertTriangle } from 'lucide-react';
import { userService } from '../../apis/UserApi';
import { Pagination } from '../../components/common/Pagination';
import { DataTable, type Column } from '../../components/common/DataTable';
import { toast } from 'react-toastify';

export interface User {
  id: number;
  fullName: string;
  dateOfBirth: string;
  email: string;
  phoneNumber: string;
  address: string;
}

export default function UserPage() {
  const [users, setUsers] = useState<User[]>([]);
  const [loading, setLoading] = useState(false);
  
 
  const [inputValue, setInputValue] = useState(''); 
  const [searchTerm, setSearchTerm] = useState(''); 
  const [pagination, setPagination] = useState({
    page: 1, pageSize: 5, totalItems: 0, totalPages: 0
  });

  const [isFormOpen, setIsFormOpen] = useState(false);
  const [selectedUser, setSelectedUser] = useState<User | null>(null);

  const fetchUsers = async (page: number, search: string) => {
    setLoading(true);
    try {
      const res = await userService.getUsers(page, pagination.pageSize, search);
      setUsers(res.data?.elements || []);
      setPagination(prev => ({
        ...prev,
        page: res.data?.page || 1,
        pageSize: res.data?.pageSize || 5,
        totalItems: res.data?.totalItems || 0,
        totalPages: res.data?.totalPages || 0

      }));
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchUsers(pagination.page, searchTerm);
  }, [pagination.page, searchTerm]);

  const handleSearch = (e: React.FormEvent) => {
    e.preventDefault();
    setSearchTerm(inputValue);
    setPagination(p => ({ ...p, page: 1 }));
  };

  const handleReset = () => {
    setInputValue('');
    setSearchTerm('');
    setPagination(p => ({ ...p, page: 1 }));
  };

  const columns: Column<User>[] = [
    {
      header: "Người dùng",
      render: (u: User) => (
        <div>
          <div className="font-bold text-slate-800 group-hover:text-blue-600 transition-colors">{u.fullName}</div>
          <div className="text-[10px] text-slate-400 font-bold uppercase mt-0.5 tracking-tighter">ID: #{u.id}</div>
        </div>
      )
    },
    {
      header: "Liên hệ",
      render: (u: User) => (
        <div className="space-y-1">
          <div className="text-sm font-medium text-slate-700 flex items-center gap-2"><Mail size={14} className="text-slate-300"/>{u.email}</div>
          <div className="text-xs text-slate-500 flex items-center gap-2"><Phone size={14} className="text-slate-300"/>{u.phoneNumber}</div>
        </div>
      )
    },
    {
      header: "Ngày sinh",
      className: "text-center",
      render: (u: User) => <div className="text-sm text-slate-600 font-medium flex items-center justify-center gap-2"><Calendar size={14} className="text-slate-300"/>{u.dateOfBirth}</div>
    },
    {
      header: "Thao tác",
      className: "text-right",
      render: (u: User) => (
        <div className="flex justify-end gap-2">
          <button onClick={() => { setSelectedUser(u); setIsFormOpen(true); }} className="p-2.5 bg-slate-50 text-slate-400 rounded-xl hover:bg-blue-600 hover:text-white transition-all"><Edit size={18} /></button>
          <DeleteConfirm 
            onConfirm={async () => {
              await handleDeleteConfirm(u.id);
            }} 
          />
        </div>
      )
    }
  ];
  const handleDeleteConfirm = async (id: number) => {
    try {
      const res = await userService.deleteUser(id);
      if (res.success) {
        toast.success("Xóa người dùng thành công");
      } else {
        toast.error("Có lỗi xảy ra vui lòng thử lại!");
      }
      await fetchUsers(pagination.page, searchTerm);
    } catch (error) {
      console.error('Error deleting user:', error);
    }
  };

  return (
    <div className="max-w-7xl mx-auto p-6 min-h-screen bg-slate-50/20">
      <div className="flex flex-col md:flex-row justify-between items-center mb-10 gap-4">
        <div>
          <h1 className="text-4xl font-black text-slate-900 tracking-tight">Quản lý Nhân sự</h1>
          <p className="text-slate-500 mt-1 font-medium">Hệ thống lưu trữ dữ liệu tập trung</p>
        </div>
        <button onClick={() => { setSelectedUser(null); setIsFormOpen(true); }} className="w-full md:w-auto bg-blue-600 text-white px-8 py-4 rounded-2xl flex items-center justify-center gap-2 font-bold shadow-xl shadow-blue-100 hover:bg-blue-700 transition-all active:scale-95">
          <UserPlus size={20} /> Thêm thành viên
        </button>
      </div>

    
      <form onSubmit={handleSearch} className="flex flex-col md:flex-row gap-3 mb-8">
        <div className="relative flex-1 group">
          <Search className="absolute left-5 top-1/2 -translate-y-1/2 text-slate-400 group-focus-within:text-blue-500 transition-colors" size={20} />
          <input 
            type="text" 
            value={inputValue}
            placeholder="Nhập tên, email hoặc số điện thoại..." 
            className="w-full pl-14 pr-6 py-4.5 h-14 bg-white rounded-3xl border border-slate-100 outline-none focus:ring-4 focus:ring-blue-500/5 transition-all shadow-sm"
            onChange={(e) => setInputValue(e.target.value)}
          />
        </div>
        <div className="flex gap-2">
            <button type="submit" className="flex-1 md:flex-none bg-slate-900 text-white px-8 py-4.5 rounded-3xl font-bold hover:bg-black transition-all shadow-lg flex items-center justify-center gap-2">
                <Search size={18} /> Tìm kiếm
            </button>
            <button type="button" onClick={handleReset} className="flex justify-center items-center p-4.5 h-14 w-14 bg-white text-slate-400 rounded-3xl border border-slate-100 hover:text-red-500 transition-all shadow-sm">
                <RotateCcw size={24} />
            </button>
        </div>
      </form>

      <DataTable columns={columns} data={users} isLoading={loading} />

      <Pagination 
        currentPage={pagination.page}
        totalPages={pagination.totalPages}
        totalItems={pagination.totalItems}
        pageSize={pagination.pageSize}
        onPageChange={(page: number) => setPagination(p => ({ ...p, page }))}
      />

      {isFormOpen && (
        <UserForm 
          initialData={selectedUser || undefined} 
          fetchUsers={() => fetchUsers(pagination.page, searchTerm)}
          onClose={() => setIsFormOpen(false)}
          isEdit={!!selectedUser}
        />
      )}
    </div>
  );
}



interface DeleteConfirmProps {
  onConfirm: () => void;
  title?: string;
}

export const DeleteConfirm: React.FC<DeleteConfirmProps> = ({ onConfirm}) => {
  const [isOpen, setIsOpen] = useState(false);
  const popoverRef = useRef<HTMLDivElement>(null);

  
  useEffect(() => {
    const handleClickOutside = (event: MouseEvent) => {
      if (popoverRef.current && !popoverRef.current.contains(event.target as Node)) {
        setIsOpen(false);
      }
    };
    if (isOpen) document.addEventListener('mousedown', handleClickOutside);
    return () => document.removeEventListener('mousedown', handleClickOutside);
  }, [isOpen]);

  return (
    <div className="relative inline-block" ref={popoverRef}>
      <button
        onClick={() => setIsOpen(!isOpen)}
        className={`p-2.5 rounded-xl transition-all shadow-sm ${
          isOpen ? 'bg-red-600 text-white' : 'bg-slate-50 text-slate-400 hover:bg-red-100 hover:text-red-600'
        }`}
      >
        <Trash2 size={18} />
      </button>

      {isOpen && (
        <div className="absolute top-1/2 right-full mr-3 -translate-y-1/2 w-56 bg-white rounded-2xl shadow-2xl border border-slate-100 p-4 z-[150] animate-in fade-in slide-in-from-right-2 duration-200">
          <div className="flex items-center gap-2 text-red-600 mb-2">
            <AlertTriangle size={16} />
            <span className="text-[11px] font-bold uppercase tracking-wider">Xác nhận xóa?</span>
          </div>
          <p className="text-[10px] text-slate-500 mb-3 leading-relaxed text-left">
            Dữ liệu nhân sự này sẽ bị loại bỏ khỏi hệ thống.
          </p>
          <div className="flex gap-2">
            <button
              onClick={() => setIsOpen(false)}
              className="flex-1 py-2 text-[10px] font-bold text-slate-500 bg-slate-100 rounded-lg hover:bg-slate-200 transition-colors"
            >
              Hủy
            </button>
            <button
              onClick={() => {
                onConfirm();
                setIsOpen(false);
              }}
              className="flex-1 py-2 text-[10px] font-bold text-white bg-red-600 rounded-lg hover:bg-red-700 transition-colors shadow-md shadow-red-100"
            >
              Xác nhận
            </button>
          </div>
          
          
          <div className="absolute top-1/2 left-full -translate-y-1/2 -ml-1.5 w-3 h-3 bg-white border-t border-r border-slate-100 rotate-45"></div>
        </div>
      )}
    </div>
  );
};