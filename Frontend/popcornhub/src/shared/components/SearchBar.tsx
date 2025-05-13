import React, { useState } from 'react';
import styles from './SearchBar.module.scss';
import InputBase from '@mui/material/InputBase';
import SearchIcon from '@mui/icons-material/Search';

interface SearchBarProps {
  onSearchChange: (searchQuery: string) => void; 
}

const SearchBar: React.FC<SearchBarProps> = ({ onSearchChange }) => {
  const [inputValue, setInputValue] = useState("");

  const handleKeyDown = (event: React.KeyboardEvent<HTMLInputElement>) => {
    if (event.key === "Enter") {
      onSearchChange(inputValue);
    }
  };

  const handleInputChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setInputValue(event.target.value);
  };

  return (
    <div className={styles.searchWrapper}>
      <div className={styles.searchIcon}>
        <SearchIcon />
      </div>
      <InputBase
        placeholder="Search Movie…"
        value={inputValue}
        onChange={handleInputChange} 
        onKeyDown={handleKeyDown} 
        classes={{
          root: styles.inputRoot,
          input: styles.inputInput,
        }}
        inputProps={{ 'aria-label': 'search' }}
      />
    </div>
  );
};

export default SearchBar;
