import axios from 'axios';

import type { AxiosRequestConfig, Method, ResponseType } from 'axios';
import momentTz from 'moment-timezone';
import { useLoadingStore } from '../store/useLoadingStore';



const CONFIG = {
  ssl: false, // Set to true if using HTTPS
  api_url: import.meta.env.API_URL || 'https://demo-dotnet.onrender.com/api/v1', 
};

const HTTP_METHODS: Method[] = ['get', 'post', 'put', 'delete', 'patch'];

class ApiService {
  private url: string;
  private headers: any;

  // Define dynamic methods that TypeScript recognizes.
  public get!: <T>(url?: string, body?: any, headers?: any, rType?: ResponseType) => Promise<T>;
  public post!: <T>(url?: string, body?: any, headers?: any, rType?: ResponseType) => Promise<T>;
  public put!: <T>(url?: string, body?: any, headers?: any, rType?: ResponseType) => Promise<T>;
  public delete!: <T>(url?: string, body?: any, headers?: any, rType?: ResponseType) => Promise<T>;
  public patch!: <T>(url?: string, body?: any, headers?: any, rType?: ResponseType) => Promise<T>;

  constructor(urlSegment: string | null = null, headers: any = null) {
    this.headers = headers;
    const protocol = CONFIG.ssl ? 'https' : 'http';
    this.url = `${protocol}://${CONFIG.api_url}${urlSegment ? `/${urlSegment}` : ''}`;

    // Initialize dynamic methods.
    HTTP_METHODS.forEach((method) => {
      (this as any)[method.toLowerCase()] = (
        path: string = '',
        body: any = null,
        customHeaders: any = null,
        responseType: ResponseType | null = null
      ) => {
        return this.callApi(method, path, body, customHeaders, responseType);
      };
    });
  }

  private async callApi(
    method: Method,
    path: string = '',
    body: any = null,
    customHeaders: any = null,
    responseType: ResponseType | null = null
  ) {
    useLoadingStore.getState().show();
    console.log('callApi', 'okokkokokok');
    // Save tokens in cookies with httponly.
    const config: AxiosRequestConfig = {
      url: `${this.url}/${path}`,
      method,
      withCredentials: true, // REQUIRED to send/receive HttpOnly Cookies
      headers: {
        'Content-Type': 'application/json',
        'Accept': 'application/json',
        'Timezone': momentTz.tz.guess(),
      
        ...this.headers,
        ...customHeaders,
      },
      responseType: responseType || 'json',
    };

    // Xử lý dữ liệu gửi đi dựa trên method
    if (method.toLowerCase() === 'get') {
      config.params = body; // Params cho query string
    } else {
      config.data = body;   // Body cho POST, PUT, PATCH...
    }

    try {
      const response = await axios(config);
      return response.data;
    } catch (error: any) {
      this.handleError(error);
      throw error;
    } finally {
      useLoadingStore.getState().hide();
    }
  }

  private handleError(error: any) {
    if (error.response) {
      const status = error.response.status;
     
      if (status === 401) {
        console.warn('Session expired, redirecting to login...');
        
      }
    }
    return Promise.reject(error);
  }
}

export default ApiService;