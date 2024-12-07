using DevFramework.core3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.core3.DataAccess.NHhibernate
{
    public class NhQeryableRepository<T> : IQueryableRepository<T> where T : class, IEntity, new()

    {
        //Taploya abone olmamız lazım bunun için _entities yapıyoruz.Aynı DbSet gibi abone oluyoruz.
        private IQueryable<T> _entities;
        private NHibernateHelper _nHibernateHelper;
        public NhQeryableRepository(NHibernateHelper nHibernateHelper)
        {
            _nHibernateHelper = nHibernateHelper;
        }

      
        public IQueryable<T> Table 
        {
            get
            {
                return this.Entities;
            }               
        }
        public virtual IQueryable<T> Entities 
        {
            get
            {
                if (_entities==null)
                {
                    //_entities eğer null ise Query abone olmamız gerekiyor. 
                    _entities = _nHibernateHelper.OpenSession().Query<T>();
                }
                return _entities;
            }
                
                
               
        }
    }
}
