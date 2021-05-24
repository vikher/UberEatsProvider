using ClubersProviderMobile.Prism.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace ClubersProviderMobile.Prism.Services
{
    public class ApiService : IApiService
    {
        //List<Order> Orders = new List<Order>() 
        //        {
        //            new Order(){ Id = 1, PaymentMethod ="Tarjeta de débito", TotalPrice = 100, StatusName = StatusName.onsite, Products = new List<Product>(), QuantityProducts = 3, HourOrder = DateTime.Now.ToShortTimeString(), TableNumber = "mesa 4", Customer = new Customer(), Collaborator = new EstablishmentStaff(), DeliveryMan = new DeliveryMan(), TrackingStatus = TrackingStatus.NewOrder, OrderDate = DateTime.Now},
        //            new Order(){ Id = 2, PaymentMethod ="Tarjeta de débito", TotalPrice = 300, StatusName = StatusName.home, Products = new List<Product>(),  QuantityProducts = 3, HourOrder = DateTime.Now.ToShortTimeString(), TableNumber = null, Customer = new Customer(), Collaborator = new EstablishmentStaff(), DeliveryMan = new DeliveryMan(), TrackingStatus = TrackingStatus.NewOrder, OrderDate = DateTime.Now },
        //            new Order(){ Id = 3, PaymentMethod ="Tarjeta de débito", TotalPrice = 200, StatusName = StatusName.onsite, Products = new List<Product>(), QuantityProducts = 3, HourOrder = DateTime.Now.ToShortTimeString(), TableNumber = "mesa 5", Customer = new Customer(), Collaborator = new EstablishmentStaff(), DeliveryMan = new DeliveryMan(), TrackingStatus = TrackingStatus.InProcess, OrderDate = DateTime.Now },
        //            new Order(){ Id = 4, PaymentMethod ="Efectivo", TotalPrice = 400, StatusName = StatusName.onsite, Products = new List<Product>(),  QuantityProducts = 3, HourOrder = DateTime.Now.ToShortTimeString(), TableNumber = "mesa 4", Customer = new Customer(), Collaborator = new EstablishmentStaff(), DeliveryMan = new DeliveryMan(), TrackingStatus = TrackingStatus.InProcess, OrderDate = DateTime.Now },
        //            new Order(){ Id = 5, PaymentMethod ="Tarjeta de débito", TotalPrice = 600, StatusName = StatusName.onsite, Products = new List<Product>(),  QuantityProducts = 3, HourOrder = DateTime.Now.ToShortTimeString(), TableNumber = "mesa 4", Customer = new Customer(), Collaborator = new EstablishmentStaff(), DeliveryMan = new DeliveryMan(), TrackingStatus = TrackingStatus.Review, OrderDate = DateTime.Now },
        //            new Order(){ Id = 6, PaymentMethod ="Tarjeta de débito", TotalPrice = 100, StatusName = StatusName.cancelled, Products = new List<Product>(),  QuantityProducts = 3, HourOrder = DateTime.Now.ToShortTimeString(), TableNumber = null, Customer = new Customer(), Collaborator = new EstablishmentStaff(), DeliveryMan = new DeliveryMan(), TrackingStatus = TrackingStatus.Ready, OrderDate = DateTime.Now },
        //            new Order(){ Id = 7, PaymentMethod ="Tarjeta de débito", TotalPrice = 100, StatusName = StatusName.home, Products = new List<Product>(),  QuantityProducts = 3, HourOrder = DateTime.Now.ToShortTimeString(), TableNumber = null, Customer = new Customer(), Collaborator = new EstablishmentStaff(), DeliveryMan = new DeliveryMan(), TrackingStatus = TrackingStatus.Ready, OrderDate = DateTime.Now },
        //        };

        //List<EstablishmentStaff> Collaborators = new List<EstablishmentStaff>()
        //        {
        //            new EstablishmentStaff(){ Id = 1, FirstName = "Victor", LastName= "Manuel" },
        //            new EstablishmentStaff(){ Id = 2,  FirstName = "Jair", LastName= "Garcia" },
        //            new EstablishmentStaff(){ Id = 3,  FirstName = "Camila", LastName= "Garcia"},
        //            new EstablishmentStaff(){ Id = 4,  FirstName = "Berenice", LastName= "Garcia" },
        //        };

        //List<CollaboratorBalance> WaiterBalances = new List<CollaboratorBalance>()
        //    {
        //        new CollaboratorBalance(){Id = 1, Amount = 95, Date= DateTime.Now.AddDays(-1), Collaborator = new EstablishmentStaff()},
        //        new CollaboratorBalance(){Id = 2, Amount = 50, Date= DateTime.Now, Collaborator = new EstablishmentStaff()},
        //        new CollaboratorBalance(){Id = 3, Amount = 38, Date= DateTime.Now, Collaborator = new EstablishmentStaff()},
        //        new CollaboratorBalance(){Id = 3, Amount = 80, Date= DateTime.Now, Collaborator = new EstablishmentStaff()}
        //    };

        //List<Availability> AvailabilityList = new List<Availability>()
        //        {
        //            new Availability(){ Id = 1, Name = "Disponible" },
        //            new Availability(){ Id = 2,  Name = "NoDisponible" },
        //        };
        //List<Submenu> Submenus = new List<Submenu>()
        //    {
        //        new Submenu(){Id = 1, Name = "McTrío 3X3", Products = new List<Product>()},
        //        new Submenu(){Id = 2, Name = "Postres", Products = new List<Product>()},
        //        new Submenu(){Id = 3, Name = "Bebidas", Products = new List<Product>()},
        //    };
        //List<Product> Products = new List<Product>()
        //        {
        //            new Product(){ Id = 1, Price = 80, Name = "Hamburguesa clásica", Description = "Carne de res, lechuga,​ tomate,queso, aderezo.", Order = new Order(), Availability = new Availability(){ Id = 1, Name = "Disponible" },ImageUrl = "https://firebasestorage.googleapis.com/v0/b/clubers-278716.appspot.com/o/mctrio.png?alt=media&token=5459b41b-18e6-4c94-a1c3-c479d90b6540", Quantity = 1, TrackingStatus = TrackingStatus.InProcess, Submenu = new Submenu(), Ingredients = new List<Ingredients>(), Additionals = new List<Additional>()  },
        //            new Product(){ Id = 2, Price = 25, Name = "Helado de vainilla", Description = "vainilla", Order = new Order() , Availability =new Availability(){ Id = 2, Name = "NoDisponible" }, ImageUrl = "https://firebasestorage.googleapis.com/v0/b/clubers-278716.appspot.com/o/helado_vainilla.jpg?alt=media&token=a6bea4e4-045f-4a48-86f2-517c402a7cb5", Quantity = 1, TrackingStatus = TrackingStatus.NewOrder, Submenu = new Submenu()},
        //            new Product(){ Id = 3, Price = 15, Name = "Coca cola", Description = "Refresco Individual", Order = new Order(), Availability = new Availability(){ Id = 1, Name = "Disponible" },ImageUrl = "https://firebasestorage.googleapis.com/v0/b/clubers-278716.appspot.com/o/cocacola.jpg?alt=media&token=b1cb760a-e4bf-4c4f-9b7c-d4254e2bb828", Quantity = 1, TrackingStatus = TrackingStatus.InProcess, Submenu = new Submenu() },
        //            new Product(){ Id = 4, Price = 25, Name = "Pay de piña", Description = "Delicioso pay de piña bañado de...", Order = new Order() , Availability =new Availability(){ Id = 1, Name = "Disponible" }, ImageUrl = "https://firebasestorage.googleapis.com/v0/b/clubers-278716.appspot.com/o/pay%20de%20pi%C3%B1a.jpg?alt=media&token=48c6d2c9-eb54-4af6-b6c4-80f38eeced28", Quantity = 1, TrackingStatus = TrackingStatus.InProcess, Submenu = new Submenu()},
        //        };
        //List<Ingredients> Ingredients = new List<Ingredients>()
        //        {
        //            new Ingredients(){Id = 1, Name = "Mayonesa"},
        //            new Ingredients(){Id = 2, Name = "Capsut"},
        //            new Ingredients(){Id = 3, Name = "Chiles" }
        //        };
        //List<Additional> Additionals = new List<Additional>()
        //        {
        //            new Additional(){Id = 1, Name = "Salsa de aguacate", Price = 10},
        //            new Additional(){Id = 2, Name = "Chiles toreados"}
        //        };
        //List<Customer> Customers = new List<Customer>()
        //{
        //    new Customer(){ Id = 1, FirstName = "Berenice", MiddleName = "", LastName = "Monajaras", SecondLastName = "", ImageUrl = "https://firebasestorage.googleapis.com/v0/b/clubers-278716.appspot.com/o/Customer.jpg?alt=media&token=3e969f14-feaf-4ffe-81c4-1f0694a3d8a3", Orders = new List<Order>() },
        //    new Customer(){ Id = 2, FirstName = "Juan Carlos", MiddleName = "", LastName = "Inés", SecondLastName = "", ImageUrl = "https://firebasestorage.googleapis.com/v0/b/clubers-278716.appspot.com/o/Customer3.jpg?alt=media&token=f4f231ce-5065-4dcc-86f0-d52223013342", Orders = new List<Order>() },
        //    new Customer(){ Id = 3, FirstName = "Maria", MiddleName = "Del Carmen", LastName = "Medina", SecondLastName = "Olivier", ImageUrl = "https://firebasestorage.googleapis.com/v0/b/clubers-278716.appspot.com/o/Customer.jpg?alt=media&token=3e969f14-feaf-4ffe-81c4-1f0694a3d8a3", Orders = new List<Order>() },
        //};
        //DeliveryMan DeliveryMan = new DeliveryMan() { Id = 2, FullName = "Ricardo López", ImageUrl = "https://firebasestorage.googleapis.com/v0/b/clubers-278716.appspot.com/o/Customer3.jpg?alt=media&token=f4f231ce-5065-4dcc-86f0-d52223013342" };

        //Establishment Establishment = new Establishment() { Id = 1, Email = "edyslp@outlook.com", ImageUrl = "https://firebasestorage.googleapis.com/v0/b/clubers-278716.appspot.com/o/restaurante.jpg?alt=media&token=1b255adf-fa00-4016-a726-ee88b689205a", Name = "Eddies Restaurante", PhoneNumber = "444 473 00405", OnSiteServiceCloseHours = DateTime.Now, OnSiteServiceOpenHours = DateTime.Now, HomeServiceCloseHours = DateTime.Now, HomeServiceOpenHours = DateTime.Now, Address = new Address() { Id = 1, Street = "Av. Cordillera de los Himalaya 400 lomas 3ra secc", City = "Mexico", State = MexicoStates.SanLuisPotosí, Country = "Mexico", PostalCode = "55210" } };
        List<Tip> Tips = new List<Tip>()
        {
            new Tip(){Id = 1, Amount = 0},
            new Tip(){Id = 1, Amount = 10},
            new Tip(){Id = 2, Amount = 20}
        };
        public ApiService()
        {
            //Seeder();
        }
        //public void Seeder()
        //{
        //    WaiterBalances[0].Collaborator = Collaborators[0];
        //    WaiterBalances[1].Collaborator = Collaborators[1];
        //    WaiterBalances[2].Collaborator = Collaborators[2];
        //    WaiterBalances[3].Collaborator = Collaborators[3];

        //    Products[0].Order = Orders[0];
        //    Products[1].Order = Orders[0];
        //    Products[0].Order = Orders[1];
        //    Products[1].Order = Orders[1];
        //    Products[0].Order = Orders[2];
        //    Products[1].Order = Orders[2];
        //    Products[0].Order = Orders[3];
        //    Products[1].Order = Orders[3];
        //    Products[0].Order = Orders[4];
        //    Products[1].Order = Orders[4];
        //    Products[0].Order = Orders[5];
        //    Products[1].Order = Orders[5];
        //    Orders[0].Products.Add(Products[0]);
        //    Orders[0].Products.Add(Products[1]);
        //    Orders[1].Products.Add(Products[1]);
        //    Orders[1].Products.Add(Products[0]);
        //    Orders[2].Products.Add(Products[0]);
        //    Orders[2].Products.Add(Products[1]);
        //    Orders[3].Products.Add(Products[0]);
        //    Orders[3].Products.Add(Products[2]);
        //    Orders[4].Products.Add(Products[0]);
        //    Orders[4].Products.Add(Products[1]);
        //    Orders[5].Products.Add(Products[1]);
        //    Orders[5].Products.Add(Products[0]);
        //    Orders[6].Products.Add(Products[1]);
        //    Orders[6].Products.Add(Products[0]);

        //    Orders[0].Customer = Customers[0];
        //    Orders[1].Customer = Customers[1];
        //    Orders[2].Customer = Customers[2];
        //    Orders[3].Customer = Customers[0];
        //    Orders[4].Customer = Customers[1];
        //    Orders[5].Customer = Customers[2];
        //    Orders[6].Customer = Customers[0];

        //    Customers[0].Orders.Add(Orders[0]);

        //    Orders[0].Collaborator = Collaborators[0];
        //    Orders[1].Collaborator = Collaborators[1];
        //    Orders[2].Collaborator = Collaborators[1];
        //    Orders[3].Collaborator = Collaborators[2];
        //    Orders[6].Collaborator = Collaborators[3];

        //    Orders[4].DeliveryMan = DeliveryMan;
        //    Orders[5].DeliveryMan = DeliveryMan;           

        //    Products[0].Ingredients.Add(Ingredients[0]);
        //    Products[0].Ingredients.Add(Ingredients[1]);
        //    Products[0].Ingredients.Add(Ingredients[2]);

        //    Products[0].Additionals.Add(Additionals[0]);
        //    Products[0].Additionals.Add(Additionals[1]);

        //    Products[0].Submenu = Submenus[0];
        //    Products[1].Submenu = Submenus[1];
        //    Products[2].Submenu = Submenus[2];
        //    Products[3].Submenu = Submenus[1];

        //    Submenus[0].Products.Add(Products[0]);
        //    Submenus[1].Products.Add(Products[1]);
        //    Submenus[2].Products.Add(Products[2]);
        //    Submenus[1].Products.Add(Products[3]);
        //}
        public bool CheckConnection()
        {
            return Connectivity.NetworkAccess == NetworkAccess.Internet;
        }

        public async Task<Response> GetTokenAsync(string urlBase, string servicePrefix, string controller, TokenRequest request)
        {
            try
            {
                string requestString = JsonConvert.SerializeObject(request);
                StringContent content = new StringContent(requestString, Encoding.UTF8, "application/json");
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };

                string url = $"{servicePrefix}{controller}";
                HttpResponseMessage response = await client.PostAsync(url, content);
                string result = await response.Content.ReadAsStringAsync();

                Response ResponseResult = JsonConvert.DeserializeObject<Response>(result);
                if (ResponseResult.ResultCode != ResultCode.Success)
                {
                    return new Response
                    {
                        ResultCode = ResultCode.Warning,
                        ResultMessages = new List<string> { ResponseResult.ResultMessages[0] }
                    };
                }

                TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(result);
                return new Response
                {
                    ResultCode = ResultCode.Success,
                    //IsSuccess = true,
                    Result = token
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    ResultCode = ResultCode.Warning,
                    ResultMessages = new List<string> { ex.Message }
                };
            }
        }

        public async Task<Response> GetUserById(
            string urlBase,
            string servicePrefix,
            string controller,
            string tokenType,
            string accessToken)
        {
            try
            {
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                string url = $"{servicePrefix}{controller}/{3}";
                HttpResponseMessage response = await client.GetAsync(url);
                string result = await response.Content.ReadAsStringAsync();

                Response ResponseResult = JsonConvert.DeserializeObject<Response>(result);
                if (ResponseResult.ResultCode != ResultCode.Success)
                {
                    return new Response
                    {
                        ResultCode = ResultCode.Warning,
                        ResultMessages = new List<string> { ResponseResult.ResultMessages[0] }
                    };
                }

                UserResponse userResponse = JsonConvert.DeserializeObject<UserResponse>(result);


                return new Response
                {
                    ResultCode = ResultCode.Success,
                    Result = userResponse
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    ResultCode = ResultCode.Warning,
                    ResultMessages = new List<string> { ex.Message }
                };
            }
        }
        public async Task<Response> GetUserByEmailAsync(string urlBase, string servicePrefix, string controller, string email, string tokenType, string accessToken)
        {
            try
            {
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                string url = $"{servicePrefix}{controller}/{email}";
                HttpResponseMessage response = await client.GetAsync(url);
                string answer = await response.Content.ReadAsStringAsync();

                Response ResponseResult = JsonConvert.DeserializeObject<Response>(answer);
                if (ResponseResult.ResultCode != ResultCode.Success)
                {
                    return new Response
                    {
                        ResultCode = ResultCode.Warning,
                        ResultMessages = new List<string> { ResponseResult.ResultMessages[0] }
                    };
                }

                UserResponse userResponse = JsonConvert.DeserializeObject<UserResponse>(answer);
                return new Response
                {
                    ResultCode = ResultCode.Success,
                    Result = userResponse
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    ResultCode = ResultCode.Warning,
                    ResultMessages = new List<string> { ex.Message }
                };
            }
        }
        /*public async Task<Response> GetUserByEmail(
            string urlBase,
            string servicePrefix,
            string controller,
            string tokenType,
            string accessToken,
            EmailRequest request)
        {
            try
            {
                string requestString = JsonConvert.SerializeObject(request);
                StringContent content = new StringContent(requestString, Encoding.UTF8, "application/json");
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                string url = $"{servicePrefix}{controller}";
                HttpResponseMessage response = await client.PostAsync(url, content);
                string result = await response.Content.ReadAsStringAsync();

                Response ResponseResult = JsonConvert.DeserializeObject<Response>(result);
                if (ResponseResult.ResultCode != ResultCode.Success)
                {
                    return new Response
                    {
                        ResultCode = ResultCode.Warning,
                        ResultMessages = new List<string> { ResponseResult.ResultMessages[0] }
                    };
                }

                UserResponse userResponse = JsonConvert.DeserializeObject<UserResponse>(result);
                return new Response
                {
                    ResultCode = ResultCode.Success,
                    Result = userResponse
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    ResultCode = ResultCode.Warning,
                    ResultMessages = new List<string> { ex.Message }
                };
            }
        }*/
        public async Task<RecoverPasswordResponse> RecoverPasswordAsync(string urlBase, string servicePrefix, string controller, EmailRequest emailRequest)
        {
            try
            {
                string request = JsonConvert.SerializeObject(emailRequest);
                StringContent content = new StringContent(request, Encoding.UTF8, "application/json");
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };

                string url = $"{servicePrefix}{controller}";
                HttpResponseMessage response = await client.PostAsync(url, content);
                string answer = await response.Content.ReadAsStringAsync();
                RecoverPasswordResponse obj = JsonConvert.DeserializeObject<RecoverPasswordResponse>(answer);
                return obj;
            }
            catch (Exception ex)
            {
                return new RecoverPasswordResponse
                {
                    result = false,
                    ResultMessages = new List<string> { ex.Message }
                };
            }
        }

        //public async Task<Response> GetAllCollaboratorBalancesAsync<T>(
        //            string urlBase,
        //            string servicePrefix,
        //            string controller,
        //            string tokenType,
        //            string accessToken)
        //{
        //    try
        //    {
        //        //Seeder();
        //        return new Response
        //        {
        //            ResultCode = ResultCode.Success,
        //            //Result = WaiterBalances
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Response
        //        {
        //            ResultCode = ResultCode.Warning,
        //            ResultMessages = new List<string> { ex.Message }
        //        };
        //    }
        //}
        public async Task<OrdersResponse> GetAllOrdersByEstablishmentIdAsync(string urlBase, string servicePrefix, string controller, int id, string tokenType, string accessToken)
        {
            try
            {
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                string url = $"{servicePrefix}{controller}/{id}";
                HttpResponseMessage response = await client.GetAsync(url);
                string answer = await response.Content.ReadAsStringAsync();

                Response ResponseResult = JsonConvert.DeserializeObject<Response>(answer);
                if (ResponseResult.ResultCode != ResultCode.Success)
                {
                    return new OrdersResponse
                    {
                        ResultCode = ResultCode.Warning,
                        ResultMessages = new List<string> { ResponseResult.ResultMessages[0] }
                    };
                }

                OrdersResponse orders = JsonConvert.DeserializeObject<OrdersResponse>(answer);
                return new OrdersResponse
                {
                    ResultCode = ResultCode.Success,
                    Result = orders.Result
                };
            }
            catch (Exception ex)
            {
                return new OrdersResponse
                {
                    ResultCode = ResultCode.Warning,
                    ResultMessages = new List<string> { ex.Message }
                };
            }
        }
        //public async Task<Response> GetAllOrdersAsync<T>(
        //            string urlBase,
        //            string servicePrefix,
        //            string controller,
        //            string tokenType,
        //            string accessToken)
        //{
        //    try
        //    {
        //        //Seeder();
        //        return new Response
        //        {
        //            ResultCode = ResultCode.Success,
        //            //Result = Orders
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Response
        //        {
        //            ResultCode = ResultCode.Warning,
        //            ResultMessages = new List<string> { ex.Message }
        //        };
        //    }
        //}

        public async Task<Response> GetTipsAsync<T>(string urlBase, string servicePrefix, string controller, string tokenType, string accessToken)
        {
            try
            {
                //Seeder();
                return new Response
                {
                    ResultCode = ResultCode.Success,
                    Result = Tips
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    ResultCode = ResultCode.Warning,
                    ResultMessages = new List<string> { ex.Message }
                };
            }
        }

        public async Task<Response> GetTipsByEstablishmentIDAsync(string urlBase, string servicePrefix, string controller, int id, string tokenType, string accessToken)
        {
            try
            {
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                string url = $"{servicePrefix}{controller}/{id}";
                HttpResponseMessage response = await client.GetAsync(url);
                string answer = await response.Content.ReadAsStringAsync();

                Response ResponseResult = JsonConvert.DeserializeObject<Response>(answer);
                if (ResponseResult.ResultCode != ResultCode.Success)
                {
                    return new Response
                    {
                        ResultCode = ResultCode.Warning,
                        ResultMessages = new List<string> { ResponseResult.ResultMessages[0] }
                    };
                }

                TipResponse tipResponse = JsonConvert.DeserializeObject<TipResponse>(answer);
                return new Response
                {
                    ResultCode = ResultCode.Success,
                    Result = tipResponse
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    ResultCode = ResultCode.Warning,
                    ResultMessages = new List<string> { ex.Message }
                };
            }
        }
        public async Task<Response> GetCustomerByINEAsync(string urlBase, string servicePrefix, string controller, string ine, int establishmentId, string tokenType, string accessToken)
        {
            try
            {
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                string url = $"{servicePrefix}{controller}/{ine}/{establishmentId}";
                HttpResponseMessage response = await client.GetAsync(url);
                string answer = await response.Content.ReadAsStringAsync();

                Response ResponseResult = JsonConvert.DeserializeObject<Response>(answer);
                if (ResponseResult.ResultCode != ResultCode.Success)
                {
                    return new Response
                    {
                        ResultCode = ResultCode.Warning,
                        ResultMessages = new List<string> { ResponseResult.ResultMessages[0] }
                    };
                }

                CustomersResponse customer = JsonConvert.DeserializeObject<CustomersResponse>(answer);
                return new Response
                {
                    ResultCode = ResultCode.Success,
                    Result = customer.Result
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    ResultCode = ResultCode.Warning,
                    ResultMessages = new List<string> { ex.Message }
                };
            }
        }
        public async Task<Response> GetCustomerByIdAsync(string urlBase, string servicePrefix, string controller, int id, int establishmentId, string tokenType, string accessToken)
        {
            try
            {
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                string url = $"{servicePrefix}{controller}/{id}/{establishmentId}";
                HttpResponseMessage response = await client.GetAsync(url);
                string answer = await response.Content.ReadAsStringAsync();

                Response ResponseResult = JsonConvert.DeserializeObject<Response>(answer);
                if (ResponseResult.ResultCode != ResultCode.Success)
                {
                    return new Response
                    {
                        ResultCode = ResultCode.Warning,
                        ResultMessages = new List<string> { ResponseResult.ResultMessages[0] }
                    };
                }

                CustomersResponse customer = JsonConvert.DeserializeObject<CustomersResponse>(answer);
                return new Response
                {
                    ResultCode = ResultCode.Success,
                    Result = customer.Result
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    ResultCode = ResultCode.Warning,
                    ResultMessages = new List<string> { ex.Message }
                };
            }
        }
        //public async Task<Response> GetAllCustomerAsync<T>(
        //            string urlBase,
        //            string servicePrefix,
        //            string controller,
        //            string tokenType,
        //            string accessToken)
        //{
        //    try
        //    {
        //        return new Response
        //        {
        //            ResultCode = ResultCode.Success,
        //            Result = Customers
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Response
        //        {
        //            ResultCode = ResultCode.Warning,
        //            ResultMessages = new List<string> { ex.Message }
        //        };
        //    }
        //}

        //public async Task<Response> GetAllCollaboratorsAsync<T>(
        //            string urlBase,
        //            string servicePrefix,
        //            string controller,
        //            string tokenType,
        //            string accessToken)
        //{
        //    try
        //    {
        //        return new Response
        //        {
        //            ResultCode = ResultCode.Success,
        //            Result = Collaborators
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Response
        //        {
        //            ResultCode = ResultCode.Warning,
        //            ResultMessages = new List<string> { ex.Message }
        //        };
        //    }
        //}
        public async Task<PutResponse> NewCommandAsync(string urlBase, string servicePrefix, string controller, string tokenType, string accessToken, Orders orderRequest)
        {
            try
            {
                string requestString = JsonConvert.SerializeObject(orderRequest);
                StringContent content = new StringContent(requestString, Encoding.UTF8, "application/json");
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                string url = $"{servicePrefix}{controller}";
                HttpResponseMessage response = await client.PostAsync(url, content);
                string result = await response.Content.ReadAsStringAsync();

                Response ResponseResult = JsonConvert.DeserializeObject<Response>(result);
                if (ResponseResult.ResultCode != ResultCode.Success)
                {
                    return new PutResponse
                    {
                        ResultCode = ResultCode.Warning,
                        ResultMessages = new List<string> { ResponseResult.ResultMessages[0] }
                    };
                }

                //EstablishmentStaffDetailResponse establishmentStaff = JsonConvert.DeserializeObject<EstablishmentStaffDetailResponse>(result);
                return new PutResponse
                {
                    ResultCode = ResultCode.Success,
                    Result = true
                };
            }
            catch (Exception ex)
            {
                return new PutResponse
                {
                    ResultCode = ResultCode.Warning,
                    ResultMessages = new List<string> { ex.Message }
                };
            }
        }

        public async Task<PutResponse> PutCommandAsync<Orders>(string urlBase, string servicePrefix, string controller, Orders model, string tokenType, string accessToken)
        {
            try
            {
                string request = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(request, Encoding.UTF8, "application/json");
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                string url = $"{servicePrefix}{controller}";
                HttpResponseMessage response = await client.PutAsync(url, content);
                string answer = await response.Content.ReadAsStringAsync();

                Response ResponseResult = JsonConvert.DeserializeObject<Response>(answer);
                if (ResponseResult.ResultCode != ResultCode.Success)
                {
                    return new PutResponse
                    {
                        ResultCode = ResultCode.Warning,
                        ResultMessages = new List<string> { ResponseResult.ResultMessages[0] }
                    };
                }

                //T obj = JsonConvert.DeserializeObject<T>(answer);
                return new PutResponse
                {
                    ResultCode = ResultCode.Success,
                    Result = true
                };
            }
            catch (Exception ex)
            {
                return new PutResponse
                {
                    ResultCode = ResultCode.Warning,
                    ResultMessages = new List<string> { ex.Message }
                };
            }
        }
        public async Task<EstablishmentStaffDetailResponse> GetCollaboratorsDetalisByEstablishmentIDAsync(string urlBase, string servicePrefix, string controller, string tokenType, string accessToken, CollaboratorDetailRequest collaboratorDetailRequest)
        {
            try
            {
                string requestString = JsonConvert.SerializeObject(collaboratorDetailRequest);
                StringContent content = new StringContent(requestString, Encoding.UTF8, "application/json");
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                string url = $"{servicePrefix}{controller}";
                HttpResponseMessage response = await client.PostAsync(url, content);
                string result = await response.Content.ReadAsStringAsync();

                Response ResponseResult = JsonConvert.DeserializeObject<Response>(result);
                if (ResponseResult.ResultCode != ResultCode.Success)
                {
                    return new EstablishmentStaffDetailResponse
                    {
                        ResultCode = ResultCode.Warning,
                        ResultMessages = new List<string> { ResponseResult.ResultMessages[0] }
                    };
                }

                EstablishmentStaffDetailResponse establishmentStaff = JsonConvert.DeserializeObject<EstablishmentStaffDetailResponse>(result);
                return new EstablishmentStaffDetailResponse
                {
                    ResultCode = ResultCode.Success,
                    Result = establishmentStaff.Result
                };
            }
            catch (Exception ex)
            {
                return new EstablishmentStaffDetailResponse
                {
                    ResultCode = ResultCode.Warning,
                    ResultMessages = new List<string> { ex.Message }
                };
            }
        }

        public async Task<Response> GetAllCollaboratorByEstablishmentIDAsync(string urlBase, string servicePrefix, string controller, int id, string tokenType, string accessToken)
        {
            try
            {
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                string url = $"{servicePrefix}{controller}/{id}";
                HttpResponseMessage response = await client.GetAsync(url);
                string answer = await response.Content.ReadAsStringAsync();

                Response ResponseResult = JsonConvert.DeserializeObject<Response>(answer);
                if (ResponseResult.ResultCode != ResultCode.Success)
                {
                    return new Response
                    {
                        ResultCode = ResultCode.Warning,
                        ResultMessages = new List<string> { ResponseResult.ResultMessages[0] }
                    };
                }

                CollaboratorResponse collaborator = JsonConvert.DeserializeObject<CollaboratorResponse>(answer);
                return new Response
                {
                    ResultCode = ResultCode.Success,
                    Result = collaborator
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    ResultCode = ResultCode.Warning,
                    ResultMessages = new List<string> { ex.Message }
                };
            }
        }

        //public async Task<Response> GetAvailavilityListAsync<T>(
        //            string urlBase,
        //            string servicePrefix,
        //            string controller,
        //            string tokenType,
        //            string accessToken)
        //{
        //    try
        //    {
        //        //Seeder();
        //        return new Response
        //        {
        //            ResultCode = ResultCode.Success,
        //            Result = AvailabilityList
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Response
        //        {
        //            ResultCode = ResultCode.Warning,
        //            ResultMessages = new List<string> { ex.Message }
        //        };
        //    }
        //}              
        //public async Task<Response> GetAllSubmenusAsync<T>(
        //   string urlBase,
        //   string servicePrefix,
        //   string controller,
        //   string tokenType,
        //   string accessToken)
        //{
        //    try
        //    {
        //        //Seeder();
        //        return new Response
        //        {
        //            ResultCode = ResultCode.Success,
        //            Result = Submenus
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Response
        //        {
        //            ResultCode = ResultCode.Warning,
        //            ResultMessages = new List<string> { ex.Message }
        //        };
        //    }
        //}
        //public async Task<Response> GetAllProductsAsync<T>(
        //    string urlBase,
        //    string servicePrefix,
        //    string controller,
        //    string tokenType,
        //    string accessToken)
        //{
        //    try
        //    {
        //        //Seeder();
        //        return new Response
        //        {
        //            ResultCode = ResultCode.Success,
        //            Result = Products
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Response
        //        {
        //            ResultCode = ResultCode.Warning,
        //            ResultMessages = new List<string> { ex.Message }
        //        };
        //    }
        //}
        public async Task<Response> GetAllProductsByEstablishmentIDAsync(string urlBase, string servicePrefix, string controller, int id, string tokenType, string accessToken)
        {
            try
            {
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                string url = $"{servicePrefix}{controller}/{id}";
                HttpResponseMessage response = await client.GetAsync(url);
                string answer = await response.Content.ReadAsStringAsync();

                Response ResponseResult = JsonConvert.DeserializeObject<Response>(answer);
                if (ResponseResult.ResultCode != ResultCode.Success)
                {
                    return new Response
                    {
                        ResultCode = ResultCode.Warning,
                        ResultMessages = new List<string> { ResponseResult.ResultMessages[0] }
                    };
                }

                ProductsResponse products = JsonConvert.DeserializeObject<ProductsResponse>(answer);
                return new Response
                {
                    ResultCode = ResultCode.Success,
                    Result = products
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    ResultCode = ResultCode.Warning,
                    ResultMessages = new List<string> { ex.Message }
                };
            }
        }

        public async Task<Response> GetEstablishmentAsync(
            string urlBase,
            string servicePrefix,
            string controller,
            string tokenType,
            string accessToken,
            string id)
        {
            try
            {
                //Seeder();
                return new Response
                {
                    ResultCode = ResultCode.Success,
                    //Result = Establishment
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    ResultCode = ResultCode.Warning,
                    ResultMessages = new List<string> { ex.Message }
                };
            }
        }
        public async Task<Response> GetEstablishmentByIdAsync(string urlBase, string servicePrefix, string controller, int id, string tokenType, string accessToken)
        {
            try
            {
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                string url = $"{servicePrefix}{controller}/{id}";
                HttpResponseMessage response = await client.GetAsync(url);
                string answer = await response.Content.ReadAsStringAsync();

                Response ResponseResult = JsonConvert.DeserializeObject<Response>(answer);
                if (ResponseResult.ResultCode != ResultCode.Success)
                {
                    return new Response
                    {
                        ResultCode = ResultCode.Warning,
                        ResultMessages = new List<string> { ResponseResult.ResultMessages[0] }
                    };
                }

                EstablishmentResponse establishment = JsonConvert.DeserializeObject<EstablishmentResponse>(answer);
                return new Response
                {
                    ResultCode = ResultCode.Success,
                    Result = establishment
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    ResultCode = ResultCode.Warning,
                    ResultMessages = new List<string> { ex.Message }
                };
            }
        }
        public async Task<Response> PutNumberOfPackagesDeliveredAsync(string urlBase, string servicePrefix, string controller, int id, int numberOfPackagesDelivered, string tokenType, string accessToken)
        {
            try
            {
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                string url = $"{servicePrefix}{controller}/{id}/{numberOfPackagesDelivered}";
                HttpResponseMessage response = await client.PutAsync(url, null);
                string answer = await response.Content.ReadAsStringAsync();

                Response ResponseResult = JsonConvert.DeserializeObject<Response>(answer);
                if (ResponseResult.ResultCode != ResultCode.Success)
                {
                    return new Response
                    {
                        ResultCode = ResultCode.Warning,
                        ResultMessages = new List<string> { ResponseResult.ResultMessages[0] }
                    };
                }

                PutResponse PutResult = JsonConvert.DeserializeObject<PutResponse>(answer);
                return new Response
                {
                    ResultCode = ResultCode.Success,
                    Result = PutResult.Result
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    ResultCode = ResultCode.Warning,
                    ResultMessages = new List<string> { ex.Message }
                };
            }
        }
        public async Task<Response> UpdateTrackingStatus(string urlBase, string servicePrefix, string controller, int id, int statusId, string tokenType, string accessToken)
        {
            try
            {
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                string url = $"{servicePrefix}{controller}/{id}/{statusId}";
                HttpResponseMessage response = await client.PutAsync(url, null);
                string answer = await response.Content.ReadAsStringAsync();

                Response ResponseResult = JsonConvert.DeserializeObject<Response>(answer);
                if (ResponseResult.ResultCode != ResultCode.Success)
                {
                    return new Response
                    {
                        ResultCode = ResultCode.Warning,
                        ResultMessages = new List<string> { ResponseResult.ResultMessages[0] }
                    };
                }

                PutResponse PutResult = JsonConvert.DeserializeObject<PutResponse>(answer);
                return new Response
                {
                    ResultCode = ResultCode.Success,
                    Result = PutResult.Result
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    ResultCode = ResultCode.Warning,
                    ResultMessages = new List<string> { ex.Message }
                };
            }
        }
        public async Task<Response> UpdateProductAvailable(string urlBase, string servicePrefix, string controller, int id, bool available, string tokenType, string accessToken)
        {
            try
            {
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                string url = $"{servicePrefix}{controller}/{id}/{available}";
                HttpResponseMessage response = await client.PutAsync(url,null);
                string answer = await response.Content.ReadAsStringAsync();

                Response ResponseResult = JsonConvert.DeserializeObject<Response>(answer);
                if (ResponseResult.ResultCode != ResultCode.Success)
                {
                    return new Response
                    {
                        ResultCode = ResultCode.Warning,
                        ResultMessages = new List<string> { ResponseResult.ResultMessages[0] }
                    };
                }

                PutResponse PutResult = JsonConvert.DeserializeObject<PutResponse>(answer);
                return new Response
                {
                    ResultCode = ResultCode.Success,
                    Result = PutResult.Result
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    ResultCode = ResultCode.Warning,
                    ResultMessages = new List<string> { ex.Message }
                };
            }
        }
        //UpdateEstablishmentReceive
        public async Task<Response> UpdateEstablishmentReceive(string urlBase, string servicePrefix, string controller, int id, bool receive, string tokenType, string accessToken)
        {
            try
            {
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                string url = $"{servicePrefix}{controller}/{id}/{receive}";
                HttpResponseMessage response = await client.PutAsync(url, null);
                string answer = await response.Content.ReadAsStringAsync();

                Response ResponseResult = JsonConvert.DeserializeObject<Response>(answer);
                if (ResponseResult.ResultCode != ResultCode.Success)
                {
                    return new Response
                    {
                        ResultCode = ResultCode.Warning,
                        ResultMessages = new List<string> { ResponseResult.ResultMessages[0] }
                    };
                }

                PutResponse PutResult = JsonConvert.DeserializeObject<PutResponse>(answer);
                return new Response
                {
                    ResultCode = ResultCode.Success,
                    Result = PutResult.Result
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    ResultCode = ResultCode.Warning,
                    ResultMessages = new List<string> { ex.Message }
                };
            }
        }
    }
}
