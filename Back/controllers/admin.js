const admin = require('../models/index').admin;
const crypto = require('crypto');
module.exports.list = async function (req, res) {
    const admins = await admin.findAll({exclude: ['password']});
    for (var adm in admins) {
        delete adm.password;
    }
    res.send(admins);

}

module.exports.info = async function (req, res) {
    const adm = await admin.findByPk(req.params.id);

    delete adm.password;
    res.send(adm);
}

module.exports.add = async function (req, res) {
    adm = req.body;
    adm.password = crypto.createHash('sha256').update(req.body.password).digest('base64');

    await admin.create(adm);

    res.status(200).send();
}

module.exports.update = async function (req, res) {
    if (req.body.password) {
        req.body.password = crypto.createHash('sha256').update(req.body.password).digest('base64');
    }

    const adm = await admin.findByPk(req.body.id);

    if (adm == null) {
        res.status(404).send('Администратор не найден')
    } else {
        adm.set(req.body);
        await adm.save();

        res.status(200).send();
    }
}

module.exports.delete = async function (req, res) {
    await admin.destroy({
        where: {
            id: req.params.id
        }
    });

    res.status(200).send();
}