using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSiteBanDienThoai.Areas.Admin.Models
{
    public class ResponseData
    {
        public object obj { get; set; }
        public bool status { get; set; }
        public string exception { get; set; }
        public string mess { get; set; }    
        public Paging page { get; set; }    

        public ResponseData(object _obj, bool _status, string _exception, string _mess)
        {
            this.obj = _obj;
            this.status = _status;
            this.exception = _exception;
            this.mess = _mess;
        }
        public ResponseData(Paging _page, bool _status, string _exception, string _mess)
        {
            page = _page;
            this.status = _status;
            this.exception = _exception;
            this.mess = _mess;
        }
    }
    public class Paging
    {
        public int currentPage { get; set; }
        public int totalPage { get; set; }
        public int sizeOnPage { get; set; }
        public object result { get; set; }
        public Paging(int _currentPage,int _totalPage,int _sizeOnPage,object _result)
        {
            currentPage = _currentPage;
            totalPage = _totalPage;
            sizeOnPage = _sizeOnPage;
            result = _result;
        }
        public Paging() { }
        public List<T> GetCurrentResult<T>(List<T> dataSource)
        {
            if(this.totalPage!=0)
                return dataSource.Skip((currentPage-1) * sizeOnPage).Take(sizeOnPage).ToList();
            if (dataSource.Count % this.sizeOnPage == 0)
            {
                this.totalPage = dataSource.Count / this.sizeOnPage;
            }
            else if(dataSource.Count < this.sizeOnPage)
            {
                this.totalPage = 1;
            }
            else
            {
                this.totalPage = (dataSource.Count / this.sizeOnPage) + 1;
            }
            return dataSource.Skip((currentPage-1) * sizeOnPage).Take(sizeOnPage).ToList();
        }
    }
    public class OrderPaging : Paging
    {
        public int customerId { get; set; }
        public OrderPaging(int _currentPage, int _totalPage, int _sizeOnPage, object _result,int _customerId):base(_currentPage,_totalPage,_sizeOnPage,_result)
        {
            customerId = _customerId;
        }
        public OrderPaging()
        {

        }
    }
    public class BillPaging : Paging
    {
        public int customerId { get; set; }
        public BillPaging(int _currentPage, int _totalPage, int _sizeOnPage, object _result, int _customerId) : base(_currentPage, _totalPage, _sizeOnPage, _result)
        {
            customerId = _customerId;
        }
        public BillPaging()
        {

        }
    }
}