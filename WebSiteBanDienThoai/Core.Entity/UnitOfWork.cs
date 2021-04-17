using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSiteBanDienThoai.Core.Entity.Repositories;
using WebSiteBanDienThoai.Entity;
using WebSiteBanDienThoai.Persistence.Repositories;

namespace WebSiteBanDienThoai.Core.Entity
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly QLBHDienThoaiEntities _context;

        public UnitOfWork(QLBHDienThoaiEntities context)
        {
            _context = context;
            Product = new ProductRepository(_context);
            Account = new AccountRepository(_context);
            TypeProduct = new TypeProductRepository(_context);
            Supplier = new SupplierRespository(_context);
            Employee = new EmployeeRepository(_context);
            OrderInvoice = new OrderInvoiceRepository(_context);
            OrderInvoiceDetail = new OrderInvoiceDetailRepository(_context);
            Cart = new CartRepository(_context);
            CartDetail = new CartDetailRepository(_context);
            Promotion = new PromotionRepository(_context);
            Customer = new CustomerRepository(_context);
            BillOfSale = new BillOfSaleRepository(_context);
            BillSaleDetail = new BillSaleDetailOfSaleRepository(_context);
            Payment = new PaymentRepository(_context);
            Receipt = new ReceiptRepository(_context);
            ReceiptDetail = new ReceiptDetailRepository(_context);
            Pay = new PayRepository(_context);
            PayDetail = new PayDetailRepository(_context);
            Comment = new CommentRepository(_context);
        }


        public IProductRepository Product { get; private set; }
        public IAccountRepository Account { get; private set; }        
        public ITypeProductRepository TypeProduct { get; private set; }
        public ISupplierRepository Supplier { get; private set; }
        public IEmployeeRepository Employee { get; private set; }
        public IOrderInvoiceRepository OrderInvoice { get; private set; }
        public IOrderInvoiceDetailRepository OrderInvoiceDetail { get; private set; }
        public ICartRepository Cart { get; private set; }
        public ICartDetailRepository CartDetail { get; private set; }
        public IPromotionRepository Promotion { get; private set; }
        public ICustomerRepository Customer { get; private set; }
        public IBillSaleDetailOfSaleRepository BillSaleDetail { get; private set; }
        public IBillOfSaleRepository BillOfSale { get; private set; }
        public IPaymentRepository Payment { get; private set; }
        public IReceiptRepository Receipt { get; private set; }
        public IReceiptDetailRepository ReceiptDetail { get; private set; }
        public IPayRepository Pay { get; private set; }
        public IPayDetailRepository PayDetail { get; private set; }
        public ICommentRepository Comment { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}