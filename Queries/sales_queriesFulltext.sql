select distinct title, year, sum(amount) over (partition by title, year):: money
from raw_sales
where search_field @@ to_tsquery('love:*');

select film.film_id, title,
group_concat(first_name || ' ' || last_name ) as actors,
to_tsvector(concat(title, ' ',group_concat(first_name || ' ' || last_name ))) as search_field
from film
inner join film_actor on film.film_id = film_actor.film_id
INNER JOIN actor on actor.actor_id = film_actor.actor_id, 
inner join inventory on inventory.film_id = film.film_id,
inner join rental on rental_inventory_id = inventory.inventory_id 
inner join payment on payment.rental_id = rental.rental_id
group by film.film_id, title;

select * from film;