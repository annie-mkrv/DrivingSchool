const admin = require("../models").admin;
const student = require("../models").student;
const crypto = require('crypto');
const { response } = require("express");
module.exports.login = async function (req, res) {
    username = req.body.username;
    password = req.body.password;


    const userAdmin = await admin.findOne({
        where: { username },
        raw: true
    })

    const userStudent = await student.findOne({
        where: { username },
        raw: true
    })

    hashedPassword = crypto.createHash('sha256').update(password).digest('base64');
    const response = {};

    if (userAdmin == null && userStudent == null) {
        res.status(404).send();
        return;
    }
    if (userAdmin && userAdmin.password == hashedPassword) {
        response['userAdmin'] = userAdmin;
        delete response.userAdmin.password;
    }
    if (userStudent && userStudent.password == hashedPassword) {
        response['userStudent'] = userStudent;
        delete response.userStudent.password;
    }

    console.log(response);

    if (response.userAdmin == undefined && response.userStudent == undefined) {
        res.status(401).send();
        return;
    }

    res.status(200).send(response);
}