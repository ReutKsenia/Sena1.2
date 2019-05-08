CREATE DATABASE ToDo;

use ToDo;

create table "User"
(
	Id int primary key identity(1,1),
	UserName nvarchar(50) not null,
	Login nvarchar(50) not null,
	Password nvarchar(200) not null,
	Foto image
);

create table List
(
	id int primary key identity(1,1),
	Title nvarchar(50) not null,
	DateCreate date,
	UserId int foreign key references "User"(id)
);

create table Task
(
	id int primary key identity(1,1),
	Category nvarchar(50) not null,
	Completed bit,
	Priority nvarchar(50),
	DateExpected date,
	Value nvarchar(max) not null,
	ListId int foreign key references List(id)
);