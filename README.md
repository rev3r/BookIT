# Getting started

## Using dotnet

[Install](https://git-scm.com/downloads) git

[Install](https://dotnet.microsoft.com/en-us/download) dotnet (version 9.0)

```sh
git clone https://github.com/rev3r/BookIT.git
```
```sh
cd BookIT
```
```sh
dotnet run --project ConsoleApp -v q --hotels <path> --bookings <path>
```

## Using docker

[Install](https://git-scm.com/downloads) git

[Install](https://docs.docker.com/get-started/get-docker/) docker

```sh
git clone https://github.com/rev3r/BookIT.git
```
```sh
cd BookIT
```
```sh
docker build -t bookit -f ConsoleApp/Dockerfile .
```
```sh
docker run -it bookit --hotels <path> --bookings <path>
```
