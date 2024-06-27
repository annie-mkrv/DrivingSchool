'use strict';
const {
  Model
} = require('sequelize');
module.exports = (sequelize, DataTypes) => {
  class instructor extends Model {
    /**
     * Helper method for defining associations.
     * This method is not a part of Sequelize lifecycle.
     * The `models/index` file will call this method automatically.
     */
    static associate(models) {
        this.belongsTo(models.car, {
            foreignkey: "carId"
        })

        this.hasMany(models.classes, {
            foreignkey: "instructorId"
        })
    }
  }
  instructor.init({
    fio: DataTypes.STRING,
    phone: DataTypes.STRING,
    carId: DataTypes.INTEGER
  }, {
    sequelize,
    modelName: 'instructor',
  });
  return instructor;
};