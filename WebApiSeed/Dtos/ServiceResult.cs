using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiSeed.Dtos
{
    public class ServiceResult<TDto, TServiceResult>
        where TDto : BaseDto
        where TServiceResult : IServiceResult
    {
        public TDto Dto { get; set; }
        public TServiceResult Result { get; set; }
    }
}