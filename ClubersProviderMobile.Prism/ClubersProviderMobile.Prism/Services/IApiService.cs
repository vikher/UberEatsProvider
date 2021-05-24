using ClubersProviderMobile.Prism.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ClubersProviderMobile.Prism.Services
{
    public interface IApiService
    {
        bool CheckConnection();
        Task<PutResponse> PutCommandAsync<Orders>(string urlBase, string servicePrefix, string controller, Orders model, string tokenType, string accessToken);
        Task<Response> GetTokenAsync(string urlBase, string servicePrefix, string controller, TokenRequest request);
        /*Task<Response> GetUserByEmail(string urlBase, string servicePrefix, string controller, string tokenType, string accessToken, EmailRequest request);*/
        Task<Response> GetUserByEmailAsync(string urlBase, string servicePrefix, string controller, string email, string tokenType, string accessToken);
        Task<RecoverPasswordResponse> RecoverPasswordAsync(string urlBase, string servicePrefix, string controller, EmailRequest emailRequest);
        Task<PutResponse> NewCommandAsync(string urlBase, string servicePrefix, string controller, string tokenType, string accessToken, Orders orderRequest);
        Task<EstablishmentStaffDetailResponse> GetCollaboratorsDetalisByEstablishmentIDAsync(string urlBase, string servicePrefix, string controller, string tokenType, string accessToken, CollaboratorDetailRequest collaboratorDetailRequest);
        Task<Response> GetUserById(string urlBase, string servicePrefix, string controller, string tokenType, string accessToken);
        //Task<Response> GetAllOrdersAsync<T>(
        //    string urlBase,
        //    string servicePrefix,
        //    string controller,
        //    string tokenType,
        //    string accessToken);

        //Task<Response> GetAllCollaboratorBalancesAsync<T>(
        //    string urlBase,
        //    string servicePrefix,
        //    string controller,
        //    string tokenType,
        //    string accessToken);
        //Task<Response> GetAvailavilityListAsync<T>(
        //    string urlBase,
        //    string servicePrefix,
        //    string controller,
        //    string tokenType,
        //    string accessToken);

        //Task<Response> GetAllProductsAsync<T>(
        //    string urlBase,
        //    string servicePrefix,
        //    string controller,
        //    string tokenType,
        //    string accessToken);
        //Task<Response> GetAllSubmenusAsync<T>(
        //   string urlBase,
        //   string servicePrefix,
        //   string controller,
        //   string tokenType,
        //   string accessToken);

        Task<Response> GetEstablishmentAsync(
            string urlBase,
            string servicePrefix,
            string controller,
            string tokenType,
            string accessToken,
            string id);
        Task<OrdersResponse> GetAllOrdersByEstablishmentIdAsync(string urlBase, string servicePrefix, string controller, int id, string tokenType, string accessToken);
        Task<Response> GetEstablishmentByIdAsync(string urlBase, string servicePrefix, string controller, int id, string tokenType, string accessToken);
        Task<Response> GetTipsByEstablishmentIDAsync(string urlBase, string servicePrefix, string controller, int id, string tokenType, string accessToken);
        Task<Response> GetAllCollaboratorByEstablishmentIDAsync(string urlBase, string servicePrefix, string controller, int id, string tokenType, string accessToken);
        Task<Response> GetAllProductsByEstablishmentIDAsync(string urlBase, string servicePrefix, string controller, int id, string tokenType, string accessToken);
        Task<Response> PutNumberOfPackagesDeliveredAsync(string urlBase, string servicePrefix, string controller, int id, int numberOfPackagesDelivered, string tokenType, string accessToken);
        Task<Response> UpdateTrackingStatus(string urlBase, string servicePrefix, string controller, int id, int statusId, string tokenType, string accessToken);
        Task<Response> UpdateProductAvailable(string urlBase, string servicePrefix, string controller, int id, bool available, string tokenType, string accessToken);
        Task<Response> UpdateEstablishmentReceive(string urlBase, string servicePrefix, string controller, int id, bool receive, string tokenType, string accessToken);
        Task<Response> GetCustomerByINEAsync(string urlBase, string servicePrefix, string controller, string ine, int establishmentId, string tokenType, string accessToken);
        Task<Response> GetCustomerByIdAsync(string urlBase, string servicePrefix, string controller, int id, int establishmentId, string tokenType, string accessToken);
        //Task<Response> GetAllCustomerAsync<T>(
        //    string urlBase,
        //    string servicePrefix,
        //    string controller,
        //    string tokenType,
        //    string accessToken);
        //Task<Response> GetAllCollaboratorsAsync<T>(
        //    string urlBase,
        //    string servicePrefix,
        //    string controller,
        //    string tokenType,
        //    string accessToken);
        Task<Response> GetTipsAsync<T>(
            string urlBase,
            string servicePrefix,
            string controller,
            string tokenType,
            string accessToken);
    }
}
