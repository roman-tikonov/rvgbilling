﻿using System;

using RVGlib.Domain;
using FluentNHibernate.Framework;
using Iesi.Collections.Generic;
using FluentNHibernate.Cfg;

namespace RVGlib.Framework
{
    class DBConnector
    {
        protected FluentNHibernate.Framework.SessionSource SessionSource { get; set; }
        protected NHibernate.ISession Session { get; private set; }

        //protected DateTime date = DateTime.Today;
        public DBConnector()
        {
            SessionSource = new SessionSource(new Model());
            Session = SessionSource.CreateSession();
        }

        public Entity Get<T>(Int64 id) where T:Entity
        {
            Session.Flush();
            Session.Clear();
            Entity res=Session.Get<T>(id);
            Session.Close();
            return res;
        }

        public void Save<T>(Entity en) where T:Entity
        {
            Session.Flush();
            Session.Clear();
            Session.Save(en);
            Session.Close();
        }

        public void Delete<T>(Entity en) where T : Entity
        {
            Session.Flush();
            Session.Clear();
            Session.Delete(en);
            Session.Close();
        }

        public void Update<T>(Entity en) where T : Entity
        {
            Session.Flush();
            Session.Clear();
            Session.Update(en);
            Session.Close();
        }

    }
}