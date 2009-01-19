﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ExcelWorkLib;

namespace RVGBilling.Import
{
    class ExcelTransmitter : ITransmitter
    {
        public void Export(string filename, string[][] data)
        {
            System.Console.WriteLine("Создаю Excel application...");
            ExcelConnector _ExcelClient = null;
            try
            {
                _ExcelClient = new ExcelConnector(false, filename, true, 1);
                _ExcelClient.SetCellRange(1, 1, data);
            }
            catch (ExcelConnectorException ex) { System.Console.WriteLine(ex.Message); }
            finally
            {
                System.Console.WriteLine("Закрываю Excel application...");
                if (_ExcelClient != null) _ExcelClient.Close();

            }
        }

        // not tested yet
        public string[][] Import(string filename, int RowCount, int ColCount)
        {
            System.Console.WriteLine("Создаю Excel application...");
            ExcelConnector _ExcelClient = null;
            string[][] res = null;
            try
            {
                _ExcelClient = new ExcelConnector(false, filename, false, 1);
                res = _ExcelClient.GetCellRange(1, 1, RowCount, ColCount);
                return res;
            }
            catch (ExcelConnectorException ex) { System.Console.WriteLine(ex.Message); return res; }
            finally
            {
                System.Console.WriteLine("Закрываю Excel application...");
                if (_ExcelClient != null) _ExcelClient.Close();

            }
        }

        // not tested yet
        public string[][] Import(string filename)
        {
            System.Console.WriteLine("Создаю Excel application...");
            ExcelConnector _ExcelClient = null;
            string[][] res = null;
            try
            {
                _ExcelClient = new ExcelConnector(false, filename, false, 1);
                res = _ExcelClient.GetNotEmptyCellRange(1, 1);
                return res;
            }
            catch (ExcelConnectorException ex) { System.Console.WriteLine(ex.Message); return res; }
            finally
            {
                System.Console.WriteLine("Закрываю Excel application...");
                if (_ExcelClient != null) _ExcelClient.Close();

            }
        }
    }
}