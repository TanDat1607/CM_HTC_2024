import Plot from "react-plotly.js";

const ChartPlotly = ({ valueX, valueY, title, nameX, nameY }) => {
  const chartData = [
    {
      x: valueX,
      y: valueY,
      type: "scatter",
    },
  ];

  // Khai báo layout bên trong function
  const chartLayout = {
    title: title,
    xaxis: {
      title: nameX,
    },
    yaxis: {
      title: nameY,
    },
    autosize: true,
  };

  return (
    <div className="bg-red-500 mt-2 shadow-boxContainer rounded-3xl overflow-hidden">
      <Plot
        data={chartData} // Sử dụng chartData
        layout={chartLayout} // Sử dụng chartLayout
        style={{ width: "100" }}
      />
    </div>
  );
};

export default ChartPlotly;
