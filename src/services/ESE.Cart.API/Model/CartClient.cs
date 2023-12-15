namespace ESE.Cart.API.Model
{
    public class CartClient
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public decimal PriceTotal { get; set; }
        public List<CartItem> Items { get; set;}

        public CartClient(Guid clientId) 
        {
            Id = Guid.NewGuid();
            ClientId = clientId;
        }

        public CartClient() { }
    }
}
