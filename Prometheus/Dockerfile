FROM prom/prometheus:v2.55.1 as image-prometheus-base
COPY ./prometheus.yml /etc/prometheus

VOLUME ./data
EXPOSE 9090