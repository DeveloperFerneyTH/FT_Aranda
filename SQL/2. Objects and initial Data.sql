print '--****** Creación de tablas y data inicial para la prueba de Aranda. Realizada por Ferney Talero ******--'
print '***** Inicio de ejecución ' + Convert(varchar(50), getdate()) + ' *****'

Use FTArandaDB
Declare @tableName varchar(50)

Set @tableName = 'Role'
IF OBJECT_ID(@tableName) is not null Begin
	print '--> Eliminando tabla ' + @tableName
	Drop table [User]
	Drop table [Role]
End

print '--> Creando tabla ' + @tableName
Create table [Role]
(
	ID int identity primary key,
	[Name] varchar(50) unique not null,
	[Description] varchar(max) null,
	CreatedDate datetime default getdate() null,
	UpdatedDate datetime default getdate() null
)

Set @tableName = 'User'
IF OBJECT_ID(@tableName) is not null Begin
	print '--> Eliminando tabla ' + @tableName
	Drop table [User]
End

print '--> Creando tabla ' + @tableName
Create table [User]
(
	ID int identity primary key,
	UserName varchar(50) unique not null,
	[Password] varchar(max) not null,
	FirtsName varchar(100) not null,
	LastName varchar(100) not null,
	[Address] varchar(max) not null,
	Phone varchar(20) not null,
	Email varchar(50) not null,
	Role_ID int foreign key references [Role](ID) not null,
	CreatedDate datetime default getdate() null,
	UpdatedDate datetime default getdate() null
)

Set @tableName = 'Login'
IF OBJECT_ID(@tableName) is not null Begin
	print '--> Eliminando tabla ' + @tableName
	Drop table [Login]
End

print '--> Creando tabla ' + @tableName
Create table [Login]
(
	ID int identity primary key,
	UserName varchar(50) not null,
	Attemps int default 0 not null,
	Blocked bit null,
	LastEntryDate datetime default getdate() null
)

print '**** Insertando data inicial ****'
print '--> Insertando Roles '
Insert Into [Role]
Values	('Administrador', 'Permisos absolutos sobre todo el sistema', default, default),
		('Visitante', 'Solo observa un mensaje de bienvenida y noticias de la empresa (información fija)', default, default),
		('Asistente', 'Observar el mensaje de bienvenida, listar usuarios, filtrar por nombre y filtrar por rol', default, default),
		('Editor', 'Lo que hace el asistente y además edición de los datos de cualquier usuario', default, default)

print '--> Insertando Usuarios'
Insert Into [User]
Values	('ADMON', '123', 'Ferney', 'Talero', 'Calle 77b #129-70', '3134402123', 'ferney.th@gmail.com', 1, default, null)

print '***** Fin de ejecución ' + Convert(varchar(50), getdate()) + ' *****'