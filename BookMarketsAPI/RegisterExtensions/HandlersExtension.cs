using Logic.Abstractions.Handlers;
using Models.Requests;
using Models;
using Models.FullEntities;
using Logic.Handlers;
using Models.Pagination.Sorting;

namespace BookMarketsAPI.RegisterExtensions;

public static class HandlersExtension
{
    public static IServiceCollection AddHandlers(
        this IServiceCollection services)
        => services
            .AddScoped<
                IRequestHandler<RequestGetOneByAddress<Id<Address>>, Id<Address>>, 
                AddressIdGetHandler>()
            .AddScoped<IRequestHandler<
                RequestAddEntity<Models.ForCreate.Author>>, 
                AuthorAddHandler>()
            .AddScoped<
                IRequestHandler<RequestDeleteEntityById<Author, Author>>, 
                AuthorDeleteHandler>()
            .AddScoped<IRequestHandler<
                RequestGetManyByLastNameWithPagination<AuthorSorting>, IList<Author>>, 
                AuthorsGetByLastNameHandler>()
            .AddScoped<
                IRequestHandler<RequestGetManyWithPagination<AuthorSorting>, IList<Author>>,
                AuthorsGetHandler>()
            .AddScoped<
                IRequestHandler<RequestUpdateEntity<Author>>,
                AuthorUpdateHandler>()
            .AddScoped<
                IRequestHandler<RequestAddEntity<Models.ForCreate.Book>>,
                BookAddHandler>()
            .AddScoped<
                IRequestHandler<RequestDeleteEntityById<Product, Book>>,
                BookDeleteHandler>()
            .AddScoped<
                IRequestHandler<RequestGetOneById<Product, Book>, Book>,
                BookGetByIdHandler>()
            .AddScoped<
                IRequestHandler<RequestGetManyByIdWithPagination<Author, BookSorting>, IList<Models.SimpleEntities.Book>>,
                BooksGetByAuthorHandler>()
            .AddScoped<
                IRequestHandler<RequestGetManyByKeyWordsWithPagination<BookSorting>, IList<Models.SimpleEntities.Book>>,
                BooksGetByKeyWordsHandler>()
            .AddScoped<
                IRequestHandler<RequestGetManyByNameWithPagination<Product, BookSorting>, IList<Models.SimpleEntities.Book>>,
                BooksGetByNameHandler>()
            .AddScoped<
                IRequestHandler<RequestGetManyWithPagination<BookSorting>, IList<Models.SimpleEntities.Book>>,
                BooksGetHandler>()
            .AddScoped<
                IRequestHandler<RequestRemoveProductFromBooks>,
                BookToProductHandler>()
            .AddScoped<
                IRequestHandler<RequestUpdateEntity<Models.ForUpdate.Book>>,
                BookUpdateHandler>()
            .AddScoped<
                IRequestHandler<RequestAddEntity<Models.ForCreate.Customer>>,
                CustomerAddHandler>()
            .AddScoped<
                IRequestHandler<RequestGetOneByEmail<Customer>, Customer>,
                CustomerGetByEmailHandler>()
            .AddScoped<
                IRequestHandler<RequestGetOneById<Customer, Models.ForUpdate.Customer>, Models.ForUpdate.Customer>,
                CustomerGetByIdHandler>()
            .AddScoped<
                IRequestHandler<RequestGetManyByLastNameWithPagination<CustomerSorting>, IList<Models.ForUpdate.Customer>>,
                CustomersGetByLastNameHandler>()
            .AddScoped<
                IRequestHandler<RequestGetManyByIdByTimeIntervalWithPagination<Shop, CustomerSorting>, IList<Models.ForUpdate.Customer>>,
                CustomersGetByShopIdAndTimeIntervalHandler>()
            .AddScoped<
                IRequestHandler<RequestGetManyWithPagination<CustomerSorting>, IList<Models.ForUpdate.Customer>>,
                CustomersGetHandler>()
            .AddScoped<
                IRequestHandler<RequestUpdateEntity<Customer>>,
                CustomerUpdateHandler>()
            .AddScoped<
                IRequestHandler<RequestAddEntity<Models.ForCreate.Employee>>,
                EmployeeAddHandler>()
            .AddScoped<
                IRequestHandler<RequestDeleteEntityById<Employee, Employee>>,
                EmployeeDeleteHandler>()
            .AddScoped<
                IRequestHandler<RequestGetOneById<Employee, Models.ForUpdate.Employee>, Models.ForUpdate.Employee>,
                EmployeeGetByIdHandler>()
            .AddScoped<
                IRequestHandler<RequestGetOneByLogin<Employee>, Employee>,
                EmployeeGetByLoginHandler>()
            .AddScoped<
                IRequestHandler<RequestGetManyByLastNameWithPagination<EmployeeSorting>, IList<Models.ForUpdate.Employee>>,
                EmployeesGetByLastNameHandler>()
            .AddScoped<
                IRequestHandler<RequestGetManyByIdWithPagination<Shop, EmployeeSorting>, IList<Models.ForUpdate.Employee>>,
                EmployeesGetByShopIdHandler>()
            .AddScoped<
                IRequestHandler<RequestGetManyByIdWithPagination<Warehouse, EmployeeSorting>, IList<Models.ForUpdate.Employee>>,
                EmployeesGetByWarehouseIdHandler>()
            .AddScoped<
                IRequestHandler<RequestGetManyWithPagination<EmployeeSorting>, IList<Models.ForUpdate.Employee>>,
                EmployeesGetHandler>()
            .AddScoped<
                IRequestHandler<RequestUpdateEntity<Models.ForUpdate.Employee>>,
                EmployeeUpdateHandler>()
            .AddScoped<
                IRequestHandler<RequestUpdateEmployeePassword>,
                EmployeeUpdatePasswordHandler>()
            .AddScoped<
                IRequestHandler<RequestAddEntity<Models.ForCreate.Order>>,
                OrderAddHandler>()
            .AddScoped<
                IRequestHandler<RequestGetManyByIdByTimeIntervalWithPagination<Customer, OrderSorting>, IList<Models.ForUpdate.Order>>,
                OrdersGetByCustomerIdAndTimeIntervalHandler>()
            .AddScoped<
                IRequestHandler<RequestGetManyByIdWithPagination<Customer, OrderSorting>, IList<Models.ForUpdate.Order>>,
                OrdersGetByCustomerIdHandler>()
            .AddScoped<
                IRequestHandler<RequestGetManyByIdByTimeIntervalWithPagination<Shop, OrderSorting>, IList<Models.ForUpdate.Order>>,
                OrdersGetByShopIdAndTimeIntervalHandler>()
            .AddScoped<
                IRequestHandler<RequestGetManyByIdWithPagination<Shop, OrderSorting>, IList<Models.ForUpdate.Order>>,
                OrdersGetByShopIdHandler>()
            .AddScoped<
                IRequestHandler<RequestGetManyByTimeIntervalWithPagination<OrderSorting>, IList<Models.ForUpdate.Order>>,
                OrdersGetByTimeIntervalHandler>()
            .AddScoped<
                IRequestHandler<RequestGetManyWithPagination<OrderSorting>, IList<Models.ForUpdate.Order>>,
                OrdersGetHandler>()
            .AddScoped<
                IRequestHandler<RequestUpdateOrderStatus>,
                OrderUpdateStatusHandler>()
            .AddScoped<
                IRequestHandler<RequestAddEntity<Models.ForCreate.Product>>,
                ProductAddHandler>()
            .AddScoped<
                IRequestHandler<RequestUpdateProductCountInEntity<Shop>>,
                ProductCountInShopUpdateHandler>()
            .AddScoped<
                IRequestHandler<RequestUpdateProductCountInEntity<Warehouse>>,
                ProductCountInWarehouseUpdateHandler>()
            .AddScoped<
                IRequestHandler<RequestDeleteEntityById<Product, Product>>,
                ProductDeleteHandler>()
            .AddScoped<
                IRequestHandler<RequestGetOneById<Product, Product>, Product>,
                ProductGetByIdHandler>()
            .AddScoped<
                IRequestHandler<RequestGetManyByIdWithPagination<Shop, ProductCountSorting>, IList<Models.SimpleEntities.ProductCount>>,
                ProductsCountsInShopGetHandler>()
            .AddScoped<
                IRequestHandler<RequestGetManyByIdWithPagination<Warehouse, ProductCountSorting>, IList<Models.SimpleEntities.ProductCount>>,
                ProductsCountsInWarehouseGetHandler>()
            .AddScoped<
                IRequestHandler<RequestGetManyByKeyWordsWithPagination<ProductSorting>, IList<Models.SimpleEntities.Product>>,
                ProductsGetByKeyWordsHandler>()
            .AddScoped<
                IRequestHandler<RequestGetManyByNameWithPagination<Product, ProductSorting>, IList<Models.SimpleEntities.Product>>,
                ProductsGetByNameHandler>()
            .AddScoped<
                IRequestHandler<RequestGetManyWithPagination<ProductSorting>, IList<Models.SimpleEntities.Product>>,
                ProductsGetHandler>()
            .AddScoped<
                IRequestHandler<RequestAddProductInBooks>,
                ProductToBookHandler>()
            .AddScoped<
                IRequestHandler<RequestUpdateEntity<Product>>,
                ProductUpdateHandler>()
            .AddScoped<
                IRequestHandler<RequestAddEntity<Models.ForCreate.Shop>>,
                ShopAddHandler>()
            .AddScoped<
                IRequestHandler<RequestDeleteEntityById<Shop, Shop>>,
                ShopDeleteHandler>()
            .AddScoped<
                IRequestHandler<RequestGetManyWithPagination<ShopSorting>, IList<Shop>>,
                ShopsGetHandler>()
            .AddScoped<
                IRequestHandler<RequestUpdateEntity<Models.ForUpdate.Shop>>,
                ShopUpdateHandler>()
            .AddScoped<
                IRequestHandler<RequestAddEntity<Models.ForCreate.Warehouse>>,
                WarehouseAddHandler>()
            .AddScoped<
                IRequestHandler<RequestDeleteEntityById<Warehouse, Warehouse>>,
                WarehouseDeleteHandler>()
            .AddScoped<
                IRequestHandler<RequestGetManyWithPagination<WarehouseSorting>, IList<Warehouse>>,
                WarehouseGetHandler>()
            .AddScoped<
                IRequestHandler<RequestUpdateEntity<Models.ForUpdate.Warehouse>>,
                WarehouseUpdateHandler>();
}
