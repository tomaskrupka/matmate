#!/bin/bash

# Start the ASP.NET app in the background
dotnet Api.dll &

# Start Nginx in the foreground
nginx -g 'daemon off;'
