import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react-swc';
import * as path from 'path';

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [react()],
  preview: {
    port: 3000,
    strictPort: true,
  },
  server: {
    port: 3030,
    strictPort: true,
    host: true,
    //origin:"http://0.0.0.0:3030"
  },
  resolve: {
    alias: {
     '@': path.resolve(__dirname, 'src'),
    }
  }
});
