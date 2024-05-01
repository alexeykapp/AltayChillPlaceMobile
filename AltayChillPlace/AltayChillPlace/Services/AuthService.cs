﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using AltayChillPlace.Interface;
using AltayChillPlace.RestClient;

namespace AltayChillPlace.Services
{
    public class AuthService : IAuthService
    {
        private readonly AuthClient _authClient;
        private readonly TokenService _tokenService;

        public AuthService(AuthClient authClient, TokenService tokenService)
        {
            _authClient = authClient;
            _tokenService = tokenService;
        }
        public async Task<bool> LoginAsync(string username, string password)
        {
            try
            {
                var tokenResponse = await _authClient.AuthenticateAsync(username, password);
                if (tokenResponse != null && !string.IsNullOrEmpty(tokenResponse.AccessToken))
                {
                    await _tokenService.SaveTokensAsync(tokenResponse);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Login failed: {ex.Message}");
            }
            return false;
        }
        public async Task<bool> LogoutAsync()
        {
            await _tokenService.ClearTokensAsync();
            return true;
        }
        public async Task<string> GetAccessTokenAsync()
        {
            return await _tokenService.GetAccessTokenAsync();
        }
    }
}
