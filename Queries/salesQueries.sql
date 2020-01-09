select title, sum(amount), quarter, year 
from raw_sales
group by (title, quarter, year)
order by title;


with sales_rollup as(
	select distinct title, year,
	sum(amount) over (partition by year, title) as "Quarterly Sales",
	sum(amount) over (partition by year) as "Total Year",
	sum(amount) over (partition by title,year)/sum(amount) over (partition by year) *100 as "Percent Total"
	from raw_sales
	order by title
)

select sum("Percent Total") from sales_rollup; --percent
