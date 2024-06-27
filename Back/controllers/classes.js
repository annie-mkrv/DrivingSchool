const classes = require('../models/index').classes;
const student = require('../models/index').student;
const instructor = require('../models/index').instructor;
const car = require('../models/index').car;


const { Op } = require('sequelize');

module.exports.list = async function (req, res) {
    const date = req.query.date; // Assuming the date is sent as 'YYYY-MM-DD'
    if (!date) {
        return res.status(400).send('Date parameter is required');
    }

    const startOfDay = new Date(date);
    const endOfDay = new Date(date);
    endOfDay.setDate(endOfDay.getDate() + 1); // End of the day is the start of the next day

    const classesofday = await classes.findAll
        ({
            include: [
                {
                    model: instructor,
                    include: [
                        {
                            model: car, // Include the Car model associated with Instructor
                            required: false // Set to true if you want to enforce that each Instructor must have a Car
                        }
                    ]
                },
                { model: student }
            ],

            where: {
                date: {
                    [Op.between]: [startOfDay, endOfDay]
                }
            }
        });

    res.send(classesofday);
}
module.exports.listByStudent = async function (req, res) {
    const { studentId } = req.query;

    if (!studentId) {
        return res.status(400).send('Student ID parameter is required');
    }

    try {
        const classesByStudent = await classes.findAll({
            include: [
                {
                    model: instructor,
                    include: [
                        {
                            model: car, // Include the Car model associated with Instructor
                            required: false // Set to true if you want to enforce that each Instructor must have a Car
                        }
                    ]
                },
                {
                    model: student,
                    where: { id: studentId } // Filter by studentId
                }
            ],
            order: [['date', 'DESC']] // Order by date field in descending order
        });

        res.send(classesByStudent);
    } catch (error) {
        console.error('Error fetching classes:', error);
        res.status(500).send('Internal server error');
    }
}


//module.exports.info = async function (req, res) {
//    const classeses = await classes.findbypk(req.params.id);
//    res.send(classeses);
//}


module.exports.add = async function (req, res) {
    await classes.create(req.body);
    res.status(200).send();
}

//module.exports.update = async function (req, res) {
//    const classeses = await classes.findByPk(req.body.id);

//    if (classeses == null) {
//        res.status(404).send('Занятие не найдено')
//    } else {
//        classeses.set(req.body);
//        await classeses.save();
//        res.status(200).send();
//    }
//}

module.exports.delete = async function (req, res) {
    await classes.destroy({
        where: {
            id: req.params.id
        }
    });
    res.status(200).send();
}

