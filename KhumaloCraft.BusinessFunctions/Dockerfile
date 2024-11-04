# Use the official Azure Functions .NET runtime image
FROM mcr.microsoft.com/azure-functions/dotnet:4

# Set the working directory for the function app
WORKDIR /home/site/wwwroot

# Copy the compiled function app files to the container
COPY ./bin/Release/net6.0/publish/ /home/site/wwwroot

# Expose the default port for Azure Functions
EXPOSE 80

# Run Azure Functions when the container starts
CMD ["func", "start", "--dotnet"]
