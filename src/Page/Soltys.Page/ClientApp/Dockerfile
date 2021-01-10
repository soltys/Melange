FROM node:14.15.1 as builder

WORKDIR /usr/src/app

ENV PATH /usr/src/app/node_modules/.bin:$PATH
COPY package.json /usr/src/app/package.json
RUN yarn install --silent
COPY . /usr/src/app
RUN yarn build

### STAGE 2: Production Environment ###
FROM nginx:1.17-alpine
COPY nginx.conf /etc/nginx/nginx.conf
COPY --from=builder /usr/src/app/build /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
