#!../env/bin/python
from bs4 import BeautifulSoup
import datetime
from urllib.request import urlopen, Request
import simplejson as json
import timestring
import copy
from vigeo import db
from models import Event
import json
import sqlalchemy


def query_local_events(location):
    events = db.session.query(Event).filter(sqlalchemy.func.ST_DWITHIN(Event.location, location, 30 * 1609.34, True)).all()
    return events

lat = "39.1653"
lon = "-86.5264"
location = "POINT(" + lon + " " + lat + ")"
#uresp = urlopen('https://api.seatgeek.com/2/events?&lat='+lat+'&lon='+lon+'&range=30mi')
uresp = urlopen('https://api.seatgeek.com/2/events?postal_code=10001&range=50mi&per_page=1000')
jresp = uresp.read().decode('utf-8')
data = json.loads(jresp)
pinged_local_events = []
tags =[]
for event in data['events'][:]:
   pinged_local_events.append(event)
   tags.append(event["taxonomies"][0]["name"])


new_local_events = []
local_events = query_local_events(location)
for event in pinged_local_events:
   for local in local_events:
        if event['title'] == local.title:
            continue
        else:
            new_local_events.append(event)
            # will just save to database later
#print(local_events)
#print("new ")
#print(pinged_local_events)

"""uresp = urlopen('https://api.seatgeek.com/2/events?&postal_code=47406&range=20mi')
jresp = uresp.read().decode('utf-8')
data = json.loads(jresp)
for event in data['events'][:]:
    print('Title:          ' + event['title'])
    print('Date:           ' + event['datetime_local'].split('T')[0])
    print('Start Time:           ' + event['datetime_local'].split('T')[1])
    print('Type:           ' + event['type']) #+  'Or we can try:   ' # + event[
    print('Venue:          ' + event['venue']['name'])
    print('     Address:          ' + event['venue']['address'])
    print('     And or:          ' + event['venue']['display_location'])
    print('     For app loc:    ' + event['venue']['address'] + ', ' + event['venue']['display_location'])
    print('     City:          ' + event['venue']['city'])
    print('     Lat:    ' +  str(event['venue']['location']['lat']))
    print('     Lon:    ' +  str(event['venue']['location']['lon']))
    title = event['title']
    starttime = timestring.Date(str(event['datetime_local'].split('T')[1]))
    endtime = copy.copy(starttime)
    endtime.hour += 1
    address = event['venue']['address'].strip()
    lng = str(event['venue']['location']['lon'])
    lat = str(event['venue']['location']['lat'])
    event = Event(title, str(starttime), str(endtime), address, "POINT(" + lng + " " + lat + ")", "")
    db.session.add(event)
    db.session.commit()"""
