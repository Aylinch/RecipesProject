using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RecipesProject.Areas.Admin.Models;
using RecipesProject.Data;
using RecipesProject.Data.Entities;
using RecipesProject.Models;
using RecipesProject.Models.RecipeViewModels;

namespace RecipesProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RecipesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RecipesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Recipes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Recipes
                .Where(r => r.IsApproved == false)
                .Include(r => r.Category);
            return View("Index", await applicationDbContext.ToListAsync());
        }

        // GET: Admin/Recipes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Recipes == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes
                .Include(r => r.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        // GET: Admin/Recipes/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var existingRecipe = await _context.Recipes
                .Include(c => c.Category)
                .Include(r => r.RecipeIngredients)
                    .ThenInclude(ri => ri.Ingredient)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (existingRecipe == null)
            {
                return NotFound();
            }
            var editViewModel = new EditRecipeViewModel
            {
                Id = existingRecipe.Id,
                Title = existingRecipe.Title,
                Description = existingRecipe.Description,
                Instructions = existingRecipe.Instructions,
                PrepTime = existingRecipe.PrepTime,
                CookTime = existingRecipe.CookTime,
                TotalTime = existingRecipe.TotalTime,
                Servings = existingRecipe.Servings,
                Image = existingRecipe.Image,
                CategoryId = existingRecipe.CategoryId,
                CategoryName = existingRecipe?.Category?.Name,
                Ingredients = existingRecipe?.RecipeIngredients.Select((ri, index) => new IngredientViewModel
                {
                    Index = index,
                    Id = ri.Ingredient!.Id,
                    Name = ri.Ingredient.Name,
                    Quantity = ri.IngredientQuanitity,
                }).ToList()
            };

            var categories = await _context.Categories.Where(x => x.Name != editViewModel.CategoryName).Select(x => new CategoryViewModel
            {
                Id = x.Id,
                Name = x.Name,
            }).ToListAsync();
            editViewModel.Categories = categories;
            return View(editViewModel);
        }


        // POST: Admin/Recipes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] Guid id, EditRecipeViewModel model)
        {
            if (model == null)
            {
                return BadRequest(); 
            }

            if (ModelState.IsValid)
            {
                var existingRecipe = await _context.Recipes
                    .Include(r => r.RecipeIngredients)
                    .FirstOrDefaultAsync(r => r.Id == model.Id);

                if (existingRecipe == null)
                {
                    return NotFound();
                }

                existingRecipe.Title = model.Title;
                existingRecipe.Description = model.Description;
                existingRecipe.Instructions = model.Instructions;
                existingRecipe.PrepTime = model.PrepTime;
                existingRecipe.CookTime = model.CookTime;
                existingRecipe.TotalTime = model.TotalTime;
                existingRecipe.Servings = model.Servings;
                existingRecipe.Image = model.Image;
                existingRecipe.CategoryId = model.CategoryId;

                existingRecipe.RecipeIngredients.Clear();
                if (model.Ingredients != null)
                {
                    foreach (var ingredientViewModel in model.Ingredients)
                    {
                        if (ingredientViewModel == null)
                        {
                            continue; 
                        }

                        var ingredient = new Ingredient
                        {
                            Id = ingredientViewModel.Id,
                            Name = ingredientViewModel.Name
                        };

                        existingRecipe.RecipeIngredients.Add(new RecipeIngredients
                        {
                            RecipeId = existingRecipe.Id,
                            Ingredient = ingredient,
                            IngredientQuanitity = ingredientViewModel.Quantity
                        });
                    }
                }

                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            var categories = await _context.Categories
                .Where(x => x.Name != model.CategoryName)
                .Select(x => new CategoryViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                }).ToListAsync();

            model.Categories = categories;
            return View(model);
        }

        // GET: Admin/Recipes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Recipes == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes
                .Include(r => r.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }
        // POST: Admin/Recipes/DeleteConfirmed/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveRecipe(Guid id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe != null)
            {
                recipe.IsApproved = true;
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        private bool RecipeExists(Guid id)
        {
            return (_context.Recipes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
