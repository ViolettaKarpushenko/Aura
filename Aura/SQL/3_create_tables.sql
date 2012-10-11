CREATE TABLE [t_unit_categories](
     [ID]       integer         NOT NULL PRIMARY KEY ASC AUTOINCREMENT
    ,[CName]    nvarchar(50)    NOT NULL UNIQUE
    ,[UName]    nvarchar(50)    NOT NULL UNIQUE
    ,[SUName]   nvarchar(5)     NOT NULL UNIQUE
);

CREATE TABLE [t_units](
     [ID]           integer         NOT NULL PRIMARY KEY ASC AUTOINCREMENT
    ,[CID]          integer         NOT NULL REFERENCES [t_units_category] ([ID])
    ,[Factor]       decimal(10, 5)  NOT NULL DEFAULT 1
    ,[Prefix]       nvarchar(50)    NULL
    ,[SPrefix]      nvarchar(5)     NULL
    ,[AlterName]    nvarchar(50)    NULL
);

CREATE TABLE [t_regions](
     [ID]   integer         NOT NULL PRIMARY KEY ASC AUTOINCREMENT
    ,[Name] nvarchar(50)    NOT NULL UNIQUE
);

CREATE TABLE [t_water_resources](
     [ID]               integer         NOT NULL PRIMARY KEY ASC AUTOINCREMENT REFERENCES [t_regions] ([ID])
    ,[RW]               decimal(10, 5)  NULL        --re4noi stok
    ,[UW]               decimal(10, 5)  NULL        --pod3emnye vody
    ,[LW]               decimal(10, 5)  NULL        --ob'em vody v o3erah
);

CREATE TABLE [t_territorial_resources](
     [ID]   integer         NOT NULL PRIMARY KEY ASC AUTOINCREMENT REFERENCES [t_regions] ([ID])
    ,[GA]   decimal(10, 5)  NULL    --zemel'nye
    ,[AA]   decimal(10, 5)  NULL    --s/x
    ,[LA]   decimal(10, 5)  NULL    --ozernye
);

CREATE TABLE [t_biological_resources](
     [ID]   integer         NOT NULL PRIMARY KEY ASC AUTOINCREMENT REFERENCES [t_regions] ([ID])
    ,[WO]   decimal(10, 5)  NULL    --drevesina
    ,[MP]   decimal(10, 5)  NULL    --lekarstvennye
    ,[FP]   decimal(10, 5)  NULL    --piwevye
    ,[MU]   decimal(10, 5)  NULL    --griby
    ,[PH]   decimal(10, 5)  NULL    --fitoplankton
    ,[MC]   decimal(10, 5)  NULL    --makrofity
);

CREATE TABLE [t_animals](
     [ID]   integer         NOT NULL PRIMARY KEY ASC AUTOINCREMENT REFERENCES [t_regions] ([ID])
);

CREATE TABLE [t_animal_orders](
     [ID]   integer         NOT NULL PRIMARY KEY ASC AUTOINCREMENT
    ,[Name] nvarchar(50)    NOT NULL UNIQUE
);

CREATE TABLE [t_animal_species](
     [ID]   integer         NOT NULL PRIMARY KEY ASC AUTOINCREMENT
    ,[Name] nvarchar(50)    NOT NULL UNIQUE
    ,[OID]  integer         NOT NULL REFERENCES [t_animal_orders] ([ID])
);

CREATE TABLE [t_animal_species_to_animals](
     [SID]      integer         NOT NULL REFERENCES [t_animal_species] ([ID])
    ,[AID]      integer         NOT NULL REFERENCES [t_animals] ([ID])
    ,[Quantity] decimal(10, 5)  NULL
    ,PRIMARY KEY([SID], [AID])
);

CREATE TABLE [t_animal_others](
     [ID]   integer         NOT NULL PRIMARY KEY ASC AUTOINCREMENT
    ,[Name] nvarchar(50)    NOT NULL UNIQUE
);

CREATE TABLE [t_animal_others_to_animals](
     [OID]      integer         NOT NULL REFERENCES [t_animal_others] ([ID])
    ,[AID]      integer         NOT NULL REFERENCES [t_animals] ([ID])
    ,[Quantity] decimal(10, 5)  NULL
    ,PRIMARY KEY([OID], [AID])
);

CREATE TABLE [t_mineral_resources](
     [ID]   integer         NOT NULL PRIMARY KEY ASC AUTOINCREMENT REFERENCES [t_regions] ([ID])
    ,[DL]   decimal(10, 5)  NULL --dolomity
    ,[AR]   decimal(10, 5)  NULL --glinistye
    ,[GS]   decimal(10, 5)  NULL --graviino-pes4anye
    ,[SA]   decimal(10, 5)  NULL --peski
    ,[PE]   decimal(10, 5)  NULL --torf
    ,[SP]   decimal(10, 5)  NULL --sapropel
);
--------------------------------------------------------
