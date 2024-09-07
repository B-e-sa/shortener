[Leia em português](#introduction)

# Shortener

## Introduction

Shortener URL shortening application API.

## Architecture

The application follows a clean architecture:
```
Src
|__Application       # Manages business logic, acts as an intermediary between the presentation and domain layers.
|__Domain            # Centralizes business logic, domain entities, and services.
|__Infrastructure    # Data persistence and external services.
|__Presentation      # Interacts with the user. Controllers are located in this layer.
|__Web               # Application entry point and general management.
|__Tests
```

## How to run

Add a connection string for your database in the **appsettings.json** file, located in the **Web** directory.<br><br>
The application already has an appsettings.Example, so you can remove the ".Example" and simply run the application with the default connection set in the file.<br>

1. Start your database within Docker
```bash
docker compose up
```

2. Inside the Web directory,
```bash
dotnet run
```

---

# Shortener

## Introdução

API da aplicação de encurtamento de URL, Shortener

## Arquitetura

A aplicação implementa uma arquitetura clean:
```
Src
|__Application       # Gerencia as lógicas de negócio, intermediário entre a camada de apresentação e domínio.
|__Domain            # Centraliza as lógicas de negócio, entidades de domínio e serviços
|__Infrastructure    # Persistência de dados e serviços externos
|__Presentation      # Interage com o usuário. No projeto, se localizam os controllers
|__Web               # Entrada da aplicação e gerenciamento geral
|__Tests
```

## Como rodar

Adicione uma string de conexão de seu banco de dados ao arquivo **appsettings.json**, dentro do diretório **Web**.<br><br>
A aplicação já possui um appsettings.Example, então você pode remover o .Example e apenas rodar o aplicativo com a conexão default estabelecida no arquivo<br>

1. Inicialize seu banco de dados dentro do Docker
```bash
docker compose up
```

2. Dentro do diretório Web,
```bash
dotnet run
```
