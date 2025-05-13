import { useEffect, useState } from 'react';
import { useCheckMutation } from '../services/authServices';

interface Props {
  children: React.ReactNode;
  fallback?: React.ReactNode;
}

const RequireAuth = ({ children, fallback = null }: Props) => {
  const [checkAuth] = useCheckMutation(); 
  const [isAuthenticated, setIsAuthenticated] = useState<boolean | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const validateAuth = async () => {
      try {
        await checkAuth().unwrap();
        setIsAuthenticated(true);
      } catch {
        setIsAuthenticated(false);
      } finally {
        setLoading(false);
      }
    };

    validateAuth();
  }, [checkAuth]);

  if (loading || isAuthenticated === null) return null;
  if (!isAuthenticated) return <>{fallback}</>;

  return <>{children}</>;
};

export default RequireAuth;
