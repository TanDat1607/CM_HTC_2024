import { Route, Routes } from "react-router-dom";

// pages - home
import Home from "../pages/Home/Home.jsx";
//pages - settings
import Setting from "../pages/Setting/Setting.jsx";

// pages - timewaveform
import TimeWaveForm from "../pages/Vibration/TimeWaveForm/TimeWaveForm.jsx";
// pages - Vibration - fft
import FFTAcc from "../pages/Vibration/FFT/FFTAcc.jsx";
import FFTVel from "../pages/Vibration/FFT/FFTVel.jsx";
import FFTEnv from "../pages/Vibration/FFT/FFTEnv.jsx";
// pages - Vibration - historian
import HisAcc from "../pages/Vibration/Historian/HisAcc.jsx";
import HisVel from "../pages/Vibration/Historian/HisVel.jsx";
import HisEnv from "../pages/Vibration/Historian/HisEnv.jsx";
// pages - vibration - waterfall
import WatAcc from "../pages/Vibration/Waterfall/WatAcc.jsx";
import WatVel from "../pages/Vibration/Waterfall/WatVel.jsx";
import WatEnv from "../pages/Vibration/Waterfall/WatEnv.jsx";
// pages - power - timewaveform
import TimeWaveFormCur from "../pages/Power/TimeWaveForm/TimeWaveFormCur.jsx";
import TimeWaveFormVol from "../pages/Power/TimeWaveForm/TimeWaveFormVol.jsx";
// pages - power - fft
import FFTCur from "../pages/Power/FFT/FFTCur.jsx";
import FFTVol from "../pages/Power/FFT/FFTVol.jsx";
// pages - power - historian
import HisCur from "../pages/Power/Historian/HisCur.jsx";
import HisVol from "../pages/Power/Historian/HisVol.jsx";
// pages - power - waterfall
import WatCur from "../pages/Power/Watefall/WatCur.jsx";
import WatVol from "../pages/Power/Watefall/WatVol.jsx";
const AppRoutes = () => {
  return (
    <Routes>
      {/* pages - home */}
      <Route path="/" element={<Home />} />
      {/* pages - settings */}
      <Route path="/setting" element={<Setting />} />
      {/* pages - timewaveform */}
      <Route path="/vibration/timewaveform" element={<TimeWaveForm />} />
      {/* pages - Vibration - fft */}
      <Route path="/vibration/fft/acc" element={<FFTAcc />} />
      <Route path="/vibration/fft/env" element={<FFTEnv />} />
      <Route path="/vibration/fft/vel" element={<FFTVel />} />
      {/* pages - Vibration - historian */}
      <Route path="/vibration/historian/acc" element={<HisAcc />} />
      <Route path="/vibration/historian/env" element={<HisEnv />} />
      <Route path="/vibration/historian/vel" element={<HisVel />} />
      {/* pages - vibration - waterfall */}
      <Route path="/vibration/waterfall/acc" element={<WatAcc />} />
      <Route path="/vibration/waterfall/env" element={<WatEnv />} />
      <Route path="/vibration/waterfall/vel" element={<WatVel />} />
      {/* pages - power - timewaveform */}
      <Route path="/power/timewaveform/cur" element={<TimeWaveFormCur />} />
      <Route path="/power/timewaveform/vol" element={<TimeWaveFormVol />} />
      {/* pages - power - fft */}
      <Route path="/power/fft/cur" element={<FFTCur />} />
      <Route path="/power/fft/vol" element={<FFTVol />} />
      {/* pages - power - historian */}
      <Route path="/power/historian/cur" element={<HisCur />} />
      <Route path="/power/historian/vol" element={<HisVol />} />
      {/* pages - power - waterfall */}
      <Route path="/power/waterfall/cur" element={<WatCur />} />
      <Route path="/power/waterfall/vol" element={<WatVol />} />
    </Routes>
  );
};

export default AppRoutes;
