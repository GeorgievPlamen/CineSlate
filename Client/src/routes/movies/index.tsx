import Movies from '@/modules/Movies';
import { createFileRoute } from '@tanstack/react-router';
import z from 'zod';

// interface SearchParams {
//   search: string;
//   genreIds: number[];
// }

const moviesParamsSchema = z.object({
  search: z.string().optional(),
  genreIds: z.array(z.number()).optional(),
});

// type MoviesSearch = z.infer<typeof moviesParamsSchema>;

export const Route = createFileRoute('/movies/')({
  component: RouteComponent,
  validateSearch: (search) => moviesParamsSchema.parse(search),
});

function RouteComponent() {
  return <Movies />;
}
