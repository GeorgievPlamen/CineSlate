import { Paged } from '@/common/models/paged';
import { InfiniteData } from '@tanstack/react-query';

export default function ToPagedData<T>(
  data: InfiniteData<Paged<T>, number>
): Paged<T> {
  return {
    values: data.pages.flatMap((page) => page.values),
    currentPage: data.pages[data.pages.length - 1].currentPage,
    hasNextPage: data.pages[data.pages.length - 1].hasNextPage,
    hasPreviousPage: data.pages[data.pages.length - 1].hasPreviousPage,
    totalCount: data.pages[data.pages.length - 1].totalCount,
  };
}
