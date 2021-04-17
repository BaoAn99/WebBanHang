using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanDienThoai.Core.Entity;
using WebSiteBanDienThoai.Entity;
using WebSiteBanDienThoai.Model;

namespace WebSiteBanDienThoai.Controllers
{
    public class CommentController : Controller
    {
        [HttpPost]
        public ContentResult Gets(int ProductId)
        {
            UnitOfWork uow = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
            var commentList = uow.Comment.GetAll().Where(x => x.ProductID == ProductId);
            var outPut = new List<CommentView>();
            foreach(var item in commentList)
            {
                outPut.Add(CommentView.Convert(item, uow.Customer.Get(item.CustomerID.GetValueOrDefault()).Fullname));
            }
            return Content(ResponseData.ToJson(new ResponseData {obj= outPut,status = true }));
        }
        [HttpPost]
        public ContentResult Post(Comment cmt)
        {
            UnitOfWork uow = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
            uow.Comment.Add(cmt);
            uow.Complete();
            var outPut = new List<CommentView>();
            outPut.Add(CommentView.Convert(cmt, uow.Customer.Get(cmt.CustomerID.GetValueOrDefault()).Fullname));
            return Content(ResponseData.ToJson(new ResponseData { obj = outPut, status = true }));
        }
    }
}