CREATE SCHEMA tibber;


CREATE TABLE tibber.cleaning_record (
                          id serial PRIMARY KEY,
                          timestamp timestamp NOT NULL,
                          commands integer NOT NULL, 
                          result integer NOT NULL, 
                          duration double precision NOT NULL
);

INSERT INTO tibber.cleaning_record (timestamp, commands, result, duration) VALUES (CURRENT_TIMESTAMP, 2, 4, 0.000123);