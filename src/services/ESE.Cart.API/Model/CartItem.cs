namespace ESE.Cart.API.Model
{
    public class CartItem
    {
        public CartItem() 
        { 
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public int Quantidade { get; set; }
        public Decimal Price { get; set; }
        public string Image {  get; set; }

        public Guid CartId { get; set; }

        public CartClient CartClient { get; set; }
    }

}
