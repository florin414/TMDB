import React from 'react';
import { Outlet } from 'react-router-dom';
import TopNavbar from '../shared/components/TopNavbar';

const MoviesLayout: React.FC = () => {
  return (
    <div>
      <TopNavbar />
      <main>
        <Outlet />
      </main>
    </div>
  );
};

export default MoviesLayout;
