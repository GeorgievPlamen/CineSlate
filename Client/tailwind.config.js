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
        primary: '#8B80F9',
        secondary: '#1e293b',
        secondaryLight: '#cbd5e1',
        error: '#ef4444',
        placeholderGrey: '#475569',
      },
    },
  },
  plugins: [],
};
