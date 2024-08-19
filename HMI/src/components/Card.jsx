import { useState } from "react";

const Card = ({ title, value, unit }) => {
  // State để quản lý giá trị cài đặt threshold
  const [threshold, setThreshold] = useState("");

  // Xác định màu nền dựa trên so sánh với threshold
  const isOverThreshold = parseFloat(value) > parseFloat(threshold || 0);
  const bgColor = isOverThreshold ? "bg-red-100" : "bg-green-100";

  // Hàm xử lý khi người dùng nhập giá trị threshold
  const handleThresholdChange = (e) => {
    const newValue = e.target.value.replace(/^0+(?!\.|$)/, ""); // Loại bỏ số 0 ở đầu
    setThreshold(newValue);
  };

  return (
    <div
      className={`p-2 rounded-lg shadow-boxContainer md:max-w-md max-w-sm w-full ${bgColor}`}
    >
      {/* Title */}
      <div className="text-center text-md md:text-lg lg:text-xl font-medium text-gray-600">
        {title}
      </div>

      {/* Value Display */}
      <div className="flex justify-center items-baseline mt-2">
        <div className="text-2xl md:text-3xl lg:text-4xl font-semibold text-gray-800">
          {parseFloat(value).toFixed(3)}
        </div>
        <div className="text-sm md:text-base lg:text-lg font-medium text-gray-600 ml-1">
          {unit}
        </div>
      </div>

      {/* Threshold Input */}
      <div className="flex justify-center items-center mt-4">
        <label className="text-sm md:text-md lg:text-lg text-gray-600 mr-2">
          Set Threshold:
        </label>
        <input
          type="number"
          value={threshold}
          onChange={handleThresholdChange}
          className="border border-gray-300 rounded p-1 w-20 text-sm text-gray-700"
        />
        <span className="text-sm md:text-md lg:text-lg text-gray-600 ml-1">
          {unit}
        </span>
      </div>
    </div>
  );
};

export default Card;
