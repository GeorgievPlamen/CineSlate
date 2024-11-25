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
        // primary: '#8B80F9',
        primary: {
          DEFAULT: '#8B80F9',
          hover: '#4f46e5',
          active: '#6366f1',
          selected: '#4338ca',
        },
        background: {
          DEFAULT: '#1e293b',
          light: '#cbd5e1',
        },
        error: '#ef4444',
        placeholder: '#475569',
      },
    },
  },
  plugins: [],
};
