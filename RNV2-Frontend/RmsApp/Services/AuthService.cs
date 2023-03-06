using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Net.Http;
using RmsApp.Dtos;
using RmsApp.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Components;


public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly IJSRuntime _jsRuntime;
    private readonly IFlashMessageService _flashMessageService;
    public static LoginDto User { get; set; }
    public static bool IsLoggedIn { get; set; }

    public AuthService(HttpClient httpClient, IFlashMessageService flashMessageService, IJSRuntime jsRuntime)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(Constants.IdentityUri);
        _flashMessageService = flashMessageService;
        _jsRuntime = jsRuntime;
    }

    public async Task<bool> LoginAsync(string username, string password)
    {
        var response = await _httpClient.PostAsJsonAsync("api/auth/login", new { UserName = username, Password = password });
        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("Login successfully");
            User = await response.Content.ReadFromJsonAsync<LoginDto>();
            Console.WriteLine("user role is " + User.Role);
            IsLoggedIn = true;
            Console.WriteLine("login status is" + IsLoggedIn);
            if (User.Logo != null)
            {
                //User.Logo = Utils.BuildLogoPath(User.Logo);
            }
            return true;
        }
        Console.WriteLine("Login failed, login again");
        return false;
    }

    public void Logout()
    {
        IsLoggedIn = false;
    }
}
