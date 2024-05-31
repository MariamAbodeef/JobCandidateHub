
using JobCandidateHub.Domain.Dto;
using JobCandidateHub.Domain.Entities;
using AutoMapper;

namespace JobCandidateHub.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<JobCandidate, JobCandidateDto>()
                .ForMember(d => d.FirstName, y => y.MapFrom(x => x.FirstName))
                .ForMember(d => d.LastName, y => y.MapFrom(x => x.LastName))
                .ForMember(d => d.Email, y => y.MapFrom(x => x.Email))
                .ForMember(d => d.GitHubProfileUrl, y => y.MapFrom(x => x.GitHubProfileUrl))
                .ForMember(d => d.LinkedInProfileUrl, y => y.MapFrom(x => x.LinkedInProfileUrl))
                .ForMember(d => d.PhoneNumber, y => y.MapFrom(x => x.PhoneNumber))
                .ForMember(d => d.BestCallTime, y => y.MapFrom(x => x.BestCallTime))
                .ForMember(d => d.Comment, y => y.MapFrom(x => x.Comment))
                .ReverseMap();
            //CreateMap<List<JobCandidate>, List<JobCandidateDto>>().ReverseMap();
        }
    }
}
