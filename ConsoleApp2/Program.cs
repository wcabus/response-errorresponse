using System;
using System.Threading.Tasks;
using ConsoleApp2.Customers;
using ConsoleApp2.Http;

namespace ConsoleApp2
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new CustomerService();
            var response = await client.GetCustomerById(404);
            if (response is IErrorResponse<Customer> error)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"{error.StatusCode} - {error.Error}");
                if (error.HasException)
                {
                    Console.WriteLine(error.Exception);
                }

                Console.ResetColor();
               
                return;
            }

            Console.WriteLine(response.Value);
        }
    }
}
