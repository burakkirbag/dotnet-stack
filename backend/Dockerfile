FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine as builder

WORKDIR /source
COPY . .

RUN dotnet restore
RUN dotnet publish -c Release -o /app/

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine as baseimage
WORKDIR /app
COPY --from=builder /app .
ENV ASPNETCORE_ENVIRONMENT=Production

CMD [ "dotnet", "stack.dll" ]