# LogParser
Test task

## CLI
Commands:
* --help
* -a [path] - parse specified access log file
* -e [path] (not implemented) - parse specified error log file

## API
* **Get all**  
GET /api/log/access

* **Get all in period between *start* and *end*, 
sorted by datetime and in amount of *limit* and with specified *offset***  
GET /api/log/access?offset=1&start=03/09/2004&end=03/09/2005&limit=10

* **Get top hosts in period between *start* and *end* in specified *amount* on asc/desc order**  
GET /api/log/access/hosts?amount=10&start=03/09/2004&end=&desc=true

* **Get top routes in period between *start* and *end* in specified *amount* on asc/desc order**  
GET /api/log/access/routes?amount=10&start=03/09/2004&end=&desc=true

* **Add new access log data to the database**  
POST /api/log/access
