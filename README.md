Dnsk - dotnet starter kit
=========================

This project is the basic starting point for my dotnet based projects, 
revolving around the primary technologies:

* Client - Blazor WASM
* Server - Aspnet core with RPC pattern
* DB - Ef core


To build and run unit tests:
```bash
./bin/pre
```
To build and run the app:
```bash
./bin/run
```
If there has been a db schema change you can pass parameter `nuke` to either `./bin/pre` or `./bin/run` to delete
docker containers and rebuild them, this is typically useful if there has been a db schema change.