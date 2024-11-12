FROM docker.io/library/mysql:8.0.29 as mysql-local

ENV MYSQL_RANDOM_ROOT_PASSWORD="1"
ENV MYSQL_USER="mysql"
ENV MYSQL_PASSWORD="123456"
ENV MYSQL_DATABASE="poc-prometheus-grafana"

COPY ./dump/acesso.sql /docker-entrypoint-initdb.d
COPY ./dump/criar-tabelas.sql /docker-entrypoint-initdb.d

VOLUME ./dbmysql-volume
EXPOSE 3306