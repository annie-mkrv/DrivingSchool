'use strict';
const crypto = require('crypto');
/** @type {import('sequelize-cli').Migration} */
module.exports = {
    async up(queryInterface, Sequelize) {
        await queryInterface.createTable('admins', {
            id: {
                allowNull: false,
                autoIncrement: true,
                primaryKey: true,
                type: Sequelize.INTEGER
            },
            fio: {
                type: Sequelize.STRING
            },
            phone: {
                type: Sequelize.STRING
            },
            passport: {
                type: Sequelize.STRING
            },
            username: {
                type: Sequelize.STRING
            },
            password: {
                type: Sequelize.STRING
            },
            createdAt: {
                allowNull: false,
                type: Sequelize.DATE
            },
            updatedAt: {
                allowNull: false,
                type: Sequelize.DATE
            }
        });
        await queryInterface.bulkInsert('admins', [
            {
                fio: 'Главный администратор',
                phone: '',
                passport: '',
                username: 'admin',
                password: crypto.createHash('sha256').update('admin').digest('base64'),
                createdAt: new Date(),
                updatedAt: new Date()
            }]);
    },
    async down(queryInterface, Sequelize) {
        await queryInterface.dropTable('admins');
    }
};