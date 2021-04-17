class TemplateClass{
    constructor(_templateId,_parrentId,_renderFollow){
        this.templateId = _templateId;
        this.parrentId = _parrentId;
        this.renderFollow = _renderFollow;
    }
}
class ResponseData{
    constructor(_obj, _status,_exception,_mess) {
        this.obj = _obj;
        this.status = _status;
        this.exception = _exception;
        this.mess = _mess;
    }	
}
class Server{
    constructor(ptype,purl,pdata){
        this.type=ptype;
        this.url = purl;
        this.data = pdata;        
    }    
    _requestCallBack(psuccess, pfailure, perror){
        jQuery.ajax({
            type: this.type,
            url: this.url,
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(this.data),
            success: function(response) {
                psuccess(response);
            },
            failure: function(response) {
                pfailure(response);
            },
            error: function(response) {
                perror(response);
            },
        });
    }
}
class Paging{
    constructor(_currentPage,_totalPage,_sizeOnPage,_result,_server) {
        this.currentPage = _currentPage;
        this.totalPage = _totalPage;
        this.sizeOnPage = _sizeOnPage;
        this.result = _result;
        this.server = _server;
    }
    _comeToPage(newPage, psuccess, pfailure, perror) {
        this.currentPage=newPage;
        this.server._requestCallBack(psuccess, pfailure, perror);
    }
    _getData(){
        return {currentPage:this.currentPage,totalPage:this.totalPage,sizeOnPage:this.sizeOnPage,result:this.result};
    }
    _setData(_page){
        this.currentPage = _page.currentPage;
        this.totalPage = _page.totalPage;
        this.sizeOnPage = _page.sizeOnPage;
        this.result = _page.result;
        this.server = _page.server;
    }
}
class Order extends Paging{
    constructor(_page,_template){
        super(_page.currentPage,_page.totalPage,_page.sizeOnPage,_page.result,_page.server);
        this.template = _template;
    }
    _resetInterface(){
        var html = '';
        var Template = $(this.template.templateId).html();
        var renderFollow = this.template.renderFollow;
        $.each(this.result, function (index, elm) {
            html += Mustache.render(Template, 
                {
                    CartID: elm.CartID,
                    Date: elm.DateStr,
                    CustomerName:elm.Customer.Fullname,
                    Phone:elm.Customer.Phone,
                    Adress:elm.Customer.Address,
                    Status:elm.Status,
                    TotalPrice: elm.TotalPrice,
                    statusColor:elm.statusColor,
                }
        );});
        $(this.template.parrentId).append(html);
    }    
}
class OrderDetail extends Paging{
    constructor(_page,_template){
        super(_page.currentPage,_page.totalPage,_page.sizeOnPage,_page.result,_page.server);
        this.template = _template;
    }
    _resetInterface(){
        var html = '';
        var Template = $(this.template.templateId).html();
        var renderFollow = this.template.renderFollow;
        $.each(this.result, function (index, elm) {
            html += Mustache.render(Template, 
                {
                    CartDetailID: elm.CartDetailID,
                    ProductID: elm.ProductID,
                    ProductName:elm.Product.Name,
                    Amount:elm.Amount,
                    PromotionName:elm.PromotionName,
                    Sale:elm.Sale,
                    Price:elm.PriceProduct,                    
                }
        );});
        $(this.template.parrentId).empty();
        $(this.template.parrentId).append(html);
    }    
}
class Bill extends Paging{
    constructor(_page,_template){
        super(_page.currentPage,_page.totalPage,_page.sizeOnPage,_page.result,_page.server);
        this.template = _template;
    }
    _resetInterface(){
        var html = '';
        var Template = $(this.template.templateId).html();
        var renderFollow = this.template.renderFollow;
        $.each(this.result, function (index, elm) {
            html += Mustache.render(Template, 
                {
                    BillID: elm.BillID,
                    BuyDate: elm.DateStr,
                    Status:elm.Status,
                    EmployeeSell:elm.SaleEmployeeName,
                    EmployeeShip:elm.DeliveryEmployeeName,
                    TotalPrice:elm.TotalPrice,
                }
        );});
        $(this.template.parrentId).append(html);
    }    
}
//Init
