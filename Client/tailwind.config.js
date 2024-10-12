/** @type {import('tailwindcss').Config} */
export default {
  content: ['./index.html', './src/**/*.{js,ts,jsx,tsx}'],
  theme: {
    extend: {
      fontFamily: {
        arvo: ['Arvo', 'Georgia'],
        roboto: ['Roboto', 'sans-serif'],
      },
    },
  },
  plugins: [],
};
