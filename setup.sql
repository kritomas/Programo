create table Lang
(
	id int primary key identity(1, 1),
	name varchar(40) not null
);

create table Programmer
(
	id int primary key identity(1, 1),
	username varchar(100) not null,
);

create table Certification
(
	id int primary key identity(1, 1),
	lang_id int foreign key references Lang(id) not null,
	programmer_id int foreign key references Programmer(id) not null,
	date_start datetime not null,
	date_end datetime not null,
	check(date_end >= date_start)
);

create table Project
(
	id int primary key identity(1, 1),
	name varchar(250) not null,
	is_abandoned bit not null
);

create table Work
(
	id int primary key identity(1, 1),
	project_id int foreign key references Project(id) not null,
	programmer_id int foreign key references Programmer(id) not null,
	date_start datetime not null,
	date_end datetime not null,
	check(date_end >= date_start)
);

create table Feature
(
	id int primary key identity(1, 1),
	project_id int foreign key references Project(id) not null,
	name varchar(250) not null,
	is_complete bit not null default 0
);