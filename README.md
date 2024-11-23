# Database
```bash
sudo docker run -d --restart always --name development-database \
  -p 5432:5432 \
  -e POSTGRES_USER=postgres \
  -e POSTGRES_PASSWORD=developer-password \
  -e POSTGRES_DB=devdb \
  -v /home/carl/Docker/postgres:/var/lib/postgresql/data \
  postgres:17.0
```

# Integration Tests
Add ```appsettings.integrationtest.json``` to the root of the ```Test``` project to run integration tests.