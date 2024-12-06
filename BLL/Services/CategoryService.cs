using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface ICategoryService
    {
        public IQueryable<CategoryModel> Query();
        public ServiceBase Create(Category record);
        public ServiceBase Update(Category record);
        public ServiceBase Delete(int id);
    }
    public class CategoryService : ServiceBase, ICategoryService
    {
        public CategoryService(Db db) : base(db)
        {
        }

        public ServiceBase Create(Category record)
        {
            if (_db.Categorys.Any(x => x.Name.ToUpper() == record.Name.ToUpper().Trim()))
                return Error("Category with the same name exists!");
            record.Name = record.Name.Trim();
            _db.Categorys.Add(record);
            _db.SaveChanges();
            return Success("Category created successfully."); throw new NotImplementedException();
        }

        public ServiceBase Delete(int id)
        {
            Category entity = _db.Categorys.Include(x => x.Books).SingleOrDefault(x => x.Id == id);
            if (entity.Books.Any())
                return Error("Category has relational users!");
            _db.Categorys.Remove(entity);
            _db.SaveChanges();
            return Success("Category deleted successfully.");
        }

        public IQueryable<CategoryModel> Query()
        {
            return _db.Categorys.OrderBy(x => x.Name).Select(x => new CategoryModel() { Record = x });
        }

        public ServiceBase Update(Category record)
        {
            if (_db.Categorys.Any(x => x.Id != record.Id && x.Name.ToUpper() == record.Name.ToUpper().Trim()))
            {
                Category entity = _db.Categorys.SingleOrDefault(x => x.Id == record.Id);
                entity.Name = record.Name.Trim();
                if (entity is null)
                    return Error("Category can't be found!");
                _db.Categorys.Update(entity);
                _db.SaveChanges();
                return Success("Category updated successfully.");

            }
            return Error("Category with the same name exists!");
        }
    }
}
