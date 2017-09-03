import timestring
import json
from vigeo import db
from datetime import datetime
from sqlalchemy import text
from sqlalchemy.dialects import postgresql
from sqlalchemy.dialects.postgresql import JSON, ARRAY, array
from sqlalchemy import (
    create_engine, func, Column, ForeignKey, Index,
    DateTime, String, Integer, BigInteger, SmallInteger,
    Boolean, LargeBinary, Float)
#from geoalchemy2.shape import to_shape
#import geoalchemy2
#from geoalchemy2 import Geometry
#from shapely.geometry import Point
#from passlib.apps import custom_app_context as pwd_context

        
def commit():
        try:
                db.session.commit()
        except:
                print("failed------------------------------------!!!!!!!111")
                db.session.rollback()

class User(db.Model):
        id = db.Column(db.Integer, primary_key=True)
        fb_id = db.Column(db.String)
        access_token = db.Column(db.String) 
        token_type = db.Column(db.String)
        full_name = db.Column(db.String)
        first_name = db.Column(db.String)
        last_name = db.Column(db.String)
        email = db.Column(db.String, unique=True)
        age = db.Column(db.Integer)
        gender = db.Column(db.String)
        friends = db.Column(ARRAY(String), default=[])
        profile_picture = db.Column(db.String)
        interests = db.Column(ARRAY(String), default=[])
        created_on = db.Column(db.DateTime, default=datetime.now) 
                #db.TimeStamp, default=now()
                #last_login = db.Column(db.DateTime)
        attending = db.Column(JSON, default=[])

        def getUserId(self, id):
                return unicode(self.id)
                
        #? depends on when we decide
        def getSettings(self):
                return {'full_name': self.full_name, 'email': self.email, 'age': self.age}

        #? depends on when we decide
        def getInterests(self):
                return {'interests': self.interests }

        def joinEvent(self, event):
                self.attending.append(event)
                db.session.query(User).filter(User.id == self.id).update({"attending": self.attending})
                commit()

        def userReport(self):
                return {'id': self.id, 'full_name': self.full_name,\
                                'email': self.email, 'age': self.age,\
                                'created_on': self.created_on, \
                                'token_type': self.token_type, 'intersts': self.interests,\
                                'attending': self.attending, 'friends': self.friends,\
                                'profile_picture': self.profile_picture, \
                                'interests': self.interests, 'attending': self.attending }


#psql my_database -c "CREATE EXTENSION postgis;"
class Event(db.Model):
        #generic
        id = db.Column(db.Integer, primary_key=True)
        event_id = db.Column(db.String)
        title = db.Column(db.String)
        start_time = db.Column(db.DateTime)
        end_time = db.Column(db.DateTime)
        event_url = db.Column(db.String)        
        share_url = db.Column(db.String)
        owner_id = db.Column(db.String)
        cover_image = db.Column(db.String)

        #venue  
        venue = db.Column(db.String)
        full_address = db.Column(db.String)
        city = db.Column(db.String)
        country = db.Column(db.String)
        latitude = db.Column(db.String)
        longitude = db.Column(db.String)
        state = db.Column(db.String)
        street = db.Column(db.String)
        
        #social
        attending = db.Column(JSON)
        messages = db.Column(JSON)
        gallery = db.Column(JSON)
        
        #performers = db.Column(ARRAY(String), default=[])
        #tags = db.Column(ARRAY(String), default=[])
        

        def getEvent(e_id):
            return Event.query.filter(Event.event_id==e_id).first()
                
        def getChat(self):
                return {'messages' : self.messages }
        
        def getGallery(self):
                return {'gallery': self.gallery}

        def addMessage(self, message):
                self.messages.append(message)
                db.session.query(Event).filter(Event.id == self.id).update({"messages": self.messages})
                commit()
                
        def addMedia(self, json_data):
                self.images.append(json_data)
                db.session.query(Event).filter(Event.id == self.id)\
                                .update({"images": self.images})
                commit()


        def updateAttending(self, user):
                self.attending.append(user)
                db.session.query(Event).filter(Event.id == self.id).update({"attending": self.attending})
                commit()

        def getAttending(self):
                return [user["picture"] for user in self.attending]     

        def eventReport(self):  
            return {'id': self.id, 'title': self.title, 'event_id' : self.event_id, \
                        'start_time' : str(self.start_time), 'end_time' : self.end_time, \
                        'event_url' : self.event_url, 'share_url' : self.share_url, \
                        'owner_id' : self.owner_id, 'cover_image' : self.cover_image, \
                        'venue': self.venue, 'full_address': self.full_address, \
                        'city': self.city, 'country': self.country, 'latitude': self.latitude, \
                        'longitude': self.longitude, 'state': self.state, 'street': self.street, \
                        'attending': self.attending, "gallery": self.gallery, \
                        "messages": self.messages}
