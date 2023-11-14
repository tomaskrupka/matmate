# Stage 1: Build the Quasar/Vue.js frontend
FROM node as build-stage-webapp
WORKDIR /app
COPY /webapp/ .
RUN npm install
# Build the static files
RUN npx quasar build

# Stage 2: Build the ASP.NET backend
FROM mcr.microsoft.com/dotnet/sdk AS build-stage-api
WORKDIR /source
COPY /api/Api .
RUN dotnet publish -c release -o /app

# Stage 3: Build the final image with Nginx to serve the static files and reverse proxy
FROM mcr.microsoft.com/dotnet/aspnet AS final-stage
WORKDIR /app
COPY --from=build-stage-api /app ./

# Set environment variable to listen on port 5140
ENV ASPNETCORE_URLS=http://+:5140

# Install nginx
RUN apt-get update 
RUN apt-get install -y nginx

# Copy nginx configuration
COPY nginx.conf /etc/nginx/nginx.conf

# Copy static files from the build-stage-webapp
COPY --from=build-stage-webapp /app/dist/spa /var/www/html

# Make sure the ASP.NET app runs when the container starts
ENTRYPOINT ["dotnet", "Api.dll"]

# Setup nginx to start as well
CMD ["nginx", "-g", "daemon off;"]
