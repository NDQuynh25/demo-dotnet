import React from 'react';
import { useLoadingStore } from '../../store/useLoadingStore';
import { PuffLoader } from 'react-spinners'; 

export const Loading: React.FC = () => {
  const isLoading = useLoadingStore((state) => state.isLoading);

  if (!isLoading) return null;

  return (
    <div className="fixed inset-0 z-[9999] flex items-center justify-center">
     
      <div className="absolute inset-0 bg-white backdrop-blur-sm animate-in fade-in duration-300"></div>
      
    
      <div className="relative flex flex-col items-center">
     
        <PuffLoader 
          color="#2563eb" 
          size={80} 
          speedMultiplier={1.5}
        />
        
     
        <span className="mt-4 text-blue-600 font-semibold tracking-[0.3em] uppercase text-xs animate-pulse">
          Loading
        </span>
      </div>
    </div>
  );
};