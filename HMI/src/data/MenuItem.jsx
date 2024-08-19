const MenuItem = [
  {
    title: "Home",
    link: "/",
  },
  {
    title: "Vibration",
    submenus: [
      {
        title: "TimeWaveForm",
        link: "/vibration/timewaveform",
      },
      {
        title: "FFT",
        submenus: [
          {
            title: "Acceleration",
            link: "/vibration/fft/acc",
          },
          {
            title: "Velocity",
            link: "/vibration/fft/vel",
          },
          {
            title: "Envelope",
            link: "/vibration/fft/env",
          },
        ],
      },
      {
        title: "Historian",
        submenus: [
          {
            title: "Acceleration",
            link: "/vibration/historian/acc",
          },
          {
            title: "Velocity",
            link: "/vibration/historian/vel",
          },
          {
            title: "Envelope",
            link: "/vibration/historian/env",
          },
        ],
      },
      {
        title: "Watefall",
        submenus: [
          {
            title: "Acceleration",
            link: "/vibration/waterfall/acc",
          },
          {
            title: "Velocity",
            link: "/vibration/waterfall/vel",
          },
          {
            title: "Envelope",
            link: "/vibration/waterfall/env",
          },
        ],
      },
    ],
  },
  {
    title: "Power",
    submenus: [
      {
        title: "TimeWaveForm",
        submenus: [
          {
            title: "Current",
            link: "/power/timewaveform/cur",
          },
          {
            title: "Voltage",
            link: "/power/timewaveform/vol",
          },
        ],
      },
      {
        title: "FFT",
        submenus: [
          {
            title: "Current",
            link: "/power/fft/cur",
          },
          {
            title: "Voltage",
            link: "/power/fft/vol",
          },
        ],
      },
      {
        title: "Historian",
        submenus: [
          {
            title: "Current",
            link: "/power/historian/cur",
          },
          {
            title: "Voltage",
            link: "/power/historian/vol",
          },
        ],
      },
      {
        title: "Waterfall",
        submenus: [
          {
            title: "Current",
            link: "/power/waterfall/cur",
          },
          {
            title: "Voltage",
            link: "/power/waterfall/vol",
          },
        ],
      },
    ],
  },
  {
    title: "Setting",
    link: "/setting",
  },
];

export default MenuItem;
