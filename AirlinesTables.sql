--Customer table
create table Customer(
[Id] int primary key identity(1,1) not null,
[FirstName] nvarchar(50) not null check([FirstName] <> ' '),
[LastName] nvarchar(50) not null check([LastName] <> ' '),
[Email] nvarchar(50) not null check([Email] <> ' '),
[PhoneNumber] nvarchar(20) not null check([PhoneNumber] <> ' ')
);

--Companies table
create table Companies(
[Id] int primary key identity(1,1) not null,
[Name] nvarchar(50) not null check([Name] <> ' '),
[Adress] nvarchar(100) not null check([Adress] <> ' '),
[PhoneNumber] nvarchar(20) not null check([PhoneNumber] <> ' ')
);

--Destinations table
create table Destinations(
[Id] int primary key identity(1,1) not null,
[City] nvarchar(50) not null check([City] <> ' '),
[Country] nvarchar(100) not null check([Country] <> ' '),
);

--Flights table
create table Flights(
[Id] int primary key identity(1,1) not null,
[FlightNumber] nvarchar(10) not null check([FlightNumber] <> ' '),
[DepartureDate] datetime not null,
[ArrivalDate] datetime not null,
[DepartureCity] nvarchar(50) not null check([DepartureCity] <> ' '),
[ArrivalCity] nvarchar(50) not null check([ArrivalCity] <> ' '),
[CompanyId] int not null,
[DestinationId] int not null,

 FOREIGN KEY ([CompanyId]) references Companies([Id]),
 FOREIGN KEY ([DestinationId]) references Destinations([Id])
);

--Ticket table
create table Tickets(
[Id] int primary key identity(1,1) not null,
[TicketNumber] nvarchar(20) not null check([TicketNumber] <> ' '),
[Price] money not null,
[Seat] varchar(10) not null,
[SaleDate] datetime not null,
[FlightId] int not null,
[CustomerId] int not null,

  FOREIGN KEY ([FlightId]) REFERENCES Flights(id),
  FOREIGN KEY ([CustomerId]) REFERENCES Customer(id)
);