const student = require('../models/index').student;
const crypto = require('crypto');

module.exports.list = async function (req, res) {
    const students = await student.findAll({exclude: ['password']});
    for (var stud in students) {
        delete stud.password;
    }
    res.send(students);

}

module.exports.info = async function (req, res) {
    const stud = await student.findbypk(req.params.id);

    delete stud.password;
    res.send(stud);
}

module.exports.add = async function (req, res) {
    stud = req.body;
    stud.password = crypto.createHash('sha256').update(req.body.password).digest('base64');

    await student.create(stud);

    res.status(200).send();
}

module.exports.update = async function (req, res) {
    if (req.body.password) {
        req.body.password = crypto.createHash('sha256').update(req.body.password).digest('base64');
    }

    const stud = await student.findByPk(req.body.id);

    if (stud == null) {
        res.status(404).send('Ученик не найден')
    } else {
        stud.set(req.body);
        await stud.save();

        res.status(200).send();
    }
}

module.exports.delete = async function (req, res) {
    await student.destroy({
        where: {
            id: req.params.id
        }
    });

    res.status(200).send();
}