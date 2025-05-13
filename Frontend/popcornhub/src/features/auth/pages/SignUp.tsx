import React from 'react';
import { useNavigate } from 'react-router-dom';
import { useLoginMutation, useRegisterMutation } from '../services/authServices';
import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import { signUpSchema, type SignUpData } from '../components/validation/signUpSchema';
import SignUpComponent from '../components/SignUpComponent';
import styles from './SignUp.module.scss';

const SignUpPage: React.FC = () => {
  const [registerUser] = useRegisterMutation();
  const [loginUser] = useLoginMutation();
  const navigate = useNavigate();

  const { register, handleSubmit, formState: { errors } } = useForm<SignUpData>({
    resolver: zodResolver(signUpSchema),
  });

  const handleSignUp = async (data: SignUpData) => {
      await registerUser({
        email: data.email,
        password: data.password,
        userName: data.username,
      }).unwrap();

      await loginUser({
        email: data.email,
        password: data.password,
      }).unwrap();

      navigate('/movies');
  };

  return (
    <div className={styles.signup}>
      <div className={styles.signupFormContainer}>
        <SignUpComponent
          onSubmit={handleSubmit(handleSignUp)}
          register={register}
          errors={errors}
        />
      </div>
    </div>
  );
};

export default SignUpPage;
