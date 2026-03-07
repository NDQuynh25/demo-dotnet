import type { Auth } from '../types/auth';
import ApiService from './ApiService';


const authApi = new ApiService('auth') as any;

export const authService = {
  
  login: async (data: Auth) => {
    
    return await authApi.post('login', data);
  },


  logout: async () => {
    return await authApi.post('logout');
  },
  
  me: async () => {

    return await authApi.get('me');
  }
};