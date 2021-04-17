var server={
	methods:{
		_request: function(ptype, purl, pdata, psuccress, pfailure, perror) {// Hàm dùng ajax để gửi request lên server
			jQuery.ajax({
				type: ptype,
				url: purl,
				dataType: 'json',
				contentType: "application/json; charset=utf-8",
				data: JSON.stringify(pdata),
				success: function(response) {
					psuccress(response);
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
}
var service = {
	methods:{
		_convertNumberToPrice:function(firstSymbol,number,lastSymbol){
			if(firstSymbol==null){
				return Number((number).toFixed(1)).toLocaleString()+" "+lastSymbol.toLowerCase();
			}
			return firstSymbol.toLowerCase()+" "+Number((number).toFixed(1)).toLocaleString();
		},
		_convertPriceToNumber:function(price,symbol){
			return (price.replace(symbol,"").trim().replace(/,/g,""));
		}
	}
}
var cart={
	properties:{
		dataSource:{},
		pageName:"",
		products:[],		
		totalPrice:0,		
		table:{
			properties:{
				id:"#cart-table",
				rows:{
					properties:{
						currentProductId:"",
						currentTotalPrice:0
					}
				},
				columns:{
					properties:{
						promotionId:""
					}
				}
			},
			events:{
				
			},
			methods:{
				_handlingPromotion:function(productId){
					var dataRequest = {};dataRequest.prod = {};dataRequest.prom = {};
					dataRequest.prod.ProductID = productId;
					dataRequest.prom.PromotionName = $("#tr-"+productId+" .promotion").val();
					cart.properties.table.properties.rows.properties.currentProductId = productId;
					cart.properties.table.properties.rows.properties.currentTotalPrice = $("#price-input-"+productId).val()*$("#tr-"+productId+" .amount")[0].value;;
					server.methods._request("POST","/Cart/CheckPromotion",dataRequest,function(response){
						if(response.status)
						{
							notify.methods.success._showDialog(response.mess,"../Cart");
							var productId = cart.properties.table.properties.rows.properties.currentProductId;
							var row = cart.properties.table.methods._getRow(cart.properties.table.properties.rows.properties.currentProductId);
							row.PromotionID = response.obj.PromotionID;
							var price = row.PriceProduct-row.PriceProduct*(response.obj.SaleOff/100);
							var rowTotalPriceCurrent = service.methods._convertPriceToNumber($("#tr-"+productId+" .total-price")[0].innerHTML,"₫");
							var rowTotalPrice = row.Amount*row.PriceProduct;
							var rowTotalPriceNew = price*row.Amount;
							$("#tr-"+productId+" .price")[0].innerHTML = service.methods._convertNumberToPrice(null,price,"₫");
							$("#total-price-"+productId)[0].innerHTML = service.methods._convertNumberToPrice(null,rowTotalPriceNew,"₫");
							var totalPrice = $("#total-price-hidden").val();
							$("#total-price-hidden").val(totalPrice-(rowTotalPriceCurrent-rowTotalPriceNew));
							cart.methods._resetTotalPrice(service.methods._convertNumberToPrice(null,totalPrice-(rowTotalPriceCurrent-rowTotalPriceNew),"₫"));
						}else{
							notify.methods.warning._showDialog(response.mess,"../Cart");
							var row = cart.properties.table.methods._getRow(cart.properties.table.properties.rows.properties.currentProductId);
							var productId = row.ProductID;
							row.PromotionID = null;
							var price = row.PriceProduct;
							var rowTotalPriceCurrent = service.methods._convertPriceToNumber($("#tr-"+productId+" .total-price")[0].innerHTML,"₫");
							var rowTotalPriceNew = row.Amount*row.PriceProduct;
							$("#tr-"+productId+" .price")[0].innerHTML = service.methods._convertNumberToPrice(null,price,"₫");
							$("#total-price-"+productId)[0].innerHTML = service.methods._convertNumberToPrice(null,rowTotalPriceNew,"₫");
							var totalPrice = $("#total-price-hidden").val();
							$("#total-price-hidden").val(totalPrice-(rowTotalPriceCurrent-rowTotalPriceNew));
							cart.methods._resetTotalPrice(service.methods._convertNumberToPrice(null,totalPrice-(rowTotalPriceCurrent-rowTotalPriceNew),"₫"));
						}
					},function(response){},function(response){});
				},
				_getRow:function(productId){
					return cart.properties.dataSource.find(x=>x.ProductID===productId);
				}
			}
		}		
	},
	events:{
		_init:function(){
			$("#add-to-cart").click(function(){
				cart.methods._insert($("#product-id")[0].defaultValue,$("#product-amount").val());
			});
		}
	},
	methods:{
		_insert:function(pramId,pramAmount){
			var product = {};
			product.ProductID = pramId;
			product.Amount = pramAmount;
			server.methods._request("POST","/Product/AddToCart",product,function(response){
				cart.properties.dataSource = response.obj;
				cart.properties.products=response.obj;	
				$("#my-cart #amount")[0].innerHTML = response.obj.length;
				if(response.status){
					notify.methods.success._showDialog(response.mess,"../Cart");
				}
				else{
					notify.methods.info._showDialog(response.mess,"../Cart");
				}
			},function(response){},function(response){});	
		},
		_delete:function(pramId){
			var product = {};
			product.ProductID = pramId;
			$("#tr-"+pramId).remove();
			server.methods._request("POST","/Product/DeleteToCart",product,function(response){
				cart.properties.dataSource = response.obj;
				cart.properties.products=response.obj;
				cart.methods._resetTotalPrice(response.total);
				$("#my-cart #amount")[0].innerHTML = response.obj.length;
				notify.methods.warning._showDialog(response.mess,"../Cart");
				if(cart.properties.pageName=="checkOut"&&$("#total-price")[0].innerHTML=="0đ")
				{
					window.location.href = "../Cart";					
				}
			},function(response){},function(response){});
		},	
		_update:function(pramId)
		{
			var product = {};
			product.ProductID = pramId;
			product.Amount = $("#tr-"+pramId+" .amount")[0].value;
			cart.properties.table.properties.rows.properties.currentProductId = pramId;
			cart.properties.table.properties.rows.properties.currentTotalPrice = $("#price-input-"+pramId).val()*product.Amount;
			server.methods._request("POST","/Product/EditToCart",product,function(response){
				cart.properties.dataSource = response.obj;
				cart.properties.products=response.obj;
				cart.methods._resetTotalPrice(response.total);
				$("#total-price-"+pramId)[0].innerHTML = Number(cart.properties.table.properties.rows.properties.currentTotalPrice.toFixed(1)).toLocaleString()+" ₫".toLowerCase();
			},function(response){},function(response){});
		},
		_resetTotalPrice:function(totalPrice){
			$("#total-price")[0].innerHTML = totalPrice;
		},
		_order:function(){
			var requestData = {};requestData.cart = {};
			requestData.customer = {};
			requestData.customer.Fullname = $("#customer .Fullname").val();
			requestData.customer.Phone = $("#customer .Phone").val();
			requestData.customer.Birthday = $("#customer .Birthday").val();
			requestData.customer.Gender = $("#customer .Gender").val();
            requestData.customer.Address = $("#customer .Address").val();

            requestData.cart.PaymentID = $("#PaymentId").val();
			requestData.cart.CartDetails = [];
			for(var i=0;i<cart.properties.dataSource.length;i++){
				var cartDetail = {};
				cartDetail.Amount = cart.properties.dataSource[i].Amount;
				cartDetail.ProductID = cart.properties.dataSource[i].ProductID;
				cartDetail.PromotionID = cart.properties.dataSource[i].PromotionID;
				cartDetail.PriceProduct = cart.properties.dataSource[i].PriceProduct;
				requestData.cart.CartDetails.push(cartDetail);
			}
            server.methods._request("POST", "/Cart/Oder", requestData, function (response) {                
                notify.methods.success._showDialog("Đặt hàng thành công!", "../Cart");				
				window.location.href = "/Account/Index";
				//$("#total-price-"+pramId)[0].innerHTML = Number(cart.properties.table.properties.rows.properties.currentTotalPrice.toFixed(1)).toLocaleString()+" ₫".toLowerCase();				
			},function(response){},function(response){});
		}
	},
	_init:function(){
		server.methods._request("POST","/Cart/getCart",null,function(response){
			cart.properties.dataSource = response.obj;
		},function(response){},function(response){});
	}
}
cart._init();
