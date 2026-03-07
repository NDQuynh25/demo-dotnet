import React from 'react';
import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import * as z from 'zod';
import { X, User, Mail, Calendar, Phone, MapPin } from 'lucide-react';
import { userService } from '../../../apis/UserApi';

// 1. Schema chuẩn theo Type của bạn
const userSchema = z.object({
  fullName: z.string().min(2, "Họ tên quá ngắn"),
  email: z.string().email("Email không hợp lệ"),
  dateOfBirth: z.string().min(1, "Vui lòng chọn ngày sinh"),
  phoneNumber: z.string().regex(/^[0-9]{10,11}$/, "Số điện thoại phải từ 10-11 số"),
  address: z.string().min(5, "Địa chỉ quá ngắn"),
});

export type UserFormValues = z.infer<typeof userSchema>;

interface User {
  id?: number;
  fullName: string;
  email: string;
  dateOfBirth: string;
  phoneNumber: string;
  address: string;
}
interface UserFormProps {
  initialData?: User;
  fetchUsers: () => Promise<void>;
  onClose: () => void;
  isEdit?: boolean;
}

export const UserForm: React.FC<UserFormProps> = ({ initialData, fetchUsers, onClose, isEdit = false }) => {
  const { register, handleSubmit, formState: { errors, isSubmitting } } = useForm<UserFormValues>({
    resolver: zodResolver(userSchema),
    defaultValues: initialData || {
      fullName: '', email: '', dateOfBirth: '', phoneNumber: '', address: ''
    },
  });

  const handleSubmitForm = async (data: UserFormValues) => {
    console.log(data);
    try {
      if(isEdit && initialData?.id) {
        await userService.updateUser(initialData.id, data);
      } else {
        await userService.createUser(data);
      }
    } catch (error) {
      console.error('Error submitting form:', error);
    } finally {
      onClose();
      await fetchUsers();
      
    }
  }

 return (
    <div className="fixed inset-0 z-[100] flex items-center justify-center bg-slate-900/50 backdrop-blur-sm p-4 animate-in fade-in duration-300">
      <div className="w-full max-w-lg bg-white rounded-[32px] shadow-2xl flex flex-col animate-in zoom-in-95 duration-300 max-h-[95vh] overflow-hidden">
        
        {/* Header */}
        <div className="p-6 border-b border-slate-100 flex justify-between items-center bg-slate-50/50">
          <h2 className="text-xl font-bold text-slate-800">
            {initialData ? 'Cập nhật nhân sự' : 'Thêm nhân sự mới'}
          </h2>
          <button onClick={onClose} className="p-2 hover:bg-white rounded-xl transition-all text-slate-400 hover:text-red-500">
            <X size={20} />
          </button>
        </div>

        {/* Form Body */}
        <form onSubmit={handleSubmit(handleSubmitForm)} className="flex-1 overflow-y-auto p-8 space-y-5">
          <div className="space-y-1.5">
            <label className="text-sm font-bold text-slate-700 flex items-center gap-2"><User size={14} className="text-blue-500"/> Họ và tên</label>
            <input {...register('fullName')} className={`w-full px-4 py-3 rounded-2xl bg-slate-50 border ${errors.fullName ? 'border-red-500' : 'border-slate-200'} focus:border-blue-500 focus:bg-white outline-none transition-all text-sm`} placeholder="Nguyễn Văn A" />
            {errors.fullName && <p className="text-red-500 text-[11px] font-medium">{errors.fullName.message}</p>}
          </div>

          <div className="grid grid-cols-2 gap-4">
            <div className="space-y-1.5">
              <label className="text-sm font-bold text-slate-700 flex items-center gap-2"><Calendar size={14} className="text-blue-500"/> Ngày sinh</label>
              <input type="date" {...register('dateOfBirth')} className="w-full px-4 py-3 rounded-2xl bg-slate-50 border border-slate-200 focus:border-blue-500 focus:bg-white outline-none transition-all text-sm" />
            </div>
            <div className="space-y-1.5">
              <label className="text-sm font-bold text-slate-700 flex items-center gap-2"><Phone size={14} className="text-blue-500"/> Số điện thoại</label>
              <input {...register('phoneNumber')} className={`w-full px-4 py-3 rounded-2xl bg-slate-50 border ${errors.phoneNumber ? 'border-red-500' : 'border-slate-200'} focus:border-blue-500 focus:bg-white outline-none transition-all text-sm`} placeholder="09xxx" />
            </div>
          </div>

          <div className="space-y-1.5">
            <label className="text-sm font-bold text-slate-700 flex items-center gap-2"><Mail size={14} className="text-blue-500"/> Email liên hệ</label>
            <input {...register('email')} className={`w-full px-4 py-3 rounded-2xl bg-slate-50 border ${errors.email ? 'border-red-500' : 'border-slate-200'} focus:border-blue-500 focus:bg-white outline-none transition-all text-sm`} placeholder="example@gmail.com" />
          </div>

          <div className="space-y-1.5">
            <label className="text-sm font-bold text-slate-700 flex items-center gap-2"><MapPin size={14} className="text-blue-500"/> Địa chỉ cư trú</label>
            <textarea {...register('address')} rows={3} className="w-full px-4 py-3 rounded-2xl bg-slate-50 border border-slate-200 focus:border-blue-500 focus:bg-white outline-none transition-all text-sm" placeholder="Số nhà, tên đường..." />
          </div>
        </form>

        {/* Footer */}
        <div className="p-6 border-t border-slate-100 bg-slate-50/50 flex gap-3">
          <button type="button" onClick={onClose} className="flex-1 py-3.5 rounded-2xl border border-slate-200 font-bold text-slate-600 hover:bg-white transition-all text-sm">Hủy bỏ</button>
          <button onClick={handleSubmit(handleSubmitForm)} disabled={isSubmitting} className="flex-[2] py-3.5 rounded-2xl bg-blue-600 text-white font-bold hover:bg-blue-700 shadow-lg shadow-blue-200 transition-all active:scale-95 text-sm">
            {isSubmitting ? 'Đang lưu...' : 'Lưu thông tin'}
          </button>
        </div>
      </div>
    </div>
  );
};