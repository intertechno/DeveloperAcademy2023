cd "C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\170\Tools\Binn"
$password = Read-Host 'Please enter the password for the database:'
sqlcmd -s localhost -u MyName -p $password -i MyFile1.sql
sqlcmd -s localhost -u MyName -p $password -i MyFile2.sql
sqlcmd -s localhost -u MyName -p $password -i MyFile3.sql
