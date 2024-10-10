#!/bin/bash

container_name=testing_mandatory_mysql
script_dir="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"
database_name=addresses
test_database_name=test_addresses
user=root
password=123123


docker run --name $container_name -e MYSQL_ROOT_PASSWORD=123123 -p 3306:3306 -d mysql
echo "Starting $container_name container..."
sleep 20
echo "Container started. Creating databases..."
docker exec -i $container_name mysql -u${user} -p${password} -e "CREATE DATABASE ${database_name}; CREATE DATABASE ${test_database_name};"
docker exec -i $container_name mysql --user="${user}" --database="${database_name}" --password="${password}" < "${script_dir}/addresses.sql"
docker exec -i $container_name mysql --user="${user}" --database="${test_database_name}" --password="${password}" < "${script_dir}/test-addresses.sql"

if [ $? -eq 0 ]; then
    echo "Databases created successfully!"
else
    echo "Error creating database."
    exit 1
fi

echo "Done!"