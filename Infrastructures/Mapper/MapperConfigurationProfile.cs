using Application.Common;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Mapper
{
    public  class MapperConfigurationProfile:Profile
    {
       public MapperConfigurationProfile() 
        {
            CreateMap(typeof(Pagination<>), typeof(Pagination<>));

        }
    }
}
