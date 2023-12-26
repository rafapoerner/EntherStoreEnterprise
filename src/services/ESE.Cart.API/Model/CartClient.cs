using FluentValidation;
using FluentValidation.Results;

namespace ESE.Cart.API.Model
{
    public class CartClient
    {
        internal const int MAX_QUANTITY_ITEM = 5;

        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public decimal PriceTotal { get; set; }
        public List<CartItem> Items { get; set;}
        public ValidationResult ValidationResult { get; set; }

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
            return Items != null && Items.Any(p => p.ProductId == item.ProductId);
        }

        internal CartItem GetByProductId(Guid productId)
        {
            return Items.FirstOrDefault(p => p.ProductId == productId);
        }

        internal void AddItem(CartItem item)
        {
            // Inicialize a lista se ainda não foi feito.
            if (Items == null)
            {
                Items = new List<CartItem>();
            }

            item.AssociateCart(Id);

            if (CartItemExist(item))
            {
                var itemExist = GetByProductId(item.ProductId);
                itemExist.AddUnities(item.Quantidade);
            }

            Items.Add(item);
            CalculatePriceCart();
        }

        internal void UpdateItem(CartItem item)
        {
            item.AssociateCart(Id);

            var itemExist = GetByProductId(item.ProductId);

            Items.Remove(itemExist);
            Items.Add(item);

            CalculatePriceCart();
        }

        internal void UpdateUnities(CartItem item, int unities) 
        {
            item.UpdateUnities(unities);
            UpdateItem(item);
        }

        internal void RemoveItem(CartItem item)
        {
            Items.Remove(GetByProductId(item.ProductId));

            CalculatePriceCart();
        }

        internal bool IsValid()
        {
            var errors = Items.SelectMany(i => new CartItem.ItemCartValidation().Validate(i).Errors).ToList();
            errors.AddRange(new CartClientValidation().Validate(this).Errors);
            ValidationResult = new ValidationResult(errors);

            return ValidationResult.IsValid;

        }

        public class CartClientValidation : AbstractValidator<CartClient>
        {
            public CartClientValidation()
            {
                RuleFor(c => c.ClientId)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Cliente não reconhecido");

                RuleFor(c => c.Items.Count)
                    .GreaterThan(0)
                    .WithMessage("O carrinho não possui itens");

                RuleFor(c => c.PriceTotal)
                    .GreaterThan(0)
                    .WithMessage("O valor total do carrinho precisa der maior que 0");
            }
        }
    }
}
