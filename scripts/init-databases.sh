#!/bin/bash
set -e

echo "Creating multiple databases..."

# Create databases
psql -v ON_ERROR_STOP=1 --username "$POSTGRES_USER" --dbname "$POSTGRES_DB" <<-EOSQL
    -- Create databases for microservices
    CREATE DATABASE identity_db;
    CREATE DATABASE station_db;
    CREATE DATABASE session_db;
    CREATE DATABASE staff_db;
    
    -- Grant all privileges to postgres user (optional, but explicit)
    GRANT ALL PRIVILEGES ON DATABASE identity_db TO postgres;
    GRANT ALL PRIVILEGES ON DATABASE station_db TO postgres;
    GRANT ALL PRIVILEGES ON DATABASE session_db TO postgres;
    GRANT ALL PRIVILEGES ON DATABASE staff_db TO postgres;
EOSQL

echo "✅ Databases created successfully!"
echo "📋 Available databases:"
echo "  - identity_db"
echo "  - station_db"
echo "  - session_db"
echo "  - staff_db"