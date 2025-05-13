import React from 'react';
import {
  Card,
  CardContent,
  Typography,
  Box
} from '@mui/material';
import MovieImageComponent from './MovieImageComponent';
import MovieDetailsComponent from './MovieDetailsComponent';
import styles from './MovieViewDetailsComponent.module.scss'; 

interface MovieViewCardComponentProps {
  imageUrl: string;
  description: string;
  actors: string[];
}

const MovieViewDetailsComponent: React.FC<MovieViewCardComponentProps> = ({ description, imageUrl, actors }) => {
  return (
    <Card className={styles.card}>
      <Box className={styles.imageBox}>
        <MovieImageComponent imageUrl={imageUrl} />
      </Box>
      <Box className={styles.contentBox}>
        <MovieDetailsComponent description={description} />
        <CardContent>
          <Typography variant="h6">Actors</Typography>
          {actors.length > 0 ? (
            <ul className={styles.actorsList}>
              {actors.map((actor, index) => (
                <li key={index} className={styles.actorItem}>
                  <Typography variant="body2" className={styles.actorName}>{actor}</Typography>
                </li>
              ))}
            </ul>
          ) : (
            <Typography variant="body2">No actors available.</Typography>
          )}
        </CardContent>
      </Box>
    </Card>
  );
};

export default MovieViewDetailsComponent;
