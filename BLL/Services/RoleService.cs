using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IRoleService
    {
        public IQueryable<RoleModel> Query();
        public ServiceBase Create(Role record);
        public ServiceBase Update(Role record);
        public ServiceBase Delete(int id);
    }


    public class RoleService : ServiceBase,IRoleService
    {
        public RoleService(Db db) : base(db)
        {
        }

        public ServiceBase Create(Role record)
        {
            if (_db.Roles.Any(x => x.Name.ToUpper() == record.Name.ToUpper().Trim()))
                return Error("Role with the same name exists!");
            record.Name = record.Name.Trim();
            _db.Roles.Add(record);
            _db.SaveChanges();
            return Success("Role created successfully.");
        }

        public ServiceBase Delete(int id)
        {
            Role entity = _db.Roles.Include(x => x.Users).SingleOrDefault(x => x.Id == id);
            if (entity.Users.Any())
                return Error("Role has relational users!");
            _db.Roles.Remove(entity);
            _db.SaveChanges();
            return Success("Role deleted successfully.");
        }

        public IQueryable<RoleModel> Query()
        {
            return _db.Roles.OrderBy(x => x.Name).Select(x=>new RoleModel() { Record=x});
        }

        public ServiceBase Update(Role record)
        {
            if (_db.Roles.Any(x => x.Id != record.Id && x.Name.ToUpper() == record.Name.ToUpper().Trim()))
            {
                Role entity = _db.Roles.SingleOrDefault(x => x.Id == record.Id);
                entity.Name = record.Name.Trim();
                if (entity is null)
                    return Error("Role can't be found!");
                _db.Roles.Update(entity);
                _db.SaveChanges();
                return Success("Role updated successfully.");

            }
            return Error("Role with the same name exists!");
            
        }
    }
}
