drop table if exists users;

create table users(
  id serial primary key not null,
  first varchar(25),
  last varchar(25),
  user_key varchar(12) not null unique default random_value(12),
  email_validated boolean not null default false,
  email_validated_at timestamptz,
  email_validation_token varchar(36) default random_value(36),
  reset_password_token varchar(36),
  reset_password_token_set_at timestamptz,
  current_signin_at timestamptz,
  last_signin_at  timestamptz,
  email varchar(255) unique not null,
  search tsvector,
  created_at timestamptz DEFAULT current_timestamp,
  signin_count int,
  ip inet,
  status varchar(12) default 'pending',
  profile json
);

