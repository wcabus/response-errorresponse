using System.Threading.Tasks;
using ConsoleApp2.Http;

namespace ConsoleApp2.Customers
{
    public class CustomerService : HttpService
    {
        public Task<IResponse<Customer>> GetCustomerById(int id)
        {
            return Get<Customer>($"https://api.myapp.local/customers/{id}");
        }
    }
}