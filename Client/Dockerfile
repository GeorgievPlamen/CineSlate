FROM node:22-bullseye AS build

WORKDIR /app

COPY package*.json ./

RUN --mount=type=cache,target=/app/.npm \
    npm set cache /app/.npm && \
    npm ci

COPY . .

RUN npm install

CMD ["npm". "run","dev"]