using MemberManager.Context;
using MemberManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberManager.Manager
{
    public abstract class AbstractEntityManager<T> where T : AbstractEntity
    {
        private protected readonly MemberContext db;


        public AbstractEntityManager(MemberContext _db)
        {
            db = _db;
        }

        public abstract IQueryable<T> GetEntitiesQ();

        public virtual IQueryable<T> GetByIds(List<Int64> ids)
        {
            return GetEntitiesQ().Where(item => ids.Contains(item.id));
        }

        public virtual void Delete(List<Int64> ids)
        {
            List<T> entities = GetByIds(ids).ToList();
            foreach (T entity in entities)
            {
                db.Entry(entity).State = EntityState.Deleted;
            }
            db.SaveChanges();
        }

        public virtual void Save(T entity)
        {
            Save(new List<T>() { entity });
        }

        public virtual void Save(List<T> entitys)
        {
            if (entitys != null && entitys.Count > 0)
            {
                foreach (T entity in entitys)
                {
                    if (entity.id == 0)
                    {
                        db.Entry(entity).State = EntityState.Added;
                    }
                }
                db.SaveChanges();
            }
        }
    }
}
