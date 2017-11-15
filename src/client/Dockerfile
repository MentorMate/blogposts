FROM nginx
RUN rm -rf /usr/share/nginx/html/*
COPY Docker.nginx.default.conf /etc/nginx/conf.d/
COPY dist /usr/share/nginx/html
EXPOSE 80