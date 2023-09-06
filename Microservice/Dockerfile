#https://hub.docker.com/_/microsoft-dotnet-aspnet
#5.0.8-alpine3.13-amd64, 5.0-alpine3.13-amd64, 5.0-alpine-amd64, 5.0.8-alpine3.13, 5.0-alpine3.13, 5.0-alpine 
#FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base

FROM mcr.microsoft.com/dotnet/aspnet:5.0.8-alpine3.13-amd64 AS base
#FROM mcr.microsoft.com/dotnet/aspnet:aspnet:5.0-focal-amd64 AS base
 

ARG BUILD_CONFIGURATION=Release
ENV ASPNETCORE_ENVIRONMENT=Release
ENV DOTNET_USE_POLLING_FILE_WATCHER=true
ENV ASPNETCORE_URLS=http://+:80
WORKDIR /app
EXPOSE 80
EXPOSE 443


ENV TZ=America/New_York

#--------------------------------------------------------------
# Set up Datadog APM
#RUN apt-get update && apt-get install -y curl
#RUN apt-get update -y  && apt-get upgrade -y  &&  apt-get install -y curl
RUN apk update && apk upgrade
RUN apk add --no-cache wget
RUN apk add --no-cache curl
RUN apk add --no-cache dpkg




#RUN curl -LO https://github.com/DataDog/dd-trace-dotnet/releases/download/v1.28.0/datadog-dotnet-apm_1.28.0_amd64.deb \
#    && dpkg -i ./datadog-dotnet-apm_1.28.0_amd64.deb \
#    && rm datadog-*.deb

RUN  mkdir -p /opt/datadog
RUN curl -L https://github.com/DataDog/dd-trace-dotnet/releases/download/v1.28.0/datadog-dotnet-apm-1.28.0.tar.gz \
  |  tar xzf - -C /opt/datadog

RUN apk add icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
ENV CORECLR_ENABLE_PROFILING=1
ENV CORECLR_PROFILER={846F5F1C-F9AE-4B07-969E-05C26BC060D8}
ENV CORECLR_PROFILER_PATH=/opt/datadog/Datadog.Trace.ClrProfiler.Native.so
ENV DD_INTEGRATIONS=/opt/datadog/integrations.json
ENV DD_DOTNET_TRACER_HOME=/opt/datadog

#RUN apt-get purge -y curl
#--------------------------------------------------------------



#https://hub.docker.com/_/microsoft-dotnet-sdk
#FROM mcr.microsoft.com/dotnet/core/sdk:3.1.402-buster AS build
#FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
#FROM mcr.microsoft.com/dotnet/sdk:5.0.302-focal-amd64 AS build

#ARG ARTIFACTS_ENDPOINT=https://ab-inbev.pkgs.visualstudio.com/LAS_Infra_DevOpsNuget/_packaging/ABIDevOpsNuget/nuget/v3/index.json
#ARG ACCESS_TOKEN=dcvul2txdklsezva3l4kbrpshpjclhhvbqi3w4esyy34u4foibla
#ARG USER

RUN curl -o /usr/local/share/ca-certificates/verisign.crt -SsL https://crt.sh/?d=1039083 && update-ca-certificates


#WORKDIR /src

WORKDIR /

COPY ./ABI.API.Structure.sln   ./
RUN true
  
COPY ./ABI.API.Structure/ABI.API.Structure.csproj ./ABI.API.Structure/ABI.API.Structure.csproj
#RUN true

COPY ./ABI.API.Structure.Application/ABI.API.Structure.Application.csproj ./ABI.API.Structure.Application/ABI.API.Structure.Application.csproj
RUN true

COPY ./ABI.API.Structure.Infrastructure/ABI.API.Structure.Infrastructure.csproj ./ABI.API.Structure.Infrastructure/ABI.API.Structure.Infrastructure.csproj
RUN true

COPY ./ABI.API.Structure.Domain/ABI.API.Structure.Domain.csproj ./ABI.API.Structure.Domain/ABI.API.Structure.Domain.csproj
RUN true

COPY ./ABI.API.Structure.ACL.Truck/ABI.API.Structure.ACL.Truck.csproj ./ABI.API.Structure.ACL.Truck/ABI.API.Structure.ACL.Truck.csproj
RUN true

COPY ./ABI.API.Structure.APIClient.Truck/ABI.API.Structure.APIClient.Truck.csproj ./ABI.API.Structure.APIClient.Truck/ABI.API.Structure.APIClient.Truck.csproj
RUN true

 
COPY ./ABI.API.Structure.Unit.Tests/ABI.API.Structure.Unit.Tests.csproj ./ABI.API.Structure.Unit.Tests/ABI.API.Structure.Unit.Tests.csproj
RUN true

COPY ./ABI.Api.Structure.Integration.Tests/ABI.Api.Structure.Integration.Tests.csproj ./ABI.Api.Structure.Integration.Tests/ABI.Api.Structure.Integration.Tests.csproj
RUN true

COPY ./ABI.API.Structure.ACL.Truck.Unit.Tests/ABI.API.Structure.ACL.Truck.Unit.Tests.csproj ./ABI.API.Structure.ACL.Truck.Unit.Tests/ABI.API.Structure.ACL.Truck.Unit.Tests.csproj
RUN true



#COPY ./GenerateSwagger/GenerateSwagger.csproj ./GenerateSwagger/GenerateSwagger.csproj
#RUN true
 
COPY ./nuget.config ./nuget.config
#RUN true

 
RUN dotnet restore --configfile nuget.config

 
#RUN dotnet restore
COPY . .
RUN true

#build
 
RUN dotnet build "./ABI.API.Structure/ABI.API.Structure.csproj" -c Docker -o /app


#publish
FROM build AS publish
RUN dotnet publish "./ABI.API.Structure/ABI.API.Structure.csproj" -c Docker -o /app


#App image
FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ABI.API.Structure.dll"]





