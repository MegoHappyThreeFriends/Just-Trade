﻿using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace JustTrade.Database
{
    public class NHibernateHelper
    {
        #region Private

        private static ISessionFactory _sessionFactory;

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    var configuration = new Configuration();
                    configuration.Configure();
                    configuration.AddAssembly(typeof(User).Assembly);
                    _sessionFactory = configuration.BuildSessionFactory();
                }
                return _sessionFactory;
            }
        }

        #endregion

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }

        public static void CreateDb()
        {
            var configuration = new Configuration();
            configuration.Configure();
            configuration.AddAssembly(typeof(User).Assembly);
            var schemaExport = new SchemaExport(configuration);
            schemaExport.Create(false, true);
            InsertDefaultData();
        }

        private static void InsertDefaultData()
        {
            var user = new User()
            {
                Name = "demo",
                Login = "demo",
                Password = "demo",
                IsSuperuser = true,
                Permitions = new Permition()
                {
                    Name = "Full permition"
                }
            };
            Repository<User>.Add(user);
        }



    }
}