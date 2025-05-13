import React from 'react';
import styles from './DropDown.module.scss';
import DropDownAutocomplete from '../../../shared/components/DropDownAutocomplete';

interface TagOrderDropdownProps {
  onSelect: (order: 'top' | 'latest') => void;
  loading?: boolean;
  defaultValue?: 'top' | 'latest';
}

const TagOrderDropdown: React.FC<TagOrderDropdownProps> = ({
  onSelect, loading = false, defaultValue
}) => {

  const options = [
    { id: 1, name: 'Top' },
    { id: 2, name: 'Latest' }, 
  ];

  return (
    <div className={styles.dropdownContainer}> 
      <DropDownAutocomplete
        options={options}
        getOptionLabel={(option) => option.name}
        isOptionEqualToValue={(option, value) => option.id === value.id}
        onSelect={(selected) => onSelect(selected ? selected.name.toLowerCase() as 'top' | 'latest' : 'latest')}
        loading={loading}
        label="Select Order"
        defaultValue={options.find(
          (opt) => opt.name.toLowerCase() === defaultValue
        )}
      />
    </div>
  );
};

export default TagOrderDropdown;
