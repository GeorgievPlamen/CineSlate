import { useUserStore } from '@/store/userStore';

export default function useAuth() {
  const token = useUserStore((state) => state?.user?.token);

  const isAuthenticated = token ? token?.length > 0 : false;

  return { isAuthenticated };
}
