import React from 'react';
import { Box, TextField, Button } from '@mui/material';
import type { SignUpData } from '../components/validation/signUpSchema';
import type { FieldErrors, UseFormRegister } from 'react-hook-form';

interface Props {
  onSubmit: () => void;
  register: UseFormRegister<SignUpData>;
  errors: FieldErrors<SignUpData>;
}

const SignUpComponent: React.FC<Props> = ({ onSubmit, register, errors }) => {
  return (
    <Box component="form" onSubmit={onSubmit} sx={{ display: 'grid', gap: 2, width: 300 }}>
      <TextField
        label="Email"
        type="email"
        {...register('email')}
        error={!!errors.email}
        helperText={errors.email?.message}
        required
      />
      <TextField
        label="Username"
        {...register('username')}
        error={!!errors.username}
        helperText={errors.username?.message}
        required
      />
      <TextField
        label="Password"
        type="password"
        {...register('password')}
        error={!!errors.password}
        helperText={errors.password?.message}
        required
      />
      <Button type="submit" variant="contained">Sign Up</Button>
    </Box>
  );
};

export default SignUpComponent;
