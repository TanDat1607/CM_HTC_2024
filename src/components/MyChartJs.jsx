// import React, { useEffect, useRef } from "react";
// import Chart from "chart.js/auto";

// const MyChartJs = ({ data, title, nameAxisX, nameAxisY }) => {
//   const canvasRef = useRef(null);

//   useEffect(() => {
//     const ctx = canvasRef.current.getContext("2d");
//     const chartInstance = new Chart(ctx, {
//       type: "line",
//       data: data,
//       options: {
//         responsive: true,
//         plugins: {
//           legend: {
//             position: "top",
//           },
//           title: {
//             display: true,
//             text: title,
//           },
//         },
//         scales: {
//           x: {
//             title: {
//               display: true,
//               text: nameAxisX,
//             },
//           },
//           y: {
//             title: {
//               display: true,
//               text: nameAxisY,
//             },
//           },
//         },
//       },
//     });

//     return () => {
//       chartInstance.destroy();
//     };
//   }, [data, title, nameAxisX, nameAxisY]);

//   return <canvas ref={canvasRef}></canvas>;
// };

// export default MyChartJs;
import React, { useRef, useEffect } from "react";
import { Chart } from "chart.js";

const MyChartJs = ({
  valueX,
  valueY1,
  valueY2,
  valueY3,
  title,
  xAxisName,
  yAxisName,
  nameLine1,
  nameLine2,
  nameLine3,
}) => {
  const chartRef = useRef(null);

  const ctx = chartRef.current?.getContext("2d");

  useEffect(() => {
    const ctx = chartRef.current.getContext("2d");

    const datasets = [];
    if (valueY1 && nameLine1) {
      datasets.push({
        label: nameLine1,
        data: valueY1,
        borderWidth: 3,
        fill: false,
        pointRadius: 0,
      });
    }
    if (valueY2 && nameLine2) {
      datasets.push({
        label: nameLine2,
        data: valueY2,
        borderWidth: 3,
        fill: false,
        pointRadius: 0,
      });
    }
    if (valueY3 && nameLine3) {
      datasets.push({
        label: nameLine3,
        data: valueY3,
        borderWidth: 3,
        fill: false,
        pointRadius: 0,
      });
    }

    const data = {
      labels: valueX,
      datasets: datasets,
    };

    const options = {
      scales: {
        x: {
          title: {
            display: true,
            text: xAxisName,
            font: {
              size: 16,
            },
          },
          ticks: {
            maxTicksLimit: 10,
            font: {
              size: 14,
            },
          },
        },
        y: {
          title: {
            display: true,
            text: yAxisName,
            font: {
              size: 16,
            },
          },
        },
      },
      plugins: {
        tooltip: {
          mode: "nearest",
          intersect: false,
        },
        title: {
          display: true,
          text: title,
          font: {
            size: 20,
          },
        },
      },
      layout: {
        padding: {
          left: 10,
          bottom: 10,
          top: 10,
          right: 10,
        },
      },
    };

    const chartInstance = new Chart(ctx, {
      type: "line",
      data: data,
      options: options,
    });

    return () => {
      chartInstance.destroy();
    };
  }, [
    valueX,
    valueY1,
    valueY2,
    valueY3,
    title,
    nameLine1,
    nameLine2,
    nameLine3,
    xAxisName,
    yAxisName,
  ]);

  return <canvas ref={chartRef}></canvas>;
};

export default MyChartJs;
