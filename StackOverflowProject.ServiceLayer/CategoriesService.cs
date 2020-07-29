using System;
using System.Collections.Generic;
using System.Linq;
using StackOverflowProject.DomainModels;
using StackOverflowProject.Repositories;
using StackOverflowProject.ViewModels;
using AutoMapper;
using AutoMapper.Configuration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.AccessControl;

namespace StackOverflowProject.ServiceLayer
{
    public interface ICategoriesService
    {
        void InsertCategory(CategoryViewModel cvm);
        void UpdateCategory(CategoryViewModel cvm);
        void DeleteCategory(int CategoryID);
        List<CategoryViewModel> GetCategories();
        CategoryViewModel GetCategoryByCategoryID(int CategoryID);
    }
    public class CategoriesService : ICategoriesService
    {
        ICategoriesRepository cr;
        public CategoriesService()
        {
            cr = new CategoriesRepository();
        }
        public void InsertCategory(CategoryViewModel cvm)
        {
            MapperConfiguration config = new MapperConfiguration(cfg => { cfg.CreateMap<CategoryViewModel, Category>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Category c = mapper.Map<CategoryViewModel, Category>(cvm);
            cr.insertCategory(c);
        }
        public void UpdateCategory(CategoryViewModel cvm)
        {
            MapperConfiguration config = new MapperConfiguration(cfg => { cfg.CreateMap<CategoryViewModel, Category>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Category c = mapper.Map<CategoryViewModel, Category>(cvm);
            cr.UpdateCategory(c);
        }

        public void DeleteCategory(int CategoryID)
        {
            cr.DeleteCategory(CategoryID);
        }
        public List<CategoryViewModel> GetCategories()
        {
            List<Category> c = cr.GetCategories();
            MapperConfiguration confing = new MapperConfiguration(cfg => { cfg.CreateMap<Category, CategoryViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = confing.CreateMapper();
            List<CategoryViewModel> cvm = mapper.Map<List<Category>, List<CategoryViewModel>>(c);
            return cvm;
        }
        public CategoryViewModel GetCategoryByCategoryID(int CategoryID)
        {
           Category c = cr.GetCategoryByCategaryID(CategoryID).FirstOrDefault();
            CategoryViewModel cvm = null;
            if (c != null)
            {
                MapperConfiguration config = new MapperConfiguration(cfg => { cfg.CreateMap<Category, CategoryViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                cvm = mapper.Map<Category, CategoryViewModel>(c);
            }
            return cvm;
        }
    }
}
