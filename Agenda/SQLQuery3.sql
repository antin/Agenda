-- https://dba.stackexchange.com/questions/96358/how-can-i-map-a-login-to-a-database-using-t-sql-not-ssms
create user ul from login ul;
exec sp_addrolemember 'db_owner', 'ul';