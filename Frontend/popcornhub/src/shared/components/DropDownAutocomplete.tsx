import { useState } from 'react';
import Autocomplete from '@mui/material/Autocomplete';
import TextField from '@mui/material/TextField';
import CircularProgress from '@mui/material/CircularProgress';
import styles from './DropDownAutocomplete.module.scss';
import React from 'react';

interface DropDownAutocompleteProps<T> {
    options: T[];
    label: string;
    getOptionLabel: (option: T) => string;
    isOptionEqualToValue: (option: T, value: T) => boolean;
    onSelect?: (value: T | null) => void;
    defaultValue?: T;
    loading?: boolean;
}

const DropDownAutocomplete = <T,>({
    options,
    getOptionLabel,
    isOptionEqualToValue,
    onSelect,
    defaultValue,
    loading = false,
}: DropDownAutocompleteProps<T>) => {
    const [open, setOpen] = useState(false);

    return (
        <div className={styles.dropdownAutocomplete}>
            <Autocomplete
                open={open}
                onOpen={() => setOpen(true)}
                onClose={() => setOpen(false)}
                options={options}
                getOptionLabel={getOptionLabel}
                isOptionEqualToValue={isOptionEqualToValue}
                onChange={(_, value) => onSelect?.(value)}
                defaultValue={defaultValue}
                loading={loading}
                renderInput={(params) => (
                    <TextField
                        {...params}
                        slotProps={{
                            input: {
                                ...params.InputProps,
                                endAdornment: (
                                    <React.Fragment>
                                        {loading ? <CircularProgress color="inherit" size={20} /> : null}
                                        {params.InputProps.endAdornment}
                                    </React.Fragment>
                                ),
                            },
                        }}
                    />

                )}
            />
        </div>
    );
};

export default DropDownAutocomplete;
