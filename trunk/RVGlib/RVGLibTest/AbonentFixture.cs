﻿using System;

using NUnit.Framework;
using RVGlib.Domain;
using FluentNHibernate.Framework;
using Iesi.Collections.Generic;

namespace RVGLibTest
{
    [TestFixture]
    public class AbonentFixture : BaseFixture
    {

        [Test]
        public void Can_Add_Abonent_To_Database_Revisited()
        {
            Decimal bal = 0;
            //DateTime date = DateTime.Today;
            new PersistenceSpecification<Abonent>(Session)
                .CheckProperty(x => x.address, "addr")
                .CheckProperty(x => x.phone, "123")
                .CheckProperty(x => x.mail_address, "q@mail.ru")
                .CheckProperty(x => x.reg_time, date)
                .CheckProperty(x => x.last_pay_date, date)
                .CheckProperty(x => x.balance, bal)               
                //.CheckList<Number>(x => x.Numbers, numbers)
                .VerifyTheMappings();
        }

        [Test]
        public void Can_Add_Abonent_To_Database_WithNumbers()
        {
            Decimal bal = 1;
            //DateTime date = DateTime.Today;
            var Abonent = new Abonent
            {
                address = "a",
                phone = "p",
                mail_address = "m",
                reg_time = date,
                last_pay_date = date,
                balance = bal
            };
            Session.Save(Abonent);

            Int64 rateid = 1;
            var Number = new Number { number = "111", abonent = Abonent, rate = Session.Get<Rate>(rateid) };
            Session.Save(Number);
            Abonent.Numbers.Add(Number);

            Number = new Number { number = "222", abonent = Abonent, rate = Session.Get<Rate>(rateid) };
            Session.Save(Number);
            Abonent.Numbers.Add(Number);

            Session.Update(Abonent);
            Session.Flush();
            Session.Clear();

            var fromDb = Session.Get<Abonent>(Abonent.Id);

            Assert.AreEqual(fromDb.Numbers.Count, 2);

            Assert.AreEqual(fromDb.Numbers[1].Id, Number.Id);

        }
    }
}
