﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

using RVGlib.Domain;

namespace RVGBilling
{
    /// <summary>
    /// Форма для работы с абонентами
    /// </summary>
    public partial class FormAbonent : Form
    {
        Controller ctrl;
        PrivateAbonent abonent;
        BindingSource rateBindingSource, numbersBindingSource;
        bool supported = true;

        public FormAbonent(Controller ctrl)
        {
            InitializeComponent();
            dtStartDate.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            this.ctrl = ctrl;
        }

        public FormAbonent(Controller ctrl, Abonent ab)
 	            : this(ctrl)
        {
            // заполняем combobox с тарифами
            IList<Rate> rates = ctrl.GetRates();
            rateBindingSource = new BindingSource();
            rateBindingSource.DataSource = rates;
            cbRate.DataSource = rateBindingSource;

            if (ab is PrivateAbonent)
            {
                this.abonent = (PrivateAbonent)ab;
                RefreshForm();
            }
            else if (ab is CorporateAbonent)
            {
                MessageBox.Show("Corporate abonents not supported yet!");
                supported = false;
            }
            else
            {
                MessageBox.Show("Incorrect parameter type given in constructor.");
                supported = false;
            }

            // заполняем listbox с номерами
            numbersBindingSource = new BindingSource();
            numbersBindingSource.DataSource = abonent.Numbers;
            lbNumbers.DataSource = numbersBindingSource;

        }

        // не использовать! Устарело!
        public FormAbonent(Controller ctrl,Int64 id)
            : this(ctrl)
        {
            //Int64 id = 1;
            //опасно!
            abonent = (PrivateAbonent)ctrl.conn.Get<PrivateAbonent>(id);
            this.RefreshForm();
        }

        private void lbNumbers_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = lbNumbers.SelectedIndex;
            if (index>=0)
                cbRate.SelectedItem = abonent.Numbers[index].rate;
        }

        /// <summary>
        /// смена тарифа
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChangeRate_Click(object sender, EventArgs e)
        {
            int index = lbNumbers.SelectedIndex;
            if (index >= 0)
            {
                abonent.Numbers[index].rate = (Rate)cbRate.SelectedItem;
                ctrl.conn.Update(abonent);
            }
            numbersBindingSource.ResetBindings(false);
        }
        /// <summary>
        /// добавление нового номера
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddNumber_Click(object sender, EventArgs e)
        {
            ctrl.AddNumber(abonent, rateBindingSource);
            numbersBindingSource.ResetBindings(false);
        }

        /// <summary>
        /// Обновление компонентов формы
        /// </summary>
        private void RefreshForm()
        {
            tbName.Text = abonent.name;
            tbPassport.Text = abonent.passport_series;
            int index = lbNumbers.SelectedIndex;
            if (index >= 0)
                cbRate.SelectedItem = abonent.Numbers[index].rate;
        }

        private void FormAbonent_Load(object sender, EventArgs e)
        {
            if (!supported)
                this.Close();
        }

        private void btnAddMoney_Click(object sender, EventArgs e)
        {
            int index = lbNumbers.SelectedIndex;
            if (index >= 0)
                ctrl.Payment(lbNumbers.SelectedItem.ToString(), 100);
        }

        private void btnGetDetailes_Click(object sender, EventArgs e)
        {
            int index = lbNumbers.SelectedIndex;
            if (index >= 0)
            {
                IList<Call> list = ctrl.GetCalls(abonent.Numbers[index], dtStartDate.Value, dtEndDate.Value);
                FormCallDetails form = new FormCallDetails(list);
                form.ShowDialog();
            }
        }

    }
}
