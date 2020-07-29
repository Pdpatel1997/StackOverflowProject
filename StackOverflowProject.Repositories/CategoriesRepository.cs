using System;
using System.Collections.Generic;
using System.Linq;
using StackOverflowProject.DomainModels;

namespace StackOverflowProject.Repositories
{
    public interface ICategoriesRepository
    {
        void insertCategory(Category c);
        void UpdateCategory(Category c);
        void DeleteCategory(int cid);

        List<Category> GetCategories();
        List<Category> GetCategoryByCategaryID(int cid);
        

    }
   public class CategoriesRepository:ICategoriesRepository
    {
        StackOverflowDatabaseDbContext db;
      
        public CategoriesRepository()
        {
            db = new StackOverflowDatabaseDbContext();
         
        }

        public void insertCategory(Category c)
        {
            db.Categories.Add(c);
            db.SaveChanges();
        }

        public void UpdateCategory(Category c)
        {
            Category ct = db.Categories.Where(s => s.CategoryID == c.CategoryID).SingleOrDefault();
            if (ct != null)
            {
                ct.CategoryName = c.CategoryName;
            }
        }
        public void DeleteCategory(int cid)
        {
            Category ct = db.Categories.Where(s => s.CategoryID == cid).SingleOrDefault();
            db.Categories.Remove(ct);
            db.SaveChanges();
        }

        public List<Category> GetCategories()
        {
            List<Category> ct = db.Categories.ToList();
            return ct;
        }
        public List<Category> GetCategoryByCategaryID(int cid)
        {
            List<Category> ct = db.Categories.Where(s => s.CategoryID == cid).ToList();
            return ct;
        }
    }
}
