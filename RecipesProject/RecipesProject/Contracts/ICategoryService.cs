﻿using RecipesProject.Data.Entities;

namespace RecipesProject.Contracts
{
    public interface ICategoryService
    {
        Task<List<Category>> AllCategoryAsync();
    }
}
