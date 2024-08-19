import Card from "../../components/Card";

const Home = () => {
  return (
    <>
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
    </>
  );
};

export default Home;
