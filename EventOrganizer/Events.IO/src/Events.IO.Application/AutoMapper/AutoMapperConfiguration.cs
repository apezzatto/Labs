using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace Events.IO.Application.AutoMapper
{
    public partial class AutoMapperConfiguration
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(ps =>
            {
                ps.AddProfile(new DomainToViewModelMappingProfile());
                ps.AddProfile(new ViewModelToDomainMappingProfile());
            });
        }
    }
}
