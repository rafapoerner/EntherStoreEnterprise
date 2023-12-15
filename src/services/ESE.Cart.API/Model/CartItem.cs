using FluentValidation;

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
        public string Image { get; set; }

        public Guid CartId { get; set; }

        public CartClient CartClient { get; set; }


        internal void AssociateCart(Guid cartId)
        {
            CartId = cartId;
        }

        internal decimal CalculatePrice()
        {
            return Quantidade * Price;
        }

        internal void AddUnities(int unities)
        {
            Quantidade += unities;
        }

        internal bool IsValid()
        {
            return new ItemOrderValidation().Validate(this).IsValid;
        }

        public class ItemOrderValidation : AbstractValidator<CartItem>
        {
            public ItemOrderValidation()
            {
                RuleFor(c => c.ProductId)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Id do Produto Inválido");

                RuleFor(c => c.Name)
                    .NotEmpty()
                    .WithMessage("O nome do Produto não foi informado");

                RuleFor(c => c.Quantidade)
                    .GreaterThan(0)
                    .WithMessage("A quantidade mínima de um item é 1");

                RuleFor(c => c.Quantidade)
                    .LessThan(CartClient.MAX_QUANTITY_ITEM)
                    .WithMessage($"A quantidade máxima de um item é {CartClient.MAX_QUANTITY_ITEM}");

                RuleFor(c => c.Price)
                    .GreaterThan(0)
                    .WithMessage("O valor do item precisa ser maior que 0");
            }
        }
    }
}
