using Application.Dto.ResponsiveDto;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class RequestMapper : Profile
    {
        public RequestMapper()
        {
            CreateMap<SearchCompanyResponsivDto.Bucket, Company>()
           .ForMember(d => d.Address, o => o.MapFrom(s => s.info.address.Trim()))
           .ForMember(d => d.Country, o => o.MapFrom(s => s.info.country.Trim()))
           .ForMember(d => d.Name, o => o.MapFrom(s => s.info.name.Trim()))
           .ForMember(d => d.Uid, o => o.MapFrom(s => s.info.uid.Trim()))
           .ForMember(d => d.Uname, o => o.MapFrom(s => s.info.uname.Trim()))
           .ForMember(d => d.Uuid, o => o.MapFrom(s => s.info.uuid.Trim()))
           .ForMember(d => d.Total, o => o.MapFrom(s => s.total))
           .ForMember(d => d.Count, o => o.MapFrom(s => s.count))
           .ForMember(d => d.Var, o => o.MapFrom(s => s.val.Trim()));
        }
    }
}
