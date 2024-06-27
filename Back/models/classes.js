'use strict';
const {
  Model
} = require('sequelize');
module.exports = (sequelize, DataTypes) => {
  class classes extends Model {
    /**
     * Helper method for defining associations.
     * This method is not a part of Sequelize lifecycle.
     * The `models/index` file will call this method automatically.
     */
    static associate(models) {
        this.belongsTo(models.instructor, {
            foreignkey: "instructorId"
        })
        this.belongsTo(models.student, {
            foreignkey: "studentId"
        })
    }
  }
  classes.init({
    date: DataTypes.DATE,
    studentId: DataTypes.INTEGER,
    instructorId: DataTypes.INTEGER
  }, {
    sequelize,
    modelName: 'classes',
  });
  return classes;
};