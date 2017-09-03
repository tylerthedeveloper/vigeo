from flask import Flask, request, jsonify, render_template, redirect, send_from_directory, Response, jsonify, abort, make_response
from flask_sqlalchemy import SQLAlchemy
from flask_compress import Compress
from werkzeug.debug import DebuggedApplication
from flask_mail import Mail

app = Flask(__name__)
app.config.from_object('config')
app.Debug = True
app.wsgi_app = DebuggedApplication(app.wsgi_app, True)
mail = Mail(app)
Compress(app)
db = SQLAlchemy(app)

import json
#import auth
#import profile
#import search
#import events
import routes.eventAPI
import routes.loginAPI
import routes.userAPI
import routes.adminAPI
import routes.backups.py_AE_routes
import models

@app.route('/robots.txt')
def robots():
	return send_from_directory(app.static_folder, request.path[1:])

@app.route('/')
def index():
	return ";) jobs@vigeo.io"

if __name__ == "__main__":
	app.run()

