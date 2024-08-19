import React, { useEffect, useState } from "react";
import axios from "axios";
import Card from "../../../components/Card";
import ChartPlotly from "../../../components/ChartPlotly";

const TimeWaveForm = () => {
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
    <>
      <h1 className="text-center text-4xl font-bold tracking-wider mt-4">
        Timewaveform
      </h1>
      <div className="p-6 grid gap-4 grid-cols-1 md:grid-cols-1 lg:grid-cols-3 md:gap-8 lg:gap-12">
        <div className="flex justify-center">
          <Card title="Temperature" value={75} unit="°C" />
        </div>
        <div className="flex justify-center">
          <Card title="Speed" value={1200} unit="RPM" />
        </div>
        <div className="flex justify-center">
          <Card title="Frequency" value={60} unit="Hz" />
        </div>
      </div>
      <div>
        <ChartPlotly
          valueX={chartData.valueX}
          valueY={chartData.valueY}
          title={"Time Waveform"}
          nameX={"Time (s)"}
          nameY={"Amplitude (mm/s)"}
        />
      </div>
    </>
  );
};

export default TimeWaveForm;
