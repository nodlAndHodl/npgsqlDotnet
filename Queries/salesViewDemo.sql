
create view raw_sales as
select title, description, length, rating, payment.amount, payment_date,
date_part('quarter', payment_date) as quarter, 
date_part('month', payment_date) as month, 
date_part('year', payment_date) as year,
concat('Q',date_part(
	'quarter', payment_date):: text,
	 '-' ,date_part('year', payment_date)
	 ) as qyear,
cash_words(amount :: money) as spelling_it_out,
to_tsvector(concat(title, description)) as search_field
from film
inner join inventory on inventory.film_id = film.film_id
inner join rental on rental.inventory_id = inventory.inventory_id
inner join payment on payment.rental_id = rental.rental_id;



select * from raw_sales; 
