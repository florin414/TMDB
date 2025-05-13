import React from 'react';
import {
    Card,
    CardHeader,
} from '@mui/material';
import MovieImageComponent from './MovieImageComponent';
import MovieDetailsComponent from './MovieDetailsComponent';
import { useNavigate } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import { setSelectedMovie } from '../selectedMovieSlice';

interface MovieViewCardComponentProps {
    imageUrl: string;
    title: string;
    description: string;
    movieId: number;
}

const MovieViewCardComponent: React.FC<MovieViewCardComponentProps> = ({
    imageUrl,
    title,
    description,
    movieId,
}) => {
    const dispatch = useDispatch();
    const navigate = useNavigate();

    const handleClick = () => {
        dispatch(setSelectedMovie({
            imageUrl,
            title,
            description,
            movieId
        }));
        navigate(`${movieId}/details`);
    };

    return (
        <Card
            sx={{
                display: 'block',
                marginBottom: 2,
                maxWidth: 345,
            }}
        >
            <>
                <CardHeader title={title} />
                <div onClick={handleClick}>
                    <MovieImageComponent imageUrl={imageUrl} />
                </div>
                <MovieDetailsComponent description={description} />
            </>
        </Card>
    );
};

export default MovieViewCardComponent;
