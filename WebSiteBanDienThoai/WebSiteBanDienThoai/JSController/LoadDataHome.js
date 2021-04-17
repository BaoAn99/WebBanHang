/// <reference path="/Scripts/jquery.min.js" />

var uri = "http://localhost:6688/api";
var ProductController = {
    page: 0,
    pageSize: 0,
    prop: {
        linkUrl: "",
        templateID: "",
        elmentID: "",
    },
    init: function () {
        this.LoadData();
    },
    LoadData: function () {
        $.ajax({
            url: uri + this.prop.linkUrl + "?&pageNumber=" + this.page + "&pageSize=" + this.pageSize,
            method: 'GET',
            success: function (response) {
                var html = '';
                var Template = $(prop.templateID).html();
                $.each(response, function (index, elm) {
                    html += Mustache.render(Template, {
                        ProductID: elm.ProductID,
                        ProductCode: elm.ProductCode,
                        ProductName: elm.ProductName,
                        Alias: elm.Alias,
                        MetaTitle: elm.MetaTitle,
                        Description: elm.Description,
                        ProductImage: elm.ProductImage,
                        MoreImages: elm.MoreImages,
                        Price: elm.Price,
                        PromotionPrice: elm.PromotionPrice,
                        IncludeVAT: elm.IncludeVAT,
                        Quantity: elm.Quantity,
                        CategoryID: elm.CategoryID,
                        Detail: elm.Detail,
                        Warranty: elm.Warranty,
                        CreatedDate: elm.CreatedDate,
                        CreatedBy: elm.CreatedBy,
                        ModifiedDate: elm.ModifiedDate,
                        ModifiedBy: elm.ModifiedBy,
                        MetaKeywords: elm.MetaKeywords,
                        MetaDescriptions: elm.MetaDescriptions,
                        Status: elm.Status,
                        ViewCounts: elm.ViewCounts,
                        TopHot: elm.TopHot,
                        Specifications: elm.Specifications,
                    });
                });
                $(prop.elmentID).append(html);
            }
        })
    },
    LoadMore: function (elm) {
        $(elm).click(function () {
            _page = ProductController.page += 1;
            ProductController.LoadData(ProductController.prop, _page, ProductController.pageSize);
        });
    },
    constructor: function (_page, _pageSize, _prop) {
        this.page = _page,
        this.pageSize = _pageSize,
        this.prop = _prop   
    },
}
var ProductController2 = {
    page: 0,
    pageSize: 0,
    prop: {
        linkUrl: "",
        templateID: "",
        elmentID: "",
    },
    init: function () {
        this.LoadData();
    },
    LoadData: function () {
        $.ajax({
            url: uri + this.prop.linkUrl + "?&pageNumber=" + this.page + "&pageSize=" + this.pageSize,
            method: 'GET',
            success: function (response) {
                var html = '';
                var Template = $(propGetSelling.templateID).html();
                $.each(response, function (index, elm) {
                    html += Mustache.render(Template, {
                        ProductID: elm.ProductID,
                        ProductCode: elm.ProductCode,
                        ProductName: elm.ProductName,
                        Alias: elm.Alias,
                        MetaTitle: elm.MetaTitle,
                        Description: elm.Description,
                        ProductImage: elm.ProductImage,
                        MoreImages: elm.MoreImages,
                        Price: elm.Price,
                        PromotionPrice: elm.PromotionPrice,
                        IncludeVAT: elm.IncludeVAT,
                        Quantity: elm.Quantity,
                        CategoryID: elm.CategoryID,
                        Detail: elm.Detail,
                        Warranty: elm.Warranty,
                        CreatedDate: elm.CreatedDate,
                        CreatedBy: elm.CreatedBy,
                        ModifiedDate: elm.ModifiedDate,
                        ModifiedBy: elm.ModifiedBy,
                        MetaKeywords: elm.MetaKeywords,
                        MetaDescriptions: elm.MetaDescriptions,
                        Status: elm.Status,
                        ViewCounts: elm.ViewCounts,
                        TopHot: elm.TopHot,
                        Specifications: elm.Specifications,
                    });
                });
                $(propGetSelling.elmentID).append(html);
            }
        })
    },
    LoadMore: function (elm) {
        $(elm).click(function () {
            _page = ProductController2.page += 1;
            ProductController2.LoadData(ProductController2.prop, _page, ProductController2.pageSize);
        });
    },
    constructor: function (_page, _pageSize, _prop) {
        this.page = _page,
        this.pageSize = _pageSize,
        this.prop = _prop
    },
}
var ProductController3 = {
    page: 0,
    pageSize: 0,
    prop: {
        linkUrl: "",
        templateID: "",
        elmentID: "",
    },
    init: function () {
        this.LoadData();
    },
    LoadData: function () {
        $.ajax({
            url: uri + this.prop.linkUrl + "&pageNumber=" + this.page + "&pageSize=" + this.pageSize,
            method: 'GET',
            success: function (response) {
                var html = '';
                var Template = $(prop.templateID).html();
                $.each(response, function (index, elm) {
                    html += Mustache.render(Template, {
                        ProductID: elm.ProductID,
                        ProductCode: elm.ProductCode,
                        ProductName: elm.ProductName,
                        Alias: elm.Alias,
                        MetaTitle: elm.MetaTitle,
                        Description: elm.Description,
                        ProductImage: elm.ProductImage,
                        MoreImages: elm.MoreImages,
                        Price: elm.Price,
                        PromotionPrice: elm.PromotionPrice,
                        IncludeVAT: elm.IncludeVAT,
                        Quantity: elm.Quantity,
                        CategoryID: elm.CategoryID,
                        Detail: elm.Detail,
                        Warranty: elm.Warranty,
                        CreatedDate: elm.CreatedDate,
                        CreatedBy: elm.CreatedBy,
                        ModifiedDate: elm.ModifiedDate,
                        ModifiedBy: elm.ModifiedBy,
                        MetaKeywords: elm.MetaKeywords,
                        MetaDescriptions: elm.MetaDescriptions,
                        Status: elm.Status,
                        ViewCounts: elm.ViewCounts,
                        TopHot: elm.TopHot,
                        Specifications: elm.Specifications,
                    });
                });
                $(prop.elmentID).append(html);
            }
        })
    },
    LoadMore: function (elm) {
        $(elm).click(function () {
            _page = ProductController3.page += 1;
            ProductController3.LoadData(ProductController3.prop, _page, ProductController3.pageSize);
        });
    },
    constructor: function (_page, _pageSize, _prop) {
        this.page = _page,
        this.pageSize = _pageSize,
        this.prop = _prop
    },
}
//Controller load more search
var SearchController = {
    page: 0,
    pageSize: 0,
    prop: {
        linkUrl: "",
        templateID: "",
        elmentID: "",
        keywords:"",
    },
    init: function () {
        this.LoadData();
    },
    LoadData: function () {
        $.ajax({
            url: uri + this.prop.linkUrl + "?&pageNumber=" + this.page + "&pageSize=" + this.pageSize+"&TenSP="+this.prop.keywords,
            method: 'GET',
            success: function (response) {
                var html = '';
                var Template = $(propSearch.templateID).html();
                $.each(response, function (index, elm) {
                    html += Mustache.render(Template, {
                        ProductID: elm.ProductID,
                        ProductCode: elm.ProductCode,
                        ProductName: elm.ProductName,
                        Alias: elm.Alias,
                        MetaTitle: elm.MetaTitle,
                        Description: elm.Description,
                        ProductImage: elm.ProductImage,
                        MoreImages: elm.MoreImages,
                        Price: elm.Price,
                        PromotionPrice: elm.PromotionPrice,
                        IncludeVAT: elm.IncludeVAT,
                        Quantity: elm.Quantity,
                        CategoryID: elm.CategoryID,
                        Detail: elm.Detail,
                        Warranty: elm.Warranty,
                        CreatedDate: elm.CreatedDate,
                        CreatedBy: elm.CreatedBy,
                        ModifiedDate: elm.ModifiedDate,
                        ModifiedBy: elm.ModifiedBy,
                        MetaKeywords: elm.MetaKeywords,
                        MetaDescriptions: elm.MetaDescriptions,
                        Status: elm.Status,
                        ViewCounts: elm.ViewCounts,
                        TopHot: elm.TopHot,
                        Specifications: elm.Specifications,
                    });
                });
                $(propSearch.elmentID).append(html);
            }
        })
    },
    LoadMore: function (elm) {
        $(elm).click(function () {
            _page = SearchController.page += 1;
            SearchController.LoadData(SearchController.prop, _page, SearchController.pageSize, SearchController.keywords);
        });
    },
    constructor: function (_page, _pageSize, _prop,_keywords) {
        this.page = _page,
        this.pageSize = _pageSize,
        this.prop = _prop,
        this.value = _keywords
    },
}
//$(document).ajaxStart(function () {
//    $("#wait").css("display", "block");
//});
//$(document).ajaxComplete(function () {
//    $("#wait").css("display", "none");
//});
var ArticleController = {
    page: 0,
    pageSize: 0,
    prop: {
        linkUrl: "",
        templateID: "",
        elmentID: "",
    },
    init: function () {
        this.LoadData();
    },
    LoadData: function () {
        $.ajax({
            url: uri + this.prop.linkUrl + "?&pageNumber=" + this.page + "&pageSize=" + this.pageSize,
            method: 'GET',
            success: function (ArticleResponse) {
                //alert(this.prop);//Trong phần success biến prop của articleController bị vô hiệu hóa
                var html = '';
                var Template = $(ArticleProp.templateID).html();
                $.each(ArticleResponse, function (index, elm) {
                    html += Mustache.render(Template, {
                        ArticleID: elm.ArticleID,
                        ArticleTitle: elm.Title,
                        Alias: elm.Alias,
                        ArticleDescription: elm.Description,
                        ArticleImage: elm.ArticleImage,
                        MoreImages: elm.MoreImages,
                        Price: elm.Price,
                        PromotionPrice: elm.PromotionPrice,
                        IncludeVAT: elm.IncludeVAT,
                        Quantity: elm.Quantity,
                        CategoryID: elm.ArticleCategoryID,
                        Detail: elm.Detail,
                        CreatedDate: elm.CreatedDate,
                        CreatedBy: elm.CreatedBy,
                        ModifiedDate: elm.ModifiedDate,
                        ModifiedBy: elm.ModifiedBy,
                        MetaKeywords: elm.MetaKeywords,
                        MetaDescriptions: elm.MetaDescriptions,
                        Status: elm.Status,
                        ViewCounts: elm.ViewCounts,
                        TopHot: elm.TopHot,
                        Specifications: elm.Specifications,
                        SupplierID: elm.SupplierID,
                    });
                });
                $(ArticleProp.elmentID).append(html);
            }
        })
    },
    LoadMore: function (elm) {
        $(elm).click(function () {
            _page = ArticleController.page += 1;
            ArticleController.LoadData(ArticleController.prop, _page, ArticleController.pageSize);
        });
    },
    constructor: function (_page, _pageSize, _prop) {
        this.page = _page,
        this.pageSize = _pageSize,
        this.prop = _prop
    },
}