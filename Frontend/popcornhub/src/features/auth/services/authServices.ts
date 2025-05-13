import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';
import { env } from '../../../env.schema';
import type { LoginRequest, RegisterRequest } from '../types/authTypes';
import { HTTP_METHOD_POST } from '../../../constants';

const authApi = createApi({
  reducerPath: 'auth',
  baseQuery: fetchBaseQuery({
    baseUrl: env.POPCORNHUB_MOVIE_URL,
    credentials: 'include', 
  }),

  endpoints: (builder) => ({
    register: builder.mutation<void, RegisterRequest>({
      query: (body) => ({
        url: '/auth/register',
        method: HTTP_METHOD_POST,
        body,
      }),
    }),

    login: builder.mutation<void, LoginRequest>({
      query: (body) => ({
        url: '/auth/login',
        method: HTTP_METHOD_POST,
        body,
      }),
    }),

    check: builder.mutation<void, void>({
      query: () => ({
        url: '/auth/check',
        method: HTTP_METHOD_POST,
      }),
    }),
  }),
});

export const { useRegisterMutation, useLoginMutation, useCheckMutation } = authApi;

export const { reducerPath: authReducerPath, reducer: authReducer, middleware: authMiddleware } = authApi;
