using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System.Web.Mvc
{
    public class PagerQuery<TPager, TEntityList>
    {
        public PagerQuery(TPager pager, TEntityList entityList)
        {
            this.Pager = pager;
            this.EntityList = entityList;
        }
        public TPager Pager { get; set; }
        public TEntityList EntityList { get; set; }
    }
    //public class PagerQuery<TPager, TEntityList, TModel>
    //{
    //    public PagerQuery(TPager pager, TEntityList entityList, TModel tMoled)
    //    {
    //        this.Pager = pager;
    //        this.EntityList = entityList;
    //        this.Model = tMoled;
    //    }
    //    public TPager Pager { get; set; }
    //    public TEntityList EntityList { get; set; }
    //    public TModel Model { get; set; }
    //}
}
