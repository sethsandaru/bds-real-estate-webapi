using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstateAPI.Models
{
    public class PagingResult<T>
    {
        public int Total;
        public IEnumerable<T> Data;
    }
}