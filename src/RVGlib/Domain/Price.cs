﻿using System;
using System.Globalization;

using FluentNHibernate.Framework;
using System.Collections.Generic;

namespace RVGlib.Domain
{
    /// <summary>
    /// Цена
    /// </summary>
    public class Price : Entity
    {
        /// <summary>
        /// Тариф
        /// </summary>
        public virtual Rate     rate { get; set; }
        
        /// <summary>
        /// Маска телефонных номеров
        /// </summary>
        public virtual String   mask { get; set; }

        /// <summary>
        /// Стоимость минуты соединения
        /// </summary>
        public virtual Decimal cost_per_minute { get; set; }

        public virtual string[] ToStringArray()
        {
            return new string[] { rate.name, mask, cost_per_minute.ToString() };
        }

    }

    public class PriceComparer : IEqualityComparer<Price>
    {
        public bool Equals(Price x, Price y)
        {
            return x.mask == y.mask;
        }

        public int GetHashCode(Price obj) { return obj.GetHashCode(); }

    }

}
