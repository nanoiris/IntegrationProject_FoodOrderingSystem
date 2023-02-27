// using System.IdentityModel.Tokens.Jwt;
// using System.Security.Claims;
// using Microsoft.AspNetCore.Components.Authorization;
// using Microsoft.JSInterop;
// using System.Net.Http;
// using RmsApp.Dtos;
// using RmsApp.Services;
// using System;
// using System.Linq;
// using System.Threading.Tasks;
// using System.Collections.Generic;
// using System.Diagnostics;
// using System.Net.Http;
// using System.Net.Http.Json;
// using System.Text;
// using System.Text.Json;
// using System.Threading.Tasks;
// using Microsoft.Extensions.Logging;
// using Microsoft.AspNetCore.Components;


// public class AuthService : IAuthService
// {
//     private readonly HttpClient _httpClient;
//     private readonly IJSRuntime _jsRuntime;
//     private readonly IFlashMessageService _flashMessageService;
//     private readonly AuthenticationStateProvider _authStateProvider;

//     public AuthService(HttpClient httpClient, IFlashMessageService flashMessageService, IJSRuntime jsRuntime, AuthenticationStateProvider authStateProvider)
//     {
//         _httpClient = httpClient;
//         _httpClient.BaseAddress = new Uri("http://localhost:5191/");
//         _flashMessageService = flashMessageService;
//         _jsRuntime = jsRuntime;
//         _authStateProvider = authStateProvider;
//     }

//     public async Task<AuthenticationState> GetAuthenticationStateAsync()
//     {
//         var token = await GetToken();
//         if (string.IsNullOrEmpty(token))
//         {
//             return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
//         }
//         var handler = new JwtSecurityTokenHandler();
//         var jwtToken = handler.ReadJwtToken(token);
//         var identity = new ClaimsIdentity(jwtToken.Claims, "jwt");
//         var user = new ClaimsPrincipal(identity);

//         return new AuthenticationState(user);
//     }

//     public async Task<string> GetUserRestaurantId()
//     {
//         var token = await GetToken();
//         if (token == null)
//         {
//             return null;
//         }
//         var handler = new JwtSecurityTokenHandler();
//         var jwtToken = handler.ReadJwtToken(token);
//         var claim = jwtToken.Claims.FirstOrDefault(c => c.Type == "UserRestId");
//         if (claim == null)
//         {
//             return null;
//         }
//         return claim.Value;
//     }

//     private async Task<string> GetToken()
//     {
//         return await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");
//     }

//     private async Task SetAuthState(string token)
//     {
//         if (string.IsNullOrEmpty(token))
//         {
//             await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "authToken");
//             await _authStateProvider.GetAuthenticationStateAsync(); // force refresh
//             return;
//         }
//         await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "authToken", token);
//         var handler = new JwtSecurityTokenHandler();
//         var jwtToken = handler.ReadJwtToken(token);
//         var identity = new ClaimsIdentity(jwtToken.Claims, "jwt");
//         var user = new ClaimsPrincipal(identity);
//         var authState = new AuthenticationState(user);
//         await ((CustomAuthenticationStateProvider)_authStateProvider).NotifyAuthenticationStateChangedPublic(authState);
//     }

//     public async Task<bool> LoginAsync(string username, string password)
//     {
//         var loginRequest = new LoginDto
//         {
//             UserName = username,
//             Password = password
//         };
//         var response = await _httpClient.PostAsJsonAsync("/api/auth/login", loginRequest);
//         if (response.IsSuccessStatusCode)
//         {
//             var loginResult = await response.Content.ReadFromJsonAsync<LoginResult>();
//             await SetAuthState(loginResult.Token);
//             return true;
//         }
//         return false;
//     }

//     public async Task LogoutAsync()
//     {
//         await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "authToken");
//         var authState = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
//         await ((CustomAuthenticationStateProvider)_authStateProvider).NotifyAuthenticationStateChangedPublic(authState);
//     }

// }
