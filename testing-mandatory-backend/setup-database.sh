#!/bin/bash

container_name=testing_mandatory-mysql
script_dir="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"
database_name=addresses
test_database_name=test_addresses

docker run --name $container_name -e MYSQL_ROOT_PASSWORD=123123 -p 3306:3306 -d mysql
echo "Starting $container_name..."
sleep 20
echo "Container started. Creating database..."
docker exec -i $container_name mysql -uroot -p123123 -e "CREATE DATABASE ${database_name};"
docker exec -i $container_name mysql -uroot -p123123 -e "CREATE DATABASE ${test_database_name};"

if [ $? -eq 0 ]; then
    echo "Database '${database_name}' created successfully!"
else
    echo "Error creating database."
    exit 1
fi

echo "Done!"