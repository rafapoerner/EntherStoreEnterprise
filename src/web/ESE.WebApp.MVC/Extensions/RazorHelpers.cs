using Microsoft.AspNetCore.Mvc.Razor;
using System.Security.Cryptography;
using System.Text;

namespace ESE.WebApp.MVC.Extensions
{
    public static class RazorHelpers
    {
        public static string MessageStock(this RazorPage page, int quantity)
        {
            return quantity > 0 ? $"Apenas {quantity} em estoque!" : "Poduto Esgotado!";
        }

        public static string CurrencyFormat(this RazorPage page, decimal price)
        {
            return price > 0 ? string.Format(Thread.CurrentThread.CurrentCulture, "{0:C}", price) : "Gratuito";
        }

        public static string HashMailForGravatar(this RazorPage page, string email)
        {
            var md5Hasher = MD5.Create();
            var data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(email));
            var sBuilder = new StringBuilder();
            foreach (var t in data)
            {
                sBuilder.Append(t.ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}
