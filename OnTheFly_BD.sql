CREATE DATABASE OnTheFly;

USE OnTheFly;

CREATE TABLE Passageiro(
	CPF varchar(11) PRIMARY KEY NOT NULL,
	Nome varchar(50) NOT NULL,
	Data_Nasc date NOT NULL,
	Sexo varchar(10) NOT NULL,
	Data_UltimaCompra date,
	Data_Cadastro date NOT NULL,
	Situação varchar(1) NOT NULL,
);
CREATE TABLE CiaAerea(
	CNPJ varchar(14) PRIMARY KEY NOT NULL,
	RazaoSocial varchar(50) NOT NULL,
	Data_Abertura date NOT NULL,
	Data_Cadastro date NOT NULL,
	Data_UltimoVoo date NOT NULL,
	Situacao varchar(1) NOT NULL,
);
CREATE TABLE Aeronave(
	InscricaoAnac varchar(6) PRIMARY KEY NOT NULL,
	CNPJ varchar(14) FOREIGN KEY REFERENCES CiaAerea(CNPJ),
	Data_Cadastro date NOT NULL,
	Situacao varchar(1) NOT NULL,
	UltimaVenda date NOT NULL,
	Capacidade int NOT NULL,
);
CREATE TABLE Voo(
	ID_Voo varchar(10) PRIMARY KEY NOT NULL,
	Destino varchar(3) NOT NULL,
	Data_Cadastro date NOT NULL,
	Data_Voo date NOT NULL,
	Situacao varchar(1) NOT NULL,
	Inscricao varchar(6) FOREIGN KEY REFERENCES Aeronave(InscricaoAnac),
	AssentosOcupados int NOT NULL,

);
CREATE TABLE Passagem(
	ID_Passagem varchar(10) PRIMARY KEY NOT NULL,
	ID_Voo varchar(10) FOREIGN KEY REFERENCES Voo(ID_Voo) NOT NULL,
	Data_UltimaOp date NOT NULL,
	Valor_Unit float NOT NULL,
	Valor_Total float NOT NULL,
	Situacao varchar(1) NOT NULL,
);
CREATE TABLE VendaPassagem(
	ID_Venda int PRIMARY KEY NOT NULL,
	Data_Venda date NOT NULL,
	Valor_Total float NOT NULL,
	ID_Passagem varchar(10) FOREIGN KEY REFERENCES Passagem(Id_Passagem) NOT NULL,
	CPF varchar(11) FOREIGN KEY REFERENCES Passageiro (CPf) NOT NULL,
);
CREATE TABLE Cpf_Restrito(
	Cpf varchar(11) PRIMARY KEY NOT NULL,
);
CREATE TABLE Cnpj_Restrito(
	Cnpj varchar(14) PRIMARY KEY NOT NULL,
);