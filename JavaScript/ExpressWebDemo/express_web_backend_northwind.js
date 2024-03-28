const express = require("express");
// const cors = require('cors');
const sqlite3 = require("sqlite3").verbose();
const app = express();
const port = 3000;

// Connect to SQLite database
const db = new sqlite3.Database("northwind.db", sqlite3.OPEN_READWRITE, (err) => {
    if (err) return console.error(err.message);
    console.log("Connected to the Northwind SQLite database.");
});

app.get('/', (req, res) => {
    res.send(`<!DOCTYPE html>
<html>
<head>
    <title>Customers by Country</title>
    <!-- Add styling if needed -->
</head>
<body>
    <script>
        function getCustomers() {
            var country = document.getElementById('countryInput').value;
            fetch("http://localhost:3000/customers/" + country)
                .then(response => response.json())
                .then(data => {
                    var resultsDiv = document.getElementById('results');
                    resultsDiv.innerHTML = ''; // Clear previous results
                    data.forEach(customer => {
                        resultsDiv.innerHTML += "<p>" + customer.CompanyName + "</p>";
                    });
                })
                .catch(error => console.error('Error:', error));
        }
    </script>

    <input type="text" id="countryInput" placeholder="Enter Country">
    <button onclick="getCustomers()">Search</button>
    <div id="results"></div>
    </body>
</html>`)
  });

app.get("/customers/:country", (req, res) => {
    const country = req.params.country;
    const sql = `SELECT CompanyName FROM Customers WHERE Country = ?`;

    db.all(sql, [country], (err, rows) => {
        if (err) {
            res.status(500).send(err.message);
            return;
        }
        res.json(rows);
    });
});

app.listen(port, () => {
    console.log(`Server running on http://localhost:${port}/`);
});
