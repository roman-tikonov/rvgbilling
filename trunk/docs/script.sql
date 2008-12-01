/*
Created		23.09.2008
Modified		29.11.2008
Project		
Model			
Company		
Author		
Version		
Database		PostgreSQL 8.1 
*/



/* Drop Referential Integrity Triggers */





/* Drop User-Defined Triggers */



/* Drop Domains */



/* Drop Procedures */



/* Drop Views */



/* Drop Indexes */



/* Drop Tables */
Drop table "private_abonents" Restrict;
Drop table "corporate_abonents" Restrict;
Drop table "bills" Restrict;
Drop table "numbers" Restrict;
Drop table "users" Restrict;
Drop table "calls" Restrict;
Drop table "rates" Restrict;
Drop table "prices" Restrict;
Drop table "abonents" Restrict;



/* Create Domains */



/* Create Tables */


Create table "abonents"
(
	"abonent_id" Serial NOT NULL,
	"abonent_type" Integer,
	"address" Varchar(100),
	"phone" Varchar(13),
	"reg_time" Timestamp,
	"balance" Real,
	"last_pay_date" Timestamp,
	"mail_address" Varchar(30),
 primary key ("abonent_id")
) Without Oids;


Create table "prices"
(
	"price_id" Serial NOT NULL,
	"rate_id" Integer NOT NULL,
	"cost_per_minute" Real,
	"mask" Varchar(10),
 primary key ("price_id")
) Without Oids;


Create table "rates"
(
	"rate_id" Serial NOT NULL,
	"name" Varchar(100),
 primary key ("rate_id")
) Without Oids;


Create table "calls"
(
	"call_id" Serial NOT NULL,
	"calling_number" Varchar(20),
	"start_time" Timestamp,
	"duration" Interval,
	"number_id" Integer NOT NULL,
	"cost" Real,
 primary key ("call_id")
) Without Oids;


Create table "users"
(
	"user_id" Serial NOT NULL,
	"login" Varchar(20),
	"pass" Varchar(20),
	"rights" Integer,
 primary key ("user_id")
) Without Oids;


Create table "numbers"
(
	"number_id" Serial NOT NULL,
	"abonent_id" Integer NOT NULL,
	"rate_id" Integer NOT NULL,
	"number" Varchar(10),
 primary key ("number_id")
) Without Oids;


Create table "bills"
(
	"bill_id" Serial NOT NULL,
	"abonent_id" Integer NOT NULL,
	"money" Real,
	"bill_date" Timestamp,
 primary key ("bill_id")
) Without Oids;


Create table "corporate_abonents"
(
	"corporate_id" Serial NOT NULL,
	"abonent_id" Integer NOT NULL,
	"company_name" Varchar(100),
	"account" Varchar(20),
	"bank_name" Varchar(100),
 primary key ("corporate_id","abonent_id")
) Without Oids;


Create table "private_abonents"
(
	"private_id" Serial NOT NULL,
	"abonent_id" Integer NOT NULL,
	"surname" Varchar(30),
	"name" Varchar(30),
	"patronymic" Varchar(30),
	"passport_series" Varchar(10),
	"passport_date" Timestamp,
	"passport_department" Varchar(100),
	"birth_date" Timestamp,
 primary key ("private_id","abonent_id")
) Without Oids;



/* Create Tab 'Others' for Selected Tables */


/* Create Alternate Keys */



/* Create Indexes */



/* Create Foreign Keys */

Alter table "numbers" add  foreign key ("abonent_id") references "abonents" ("abonent_id") on update restrict on delete restrict;

Alter table "bills" add  foreign key ("abonent_id") references "abonents" ("abonent_id") on update restrict on delete restrict;

Alter table "corporate_abonents" add  foreign key ("abonent_id") references "abonents" ("abonent_id") on update restrict on delete restrict;

Alter table "private_abonents" add  foreign key ("abonent_id") references "abonents" ("abonent_id") on update restrict on delete restrict;

Alter table "numbers" add  foreign key ("rate_id") references "rates" ("rate_id") on update restrict on delete restrict;

Alter table "prices" add  foreign key ("rate_id") references "rates" ("rate_id") on update restrict on delete restrict;

Alter table "calls" add  foreign key ("number_id") references "numbers" ("number_id") on update restrict on delete restrict;



/* Create Procedures */



/* Create Views */



/* Create Referential Integrity Triggers */





/* Create User-Defined Triggers */



/* Create Roles */



/* Add Roles To Roles */



/* Create Role Permissions */
/* Role permissions on tables */

/* Role permissions on views */

/* Role permissions on procedures */





