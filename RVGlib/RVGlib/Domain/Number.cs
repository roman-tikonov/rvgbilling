﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentNHibernate.Framework;

namespace RVGlib.Domain
{
    public class Number : Entity
    {
        public Number() { }

        //public Number(String number)
        //{
        //    this.number = number;
        //    //RateAssociation = rate;
        //}

        //public virtual int number_id { get; set; }
        public virtual String number { get; set; }

        //public virtual int abonent_id { get; set; }
        public virtual Abonent abonent { get; set; }
        public virtual Rate rate { get; set; }
        //public virtual int RateId { get; set; }
    }
}