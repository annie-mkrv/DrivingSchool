const carModel = require('../models/index').car;


module.exports.list = async function (req, res) {
    const car = await carModel.findAll();
    res.send(car);
}

//module.exports.info = async function (req, res) {
//    const cars = await car.findByPk(req.params.id);
//    res.send(cars);
//}


module.exports.add = async function (req, res) {
    await carModel.create(req.body);
    res.status(200).send();
}

module.exports.update = async function (req, res) {
    const cars = await carModel.findByPk(req.body.id);

    if (cars == null) {
        res.status(404).send('Автомобиль не найден')
    } else {
        cars.set(req.body);
        await cars.save();
        res.status(200).send();
    }
}

module.exports.delete = async function (req, res) {
    await carModel.destroy({
        where: {
            id: req.params.id
        }
    });
    res.status(200).send();
}

