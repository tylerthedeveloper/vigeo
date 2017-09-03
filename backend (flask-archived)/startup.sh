pkill -9 -f uwsgi*
../env/bin/uwsgi --catch-exceptions --gevent 100 --master --enable-threads --thunder-lock --socket 127.0.0.1:8080 -w vigeo:app

//lock --socket [::]:127.0.0.1:8080 -w vigeo:app


