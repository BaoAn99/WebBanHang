/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('AdmShopMobile.products', ['AdmShopMobile.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('products',
            {
                url: "/products",
                templateUrl: "/Areas/Admin/app/components/products/productListView.html"
            })
            .state('product_add', {
                url: "/product_add",
                templateUrl: "/Areas/Admin/app/components/products/productAddView.html"
            })
            .state('product_edit', {
                url: "/product_edit/:ProductID",
                templateUrl: "/Areas/Admin/app/components/products/productEditView.html"
            })
            //Nhà sản xuất
            .state('productcategory', { 
                url: "/productcategory",
                templateUrl: "/Areas/Admin/app/components/productCategory/productCategoryListView.html"
            })
            .state('productcategory_add', {
                url: "/productcategory_add",
                templateUrl: "/Areas/Admin/app/components/productCategory/productCategoryAddView.html"
            })
            .state('productcategory_edit', {
                url: "/productcategory_edit/:CategoryID",
                templateUrl: "/Areas/Admin/app/components/productCategory/productCategoryEditView.html"
            })
            //Loại sản phẩm
            .state('typeproducts', { 
                url: "/typeproduct",
                templateUrl: "/Areas/Admin/app/components/typeProduct/typeProductListView.html"
            })
            .state('typeproduct_add', {
                url: "/typeproduct_add",
                templateUrl: "/Areas/Admin/app/components/typeProduct/typeProductAddView.html"
            })
            .state('typeproduct_edit', {
                url: "/typeproduct_edit/:TypeProductID",
                templateUrl: "/Areas/Admin/app/components/typeProduct/typeProductEditView.html"
            })
             //User
            .state('users', {
                url: "/user",
                templateUrl: "/Areas/Admin/app/components/users/usersListView.html"
            })
            .state('user_add', {
                url: "/user_add",
                templateUrl: "/Areas/Admin/app/components/users/usersAddView.html"
            })
            .state('user_edit', {
                url: "/user_edit/:ID",
                templateUrl: "/Areas/Admin/app/components/users/usersEditView.html"
            })
            .state('user_role', {
                url: "/user_role",
                templateUrl: "/Areas/Admin/app/components/users/usersRoleView.html"
            })
            .state('user_block', {
                url: "/user_block",
                templateUrl: "/Areas/Admin/app/components/users/usersDarkListView.html"
            })
            //Order
            .state('orders', {
                url: "/orders",
                templateUrl: "/Areas/Admin/app/components/orders/ordersListView.html"
            })
            .state('orders_Detail', {
                url: "/orders_Detail/:BillID",
                templateUrl: "/Areas/Admin/app/components/orders/ordersDetailView.html"
            })
            //OrderInvoice
            .state('receipts', {
                url: "/receipts",
                templateUrl: "/Areas/Admin/app/components/ordersJker/ordersJkerListView.html"
            })
            .state('receipt_add', {
                url: "/receipt_add",
                templateUrl: "/Areas/Admin/app/components/ordersJker/ordersJkerAddView.html"
            })
            .state('receipt_edit', {
                url: "/receipt_edit/:ID",
                templateUrl: "/Areas/Admin/app/components/ordersJker/ordersJkerEditView.html"
            })
            //Bảo hành
            .state('guarantees', {
                url: "/guarantees",
                templateUrl: "/Areas/Admin/app/components/receipt/receiptsJkerListView.html"
            })
            .state('guarantee_add', {
                url: "/guarantee_add",
                templateUrl: "/Areas/Admin/app/components/receipt/receiptsJkerAddView.html"
            })
            .state('guarantee_edit', {
                url: "/guarantee_edit/:ID",
                templateUrl: "/Areas/Admin/app/components/receipt/receiptsJkerEditView.html"
            })
            //Trả bảo hành
            .state('returnGuarantees', {
                url: "/return_guarantees",
                templateUrl: "/Areas/Admin/app/components/returnReceipt/returnReceiptListView.html"
            })
            .state('returnGuarantee_add', {
                url: "/return_guarantee_add",
                templateUrl: "/Areas/Admin/app/components/returnReceipt/returnReceiptAddView.html"
            })
            .state('returnGuarantee_edit', {
                url: "/return_guarantee_edit/:ID",
                templateUrl: "/Areas/Admin/app/components/returnReceipt/returnReceiptEditView.html"
            })
            //Post
            .state('post', {
                url: "/post",
                templateUrl: "/Areas/Admin/app/components/post/postListView.html"
            })
            .state('post_add', {
                url: "/post_add",
                templateUrl: "/Areas/Admin/app/components/post/postAddView.html"
            })
            .state('post_edit', {
                url: "/post_edit/:ID",
                templateUrl: "/Areas/Admin/app/components/post/postEditView.html"
            }) 
            //Employee
            .state('employees', {
                url: "/employee",
                templateUrl: "/Areas/Admin/app/components/employee/employeeListView.html"
            })
            .state('employee_add', {
                url: "/employee_add",
                templateUrl: "/Areas/Admin/app/components/employee/employeeAddView.html"
            })
            .state('employee_edit', {
                url: "/employee_edit/:ID",
                templateUrl: "/Areas/Admin/app/components/employee/employeeEditView.html"
            })
            //Customer
            .state('customers', {
                url: "/customer",
                templateUrl: "/Areas/Admin/app/components/customer/customerListView.html"
            })
            .state('customer_add', {
                url: "/customer_add",
                templateUrl: "/Areas/Admin/app/components/customer/customerAddView.html"
            })
            .state('customer_edit', {
                url: "/customer_edit/:ID",
                templateUrl: "/Areas/Admin/app/components/customer/customerEditView.html"
            })
            // feedback
            .state('comments', {
                url: "/comment",
                templateUrl: "/Areas/Admin/app/components/feedback/feedbackListView.html"
            })
            .state('comment_add', {
                url: "/comment_add",
                templateUrl: "/Areas/Admin/app/components/feedback/feedbackAddView.html"
            })
            .state('comment_edit', {
                url: "/comment_edit/:ID",
                templateUrl: "/Areas/Admin/app/components/feedback/feedbackEditView.html"
            })       

            //payment
            .state('payments', {
                url: "/payment",
                templateUrl: "/Areas/Admin/app/components/payment/paymentListView.html"
            })
            .state('payment_add', {
                url: "/payment_add",
                templateUrl: "/Areas/Admin/app/components/payment/paymentAddView.html"
            })
            .state('payment_edit', {
                url: "/payment_edit/:ID",
                templateUrl: "/Areas/Admin/app/components/payment/paymentEditView.html"
            }) 
            //Category
            .state('postCategory', {
                url: "/postCategory",
                templateUrl: "/Areas/Admin/app/components/postCategory/postCategoryListView.html"
            }).state('postCategory_add', {
                url: "/postCategory-add",
                templateUrl: "/Areas/Admin/app/components/postCategory/postCategoryAddView.html"
            }).state('postCategory_edit', {
                url: "/postCategory_edit/:ArticleCategoryID",
                templateUrl: "/Areas/Admin/app/components/postCategory/postCategoryEditView.html"
            })
			//Promotion
			.state('promotions', {
                url: "/promotions",
                templateUrl: "/Areas/Admin/app/components/promotion/promotionListView.html"
            }).state('promotion_add', {
                url: "/promotion-add",
                templateUrl: "/Areas/Admin/app/components/promotion/promotionAddView.html"
            }).state('promotion_edit', {
                url: "/promotion_edit/:PromotionID",
                templateUrl: "/Areas/Admin/app/components/promotion/promotionEditView.html"
            })
            //Bill
            .state('bills', {
                url: "/bill",
                templateUrl: "/Areas/Admin/app/components/bills/billListView.html"
            }).state('bill_Detail', {
                url: "/bill_Detail/:Id",
                templateUrl: "/Areas/Admin/app/components/bills/billDetailView.html"
            })
            .state('analytic', {
                url: "/analytic",
                templateUrl: "/Areas/Admin/app/components/analytic/analyticListView.html"
            })
            .state('analyticExist', {
                url: "/analytic_exist",
                templateUrl: "/Areas/Admin/app/components/analytic/analyticExistListView.html"
            })
            .state('Statistics', {
                url: "/Statistics",
                templateUrl: "/Areas/Admin/app/components/Statistics/ThongKeListController.html"
            })
            .state('BieuDo', {
                url: "/BieuDo",
                templateUrl: "/Areas/Admin/app/components/Statistics/BieuDo.html"
            })
            .state('suppliers', {
                url: "/supplier",
                templateUrl: "/Areas/Admin/app/components/Supplier/SupplierListView.html"
            })
            .state('supplier_add', {
                url: "/add-supplier",
                templateUrl: "/Areas/Admin/app/components/Supplier/SupplierAddView.html"
            })
            .state('supplier_edit', {
                url: "/edit-supplier/:ID",
                templateUrl: "/Areas/Admin/app/components/Supplier/SupplierEditView.html"
            });
    }
})();