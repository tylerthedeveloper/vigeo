#!../env/bin/python
from bs4 import BeautifulSoup
import datetime
from urllib.request import urlopen, Request
import simplejson as json
import timestring
import copy
from vigeo import db
from models import Event

url = "http://www.visitbloomington.com/things-to-do/events/?e_ViewBy=day&e_pageNum=1&e_catID=0%2C7%2C14%2C4%2C8%2C12%2C13%2C46%2C2%2C15%2C47%2C6%2C3%2C18&e_keyword=&e_sortBy=eventDate&e_pagesize=50"

def geocode(address):
    api_key = "AIzaSyBZZBiqmFgPo9GRESa2VThklu4jgZo3xS4"
    url = "https://maps.googleapis.com/maps/api/geocode/json?address=" + address.replace(" ", "+") + "&key=" + api_key
    return urlopen(Request(url, headers={'User-Agent': 'Mozilla'})).read().decode('utf-8')


events = urlopen(Request(url, headers={'User-Agent': 'Mozilla'})).read().decode('utf-8')
soup = BeautifulSoup(events, 'html.parser')
blocks = soup.find_all(class_='evnt-listing-content')
for block in blocks:
    for title in block.find_all('a', {'title' : 'View Details'}):
        if title.string != "more":
            details = block.find_all('li')
            address = details[2]
            time = details[4]
            if "Address" not in address.find_all(text=True)[0].strip():
                continue
            address = address.find_all(text=True)[1].strip()
            geo = geocode(address)
            g = json.loads(geo)
            if ";" in time.find_all(text=True)[0].strip() or "Time" not in time.find_all(text=True)[0].strip() or time.find_all(text=True)[1].strip().count('-') > 1 or "See" in time.find_all(text=True)[1].strip() or "All" in time.find_all(text=True)[1].strip():
                continue
            try:
                time = details[4].find_all(text=True)[1].strip()
            except Exception as e:
                print(e)
                #print(title.string)
                #print(time)
                #print(address)
                continue
            try:
                gps_coords = g["results"][0]["geometry"]["location"]
                #print(time.split("-")[0].split("\n")[0].strip())
                if "am" in time:
                    starttime = timestring.Date(time.split("-")[0].strip() + " am")
                elif "pm" in time:
                    starttime = timestring.Date(time.split("-")[0].strip() + " pm")
                else:
                    starttime = timestring.Date(time.split("-")[0].strip())
                now = timestring.now()
                
                starttime.year = now.year
                starttime.month = now.month
                starttime.day = now.day
                endtime = copy.copy(starttime)
                endtime.hour += 1
                if endtime.minute + 30 > 60:
                    endtime.hour += 1
                    endtime.minute = (endtime.minute + 30) % 60
                else:
                    endtime.minute += 30
                lng = gps_coords["lng"]
                lat = gps_coords["lat"]
                event = Event(title.string.strip(), str(starttime), str(endtime), address.strip(), "POINT(" + str(lng) + " " + str(lat) + ")", "")
                db.session.add(event)
                db.session.commit()
                print("---------------------------------------------------------------------")
                print("Title:   " + title.string)
                print("Address: " + address)
                print("Start:   " + str(starttime))
                print("End:     " + str(endtime))
                print("GPS:     " + json.dumps(gps_coords))
            except Exception as e:
                print(e)
                #print(title.string)
                #print(time)
                #print(address)
                continue
