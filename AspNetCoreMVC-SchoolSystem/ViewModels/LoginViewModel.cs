﻿namespace AspNetCoreMVC_SchoolSystem.ViewModels; 
public class LoginViewModel {
    public string UserName { get; set; }
    public string Password { get; set; }
    public string ReturnUrl { get; set; }
    public bool RememberMe { get; set; }
}