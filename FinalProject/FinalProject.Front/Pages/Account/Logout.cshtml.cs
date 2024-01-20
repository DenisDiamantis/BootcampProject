// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using FinalProject.Front.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace FinalProject.Front.Pages
{
	public class LogoutModel : PageModel
	{

		private readonly HttpClient _httpClient;


		public LogoutModel(HttpClient httpClient, IContextHelper contextHelper)
		{
			_httpClient = httpClient;


		}
		public async Task<IActionResult> OnGetAsync()
		{
			ContextHelper.Token = null;
			await HttpContext.SignOutAsync("CookieAuthentication");
			return RedirectToPage("./Login");
		}
	}
}


