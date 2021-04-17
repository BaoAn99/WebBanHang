class ServerV2_0_1 {
    constructor(pType, pController) {
        this.type = pType;
        this.controller = pController;
        this.data = {};
    }
    _requestCallBack(pAction,pSuccess, pFailure, pError) {
        jQuery.ajax({
            type: this.type,
            url: this.controller+pAction,
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(this.data),
            success: function (response) {
                pSuccess(response);
            },
            failure: function (response) {
                pFailure(response);
            },
            error: function (response) {
                pError(response);
            },
        });
    }
}
class CommentClient {
    constructor(_userId,_productId,_inputId) {
        this.id = 0;
        this.content = "";
        this.date = "";
        this.userId = _userId;
        this.productId = _productId;
        this.inputId = _inputId;
    }
    _Get() {
        this.content = $(this.inputId).val();
        $(this.inputId).val("");
    }
    _Format() {
        return {Content:this.content,Date:this.date,CustomerID:this.userId,ProductId:this.productId};
    }
}
class Comment{
    constructor(_userId,_itemId,_template) {
        this.itemId = _itemId;
        this.userId = _userId;
        this.template = _template;
        this.dataSource = [];
        this.server = new ServerV2_0_1("POST", "/Comment/");
        this._getComments();
    }
    _getComments(){
        var root = this;
        root.server.data = { ProductId: this.itemId };
        root.server._requestCallBack("Gets",function(response){
            root._renderData(response);
        },function(response){

        },function(response){

        });
    }
    _addComment(pCommentObj) {
        if (pCommentObj.userId == 0) {
            notify.methods.info._showDialog("Bạn cần đăng nhập trước", "/Account/Login");
            return;
        }
        if (pCommentObj.userId == -1 || pCommentObj.userId == null) {
            notify.methods.info._showDialog("Bạn cần mua sản phẩm trước", "#");
            return;
        }
        var root = this;
        pCommentObj.date = new Date().toJSON().slice(0, 10).replace(/-/g, '/');
        root.server.data = pCommentObj._Format();
        root.server._requestCallBack("Post", function (response) {
            root._renderData(response);            
        }, function (response) {

        }, function (response) {

        });
    }
    _renderData(_response) {
        var html = '';
        var template = $(this.template.templateId).html();
        $.each(_response.obj, function (index, elm) {
            html += Mustache.render(template,
                {
                    UserName: elm.CustomerName,
                    Content: elm.Content,
                    Date: elm.Date,
                }
        );
        });
        $(this.template.parrentId).append(html);
    }
    
}