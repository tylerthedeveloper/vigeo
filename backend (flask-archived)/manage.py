#!flask/bin/python3
from flask_script import Manager
from flask.ext.migrate import Migrate, MigrateCommand
import os

from vigeo import app, db
app.config.from_object('config')
migrate = Migrate(app, db)
manager = Manager(app)

manager.add_command('db', MigrateCommand)

if __name__ == '__main__':
    manager.run()
