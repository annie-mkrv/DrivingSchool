const express = require('express');

const student = require('./controllers/student');
const admin = require('./controllers/admin');
const classes = require('./controllers/classes');
const instructor = require('./controllers/instructor');
const car = require('./controllers/car');
const { login } = require('./auth/auth');



const router = express.Router();

const studentRouter = express.Router();
studentRouter.post('/add', student.add);
studentRouter.post('/update', student.update);
studentRouter.get('/info/:id', student.info);
studentRouter.get('/list', student.list);
studentRouter.delete('/delete/:id', student.delete);

const instructorRouter = express.Router();
instructorRouter.post('/add', instructor.add);
instructorRouter.post('/update', instructor.update);
instructorRouter.get('/info/:id', instructor.info);
instructorRouter.get('/list', instructor.list);
instructorRouter.delete('/delete/:id', instructor.delete);

const classesRouter = express.Router();
classesRouter.post('/add', classes.add);
/*classesRouter.post('/update', classes.update);*/
/*classesRouter.get('/info/:id', classes.info);*/
classesRouter.get('/list', classes.list);
classesRouter.get('/list-by-student', classes.listByStudent);
classesRouter.delete('/delete/:id', classes.delete);

const adminRouter = express.Router();
adminRouter.post('/add', admin.add);
adminRouter.post('/update', admin.update);
adminRouter.get('/list', admin.list);
adminRouter.delete('/delete/:id', admin.delete);
adminRouter.get('/info/:id', admin.info);

const carRouter = express.Router();
carRouter.post('/add', car.add);
carRouter.post('/update', car.update);
carRouter.get('/list', car.list);
carRouter.delete('/delete/:id', car.delete);
/*carRouter.get('/info/:id', car.info);*/


router.use('/student', studentRouter);
router.use('/admin', adminRouter);
router.use('/classes', classesRouter);
router.use('/instructor', instructorRouter);
router.use('/car', carRouter);
router.post('/auth', login);

module.exports = router;


