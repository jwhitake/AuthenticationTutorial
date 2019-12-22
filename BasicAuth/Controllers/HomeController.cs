using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;

namespace BasicAuth.Controllers
{

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }


        public IActionResult Authenticate()
        {
            var grandmaClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "Bob"),
                new Claim(ClaimTypes.Email, "|Bob@bob.com"),
                new Claim("Grandma.Says", "Very nice boy")
            };

            var licenseClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "|Bob Stark"),
                new Claim("DrivingLicemse", "A+")
            };

            var grandmaIdentity = new ClaimsIdentity(grandmaClaims, "Grandmas Identity");
            var licenseIdentity = new ClaimsIdentity(licenseClaims, "Goverment");

            var userPrinciple = new ClaimsPrincipal(new[] { grandmaIdentity, licenseIdentity });

            HttpContext.SignInAsync(userPrinciple);

            return RedirectToAction("Index");
        }
    }
}