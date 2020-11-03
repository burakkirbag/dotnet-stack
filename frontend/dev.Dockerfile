FROM node:alpine

WORKDIR /app

ADD package.json package-lock.json ./

RUN npm install

ADD .browserslistrc .
ADD .prettierrc .
ADD .eslintrc.js .
ADD babel.config.js .
ADD vue.config.js .
ADD .env .

VOLUME [ "/app/src" ]
VOLUME [ "/app/public" ]

CMD [ "npm", "run", "serve" ]
