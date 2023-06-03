﻿using FiorelloOneToMany.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FiorelloOneToMany.ViewComponents
{
    public class FooterViewComponents:ViewComponent
    {
        private readonly ILayoutService _layoutService;
        public FooterViewComponents(ILayoutService layoutService)
        {
            _layoutService = layoutService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var datas = _layoutService.GetAllDatas();
            return await Task.FromResult(View(datas));
        }
    }
}
