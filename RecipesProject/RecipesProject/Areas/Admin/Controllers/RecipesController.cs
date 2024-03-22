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
            var applicationDbContext = _context.Recipes.Include(r => r.Category);
            return View(await applicationDbContext.ToListAsync());
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

        // GET: Admin/Recipes/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        // POST: Admin/Recipes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Instructions,PrepTime,CookTime,TotalTime,Servings,Image,CategoryId")] Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                recipe.Id = Guid.NewGuid();
                _context.Add(recipe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", recipe.CategoryId);
            return View(recipe);
        }

        // GET: Admin/Recipes/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var existingRecipe = await _context.Recipes
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
                Ingredients = existingRecipe.RecipeIngredients.Select(ri => new IngredientViewModel
                {
                    Id = ri.Ingredient!.Id,
                    Name = ri.Ingredient.Name,
                    Quantity = ri.IngredientQuanitity,
                }).ToList()
            };

            var categories = await _context.Categories.ToListAsync();
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name", existingRecipe.CategoryId);
            return View(editViewModel);
        }


        // POST: Admin/Recipes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditRecipeViewModel model)
        {
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
                foreach (var ingredientViewModel in model.Ingredients!)
                {
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
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            var categories = await _context.Categories.ToListAsync();
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name", model.CategoryId);
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

        // POST: Admin/Recipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Recipes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Recipes'  is null.");
            }
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe != null)
            {
                _context.Recipes.Remove(recipe);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecipeExists(Guid id)
        {
          return (_context.Recipes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
