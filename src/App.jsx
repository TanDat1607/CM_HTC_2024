import AppRoutes from "./routes/routes";
import Header from "./components/Header";
import { BrowserRouter as Router } from "react-router-dom";
function App() {
  return (
    <Router>
      <Header />
      <div className="container mx-auto z-10">
        <AppRoutes />
      </div>
    </Router>
  );
}

export default App;
