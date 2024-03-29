﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RVGlib.Domain
{
    /// <summary>
    /// Абонент - физ.лицо
    /// </summary>
    public class PrivateAbonent : Abonent
    {
        /// <summary>
        /// Фамилия
        /// </summary>
        public virtual String   surname { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        public virtual String   name { get; set; }
        /// <summary>
        /// Отчество
        /// </summary>
        public virtual String   patronymic { get; set; }
        /// <summary>
        /// Серия и номер паспорта
        /// </summary>
        public virtual String   passport_series { get; set; }
        /// <summary>
        /// Дата выдачи паспорта
        /// </summary>
        public virtual DateTime passport_date { get; set; }
        /// <summary>
        /// Кем выдан паспорт
        /// </summary>
        public virtual String   passport_department { get; set; }
        /// <summary>
        /// Дата рождения
        /// </summary>
        public virtual DateTime birth_date { get; set; }

        public override string[] ToStringArray()
        {
            string[] res = new string[] {  
                surname, name, patronymic, passport_series.ToString(), passport_date.ToShortDateString(), 
                passport_department, birth_date.ToShortDateString()};
            string[] buf = base.ToStringArray();
            Array.Resize(ref res, res.Length + buf.Length);
            for (int i = 0; i < buf.Length; i++)
            {
                res[res.Length - i - 1] = buf[buf.Length - i - 1];
            }
            return res;
        }
    }
}
