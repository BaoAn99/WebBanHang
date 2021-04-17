using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSiteBanDienThoai.Core.Entity.Repositories;

namespace WebSiteBanDienThoai.Core.Entity
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Product { get; }
        IAccountRepository Account { get; }
        ITypeProductRepository TypeProduct { get;}
        ISupplierRepository Supplier { get;}
        IEmployeeRepository Employee { get;}
        IOrderInvoiceRepository OrderInvoice { get; }
        IOrderInvoiceDetailRepository OrderInvoiceDetail { get; }
        ICartRepository Cart { get; }
        ICartDetailRepository CartDetail { get; }
        ICustomerRepository Customer { get; }
        IPromotionRepository Promotion { get;}
        IBillSaleDetailOfSaleRepository BillSaleDetail { get; }
        IBillOfSaleRepository BillOfSale { get; }
        IPaymentRepository Payment { get; }
        IReceiptRepository Receipt { get; }
        IReceiptDetailRepository ReceiptDetail { get; }
        IPayRepository Pay { get; }
        IPayDetailRepository PayDetail { get; }
        ICommentRepository Comment { get; }
        int Complete();
    }
}