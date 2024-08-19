/** @type {import('tailwindcss').Config} */
export default {
  content: ["./src/**/*.{js,jsx,ts,tsx}"],
  theme: {
    extend: {
      boxShadow: {
        boxContainer: "0 2px 5px 2px rgba(0,0,0,0.3)",
      },
    },
  },
  plugins: [require("daisyui")],
};
