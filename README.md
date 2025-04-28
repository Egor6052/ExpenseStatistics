# ExpenseStatistics

**ExpenseStatistics** — програма для швидкого аналізу витрат та фінансової статистики.

---
**_Content:_**
- [Task](#task-4)
- [General requirements](#general-requirements)
- [API](#api-for-testing)
- [Create app and build](#create-app-and-build)
- [Installing SDK and runtime machine](#installing-sdk-and-runtime-machine)
- [Managing PostgreSQL](#managing-postgresql)
- [Creating user in DB and table](#creating-user-in-db-and-table)
- [Test SQL scripts with data](#test-sql-scripts-with-data)

---
### Task 4:

API для отслеживания личных финансов.

**_Сущности:_** ТРАНЗАКЦИЯ, КАТЕГОРИЯ

**_Функционал:_**
Добавление категорий
Внесение доходов/расходов
Получение финансовой статистики (детализированной и сводной) за все время/за предыдущий месяц/за текущий месяц


_**Будет плюсом:**_ авторизация(транзакции привязываются к пользователю, получить можно только свою статистику), Статистика по категориям

---
### General requirements
https://docs.google.com/document/d/1z8VsEIK7fHzdaFKA2aUiP9kfheydpGkaO0p1NJZBc-Q/edit?tab=t.0
---
---
### API for testing
```sh
http://localhost:5000/index.html
```



### Create app and build
For creating app, use:

```sh
dotnet new console -n ExpenseStatistics
```
For build app, use:
```sh
dotnet build
```
For run, going to the build directory cd ./ExpenseStatistics, and:
```sh
dotnet run
```
Or run from the environment:
```sh
ASPNETCORE_ENVIRONMENT=Development dotnet run
```
---
#### Installing SDK and runtime machine
MacOS installing SDK:
```sh
brew install --cask dotnet-sdk@9
```
Fedora Linux installing SDK:
```sh
sudo dnf install dotnet-sdk-9.0
```
Install the runtime
```sh
sudo dnf install aspnetcore-runtime-9.0
```
and
```sh
sudo dnf install dotnet-runtime-9.0
```

---
### Managing PostgreSQL

Installing the PostgreSQL:
```sh
brew install postgresql@16
```
So that the terminal can find the psql command (PostgreSQL client) without specifying the full path to it.
```sh
echo 'export PATH="/usr/local/opt/postgresql@16/bin:$PATH"' >> ~/.zshrc
source ~/.zshrc
```
Adding to the system services:
```sh
brew services start postgresql@16
```

---
### Creating user in DB and table
Connection to the PostgreSQL (where _user_ - you real user):
```sh
psql -h localhost -U user postgres
```
Creating your user:
```sh
CREATE ROLE postgres WITH LOGIN PASSWORD 'psql';
ALTER ROLE postgres CREATEDB;
```
Creating the table:
```sh
CREATE DATABASE "ExpenseStatistics";
```

Give all rights:
```sh
GRANT ALL PRIVILEGES ON DATABASE "ExpenseStatistics" TO postgres;
```
Exit:
```sh
\q
```
**AND** enter again to DB _ExpenseStatistics_:
```sh
psql -h localhost -U mac ExpenseStatistics
```
Grants the postgres user permissions to the public schema:
```sh
GRANT ALL ON SCHEMA public TO postgres;
ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT ALL ON TABLES TO postgres;
```
Exit:
```sh
\q
```
And do this:
```sh
dotnet ef migrations add InitialCreate
dotnet ef database update
```
You can get this (4 rows):

List of relations
| Schema | Name                  | Type  | Owner    |
|--------|-----------------------|-------|----------|
| public | Categories             | table | postgres |
| public | Transactions           | table | postgres |
| public | Users                  | table | postgres |
| public | __EFMigrationsHistory  | table | postgres |

If you hav problems with Transactions table, yse:
```
DROP TABLE IF EXISTS public."Transactions";
```
and
```
CREATE TABLE public."Transactions" (
    TransactionId SERIAL PRIMARY KEY,
    UserId uuid NOT NULL,
    CategoryId uuid NOT NULL,
    Amount DECIMAL(10, 2) NOT NULL,
    TransactionDate TIMESTAMP NOT NULL,
    Description VARCHAR(255),
    FOREIGN KEY (UserId) REFERENCES public."Users"("Id"),
    FOREIGN KEY (CategoryId) REFERENCES public."Categories"("Id")
);
```

---
### Test SQL scripts with data

Test data for the table **Categories**:
```sh
INSERT INTO public."Categories" ("Id", "Name", "UserId")
VALUES
  (gen_random_uuid(), 'Food', (SELECT "Id" FROM public."Users" LIMIT 1 OFFSET 0)),
  (gen_random_uuid(), 'Transport', (SELECT "Id" FROM public."Users" LIMIT 1 OFFSET 1)),
  (gen_random_uuid(), 'Entertainment', (SELECT "Id" FROM public."Users" LIMIT 1 OFFSE 2)),
  (gen_random_uuid(), 'Health', (SELECT "Id" FROM public."Users" LIMIT 1 OFFSET 3)),
  (gen_random_uuid(), 'Utilities', (SELECT "Id" FROM public."Users" LIMIT 1 OFFSET 4)),
  (gen_random_uuid(), 'Education', (SELECT "Id" FROM public."Users" LIMIT 1 OFFSET 5)),
  (gen_random_uuid(), 'Shopping', (SELECT "Id" FROM public."Users" LIMIT 1 OFFSET 6)),
  (gen_random_uuid(), 'Travel', (SELECT "Id" FROM public."Users" LIMIT 1 OFFSET 7)),
  (gen_random_uuid(), 'Rent', (SELECT "Id" FROM public."Users" LIMIT 1 OFFSET 8)),
  (gen_random_uuid(), 'Salary', (SELECT "Id" FROM public."Users" LIMIT 1 OFFSET 9));

```
Test data for the **Users** table:
```sh
INSERT INTO public."Users" ("Id", "Email", "PasswordHash", "CreatedAt")
VALUES
    (gen_random_uuid(), 'user3@example.com', 'hashedpassword3', CURRENT_TIMESTAMP),
    (gen_random_uuid(), 'user4@example.com', 'hashedpassword4', CURRENT_TIMESTAMP),
    (gen_random_uuid(), 'user5@example.com', 'hashedpassword5', CURRENT_TIMESTAMP),
    (gen_random_uuid(), 'user6@example.com', 'hashedpassword6', CURRENT_TIMESTAMP),
    (gen_random_uuid(), 'user7@example.com', 'hashedpassword7', CURRENT_TIMESTAMP),
    (gen_random_uuid(), 'user8@example.com', 'hashedpassword8', CURRENT_TIMESTAMP),
    (gen_random_uuid(), 'user9@example.com', 'hashedpassword9', CURRENT_TIMESTAMP),
    (gen_random_uuid(), 'user10@example.com', 'hashedpassword10', CURRENT_TIMESTAMP),
    (gen_random_uuid(), 'user11@example.com', 'hashedpassword11', CURRENT_TIMESTAMP),
    (gen_random_uuid(), 'user12@example.com', 'hashedpassword12', CURRENT_TIMESTAMP),
    (gen_random_uuid(), 'user13@example.com', 'hashedpassword13', CURRENT_TIMESTAMP),
    (gen_random_uuid(), 'user14@example.com', 'hashedpassword14', CURRENT_TIMESTAMP),
    (gen_random_uuid(), 'user15@example.com', 'hashedpassword15', CURRENT_TIMESTAMP),
    (gen_random_uuid(), 'user16@example.com', 'hashedpassword16', CURRENT_TIMESTAMP),
    (gen_random_uuid(), 'user17@example.com', 'hashedpassword17', CURRENT_TIMESTAMP),
    (gen_random_uuid(), 'user18@example.com', 'hashedpassword18', CURRENT_TIMESTAMP),
    (gen_random_uuid(), 'user19@example.com', 'hashedpassword19', CURRENT_TIMESTAMP),
    (gen_random_uuid(), 'user20@example.com', 'hashedpassword20', CURRENT_TIMESTAMP),
    (gen_random_uuid(), 'user21@example.com', 'hashedpassword21', CURRENT_TIMESTAMP),
    (gen_random_uuid(), 'user22@example.com', 'hashedpassword22', CURRENT_TIMESTAMP);
```

Test data for the **Transactions** table:
```sh
-- User 1 transactions
INSERT INTO public.Transactions (UserId, CategoryId, Amount, TransactionDate, Description)
VALUES
(gen_random_uuid(), gen_random_uuid(), 120.00, '2025-04-25 16:00:00', 'Coffee with friends'),
(gen_random_uuid(), gen_random_uuid(), 350.00, '2025-04-26 09:00:00', 'Restaurant dinner'),
(gen_random_uuid(), gen_random_uuid(), 220.00, '2025-04-26 14:00:00', 'Online shopping'),
(gen_random_uuid(), gen_random_uuid(), 300.00, '2025-04-26 16:00:00', 'New phone'),
(gen_random_uuid(), gen_random_uuid(), 180.00, '2025-04-27 09:00:00', 'Weekend trip'),
(gen_random_uuid(), gen_random_uuid(), 90.00, '2025-04-27 12:00:00', 'Book purchase'),
(gen_random_uuid(), gen_random_uuid(), 250.00, '2025-04-27 15:00:00', 'Home renovation'),
(gen_random_uuid(), gen_random_uuid(), 350.00, '2025-04-28 10:00:00', 'Grocery shopping'),
(gen_random_uuid(), gen_random_uuid(), 400.00, '2025-04-28 13:30:00', 'Concert tickets'),
(gen_random_uuid(), gen_random_uuid(), 600.00, '2025-04-28 16:00:00', 'Travel expenses');

-- User 2 transactions
INSERT INTO public.Transactions (UserId, CategoryId, Amount, TransactionDate, Description)
VALUES
(gen_random_uuid(), gen_random_uuid(), 80.00, '2025-04-25 17:30:00', 'Taxi ride to the airport'),
(gen_random_uuid(), gen_random_uuid(), 500.00, '2025-04-26 10:00:00', 'Hotel booking'),
(gen_random_uuid(), gen_random_uuid(), 150.00, '2025-04-26 13:00:00', 'Excursion tickets'),
(gen_random_uuid(), gen_random_uuid(), 400.00, '2025-04-26 16:00:00', 'Luggage purchase'),
(gen_random_uuid(), gen_random_uuid(), 100.00, '2025-04-27 09:00:00', 'Airport food'),
(gen_random_uuid(), gen_random_uuid(), 250.00, '2025-04-27 12:30:00', 'Museum tickets'),
(gen_random_uuid(), gen_random_uuid(), 120.00, '2025-04-27 15:30:00', 'Taxi ride from airport'),
(gen_random_uuid(), gen_random_uuid(), 600.00, '2025-04-28 08:00:00', 'Travel insurance'),
(gen_random_uuid(), gen_random_uuid(), 500.00, '2025-04-28 11:00:00', 'Hotel dinner'),
(gen_random_uuid(), gen_random_uuid(), 700.00, '2025-04-28 14:00:00', 'Luxury tour');

-- User 3 transactions
INSERT INTO public.Transactions (UserId, CategoryId, Amount, TransactionDate, Description)
VALUES
(gen_random_uuid(), gen_random_uuid(), 75.00, '2025-04-25 11:00:00', 'Lunch with colleagues'),
(gen_random_uuid(), gen_random_uuid(), 400.00, '2025-04-26 10:30:00', 'Car repair'),
(gen_random_uuid(), gen_random_uuid(), 300.00, '2025-04-26 15:30:00', 'Flight tickets'),
(gen_random_uuid(), gen_random_uuid(), 150.00, '2025-04-27 10:00:00', 'Business lunch'),
(gen_random_uuid(), gen_random_uuid(), 500.00, '2025-04-27 13:00:00', 'Laptop purchase'),
(gen_random_uuid(), gen_random_uuid(), 200.00, '2025-04-27 16:00:00', 'Gasoline refill'),
(gen_random_uuid(), gen_random_uuid(), 350.00, '2025-04-28 09:00:00', 'Electronics shopping'),
(gen_random_uuid(), gen_random_uuid(), 100.00, '2025-04-28 12:00:00', 'Gift purchase'),
(gen_random_uuid(), gen_random_uuid(), 250.00, '2025-04-28 14:30:00', 'Dining out'),
(gen_random_uuid(), gen_random_uuid(), 450.00, '2025-04-28 16:00:00', 'Weekend vacation');

-- User 4 transactions
INSERT INTO public.Transactions (UserId, CategoryId, Amount, TransactionDate, Description)
VALUES
(gen_random_uuid(), gen_random_uuid(), 95.00, '2025-04-25 14:30:00', 'Books for studying'),
(gen_random_uuid(), gen_random_uuid(), 180.00, '2025-04-26 08:00:00', 'Fitness subscription'),
(gen_random_uuid(), gen_random_uuid(), 500.00, '2025-04-26 11:30:00', 'Shopping spree'),
(gen_random_uuid(), gen_random_uuid(), 250.00, '2025-04-27 09:00:00', 'Gadgets'),
(gen_random_uuid(), gen_random_uuid(), 100.00, '2025-04-27 12:00:00', 'Lunch with friends'),
(gen_random_uuid(), gen_random_uuid(), 300.00, '2025-04-27 14:30:00', 'Clothing shopping'),
(gen_random_uuid(), gen_random_uuid(), 150.00, '2025-04-28 08:00:00', 'Health insurance'),
(gen_random_uuid(), gen_random_uuid(), 80.00, '2025-04-28 11:00:00', 'Movies'),
(gen_random_uuid(), gen_random_uuid(), 400.00, '2025-04-28 13:30:00', 'Electronics shopping'),
(gen_random_uuid(), gen_random_uuid(), 550.00, '2025-04-28 16:00:00', 'Vacation package');

-- User 5 transactions
INSERT INTO public.Transactions (UserId, CategoryId, Amount, TransactionDate, Description)
VALUES
(gen_random_uuid(), gen_random_uuid(), 120.00, '2025-04-25 16:30:00', 'Concert tickets'),
(gen_random_uuid(), gen_random_uuid(), 250.00, '2025-04-26 12:00:00', 'Spa treatment'),
(gen_random_uuid(), gen_random_uuid(), 350.00, '2025-04-26 13:30:00', 'Weekend getaway'),
(gen_random_uuid(), gen_random_uuid(), 100.00, '2025-04-27 09:30:00', 'Gift shopping'),
(gen_random_uuid(), gen_random_uuid(), 200.00, '2025-04-27 12:00:00', 'Dinner at restaurant'),
(gen_random_uuid(), gen_random_uuid(), 150.00, '2025-04-27 14:30:00', 'Movie tickets'),
(gen_random_uuid(), gen_random_uuid(), 500.00, '2025-04-28 09:00:00', 'Flight booking'),
(gen_random_uuid(), gen_random_uuid(), 120.00, '2025-04-28 11:30:00', 'Concert tickets'),
(gen_random_uuid(), gen_random_uuid(), 250.00, '2025-04-28 13:00:00', 'Clothing purchase'),
(gen_random_uuid(), gen_random_uuid(), 400.00, '2025-04-28 15:00:00', 'Luxury gift');

```

You can see oll data in the table: 
```sh
SELECT * FROM public.Transactions;
```