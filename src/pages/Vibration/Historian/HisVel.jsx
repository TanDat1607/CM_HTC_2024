import React from "react";
import MyChartJs from "../../../components/MyChartJs";
import axios from "axios";
import { useEffect, useState } from "react";

const HisVel = () => {
  const [chartData, setChartData] = useState({ valueX: [], valueY: [] });

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await axios.get(
          "http://192.168.1.62:8181/api/cm/Acc/1/2024-05-02%2006%3A00"
        );
        const valueY = response.data; // Assuming the API returns an array of y-values
        const valueX = Array.from({ length: 2049 }, (_, i) => i * 2.44);
        setChartData({ valueX, valueY });
      } catch (error) {
        console.error("Error fetching data:", error);
      }
    };
    fetchData();
  }, []);
  return (
    <div className="shadow-boxContainer mt-4 rounded-badge overflow-hidden">
      <MyChartJs
        valueX={chartData.valueX}
        valueY1={chartData.valueY}
        title={"Chart Timewaveform"}
        nameLine1={"Velocity"}
        xAxisName={"Frequency (Hz)"}
        yAxisName={"Amplitude (mm/s)"}
        valueY2={[1, 2, 1, 4, 1, 1]}
        nameLine2={"Acceleration"}
        valueY3={[1, 2, 3, 4, 5, 6]}
        nameLine3={"Displacement"}
      />
    </div>
  );
};

export default HisVel;
