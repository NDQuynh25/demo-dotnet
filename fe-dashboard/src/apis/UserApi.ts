
import type { User } from '../types/user';
import ApiService from './ApiService';


const userApi = new ApiService('users') as any;

export const userService = {
  
  getUserById: async (id: number) => {
    return await userApi.get(`${id}`);
  },
  getUsers: async (page: number, limit: number, search: string = '') => {
    return await userApi.get(`?page=${page}&pageSize=${limit}&search=${search}`);
  },
  createUser: async (data: User) => {
    return await userApi.post('', data);
  },
  updateUser: async (id: number, data: User) => {
    return await userApi.put(`${id}`, data);
  },
  deleteUser: async (id: number) => {
    return await userApi.delete(`${id}`);
  }

};