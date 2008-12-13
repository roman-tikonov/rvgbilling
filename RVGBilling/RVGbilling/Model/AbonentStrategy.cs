﻿using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;
using NpgsqlTypes;
using System.Data;

namespace RVGbilling.Model
{
    class AbonentStrategy : IDBStrategy
    {
        NpgsqlConnection _Connection;



        public AbonentStrategy()
        {

        }

        public AbonentStrategy(NpgsqlConnection conn)
        {
            setConnection(conn);
        }

        #region IDBStrategy Members

        public void setConnection(NpgsqlConnection conn)
        {
            _Connection = conn;
        }


        protected virtual void formCommandVals(Abonent buf, NpgsqlCommand command)
        {
            command.Parameters[0].Value = buf.Address;
            command.Parameters[1].Value = buf.Phone;
            command.Parameters[2].Value = buf.MailAddress;
            command.Parameters[3].Value = buf.RegTime;
            command.Parameters[4].Value = buf.LastPayDate;
            command.Parameters[5].Value = buf.Balance;
        }

        protected virtual void formCommandParams(NpgsqlCommand command)
        {
            command.Parameters.Add(new NpgsqlParameter("1", NpgsqlDbType.Varchar));
            command.Parameters.Add(new NpgsqlParameter("2", NpgsqlDbType.Varchar));
            command.Parameters.Add(new NpgsqlParameter("3", NpgsqlDbType.Varchar));
            command.Parameters.Add(new NpgsqlParameter("4", NpgsqlDbType.Timestamp));
            command.Parameters.Add(new NpgsqlParameter("5", NpgsqlDbType.Timestamp));
            command.Parameters.Add(new NpgsqlParameter("6", NpgsqlDbType.Numeric));
        }

        protected virtual string InsertQueryString
        {
            get
            {
                return "INSERT INTO abonents (address, phone, mail_address, reg_time, last_pay_date, balance) "
                  + "VALUES (:1,:2,:3,:4,:5,:6)";
            }
        }
        protected virtual string UpdateQueryString
        {
            get
            {
                return "UPDATE abonents SET address = :1, phone = :2, mail_address = :3, reg_time = :4, last_pay_date = :5, balance = :6 "
                    + "WHERE abonent_id = :id";
            }
        }
        protected virtual string DeleteQueryString
        {
            get
            {
                return "DELETE FROM abonents "
                    + "WHERE abonent_id = :id";
            }
        }


        public List<DBAbstractObject> getFromDB(string query)
        {
            List<DBAbstractObject> res = new List<DBAbstractObject>();
            DBConnector dbc = new DBConnector();
            NpgsqlConnection conn = dbc.Connection;
            try
            {                
                conn.Open();
                NpgsqlCommand command = conn.CreateCommand();
                command.CommandText = "select * from abonents ";
                NpgsqlDataReader dr = command.ExecuteReader();
                Abonent buf;
                while (dr.Read())
                {
                    
                    
                    buf = new Abonent(Convert.ToInt32(dr["abonent_id"]), (String)dr["address"], (String)dr["phone"],
                        (String)dr["mail_address"], (DateTime)dr["reg_time"], (DateTime)dr["last_pay_date"],
                        (Decimal)dr["balance"]);//2fix: int -> Integer, float -> ?
                    res.Add(buf);
                }
            }
            catch (NpgsqlException ex) { logger.log(ex.ToString()); }
            finally
            {
                conn.Close();
            }

            return res;
        }

        public int addItemToDB(DBAbstractObject item)
        {
            //Abonent buf = (Abonent)item;
            DBConnector dbc = new DBConnector();
            NpgsqlConnection conn = dbc.Connection;
            try
            {
                conn.Open();
                NpgsqlCommand command = conn.CreateCommand();
                command.CommandText = InsertQueryString;
                formCommandParams(command);
                command.Prepare();
                formCommandVals((Abonent)item, command);
                command.ExecuteNonQuery();
                
                NpgsqlCommand command2 = conn.CreateCommand();
                command2.CommandText = "SELECT last_value FROM abonents_abonent_id_seq";
                //NpgsqlParameter idparameter = new NpgsqlParameter("abonent_id", NpgsqlDbType.Integer);
                //idparameter.Direction = ParameterDirection.Output;

                NpgsqlDataReader dr = command2.ExecuteReader();
                Int64 id = -1;
                while (dr.Read())
                    id = (Int64)dr[0];
                return (int)id;

            }
            catch (NpgsqlException ex) { logger.log(ex.ToString()); return -1; }
            finally
            {
                conn.Close();
            }         
        }

        public void updateItemToDB(DBAbstractObject item)
        {
            //Abonent buf = (Abonent)item;
            DBConnector dbc = new DBConnector();
            NpgsqlConnection conn = dbc.Connection;
            try
            {
                conn.Open();
                NpgsqlCommand command = conn.CreateCommand();
                command.CommandText = UpdateQueryString;

                formCommandParams(command);
                command.Parameters.Add(new NpgsqlParameter("id", NpgsqlDbType.Integer));

                command.Prepare();

                formCommandVals((Abonent)item, command);
                command.Parameters["id"].Value = item.Id;

                command.ExecuteNonQuery();
            }
            catch (NpgsqlException ex) { logger.log(ex.ToString()); }
            finally
            {
                conn.Close();
            }
        }

        public void removeItemFromDB(DBAbstractObject item)
        {
            Abonent buf = (Abonent)item;//проверка типов
            DBConnector dbc = new DBConnector();
            NpgsqlConnection conn = dbc.Connection;
            try
            {
                conn.Open();
                NpgsqlCommand command = conn.CreateCommand();
                command.CommandText = DeleteQueryString;
 
                command.Parameters.Add(new NpgsqlParameter("id", NpgsqlDbType.Integer));

                command.Prepare();

                command.Parameters["id"].Value = item.Id;

                command.ExecuteNonQuery();
            }
            catch (NpgsqlException ex) { logger.log(ex.ToString());  }
            finally
            {
                conn.Close();
            }
        }

        #endregion
    }
}