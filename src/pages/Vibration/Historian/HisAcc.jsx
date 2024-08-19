import { useEffect, useState } from "react";
import Plot from "react-plotly.js";
import axios from "axios";

const HisAcc = () => {
  const [data, setData] = useState({ x: [], y: [] });

  useEffect(() => {
    const fetchData = async () => {
      try {
        // Sử dụng API miễn phí từ JSONPlaceholder
        const response = await axios.get(
          "http://192.168.1.62:8181/api/cm/Acc/1/2024-05-02%2006%3A00"
        );
        const yData = response.data;

        const xData = Array.from({ length: 2049 }, (_, i) => (i + 1) * 2.44);
        setData({
          x: xData,
          y: yData,
        });
      } catch (error) {
        console.error(error);
      }
    };
    fetchData();

    const intervalId = setInterval(fetchData, 2000);

    // Cleanup interval on component unmount
    return () => clearInterval(intervalId);
  }, []);

  return (
    <div className="container mx-auto border-2 z-10">
      <Plot
        data={[
          {
            x: data.x,
            y: data.y,
            type: "scatter",
            mode: "lines",
            marker: { color: "red" },
          },
        ]}
        layout={{ title: "Scatter Plot Example" }}
      />
    </div>
  );
};

export default HisAcc;
