# ExpenseStatistics

**ExpenseStatistics** — програма для швидкого аналізу витрат та фінансової статистики.

---
**_Content:_**
- [Task](#task-4)
- [General requirements](#general-requirements)
- [API](#api)
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
### API 
```sh
http://localhost:5000/swagger/index.html
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


---
### Test SQL scripts with data

Test data for the table **Categories**:
```sh
INSERT INTO public.Categories (Name)
VALUES
('Food'),
('Transport'),
('Entertainment'),
('Health'),
('Utilities'),
('Education'),
('Shopping'),
('Travel'),
('Rent'),
('Salary');
```
Test data for the **Users** table:
```sh
INSERT INTO public.Users (Username, Email, PasswordHash, CreatedAt)
VALUES
('john_doe', 'john.doe@example.com', 'hashed_password_123', '2025-04-26 10:00:00'),
('jane_smith', 'jane.smith@example.com', 'hashed_password_456', '2025-04-26 10:05:00'),
('admin', 'admin@example.com', 'hashed_password_789', '2025-04-26 10:10:00'),
('mike_jones', 'mike.jones@example.com', 'hashed_password_101', '2025-04-26 10:15:00'),
('susan_lee', 'susan.lee@example.com', 'hashed_password_202', '2025-04-26 10:20:00'),
('emily_wang', 'emily.wang@example.com', 'hashed_password_303', '2025-04-26 10:25:00');
```

Test data for the **Transactions** table:
```sh
-- User 1 (user0)
INSERT INTO public.Transactions (UserId, CategoryId, Amount, TransactionDate, Description)
VALUES
(1, 1, 50.00, '2025-04-25 12:00:00', 'Groceries'),
(1, 2, 20.00, '2025-04-25 14:30:00', 'Bus ticket'),
(1, 3, 100.00, '2025-04-26 08:00:00', 'Cinema'),
(1, 4, 200.00, '2025-04-26 10:00:00', 'Medical consultation'),
(1, 5, 60.00, '2025-04-26 11:00:00', 'Electricity bill'),
(1, 6, 150.00, '2025-04-26 12:00:00', 'Online course'),
(1, 7, 120.00, '2025-04-26 13:00:00', 'Clothing'),
(1, 8, 500.00, '2025-04-26 14:00:00', 'Vacation expenses');

-- User 2 (user1)
INSERT INTO public.Transactions (UserId, CategoryId, Amount, TransactionDate, Description)
VALUES
(2, 1, 35.00, '2025-04-25 13:30:00', 'Groceries'),
(2, 2, 15.00, '2025-04-25 15:30:00', 'Train ticket'),
(2, 3, 120.00, '2025-04-26 08:30:00', 'Movie tickets'),
(2, 4, 250.00, '2025-04-26 09:00:00', 'Doctor visit'),
(2, 5, 70.00, '2025-04-26 11:30:00', 'Water bill'),
(2, 6, 200.00, '2025-04-26 12:30:00', 'Language course'),
(2, 7, 80.00, '2025-04-26 13:30:00', 'Electronics'),
(2, 8, 600.00, '2025-04-26 14:30:00', 'Holiday trip');

-- User 3 (admin)
INSERT INTO public.Transactions (UserId, CategoryId, Amount, TransactionDate, Description)
VALUES
(3, 1, 45.00, '2025-04-25 14:00:00', 'Snacks and drinks'),
(3, 2, 25.00, '2025-04-25 16:00:00', 'Taxi ride'),
(3, 3, 150.00, '2025-04-26 09:00:00', 'Concert tickets'),
(3, 4, 180.00, '2025-04-26 10:30:00', 'Medical checkup'),
(3, 5, 65.00, '2025-04-26 12:00:00', 'Gas bill'),
(3, 6, 120.00, '2025-04-26 13:00:00', 'Fitness class'),
(3, 7, 100.00, '2025-04-26 14:00:00', 'Shopping online'),
(3, 8, 550.00, '2025-04-26 15:00:00', 'Business trip');

-- User 4 (user2)
INSERT INTO public.Transactions (UserId, CategoryId, Amount, TransactionDate, Description)
VALUES
(4, 1, 60.00, '2025-04-25 10:00:00', 'Supermarket'),
(4, 2, 18.00, '2025-04-25 12:00:00', 'Subway pass'),
(4, 3, 130.00, '2025-04-26 09:00:00', 'Theater play'),
(4, 4, 250.00, '2025-04-26 10:00:00', 'Dentist visit'),
(4, 5, 55.00, '2025-04-26 11:00:00', 'Phone bill'),
(4, 6, 180.00, '2025-04-26 12:00:00', 'Cooking class'),
(4, 7, 90.00, '2025-04-26 13:00:00', 'Home appliances'),
(4, 8, 700.00, '2025-04-26 14:00:00', 'Travel insurance');

-- User 5 (user3)
INSERT INTO public.Transactions (UserId, CategoryId, Amount, TransactionDate, Description)
VALUES
(5, 1, 55.00, '2025-04-25 13:00:00', 'Vegetables'),
(5, 2, 22.00, '2025-04-25 15:00:00', 'Taxi ride'),
(5, 3, 140.00, '2025-04-26 09:30:00', 'Theme park tickets'),
(5, 4, 200.00, '2025-04-26 10:00:00', 'Optician visit'),
(5, 5, 75.00, '2025-04-26 12:30:00', 'Mobile bill'),
(5, 6, 160.00, '2025-04-26 13:30:00', 'Yoga class'),
(5, 7, 110.00, '2025-04-26 14:30:00', 'Furniture'),
(5, 8, 650.00, '2025-04-26 15:30:00', 'Cruise trip');
```