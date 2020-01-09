select distinct first_name from actor;

select count(*) as co, first_name 
from actor
group by first_name 
having count(*) > 3
order by count(*) desc;

select first_name, count(*) over (partition by first_name) as name_count
from actor;

with actor_rollup as
(
	select actor_id, first_name, last_name, 
	count(*) over (partition by first_name) as name_count, 
	count(*) over (partition by last_name) as last_name_occurance
	
	from actor
)
select first_name, last_name, name_count, last_name_occurance  from actor_rollup order by actor_id desc;

--
WITH expression_name[(column_name [,...])]
AS
    (CTE_definition)
SQL_statement;