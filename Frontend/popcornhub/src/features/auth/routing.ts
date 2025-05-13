import type { RouteObject } from 'react-router-dom';
import Login from './pages/Login';
import SignUp from './pages/SignUp';
import AuthLayout from '../../layouts/AuthLayout';

const AuthRouting = (): RouteObject => ({
  path: '/auth',
  Component: AuthLayout,
  children: [
    {
      path: 'login',
      Component: Login,
    },
    {
      path: 'signup',
      Component: SignUp,
    },
  ],
});

export default AuthRouting;
