﻿using ESE.WebApp.MVC.Models;

namespace ESE.WebApp.MVC.Services
{
    public interface ICartService
    {
        Task<CartViewModel> GetCart();
        Task<ResponseResult> AddItemCart(ItemProductViewModel Product);
        Task<ResponseResult> UpdateItemCart(Guid productId, ItemProductViewModel product);
        Task<ResponseResult> RemoveItemCart(Guid productId);
    }
}
