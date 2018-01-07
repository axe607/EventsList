@echo off
SQLCMD -S .\SQLEXPRESS -E  -i ".\1. Database\initDB.sql" -b
PAUSE
for %%G in (".\2. Tables\*.sql") do SQLCMD -S .\SQLEXPRESS -E  -i "%%G"
PAUSE
for %%G in (".\2.1 Table Relations\*.sql") do SQLCMD -S .\SQLEXPRESS -E  -i "%%G"
PAUSE
for %%G in (".\3. Functions\*.sql") do SQLCMD -S .\SQLEXPRESS -E  -i "%%G"
PAUSE
for %%G in (".\4. Procedures\*.sql") do SQLCMD -S .\SQLEXPRESS -E  -i "%%G"
PAUSE
for %%G in (".\5. Users\*.sql") do SQLCMD -S .\SQLEXPRESS -E  -i "%%G"
PAUSE
for %%G in (".\6. Sample Data\*.sql") do SQLCMD -S .\SQLEXPRESS -E  -i "%%G"
PAUSE
for %%G in (".\7. Table Triggers\*.sql") do SQLCMD -S .\SQLEXPRESS -E  -i "%%G"
PAUSE