CREATE DATABASE bdEcommerce;

USE bdEcommerce;

CREATE TABLE Usuario(
	Id int primary key auto_increment,
    Nome varchar(50) not null,
    Email varchar(50) not null,
    Senha varchar(50) not null
);

CREATE TABLE Cliente(
	CodCli int primary key auto_increment,
    NomeCli varchar(50) not null,
    TelCli varchar(20) not null,
    EmailCli varchar(50) not null
);

create table Produto(
IdPRod int primary key auto_increment,
Nome varchar (50),
Descricao varchar (100),
Preco decimal (10, 2),
quantidade varchar (100)
);


insert into Usuario (Id, Nome, Email, Senha) values (null, "Murilo", "Admin@email.com", 1234);

SELECT * FROM Usuario;
SELECT * FROM Cliente;
SELECT * FROM Produto;
