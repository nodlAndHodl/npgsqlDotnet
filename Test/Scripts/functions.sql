CREATE OR REPLACE FUNCTION random_value(len int, out result varchar(32))
as
$$
BEGIN
	SELECT SUBSTRING (md5(random()::text),0,len) into result;
END
$$
Language plpgsql;