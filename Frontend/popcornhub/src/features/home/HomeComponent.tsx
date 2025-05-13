import type React from "react";
import styles from './Home.module.scss';

const HomeComponent: React.FC = () => {
  return (
    <div className={styles.container}>
      <h1 className={styles.heading}>Home</h1>
    </div>
  );
};

export default HomeComponent;
