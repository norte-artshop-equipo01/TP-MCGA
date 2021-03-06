﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ArtShop.Data
{
    public interface IDataService<T>
    {
        List<ValidationResult> ValidateModel(T model);
        List<T> Get(
            Expression<Func<T, bool>> whereExpression = null, Func<IQueryable<T>,
            IOrderedQueryable<T>> orderFunction = null,
            string includeModels = "");
        T GetById(int id);
        //List<T> Find<T>(Func<T, bool> filter) where T : class;
        T Create(T entity);
        T Update(T entity, object key);
        void Delete(T entity);
        void Delete(int id);
    }
}
