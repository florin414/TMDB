import React from 'react';
import { useNavigate } from 'react-router-dom';
import { useLoginMutation } from '../services/authServices';
import LoginComponent from '../components/LoginComponent';
import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import { loginSchema, type LoginFormData } from '../components/validation/loginSchema';
import styles from './Login.module.scss';

const LoginPage: React.FC = () => {
  const [login, { isLoading }] = useLoginMutation();
  const navigate = useNavigate();

  const { register, handleSubmit, formState: { errors } } = useForm<LoginFormData>({
    resolver: zodResolver(loginSchema),
  });

  const onSubmit = async (data: LoginFormData) => {
    await login(data).unwrap();
    navigate('/movies/4/details');
  };

  return (
    <div className={styles.login}>
      <div className={styles.loginFormContainer}>
        <LoginComponent
          onSubmit={handleSubmit(onSubmit)}
          register={register}
          errors={errors}
          isLoading={isLoading}
        />
      </div>
    </div>
  );
};

export default LoginPage;
