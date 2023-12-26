namespace ESE.WebApp.MVC.Models
{
    public class CartViewModel
    {
        public Decimal PriceTotal { get; set; }
        public List<ItemProductViewModel> Items { get; set; }
    }

    public class ItemProductViewModel
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public int Quantidade { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
    }
}
