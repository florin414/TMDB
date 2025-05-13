import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import { Provider } from 'react-redux';
import store from './app/store';
import './index.css'
import './styles/fonts/fonts.ts'
import App from './app.tsx'
import { ThemeProvider } from '@emotion/react';
import darkTheme from './app/theme.ts';
import { CssBaseline } from '@mui/material';
import { ToastContainer } from 'react-toastify';

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <ThemeProvider theme={darkTheme}>
    <ToastContainer />
        <CssBaseline />
        <Provider store={store}>
          <App />
        </Provider>
      </ThemeProvider>
  </StrictMode>
)
