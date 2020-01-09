select generate_series(
 0, 100, 5
);

select x, x*10 from generate_series(
 100, 0, -5
) as f(x);

select x, md5(random()::text) from
 generate_series(100, 0, -1) 
 as f(x);
 
 select x from 
 generate_series('2019-10-01'::TIMESTAMP, '2020-10-01'::TIMESTAMP, '1 day')as f(x);
 
 select trunc(random() * 1000 + 1);
 
  select trunc(random() * 1000 + 1)
  from generate_series(1,1000);