import React from 'react';
import { CardContent, Typography } from '@mui/material';

interface MovieDetailsComponentProps {
  description: string;
}

const MovieDetailsComponent: React.FC<MovieDetailsComponentProps> = ({ description }) => {
  return (
    <CardContent>
      <Typography variant="body2" color="text.secondary">
        {description}
      </Typography>
    </CardContent>
  );
};

export default MovieDetailsComponent;
