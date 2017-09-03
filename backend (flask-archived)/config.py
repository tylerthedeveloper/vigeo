import os
basedir = os.path.abspath(os.path.dirname("__files__"))

CSRF_ENABLED = True
SECRET_KEY = 'you-will-never-guess'
SQLALCHEMY_DATABASE_URI = "postgresql://tylercitrin:9nv683g@localhost/vigeo"
SQLALCHEMY_MIGRATE_REPO = os.path.join(basedir, 'db_repository')


#email server
MAIL_SERVER = "mail.privateemail.com"    #'your.mailserver.com'
MAIL_PORT = 465   #465 #587
MAIL_USE_TLS = False
MAIL_USE_SSL = True
MAIL_USERNAME = "contact@vigeo.io"
MAIL_PASSWORD = "Vigeo@domain@io"
#MAIL_USERNAME = os.environ.get('MAIL_USERNAME')
#MAIL_PASSWORD = os.environ.get('MAIL_PASSWORD')


# administrator list
ADMINS = ['contact@vigeo.io']
