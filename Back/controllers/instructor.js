const instructorModel = require('../models/index').instructor;
const car = require('../models/index').car;


module.exports.list = async function (req, res) {
    const instructors = await instructorModel.findAll
        ({
            include: [
                { model: car }
            ]
        });
    res.send(instructors);
}

module.exports.info = async function (req, res) {
    const instructors = await instructorModel.findByPk(req.params.id);
    res.send(instructors);
}

module.exports.add = async function (req, res) {
    await instructorModel.create(req.body);
    res.status(200).send();
}

module.exports.update = async function (req, res) {
    const instructors = await instructorModel.findByPk(req.body.id);

    if (instructors == null) {
        res.status(404).send('Инструктор не найден')
    } else {
        instructors.set(req.body);
        await instructors.save();
        res.status(200).send();
    }
}

module.exports.delete = async function (req, res) {
    await instructorModel.destroy({
        where: {
            id: req.params.id
        }
    });
    res.status(200).send();
}

