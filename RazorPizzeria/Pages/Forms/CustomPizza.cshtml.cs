using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPizzeria.Models;
using System.Reflection;

namespace PizzaCreator.Pages.Forms
{
    public class CustomPizzaModel : PageModel
    {
        [BindProperty]
        public PizzasModel Pizza { get; set; }

        public float PizzaPrice { get; set; }
        public IActionResult OnPost()
        {
            PizzaPrice = Pizza.BasePrice;

            foreach (PropertyInfo propertyInfo in Pizza.GetType().GetProperties())
            {
                if (propertyInfo.PropertyType == typeof(bool))
                {
                    bool value = (bool)propertyInfo.GetValue(Pizza, null);

                    if (value)
                    {
                        PizzaPrice++;
                    }
                }
            }

            return RedirectToPage("../Checkout/Checkout", new { Pizza.PizzaName, PizzaPrice });
        }
    }
}