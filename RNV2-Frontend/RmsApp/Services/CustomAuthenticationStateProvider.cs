// using System.IdentityModel.Tokens.Jwt;
// using System.Security.Claims;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Components.Authorization;
// using Microsoft.Extensions.Configuration;
// using Blazored.LocalStorage;

// public class CustomAuthenticationStateProvider : AuthenticationStateProvider
// {
//     private readonly IConfiguration configuration;
//     private readonly ILocalStorageService localStorage;

//     public CustomAuthenticationStateProvider(IConfiguration configuration, ILocalStorageService localStorage)
//     {
//         this.configuration = configuration;
//         this.localStorage = localStorage;
//     }

//     public override async Task<AuthenticationState> GetAuthenticationStateAsync()
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

//     public async Task SetAuthenticationStateAsync(string token)
//     {
//         if (string.IsNullOrEmpty(token))
//         {
//             await ClearToken();
//         }
//         else
//         {
//             await SaveToken(token);
//         }
//         var authState = await GetAuthenticationStateAsync();
//         await NotifyAuthenticationStateChangedPublic(authState);
//     }


//     private async Task<string> GetToken()
//     {
//         return await localStorage.GetItemAsync<string>("authToken");
//     }

//     private async Task SaveToken(string token)
//     {
//         await localStorage.SetItemAsync("authToken", token);
//     }

//     private async Task ClearToken()
//     {
//         await localStorage.RemoveItemAsync("authToken");
//     }

//     public async Task NotifyAuthenticationStateChangedPublic(AuthenticationState state)
//     {
//         await base.NotifyAuthenticationStateChanged(state);
//     }


// }
