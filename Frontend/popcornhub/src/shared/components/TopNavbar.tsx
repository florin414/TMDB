import { useNavigate } from 'react-router-dom';
import styles from './TopNavbar.module.scss';
import { Toolbar, Typography } from '@mui/material';

const TopNavbar: React.FC = () => {
    const navigate = useNavigate();

    return (
        <Toolbar variant="dense" className={styles.topNavbar}>
            <div onClick={() => navigate(`/movies`)}>
                <Typography variant="h6" color="inherit" sx={{ mr: 2 , cursor: 'grab'}}>
                    Movies
                </Typography>
            </div>
            <div onClick={() => navigate(`/auth/signup`)}>
                <Typography variant="h6" color="inherit" sx={{ mr: 2 , cursor: 'grab'}}>
                    Authentication
                </Typography>
            </div>
        </Toolbar>
    );
};

export default TopNavbar;
