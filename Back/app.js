require('dotenv').config();
const express = require('express');
const router = require('./routes');
const path = require('path');
const bodyParser = require('body-parser');

const app = express();

app.use(bodyParser.urlencoded({ extended: true }));
app.use(bodyParser.json());

app.use('/public', express.static(path.join(__dirname, 'public')));
app.use('/', router);

const port = process.env.PORT || 8080;
app.set('port', port);
app.listen(port);

console.log('App started!');
