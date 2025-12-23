/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./Components/**/*.{razor,html,cshtml}",
    "./wwwroot/**/*.html"
  ],
  theme: {
    extend: {
      colors: {
        primary: '#10b981',
        danger: '#ef4444',
        info: '#3b82f6',
      },
    },
  },
  plugins: [],
}
