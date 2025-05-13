import React from 'react';
import styles from './DropDown.module.scss';
import type { Genre } from '../types/movieTypes';
import DropDownAutocomplete from '../../../shared/components/DropDownAutocomplete';

interface GenreFilterDropdownProps {
  genres: Genre[];
  onSelect: (selectedGenre: Genre | null) => void;
  loading?: boolean;
  defaultValue?: Genre;
}

const GenreFilterDropdown: React.FC<GenreFilterDropdownProps> = ({ 
  genres, onSelect, loading = false, defaultValue 
}) => {
  return (
    <div className={styles.dropdownontainer}>
      <DropDownAutocomplete
        options={genres}
        getOptionLabel={(option) => option.name}
        isOptionEqualToValue={(option, value) => option.id === value.id}
        onSelect={onSelect}
        loading={loading}
        defaultValue={defaultValue}
        label="Select Genre"
      />
    </div>
  );
};

export default GenreFilterDropdown;
