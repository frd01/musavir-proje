﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Tacmin.Core.Repository
{
    /// <summary>
    /// Daha önce oluşturduğumuz IGenericRepository interface ini implemente eden sınıfımız
    /// Interface üzerinde sadece metot imzalarını yani tanımlarını bildirmiştik.
    /// Bu sınıf arayüzde belirttiğimiz imzalara sahip metotları uygulamak zorunda 
    /// Generic olarak uygulanan Repository pattern tekrarlı kod yazmanın önüne geçerek kodun yönetilebilirliğini kolaylaştırmaktadır.
    /// Bu sınıfın amacı oluşturduğumuz entitylerde yapılacak CRUD işlemlerin tek noktadan yönetilmesi.
    /// Ancak dikkat ettiyseniz burada bir SaveChanges yani değişiklikleri veritabanına ilet komutu bulunmuyor
    /// Bunun nedeni bizim o işlemi UnitofWork ile yapacak oluşumuz.
    /// </summary>
    /// <typeparam name="T">Üzerinde CRUD işlemlerin yapılacağı sınıf</typeparam>
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        #region Variables
        //Bu sınıf içinde kullanılacak değişkenleri tanımlıyoruz
        //Database işlemleri için DbContext tablo işlemleri için ise DbSet sınıfını kullanacağız
        private readonly DbContext _context;
        private readonly DbSet<T> _dbset;
        #endregion

        #region Constructor
        /// <summary>
        /// Yapıcı method bizim için Dependency Injection kullanarak DbContext örneği oluşturuyor.
        /// </summary>
        /// <param name="context"></param>
        public GenericRepository(DbContext context)
        {
            //Değişkenlere değer ataması yapıyoruz
            _context = context;
            _dbset = _context.Set<T>();
        }
        #endregion

        #region GetterMethods
        /// <summary>
        /// Geri doğrudan dbSet nesnesini dönerek tablo içinde her işlemin yapılacağı bir IQueryable result'ı dönüyoruz.
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetAll()
        {
            return _dbset;
        }

        public T Get(Expression<Func<T, bool>> exp)
        {
            return _dbset.Where(exp).FirstOrDefault();
        }

        /// <summary>
        /// İstenen tipteki sınıfı gönderilen Guid tipinde Id parametresi ile arayan ve döndüren metot
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public T Find(int Id)
        {
            return _dbset.Find(Id);
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> exp)
        {
            return _dbset.Where<T>(exp);
        }
        #endregion

        #region SetterMethods
        /// <summary>
        /// Gönderilen entityToUpdate nesnesinin veritabanında update edilmesi için önce Attach ederek daha sonrada durumunu Modified yaparak kuyruğa alan metot
        /// </summary>
        /// <param name="entityToUpdate"></param>
        public T Update(T entityToUpdate)
        {
            _dbset.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
            return entityToUpdate;
        }

        /// <summary>
        /// Gönderilen entity nesnesinin veritabanına eklenmesi için kuyruğa alan metot
        /// </summary>
        /// <param name="entity">Eklenmek istenen sınıfın örneği</param>
        public T Add(T entity)
        {
            _dbset.Add(entity);
            return entity;
        }

        public List<T> AddRange(List<T> entities)
        {
            _dbset.AddRange(entities);
            return entities;
        }

        /// <summary>
        /// Gönderilen Guid tipinde Id parametresi ile aranan kaydı bularak bunu model bekleyen Delete metotuna gönderir
        /// Id üzerinden silme işlemi yapmak isteyen istemciler için tercih edilecektir.
        /// Burada aynı isimle iki adet Delete metodu görüyoruz bu çok biçimlilik yani polimorfizmdir.
        /// Metot isimleri ve döndürdükleri değerler aynı ancak bekledikleri parametre farklıdır bu nedenle derlenme ve çalışmas sırasında
        /// aynı isimli olsalar dahi sorun yaşanmaz. Bu metodu kullanırken overloadlarında 2 seçenek görünecektir. 
        /// </summary>
        /// <param name="Id">Silinmesi istenen kaydın Guid tipinde Id bilgisi</param>
        public void Delete(int Id)
        {
            Delete(Find(Id));
        }

        /// <summary>
        /// Kendisine gönderilen entityToDelete nesnesinin silinmesi için önce attach olup olmadığını kontrol ediyor eğer değil ise attach ederek
        /// silinmesi için kuyruğa ekliyoruz. SaveChanges komutu gelene kadar bu silme işlemi gerçekleşmeyecek. O komutu da UnitodWork sınıfında vereceğiz.
        /// </summary>
        /// <param name="entityToDelete"></param>
        public void Delete(T entityToDelete)
        {
            _dbset.Remove(entityToDelete);
        }

        public void Delete(Expression<Func<T, bool>> exp)
        {
            var row = Get(exp);
            if (row != null)
                _dbset.Remove(row);
        }

        public T AddIfNotExists(T entity, Expression<Func<T, bool>> predicate = null)
        {
            var exists = predicate != null ? _dbset.Any(predicate) : _dbset.Any();
            return !exists ? _dbset.Add(entity) : null;
        }

        #endregion
    }
}
