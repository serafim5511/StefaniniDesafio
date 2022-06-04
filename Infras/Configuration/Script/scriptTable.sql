Create table Pessoa (
	Id int NOT NULL IDENTITY (1,1) PRIMARY KEY,
	Nome Varchar(25)NOT NULL,
	Cpf Varchar(25)NOT NULL,
	Idade int NOT NULL,
	Id_Cidade int FOREIGN KEY REFERENCES Cidade(Id)
)

Create table Cidade (
	Id int NOT NULL IDENTITY (1,1)PRIMARY KEY,
	Nome Varchar(25)NOT NULL,
	UF Varchar(25)NOT NULL
)