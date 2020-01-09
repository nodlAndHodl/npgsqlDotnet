select amount,
payment_date,
date_part('quarter' , payment_date) as quarter,
date_part('year', payment_date) as year
from payment where date_part('year', payment_date) = 2007;