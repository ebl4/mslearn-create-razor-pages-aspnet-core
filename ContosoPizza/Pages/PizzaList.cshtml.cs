using ContosoPizza.Models;
using ContosoPizza.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Contoso.Pages
{
    public class PizzaListModel : PageModel
    {
        private readonly PizzaService _service;
        public IList<Pizza> PizzaList {get; set;} = default!;
        [BindProperty]
        public Pizza NewPizza { get; set; } = default!;

        public PizzaListModel(PizzaService service)
        {
            _service = service;
        }
        
        public void OnGet()
        {
            PizzaList = _service.GetPizzas();
        }

        public IActionResult OnPost()
        {
            // If user's input is invalid, the page method 
            // is called to re-render the page
            if (!ModelState.IsValid || NewPizza == null) 
            {
                return Page();
            }

            // Add a new pizza
            _service.AddPizza(NewPizza);

            // Redirect user to the Get page handler with updated pizzas
            return RedirectToAction("Get");
        }

        public IActionResult OnPostDelete(int id) 
        {
            _service.DeletePizza(id);

            return RedirectToAction("Get");
        }
    }
}
