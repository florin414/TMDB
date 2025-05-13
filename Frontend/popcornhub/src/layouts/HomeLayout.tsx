import { Outlet } from "react-router-dom";
import TopNavbar from "../shared/components/TopNavbar";

const HomeLayout: React.FC = () => {
    return (
      <div>
        <TopNavbar />
        <main>
          <Outlet />ss
        </main>
      </div>
    );
  };
  
  export default HomeLayout;
  