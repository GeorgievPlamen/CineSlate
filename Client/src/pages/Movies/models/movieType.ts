export interface Movie {
  id: number;
  title: string;
  releaseDate: Date;
  posterPath: string;
  rating: number;
}

export interface Genre {
  id: number;
  value: string;
}

export interface MovieDetails {
  id: number;
  title: string;
  description: string;
  releaseDate: Date;
  posterPath: string;
  genres: Genre[];
  backdropPath: string;
  budget: number;
  homepage: string;
  imdbId: string;
  originCountry: string;
  revenue: number;
  runtime: number;
  status: string;
  tagline: string;
  rating: number;
}
