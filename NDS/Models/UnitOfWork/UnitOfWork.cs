using NDS.Models.Domain;
using NDS.Models.Context;
using NDS.Models.Repository;
using System;
using System.Threading.Tasks;

namespace NDS.Models.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly NDSDbContext _context;

        public UnitOfWork(NDSDbContext context)
        {
            _context = this._context ?? context;


        }

      
        private CrudGenericMethod<Customer> _customerManager;
        private CrudGenericMethod<City> _cityManager;
        private CrudGenericMethod<Province> _provinceManager;
        private CrudGenericMethod<Order> _orderManager;
        private CrudGenericMethod<OrderDetails> _orderDetailsManager;
        private CrudGenericMethod<Staff> _staffManager;
        private CrudGenericMethod<Product> _productManager;
        private CrudGenericMethod<StaffSessionReady> _staffSessionReadyManager;
        private CrudGenericMethod<Supplier> _supplierManager;
        private CrudGenericMethod<PublishPlan> _publishPlanManager;
        private CrudGenericMethod<AdminUser> _adminUserManager;

        private CrudGenericMethod<Payment> _paymentManager;

        private CrudGenericMethod<Transaction> _transactionManager;

        private CrudGenericMethod<Customer> _personManager;



        public CrudGenericMethod<Customer> CustomerManagerUW
        {
            get
            {
                if (_customerManager == null)
                {
                    _customerManager = new CrudGenericMethod<Customer>(_context);
                }

                return _customerManager;
            }
        }



        public CrudGenericMethod<City> CityManagerUW
        {
            get
            {
                if (_cityManager == null)
                {
                    _cityManager = new CrudGenericMethod<City>(_context);
                }

                return _cityManager;
            }
        }

        public CrudGenericMethod<Province> ProvinceManagerUW
        {
            get
            {
                if (_provinceManager == null)
                {
                    _provinceManager = new CrudGenericMethod<Province>(_context);
                }

                return _provinceManager;
            }
        }

        public CrudGenericMethod<Supplier> SupplierManagerUW
        {
            get
            {
                if (_supplierManager == null)
                {
                    _supplierManager = new CrudGenericMethod<Supplier>(_context);
                }

                return _supplierManager;
            }
        }

        public CrudGenericMethod<Product> ProductManagerUW
        {
            get
            {
                if (_productManager == null)
                {
                    _productManager = new CrudGenericMethod<Product>(_context);
                }

                return _productManager;
            }
        }

        public CrudGenericMethod<Staff> StaffManagerUW
        {
            get
            {
                if (_staffManager == null)
                {
                    _staffManager = new CrudGenericMethod<Staff>(_context);
                }

                return _staffManager;
            }
        }

        public CrudGenericMethod<StaffSessionReady> StaffSRManagerUW
        {
            get
            {
                if (_staffSessionReadyManager == null)
                {
                    _staffSessionReadyManager = new CrudGenericMethod<StaffSessionReady>(_context);
                }

                return _staffSessionReadyManager;
            }
        }

        public CrudGenericMethod<Order> OrderManagerUW
        {
            get
            {
                
                if (_orderManager == null)
                {
                    _orderManager = new CrudGenericMethod<Order>(_context);
                }

                return _orderManager;
            }
        }

        public CrudGenericMethod<OrderDetails> OrderDetailsManagerUW
        {
            get
            {
               
                if (_orderDetailsManager == null)
                {
                    _orderDetailsManager = new CrudGenericMethod<OrderDetails>(_context);
                }


                return _orderDetailsManager;
            }
        }

        public CrudGenericMethod<PublishPlan> PublishPlanManagerUW
        {
            get
            {

                if (_publishPlanManager == null)
                {
                    _publishPlanManager = new CrudGenericMethod<PublishPlan>(_context);
                }


                return _publishPlanManager;
            }
        }

        public CrudGenericMethod<AdminUser> AdminUserManagerUW
        {
            get
            {

                if (_adminUserManager == null)
                {
                    _adminUserManager = new CrudGenericMethod<AdminUser>(_context);
                }


                return _adminUserManager;
            }

        }

        public CrudGenericMethod<Payment> PaymentManagerUW
        {
            get
            {

                if (_paymentManager == null)
                {
                    _paymentManager = new CrudGenericMethod<Payment>(_context);
                }


                return _paymentManager;
            }
        }

        public CrudGenericMethod<Transaction> TransactionManagerUW
        {
            get
            {

                if (_transactionManager == null)
                {
                    _transactionManager = new CrudGenericMethod<Transaction>(_context);
                }


                return _transactionManager;
            }
        }

        public CrudGenericMethod<Customer> PersonManagerUW
        {
            

            get
            {

                if (_personManager == null)
                {
                    _personManager = new CrudGenericMethod<Customer>(_context);
                }


                return _personManager;
            }
        }

        public async  Task<int> saveAsync()
        {
            return await  _context.SaveChangesAsync();
        }

        public void save()
        {
            _context.SaveChanges();
        }


        #region Dispose
        protected bool isDisposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                _context.Dispose();
            }

            isDisposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
