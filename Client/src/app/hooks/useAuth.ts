import { useAppSelector } from '../store/reduxHooks';

export default function useAuth() {
  const token = useAppSelector((state) => state?.users?.user?.token);

  const isAuthenticated = token.length > 0;

  return { isAuthenticated };
}
