/* eslint-disable react/prop-types */
import React, { useEffect, useRef, useState } from "react";
import { Chart } from "chart.js/auto";

const ChartFFT = ({ valueX, valueY, nameLine, title, nameX, nameY }) => {
  const chartRef = useRef(null);
  //   useEffect(() => {
  //     const data = {
  //       labels: valueX,
  //       datasets: [
  //         {
  //           label: nameLine,
  //           data: valueY,
  //           fill: false,
  //           borderColor: "rgb(75, 192, 192)",
  //           tension: 0.1,
  //         },
  //       ],
  //     };
  //     const options = {
  //       scales: {
  //         x: {
  //           title: {
  //             display: true,
  //             text: nameX,
  //           },
  //         },
  //         y: {
  //           title: {
  //             display: true,
  //             text: nameY,
  //           },
  //         },
  //       },
  //     };
  //     const ctx = chartRef.current.getContext("2d");
  //     const chartInstance = new Chart(ctx, {
  //       type: "line",
  //       data: data,
  //       options: options,
  //     });
  //     // Cleanup khi component bị unmount
  //     return () => {
  //       chartInstance.destroy();
  //     };
  //   }, [valueX, valueY, nameLine, title, nameX, nameY]);

  const data = {
    labels: valueX,
    datasets: [
      {
        label: nameLine,
        data: valueY,
        fill: false,
        borderColor: "rgb(75, 192, 192)",
        tension: 0.1,
      },
    ],
  };
  const options = {
    scales: {
      x: {
        title: {
          display: true,
          text: nameX,
        },
      },
      y: {
        title: {
          display: true,
          text: nameY,
        },
      },
    },
  };

  useEffect(() => {
    const ctx = chartRef.current.getContext("2d");
    const chartInstance = new Chart(ctx, {
      type: "line",
      data: data,
      options: options,
    });
    // Cleanup khi component bị unmount
    return () => {
      chartInstance.destroy();
    };
  }, [valueX, valueY, nameLine, title, nameX, nameY]);

  return <canvas ref={chartRef}></canvas>;
};

export default ChartFFT;
