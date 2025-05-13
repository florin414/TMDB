import { type Middleware, type MiddlewareAPI, isFulfilled, isRejectedWithValue } from '@reduxjs/toolkit';
import { toast } from 'react-toastify';

type MetaArg = { endpointName: string };

export const rtkAuthNotiferMiddleware: Middleware = (api: MiddlewareAPI) => (next) => (action) => {
  if (isFulfilled(action)) {
    const endpoint = (action.meta?.arg as MetaArg | undefined)?.endpointName;

    switch (endpoint) {
      case 'register':
        toast.success('Registration successful!');
        break;
      case 'login':
        toast.success('Login successful!');
        break;
      default:
        break;
    }
  }

  if (isRejectedWithValue(action)) {
    const endpoint = (action.meta?.arg as MetaArg | undefined)?.endpointName;

    switch (endpoint) {
      case 'register':
        toast.error('Registration failed. Please try again later.');
        break;
      case 'login':
        toast.error('Login failed. Please try again later.');
        break;
      default:
        break;
    }
  }

  return next(action);
};
