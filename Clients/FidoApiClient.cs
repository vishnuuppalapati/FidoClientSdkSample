using FidoClientSdk.Interfaces;
using FidoClientSdk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace FidoClientSdk.Clients
{
    public class FidoApiClient : IFidoApiClient
    {
        private readonly HttpClient _httpClient;
        public FidoApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<MakeCredentialResponse> MakeCredentialRequestAsync(MakeCredentialRequest request)
        {
            var credentialResp = new MakeCredentialResponse();
            var response = await _httpClient.PostAsJsonAsync("/api/fidomakecredentialrequest", request);
            response.EnsureSuccessStatusCode(); // Throws if not 2xx
            var result = await response.Content.ReadFromJsonAsync<MakeCredentialResponse>();
            if (result == null)
            {
                credentialResp.Status = false;
                credentialResp.Message = "Empty response body from server";
            }
            else
            {
               credentialResp = result;
            }
            return credentialResp;
        }

        public async Task<BaseResponse> MakeCredentialResponseAsync(MakeCredentialFinishRequest responseData)
        {
            var baseResp = new BaseResponse();
            var response = await _httpClient.PostAsJsonAsync("/api/fidomakecredentialresponse", responseData);
            if (response.IsSuccessStatusCode)
            {
                baseResp.Status = true;
                baseResp.Message = "Credential Created Successfully.";
            }
            else
            {
                baseResp.Status = false;
                baseResp.Message = "Credential creation failed.";
            }
            return baseResp;
        }

        public async Task<GetAssertionResponse> GetAssertionRequestAsync(GetAssertionRequest requestData)
        {
            var assertionOptionsResp = new GetAssertionResponse();
            var response = await _httpClient.PostAsJsonAsync("/api/fidogetassertion", requestData);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<GetAssertionResponse>();
            if (result == null)
            {
                assertionOptionsResp.Status = false;
                assertionOptionsResp.Message = "Empty response body from server";
            }
            else
            {
                assertionOptionsResp = result;
            }
            return assertionOptionsResp;
        }

        public async Task<BaseResponse> GetAssertionResponseAsync(GetAssertionFinishRequest responseData)
        {
            var baseResp = new BaseResponse();
            var response = await _httpClient.PostAsJsonAsync("/api/fidogetassertionresponse", responseData);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<BaseResponse>();
            return result;
        }
    }
}
