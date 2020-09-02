using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LMS.Models;

namespace LMS.App_Start
{
    public class MappingProfile : Profile
    {
        // we use mapping to transfer data either from a model to a Dto (Data Transfer Object) or from a Dto to a model
        // this is becuase we don't want to send sensitive informations if not necessary
        public MappingProfile()
        {
            Mapper.CreateMap<Book, Book>();
        }
    }
}