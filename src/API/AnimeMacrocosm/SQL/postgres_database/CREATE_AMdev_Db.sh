#!/bin/bash
BASEDIR=$(dirname $0)
psql -U postgres -f "$BASEDIR/AM_dropdb.sql" &&
createdb -U postgres amdev &&
psql -U postgres -d amdev -f "$BASEDIR/AM_schema.sql" &&
psql -U postgres -d amdev -f "$BASEDIR/AM_user.sql" &&
psql -U postgres -d amdev -f "$BASEDIR/AM_data.sql"