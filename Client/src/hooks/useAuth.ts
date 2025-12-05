import { useUserStore } from '@/common/store/store';

export default function useAuth() {
  const token = useUserStore((state) => state?.user?.token);

  const isAuthenticated = token ? token?.length > 0 : false;

  return { isAuthenticated };
}
