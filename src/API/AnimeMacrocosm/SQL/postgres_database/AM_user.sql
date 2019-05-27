-- ********************************************************************************
-- This script creates the database users and grants them the necessary permissions
-- ********************************************************************************

CREATE USER amdev_owner WITH PASSWORD 'nmh!oKgwkFdZe{Rq5YgW';

GRANT ALL 
ON ALL TABLES IN SCHEMA public
TO amdev_owner;

GRANT ALL 
ON ALL SEQUENCES IN SCHEMA public
TO amdev_owner; 

CREATE USER amdev_appuser WITH PASSWORD '&%+4XUaG:<Fm+s<6f><V';

GRANT SELECT, INSERT, UPDATE, DELETE
ON ALL TABLES IN SCHEMA public
TO amdev_appuser;

GRANT USAGE, SELECT
ON ALL SEQUENCES IN SCHEMA public
TO amdev_appuser; 