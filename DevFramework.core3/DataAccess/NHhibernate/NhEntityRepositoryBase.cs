using DevFramework.core3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.core3.DataAccess.NHhibernate
{
   public class NhEntityRepositoryBase<TEntity>:IEntityRepository<TEntity>
        where TEntity:class,IEntity,new()
    {
        private NHibernateHelper _hibernateHelper;
        public NhEntityRepositoryBase(NHibernateHelper nHibernateHelper)
        {
            _hibernateHelper = nHibernateHelper;
        }

        public TEntity Add(TEntity entity)
        {
           //Veri tapanına göre bir session acılacak ve _nHibernateHelperden alacak veriyi.
            using (var session = _hibernateHelper.OpenSession())
            {
                session.Save(entity);
                return entity;
            }
        }

        public void Delete(TEntity entity)
        {
            using (var session = _hibernateHelper.OpenSession())
            {
                session.Delete(entity);
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            //Qery=Nhibernate de bilginin veri tabanından alınmasını sağlar.
            //SingleOrDefault: SingleOrDefault anahtar sözcüğü ile dizi içerisinde bulunan elemanlardan belirlenen
            //koşula göre sadece bir tanesinin gelmesini sağlar. Örnekte int dizisinde bulunan elemanlardan belirlenen
            //koşul uyuyor ise o eleman, belirlenen koşulda eleman yok ise sıfır döner.
            using (var session = _hibernateHelper.OpenSession())
            {
                return session.Query<TEntity>().SingleOrDefault(filter);
            }
        }

        public List<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var session = _hibernateHelper.OpenSession())
            {
                return filter == null ? session.Query<TEntity>().ToList()
                    : session.Query<TEntity>().Where(filter).ToList();
            }
        }

        public TEntity Update(TEntity entity)
        {
            using (var session = _hibernateHelper.OpenSession())
            {
                session.Update(entity);
                return entity;
            }
        }
    }
}
