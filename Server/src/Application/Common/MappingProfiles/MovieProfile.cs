using Application.Movies;
using AutoMapper;
using Domain.Movies;

namespace Application.Common.MappingProfiles;

public class MovieProfile : Profile
{
    public MovieProfile()
    {
        CreateMap<MovieAggregate, Movie>();
    }
}