using Npgsql;
using System.Collections.Generic;
using System;
namespace HospitalManagement.Database.Abstract;
    public abstract class BaseRepository<T>
            where T : IEntity
    {
        public abstract List<T> GetAll();
        public abstract T GetById(int id);
        public abstract void Insert(T data);
        public abstract void Update(T data);
        public abstract void RemoveById(int id);
    }
