CREATE DATABASE DB_EMPRESAS_DOTNET;

CREATE TABLE TB_TIPO_EMPRESA(
    id INTEGER IDENTITY NOT NULL,
    nome VARCHAR(50) NOT NULL,
    sigla VARCHAR(8) NOT NULL,
    PRIMARY KEY(id)
);

CREATE TABLE TB_EMPRESA(
    id INTEGER IDENTITY NOT NULL,
    id_tipo_empresa INTEGER NOT NULL,
    nome VARCHAR(50) NOT NULL,
    numero_cnpj VARCHAR(18),
    PRIMARY KEY(id),
    FOREIGN KEY (id_tipo_empresa) REFERENCES TB_TIPO_EMPRESA (id)
);

CREATE TABLE TB_USERS(
	UserID varchar(20) NOT NULL,
	AccessKey varchar(32) NOT NULL,
	CONSTRAINT PK_Clientes PRIMARY KEY (UserID)
)
GO


INSERT INTO TB_TIPO_EMPRESA VALUES ('Sociedade Empresária Limitada', 'Ltda');
INSERT INTO TB_TIPO_EMPRESA VALUES ('Empresa Individual de Responsabilidade Limitada', 'Eireli');
INSERT INTO TB_TIPO_EMPRESA VALUES ('Sociedade Simples', 'SS');
INSERT INTO TB_TIPO_EMPRESA VALUES ('Sociedade Anônima', 'SA');

INSERT INTO TB_EMPRESA VALUES(1, 'Empresa 1', '87.737.800/0001-78');
INSERT INTO TB_EMPRESA VALUES(2, 'Empresa 2', '14.594.545/0001-88');
INSERT INTO TB_EMPRESA VALUES(3, 'Empresa 3', '99.322.516/0001-10');
INSERT INTO TB_EMPRESA VALUES(4, 'Empresa 4', '53.697.382/0001-10');
INSERT INTO TB_EMPRESA VALUES(1, 'Empresa 5', '80.024.966/0001-06');

INSERT INTO TB_USERS
           (UserID
           ,AccessKey)
     VALUES
           ('usuario01'
           ,'94be650011cf412ca906fc335f615cdc')

INSERT INTO TB_USERS
           (UserID
           ,AccessKey)
     VALUES
           ('usuario02'
           ,'531fd5b19d58438da0fd9afface43b3c')
