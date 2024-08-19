import ChartFFT from "../../../components/ChartFFT";

const FFTAcc = () => {
  const valueX = [0, 1, 2, 3, 4, 5];
  const valueY = [10, 20, 30, 40, 50, 60];
  const nameLine = "Example Line";
  const title = "Example Line Chart";
  const nameX = "Time (s)";
  const nameY = "Value";
  return (
    <div>
      <h1>Chart Acceleration</h1>
      <ChartFFT
        valueX={valueX}
        valueY={valueY}
        nameLine={nameLine}
        title={title}
        nameX={nameX}
        nameY={nameY}
      />
    </div>
  );
};

export default FFTAcc;
