namespace ESE.Cart.API.Model
{
    public class CartClient
    {
        internal const int MAX_QUANTITY_ITEM = 5;

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

        internal void CalculatePriceCart()
        {
            PriceTotal = Items.Sum(p => p.CalculatePrice());
        }

        internal bool CartItemExist(CartItem item)
        {
            return Items.Any(p => p.ProductId == item.ProductId);
        }

        internal CartItem GetByProductId(Guid productId)
        {
            return Items.FirstOrDefault(p => p.ProductId == productId);
        }    

        internal void AddItem(CartItem item) 
        {
            if (!item.IsValid()) return;

            item.AssociateCart(Id);

            if(CartItemExist(item)) 
            {
                var itemExist = GetByProductId(item.ProductId);
                itemExist.AddUnities(item.Quantidade);
            }

            Items.Add(item);
            CalculatePriceCart();
        }
    }
}
