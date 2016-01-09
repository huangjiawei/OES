﻿//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//namespace 
//    public class QueryExtention
//    {
//    }
//}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace OnlineExam
{
    public static class QueryExtention
    {
        public static SelectList ToSelectList<T>(this IQueryable<T> query, string dataValueField, string dataTextField, object selectedValue)
        {
            return new SelectList(query, dataValueField, dataTextField, selectedValue ?? -1);
        }
    }
}