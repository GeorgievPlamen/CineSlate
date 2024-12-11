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
        grey: '#64748b',
        primary: {
          DEFAULT: '#176EBF',
        },
        secondary: {
          DEFAULT: '#90E0EF',
        },
        background: {
          DEFAULT: '#1e293b',
          light: '#cbd5e1',
        },
        error: {
          DEFAULT: '#ef4444',
        },
        placeholder: '#475569',
      },
      backgroundImage: {
        'gradient-radial': 'radial-gradient(var(--tw-gradient-stops))',
      },
    },
  },
  plugins: [require('@tailwindcss/forms')],
};
