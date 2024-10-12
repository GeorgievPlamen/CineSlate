/** @type {import('tailwindcss').Config} */
export default {
  content: ['./index.html', './src/**/*.{js,ts,jsx,tsx}'],
  theme: {
    extend: {
      fontFamily: {
        arvo: ['Arvo', 'Georgia'],
        roboto: ['Roboto', 'sans-serif'],
      },
      colors: {
        dark: '#001011',
        whitesmoke: '#F5F5F5',
        indigoTropical: '#8B80F9',
        bluePersian: '#1e293b',
        grayFrench: '#cbd5e1',
        error: '#ef4444',
      },
      boxShadow: {
        light: '0 5px 20px -10px #4f46e5, 0 8px 10px -6px #001011', // light shadow for dark mode
      },
    },
  },
  plugins: [],
};
