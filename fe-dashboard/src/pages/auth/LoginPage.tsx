import React from 'react';
import { useNavigate } from 'react-router-dom';
import { ShieldCheck, ArrowRight } from 'lucide-react';
import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import * as z from 'zod';
import { authService } from '../../apis/AuthApi';


const loginSchema = z.object({
  email: z.string().email({ message: "Email không đúng định dạng" }),
  password: z.string().min(1, { message: "Mật khẩu không được bỏ trống" }),
});


type LoginFormValues = z.infer<typeof loginSchema>;

const LoginPage: React.FC = () => {
  const navigate = useNavigate();


  const {
    register,
    handleSubmit,
    formState: { errors, isSubmitting },
  } = useForm<LoginFormValues>({
    resolver: zodResolver(loginSchema),
  });


  const onSubmit = async (data: LoginFormValues) => {
    try {
      await authService.login(data);
      navigate('/users');
    } catch (error) {
      console.error("Đăng nhập thất bại", error);
      
    }
  };

  return (
    <div className="fixed inset-0 bg-[#f8fafc] flex items-center justify-center p-4">
      <div className="w-full max-w-[450px] bg-white rounded-[32px] shadow-2xl shadow-blue-900/5 border border-slate-100 p-10">
        <div className="flex flex-col">
          <div className="flex items-center gap-2 mb-8">
            <div className="bg-blue-600 p-1.5 rounded-lg text-white">
              <ShieldCheck size={20} />
            </div>
            <span className="text-lg font-bold text-slate-800 tracking-tight">AdminPro</span>
          </div>

          <h1 className="text-2xl font-bold text-slate-900 mb-5">Đăng nhập</h1>

          <form onSubmit={handleSubmit(onSubmit)} className="space-y-4 flex flex-col gap-2">
            {/* Email Field */}
            <div>
              <input
                {...register('email')}
                type="email"
                placeholder="Email"
                className={`w-full px-4 py-3.5 rounded-2xl bg-slate-50 border ${
                  errors.email ? 'border-red-500' : 'border-slate-100'
                } focus:border-blue-500 focus:bg-white focus:ring-4 focus:ring-blue-500/10 outline-none transition-all text-sm`}
              />
              {errors.email && (
                <p className="text-red-500 text-xs mt-1 ml-2">{errors.email.message}</p>
              )}
            </div>

            {/* Password Field */}
            <div>
              <input
                {...register('password')}
                type="password"
                placeholder="Mật khẩu"
                className={`w-full px-4 py-3.5 rounded-2xl bg-slate-50 border ${
                  errors.password ? 'border-red-500' : 'border-slate-100'
                } focus:border-blue-500 focus:bg-white focus:ring-4 focus:ring-blue-500/10 outline-none transition-all text-sm`}
              />
              {errors.password && (
                <p className="text-red-500 text-xs mt-1 ml-2">{errors.password.message}</p>
              )}
            </div>

            <button 
              type="submit"
              disabled={isSubmitting}
              className="w-full mt-4 bg-blue-600 hover:bg-blue-700 disabled:bg-blue-400 text-white font-bold py-3.5 rounded-2xl flex items-center justify-center gap-2 transition-all active:scale-[0.97] shadow-lg shadow-blue-200"
            >
              {isSubmitting ? "Đang xử lý..." : "Vào hệ thống"}
              {!isSubmitting && <ArrowRight size={18} />}
            </button>
          </form>

          <div className="mt-8 text-center">
             <button className="text-sm text-slate-400 hover:text-blue-600 transition-colors font-medium">
              {/* Quên mật khẩu? */}
            </button>
          </div>
        </div>
      </div>
    </div>
  );
};

export default LoginPage;