1.1) Prerequisites

install-package Confluent.Kafka
install-package FluentValidation
install-package MediatR
install-Package Microsoft.Extensions.Caching.Redis 
install-package StackExchange.Redis
install-package Swashbuckle.AspNetCore.SwaggerUI
install-package Swashbuckle.AspNetCore.SwaggerGen
install-package MongoDB.Driver
install-package Newtonsoft.Json

1.2) References

2) Recreating library

2.1) Run `dotnet new classlib  -o CM.Shared.Kernel -f netcoreapp2.1`
2.2) Move: 
	- directories:
		- *
	- files:
		- readme.recreating.txt

3) Publishing library
3.1) dotnet pack
3.2) dotnet nuget push bin/Debug/{nupkg-name} -k {nuget-api-key} -s {repository-source-uri}