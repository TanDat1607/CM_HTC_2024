import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";

// https://vitejs.dev/config/
export default defineConfig({
  server: {
    host: "192.168.1.41", // Địa chỉ IP của máy bạn
    port: 5050, // Cổng mà bạn muốn server lắng nghe, có thể thay đổi nếu cần
    open: true, // Tùy chọn này để tự động mở trình duyệt khi server khởi chạy
  },
  plugins: [react()],
});
