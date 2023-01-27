using NDS.Models.Domain;
using NDS.Models.Repository;
using System;
using System.Threading.Tasks;

namespace NDS.Models.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {


        CrudGenericMethod<Customer> CustomerManagerUW { get; }
        CrudGenericMethod<City> CityManagerUW { get; }
        CrudGenericMethod<Province> ProvinceManagerUW { get; }

        CrudGenericMethod<Supplier> SupplierManagerUW { get; }

        CrudGenericMethod<Product> ProductManagerUW { get; }

        CrudGenericMethod<Staff> StaffManagerUW { get; }

        CrudGenericMethod<Order> OrderManagerUW { get; }

        CrudGenericMethod<OrderDetails> OrderDetailsManagerUW { get; }
        CrudGenericMethod<StaffSessionReady> StaffSRManagerUW { get; }

        CrudGenericMethod<PublishPlan> PublishPlanManagerUW { get; }

        CrudGenericMethod<AdminUser> AdminUserManagerUW { get; }

        CrudGenericMethod<Payment> PaymentManagerUW { get; }

        CrudGenericMethod<Transaction> TransactionManagerUW { get; }


        CrudGenericMethod<Customer> PersonManagerUW { get; }

        Task<int> saveAsync();

        void save();
    }
}
