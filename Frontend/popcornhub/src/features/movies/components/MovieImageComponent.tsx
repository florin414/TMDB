import React from 'react';
import CardMedia from '@mui/material/CardMedia';

interface MovieImageComponentProps {
  imageUrl: string;
}

const MovieImageComponent: React.FC<MovieImageComponentProps> = ({ imageUrl }) => {
  return <CardMedia component="img" height="194" image={imageUrl} alt="Movie" />;
};

export default MovieImageComponent;
