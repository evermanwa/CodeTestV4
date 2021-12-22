using System;
using System.Net.Http;
using CodeTest.Services.dll;

namespace CodeTest.TestStub
{
    class Program
    {
        static void Main()
        {
            TestGetTaxRate();//test getting tax rate from api
            TestCalculateTaxes();// test calculating tax from api
            Console.ReadLine();
        }

        static async void TestGetTaxRate()
        {
            try
            {
                HttpClient client = new HttpClient();

                ITaxService taxService = new TaxService(new TaxJarCalculator(client));
                RateRequest inputData = new RateRequest
                {
                    Zip = "90404 ",
                    City = "Santa Monica",
                    State = "CA",
                    Country = "US"
                };
                RateResponse response = await taxService.GetTaxRate(inputData) as RateResponse;

                Console.WriteLine("Combined Rate: " + response.TaxRate.CombinedRate);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }            
        }

        static async void TestCalculateTaxes()
        {
            try
            {
                HttpClient client = new HttpClient();

                ITaxService taxService = new TaxService(new TaxJarCalculator(client));
                TaxRequest inputData = new TaxRequest()
                {
                    FromCountry = "US",
                    FromZip = "92093",
                    FromState = "CA",
                    FromCity = "La Jolla",
                    FromStreet = "9500 Bilman Drive",
                    ToCountry = "US",
                    ToZip = "90002",
                    ToState = "CA",
                    ToCity = "Los Angelas",
                    ToStreet = "1335 E 103rd St",
                    Amount = 15.0,
                    Shipping = 1.5
                };

                inputData.NexusAddresses.Add(new NexAddress()
                {
                    Id = "Main Location",
                    Country = "US",
                    Zip = "92093",
                    State = "CA",
                    City = "La Jolla",
                    Street = "9500 Gilman Drive"
                });

                inputData.LineItems.Add(new TaxLineItem()
                {
                    Id = "1",
                    Quantity = 1,
                    ProductTaxCode = "20010",
                    UnitPrice = 15.0,
                    Discount = 0.0
                });

                TaxResponse response = await taxService.CalculateTaxes(inputData) as TaxResponse;

                Console.WriteLine("Amount to Collect: " + response.TaxData.AmountToCollect);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }
        }
    }
}