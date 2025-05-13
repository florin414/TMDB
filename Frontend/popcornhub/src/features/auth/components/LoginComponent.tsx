import React from 'react';
import { Box, TextField, Button } from '@mui/material';
import type { FieldErrors, UseFormRegister } from 'react-hook-form';
import type { LoginFormData } from './validation/loginSchema';

interface LoginComponentProps {
  onSubmit: () => void;
  register: UseFormRegister<LoginFormData>;
  errors: FieldErrors<LoginFormData>;
  isLoading?: boolean;
}

const LoginComponent: React.FC<LoginComponentProps> = ({
  onSubmit,
  register,
  errors,
  isLoading
}) => {
  return (
    <Box component="form" onSubmit={onSubmit} sx={{ display: 'grid', gap: 2, width: 300 }}>
      <TextField
        label="Email"
        {...register('email')}
        error={!!errors.email}
        helperText={errors.email?.message}
      />
      <TextField
        label="Password"
        type="password"
        {...register('password')}
        error={!!errors.password}
        helperText={errors.password?.message}
      />
      <Button type="submit" variant="contained" disabled={isLoading}>
        {isLoading ? 'Logging in...' : 'Login'}
      </Button>
    </Box>
  );
};

export default LoginComponent;
