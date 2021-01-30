using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MemberManager.Context;
using MemberManager.Models;

namespace MemberManager.Manager
{
    public abstract class AbstractAppEntityManager<T> : AbstractEntityManager<T> where T : AbstractAppEntity
    {
        public AbstractAppEntityManager(MemberContext _db) : base(_db)
        {
       
        }

        public virtual int Removed(Int64 id)
        {
            return Removed(new List<Int64>() { id });
        }

        public virtual int Removed(List<Int64> ids)
        {
            List<T> entities = GetByIds(ids).ToList();
            foreach (T entity in entities)
            {
                if (entity != null && entity.id > 0)
                {
                    entity.removed = true;
                }
            }
            return db.SaveChanges();
        }
    }
}
