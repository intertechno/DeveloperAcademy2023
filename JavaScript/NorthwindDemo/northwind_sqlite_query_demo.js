const sqlite3 = require('sqlite3').verbose();
const readline = require('readline');

// Setup readline interface
const rl = readline.createInterface({
    input: process.stdin,
    output: process.stdout
});

// Connect to SQLite database
const db = new sqlite3.Database('northwind.db', sqlite3.OPEN_READWRITE, (err) => {
    if (err) {
        console.error(err.message);
    } else {
        console.log('Connected to the Northwind database.');
        listFinnishCustomers();
    }
});

// Function to list all Finnish customers
function listFinnishCustomers() {
    console.log("Finnish Customers:");
    db.all("SELECT * FROM Customers WHERE Country = 'Finland'", [], (err, rows) => {
        if (err) {
            throw err;
        }
        rows.forEach((row) => {
            console.log(`${row.CustomerID} - ${row.CompanyName}`);
        });
        askForProduct();
    });
}

// Function to ask for a product and show its price
function askForProduct() {
    rl.question('Enter a product name to get its price: ', (productName) => {
        db.get("SELECT ProductName, UnitPrice FROM Products WHERE ProductName = ?", [productName], (err, row) => {
            if (err) {
                throw err;
            }
            if (row) {
                console.log(`The unit price of ${row.ProductName} is ${row.UnitPrice}`);
            } else {
                console.log("Product not found.");
            }
            rl.close();
            db.close();
        });
    });
}
