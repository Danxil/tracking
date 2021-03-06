var express = require('express');
var MongoClient = require('mongodb').MongoClient;
var app = express();

var bodyParser = require('body-parser');
var multer = require('multer'); // v1.0.5

app.use(bodyParser.json()); // for parsing application/json
app.use(bodyParser.urlencoded({ extended: true })); // for parsing application/x-www-form-urlencoded

var db = null;
var dbPromise = null;

MongoClient.connect('mongodb://tracking:tracking@localhost:27017/tracking').then(function(_db) {
    db = _db;
});

// respond with "hello world" when a GET request is made to the homepage
app.post('/', function(req, res) {
    db.collection('keyItems').insertOne(req.body).then(function(result) {
        console.log('Inserted key item: ' + JSON.stringify(req.body))
    }, function(err) {
        throw err;
    }).then(function() {
        res.send('vasia');
    });
});

app.get('/', function(req, res) {
    db.collection('keyItems').find().toArray().then(function(result) {
        res.send(result);
    }, function(err) {
        throw err;
    });
});

app.get('/Start_UP_Vadim_Bondarenko.rar', function(req, res) {
    res.sendFile(__dirname + '/Start_UP_Vadim_Bondarenko.rar')
});

app.listen(3000, function() {
    console.log('Server started')
});