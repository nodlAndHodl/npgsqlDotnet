drop view if exists membership.pending_users;
drop table if exists membership.users CASCADE;
drop function if exists membership.the_time();
drop function if exists membership.random_string(int);
drop schema if exists membership;

create schema membership;

create or replace function membership.the_time() returns TIMESTAMPTZ as 
$$
	select now() as result;
$$ language sql;

create or replace function membership.random_string(len int) returns text as
$$
	select substring(md5(random() :: text), 0, len) as result;
$$ language sql;

create table membership.roles(
	id serial primary key not null, 
	name varchar(50) not null
)

create table membership.users_roles(
	user_id int not null,
	membership_id int not null
	
)

create table membership.users(
	id serial primary key not null, 
	user_key varchar(18) not null default membership.random_string(19), 
	email varchar(255) unique not null, 
	first_name varchar(50),
	last_name varchar(50),
	created_at timestamp not null default membership.the_time(),
	status varchar(10) not null default 'pending',
	search_field tsvector not null
);



create trigger users_search_update_refresh
before insert or update on membership.users
for each row execute procedure
tsvector_update_trigger(search_field, 'pg_catalog.english', email, first_name, last_name);

insert into membership.users ( email, first_name, last_name)
values ('shoupnb@gmail1.com', 'nick', 'shoup' ); 

insert into membership.users ( email, first_name, last_name, status)
values ('shoupnb@gmail.com', 'nick', 'shoup', 'not pe'); 

create view membership.pending_users as 
select * from membership.users where status = 'pending';

select * from membership.pending_users where search_field @@ to_tsquery('sho:* & nick');

-- this is another way to vectorize a search, however this is going to be limited in speed if table is extrememly large
select * from membership.users
where to_tsvector(concat(email, ' ', first_name, ' ', last_name )) @@ to_tsquery('sho:* & nick');

select to_tsvector($$ shoupnb@gmail.com nick shoup $$) @@ to_tsquery('NicK');