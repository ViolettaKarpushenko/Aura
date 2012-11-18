DROP VIEW IF EXISTS [v_resut];
DROP VIEW IF EXISTS [v_result_mineral_resources];
DROP VIEW IF EXISTS [v_a_mineral_resources];
DROP VIEW IF EXISTS [v_result_water_resources];
DROP VIEW IF EXISTS [v_a_water_resources];
DROP VIEW IF EXISTS [v_result_territorial_resources];
DROP VIEW IF EXISTS [v_a_territorial_resources];
DROP VIEW IF EXISTS [v_result_biological_resources];
DROP VIEW IF EXISTS [v_a_biological_resources];
DROP VIEW IF EXISTS [v_result_animals_resorces];
DROP VIEW IF EXISTS [v_a_animal_orders];
DROP VIEW IF EXISTS [v_a_animal_species];
DROP VIEW IF EXISTS [v_a_animal_others];
DROP VIEW IF EXISTS [v_t_mineral_resources];
DROP VIEW IF EXISTS [v_t_water_resources];
DROP VIEW IF EXISTS [v_t_biological_resources];
DROP VIEW IF EXISTS [v_t_territorial_resources];
--------------------------------------------------------


DROP TABLE IF EXISTS [t_mineral_resources];
DROP TABLE IF EXISTS [t_animal_others_to_animals];
DROP TABLE IF EXISTS [t_animal_species_to_animals];
DROP TABLE IF EXISTS [t_animal_species];
DROP TABLE IF EXISTS [t_animal_orders];
DROP TABLE IF EXISTS [t_animal_others];
DROP TABLE IF EXISTS [t_animals];
DROP TABLE IF EXISTS [t_water_resources];
DROP TABLE IF EXISTS [t_biological_resources];
DROP TABLE IF EXISTS [t_territorial_resources];
DROP TABLE IF EXISTS [t_regions];
DROP TABLE IF EXISTS [t_units];
DROP TABLE IF EXISTS [t_unit_categories];
--------------------------------------------------------



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



CREATE VIEW [v_a_mineral_resources] AS
    SELECT
         t.[ID]
        ,ifnull(t.[DL] / a.[DL], 0) AS 'DL'
        ,ifnull(t.[AR] / a.[AR], 0) AS 'AR'
        ,ifnull(t.[GS] / a.[GS], 0) AS 'GS'
        ,ifnull(t.[SA] / a.[SA], 0) AS 'SA'
        ,ifnull(t.[PE] / a.[PE], 0) AS 'PE'
        ,ifnull(t.[SP] / a.[SP], 0) AS 'SP'

        ,ifnull(t.[DL] / a.[DL], 0) AS 'B'
        ,ifnull(t.[AR] / a.[AR], 0) AS 'C'
        ,ifnull(t.[GS] / a.[GS], 0) AS 'D'
        ,ifnull(t.[SA] / a.[SA], 0) AS 'E'
        ,ifnull(t.[PE] / a.[PE], 0) AS 'F'
        ,ifnull(t.[SP] / a.[SP], 0) AS 'G'
    FROM [t_mineral_resources] AS t
    CROSS JOIN(
        SELECT 
             avg([DL]) AS 'DL'
            ,avg([AR]) AS 'AR'
            ,avg([GS]) AS 'GS'
            ,avg([SA]) AS 'SA'
            ,avg([PE]) AS 'PE'
            ,avg([SP]) AS 'SP'
        FROM [t_mineral_resources]
    ) AS a;

CREATE VIEW [v_result_mineral_resources] AS
    SELECT
         r.[ID]
        ,r.[Name] AS 'Район'
        ,a.[B] + a.[C] + a.[D] + a.[E] + a.[F] + a.[G] AS 'Индекс величиеы ресурсов терротории'
        ,a.[G] AS 'Индекс величины ресурвов озер'
        ,a.[G] * 100 / (a.[B] + a.[C] + a.[D] + a.[E] + a.[F] + a.[G]) AS 'Доля ресурсов озёр в суммарном запасе'
        ,a.[G] / (a.[B] + a.[C] + a.[D] + a.[E] + a.[F]) AS 'Коэффициент соотношения ресурсов'
    FROM [t_mineral_resources] AS t
    INNER JOIN [v_a_mineral_resources] AS a ON a.[ID] = t.[ID]
    JOIN [t_regions] AS r ON r.[ID] = t.[ID];

CREATE VIEW [v_a_water_resources] AS
    SELECT
         t.[ID]
        ,ifnull(t.[RW] / a.[RW], 0) AS 'RW'
        ,ifnull(t.[UW] / a.[UW], 0) AS 'UW'
        ,ifnull(t.[LW] / a.[LW], 0) AS 'LW'

        ,ifnull(t.[RW] / a.[RW], 0) AS 'B'
        ,ifnull(t.[UW] / a.[UW], 0) AS 'C'
        ,ifnull(t.[LW] / a.[LW], 0) AS 'D'
    FROM [t_water_resources] AS t
    CROSS JOIN(
        SELECT 
             avg([RW]) AS 'RW'
            ,avg([UW]) AS 'UW'
            ,avg([LW]) AS 'LW'
        FROM [t_water_resources]
    ) AS a;

CREATE VIEW [v_result_water_resources] AS
    SELECT
         r.[ID]
        ,r.[Name] AS 'Район'
        ,a.[B] + a.[C] + a.[D] AS 'Индекс величиеы ресурсов терротории'
        ,a.[D] AS 'Индекс величины ресурвов озер'
        ,a.[D] * 100 / (a.[B] + a.[C] + a.[D]) AS 'Доля ресурсов озёр в суммарном запасе'
        ,a.[D] / (a.[B] + a.[C]) AS 'Коэффициент соотношения ресурсов'
    FROM [t_water_resources] AS t
    INNER JOIN [v_a_water_resources] AS a ON a.[ID] = t.[ID]
    JOIN [t_regions] AS r ON r.[ID] = t.[ID];

CREATE VIEW [v_a_territorial_resources] AS
    SELECT
         t.[ID]
        ,ifnull(t.[GA] / a.[GA], 0) AS 'GA'
        ,ifnull(t.[AA] / a.[AA], 0) AS 'UW'
        ,ifnull(t.[LA] / a.[LA], 0) AS 'LW'

        ,ifnull(t.[GA] / a.[GA], 0) AS 'C'
        ,ifnull(t.[AA] / a.[AA], 0) AS 'D'
        ,ifnull(t.[LA] / a.[LA], 0) AS 'E'
    FROM [t_territorial_resources] AS t
    CROSS JOIN(
        SELECT 
             avg([GA]) AS 'GA'
            ,avg([AA]) AS 'AA'
            ,avg([LA]) AS 'LA'
        FROM [t_territorial_resources]
    ) AS a;

CREATE VIEW [v_result_territorial_resources] AS
    SELECT
         r.[ID]
        ,r.[Name] AS 'Район'
        ,a.[C] + a.[E] AS 'Индекс величиеы ресурсов терротории'
        ,a.[E] AS 'Индекс величины ресурвов озер'
        ,a.[E] / (a.[C] + a.[E]) AS 'Доля ресурсов озёр в суммарном запасе'
        ,a.[E] / a.[C] AS 'Коэффициент соотношения ресурсов'
    FROM [t_territorial_resources] AS t
    INNER JOIN [v_a_territorial_resources] AS a ON a.[ID] = t.[ID]
    JOIN [t_regions] AS r ON r.[ID] = t.[ID];
    
CREATE VIEW [v_a_biological_resources] AS
    SELECT
         t.[ID]
        ,ifnull(t.[WO] / a.[WO], 0) AS 'WO'
        ,ifnull(t.[MP] / a.[MP], 0) AS 'MP'
        ,ifnull(t.[FP] / a.[FP], 0) AS 'FP'
        ,ifnull(t.[MU] / a.[MU], 0) AS 'MU'
        ,ifnull(t.[PH] / a.[PH], 0) AS 'PH'
        ,ifnull(t.[MC] / a.[MC], 0) AS 'MC'

        ,ifnull(t.[WO] / a.[WO], 0) AS 'B'
        ,ifnull(t.[MP] / a.[MP], 0) AS 'C'
        ,ifnull(t.[FP] / a.[FP], 0) AS 'D'
        ,ifnull(t.[MU] / a.[MU], 0) AS 'E'
        ,ifnull(t.[PH] / a.[PH], 0) AS 'F'
        ,ifnull(t.[MC] / a.[MC], 0) AS 'G'
    FROM [t_biological_resources] AS t
    CROSS JOIN(
        SELECT 
             avg([WO]) AS 'WO'
            ,avg([MP]) AS 'MP'
            ,avg([FP]) AS 'FP'
            ,avg([MU]) AS 'MU'
            ,avg([PH]) AS 'PH'
            ,avg([MC]) AS 'MC'
        FROM [t_biological_resources]
    ) AS a;

CREATE VIEW [v_result_biological_resources] AS
    SELECT
         r.[ID]
        ,r.[Name] AS 'Район'
        ,a.[B] + a.[C] + a.[D] + a.[E] + a.[F] + a.[G] AS 'Индекс величиеы ресурсов терротории'
        ,a.[F] + a.[G] AS 'Индекс величины ресурвов озер'
        ,(a.[F] + a.[G]) * 100 / (a.[B] + a.[C] + a.[D] + a.[E] + a.[F] + a.[G]) AS 'Доля ресурсов озёр в суммарном запасе'
        ,(a.[F] + a.[G]) / (a.[B] + a.[C] + a.[D] + a.[E]) AS 'Коэффициент соотношения ресурсов'
    FROM [t_biological_resources] AS t
    INNER JOIN [v_a_biological_resources] AS a ON a.[ID] = t.[ID]
    JOIN [t_regions] AS r ON r.[ID] = t.[ID];

CREATE VIEW [v_a_animal_species] AS
    SELECT
         tt.[AID]
        ,ifnull(tt.[Quantity] / aa.[Avg], 0) AS 'Avg'
        ,s.[OID]
        ,s.[ID]
    FROM [t_animal_species] AS s
    INNER JOIN [t_animal_species_to_animals] AS tt ON tt.[SID] = s.[ID]
    INNER JOIN (
        SELECT t.[SID], avg(t.[Quantity]) AS 'Avg'
        FROM [t_animal_species_to_animals] AS t
        GROUP BY t.[SID]
    ) AS aa ON aa.[SID] = tt.[SID];

CREATE VIEW [v_a_animal_orders] AS
    SELECT
         [OID]
        ,[AID]
        ,avg([Avg]) AS 'Avg'
    FROM [v_a_animal_species]
    GROUP BY [AID], [OID];

CREATE VIEW [v_a_animal_others] AS
    SELECT
         a.[ID]
        ,tt.[Quantity] / aa.[Avg] AS 'Avg'
        ,tt.[AID]
    FROM [t_animal_others] AS a
    INNER JOIN [t_animal_others_to_animals] AS tt ON tt.[OID] = a.[ID]
    INNER JOIN (
        SELECT t.[OID], avg(t.[Quantity]) AS 'Avg'
        FROM [t_animal_others_to_animals] AS t
        GROUP BY t.[OID]
    ) AS aa ON aa.[OID] = tt.[OID];
    
CREATE VIEW [v_result_animals_resorces] AS
    SELECT
         r.[ID]
        ,r.[Name] AS 'Район'
        ,(SELECT sum(asp.[Avg]) FROM [v_a_animal_orders] AS asp WHERE asp.[AID] = a.[ID]) + (SELECT sum(aot.[Avg]) FROM [v_a_animal_others] AS aot WHERE aot.[AID] = a.[ID]) AS 'Запасы ПГХ'
        ,(SELECT sum(aot.[Avg]) FROM [v_a_animal_others] AS aot WHERE aot.[AID] = a.[ID]) AS 'Индекс величины ресурвов озер'
        ,(SELECT sum(aot.[Avg]) FROM [v_a_animal_others] AS aot WHERE aot.[AID] = a.[ID]) * 100 / ((SELECT sum(asp.[Avg]) FROM [v_a_animal_orders] AS asp WHERE asp.[AID] = a.[ID]) + (SELECT sum(aot.[Avg]) FROM [v_a_animal_others] AS aot WHERE aot.[AID] = a.[ID])) AS 'Доля ресурсов озёр в суммарном запасе'
        ,(SELECT sum(aot.[Avg]) FROM [v_a_animal_others] AS aot WHERE aot.[AID] = a.[ID]) / (SELECT sum(asp.[Avg]) FROM [v_a_animal_orders] AS asp WHERE asp.[AID] = a.[ID]) AS 'Коэффициент соотношения ресурсов'
    FROM [t_animals] AS a
    JOIN [t_regions] AS r ON r.[ID] = a.[ID];
    
CREATE VIEW [v_resut] AS
    SELECT
         r.[ID]
        ,r.[Name] AS 'Район'
        ,m.[Запасы ПГХ] + w.[Запасы ПГХ] + t.[Запасы ПГХ] + b.[Запасы ПГХ] + a.[Запасы ПГХ] AS 'Индекс величиеы ресурсов терротории'
        ,m.[Запасы озера] + w.[Запасы озера] + t.[Запасы озера] + b.[Запасы озера] + a.[Запасы озера] AS 'Индекс величины ресурвов озер'
        ,(m.[Запасы озера] + w.[Запасы озера] + t.[Запасы озера] + b.[Запасы озера] + a.[Запасы озера]) * 100 / (m.[Запасы ПГХ] + w.[Запасы ПГХ] + t.[Запасы ПГХ] + b.[Запасы ПГХ] + a.[Запасы ПГХ]) AS 'Доля ресурсов озёр в суммарном запасе'
        ,(m.[К-т баланса] + w.[К-т баланса] + t.[К-т баланса] + b.[К-т баланса] + a.[К-т баланса]) / 5 AS 'Коэффициент соотношения ресурсов'
    FROM [t_regions] AS r
    INNER JOIN [v_result_mineral_resources]     AS m ON m.[ID] = r.[ID]
    INNER JOIN [v_result_water_resources]       AS w ON w.[ID] = r.[ID]
    INNER JOIN [v_result_territorial_resources] AS t ON t.[ID] = r.[ID]
    INNER JOIN [v_result_biological_resources]  AS b ON b.[ID] = r.[ID]
    INNER JOIN [v_result_animals_resorces]      AS a ON a.[ID] = r.[ID];

CREATE VIEW [v_t_mineral_resources] AS
    SELECT
         [rg].[Name]    AS 'Район'
        ,[mr].[DL]      AS 'Доломиты, кг'
        ,[mr].[AR]      AS 'Глинистые породы, м3'
        ,[mr].[GS]      AS 'Гравийно-песчаные породы, м3, '
        ,[mr].[SA]      AS 'Пески, м3'
        ,[mr].[PE]      AS 'Торф, кг'
        ,[mr].[SP]      AS 'Сапропель, м3'
    FROM [t_mineral_resources] AS [mr]
        JOIN [t_regions] AS [rg] ON [rg].[ID] = [mr].[ID];

CREATE VIEW [v_t_water_resources] AS
    SELECT
         [rg].[Name]    AS 'Район'
        ,[wr].[RW]      AS 'Речной сток, м3'
        ,[wr].[UW]      AS 'Подземные воды, м3/ч'
        ,[wr].[LW]      AS 'Объем воды в озерах, м3'
    FROM [t_water_resources] AS [wr]
        JOIN [t_regions] AS [rg] ON [rg].[ID] = [wr].[ID];

CREATE VIEW [v_t_biological_resources] AS
    SELECT
         [rg].[Name]    AS 'Район'
        ,[br].[WO]      AS 'Древесина, м3'
        ,[br].[MP]      AS 'Лекарственные, кг'
        ,[br].[FP]      AS 'Пищевые, кг'
        ,[br].[MU]      AS 'Грибы, кг'
        ,[br].[PH]      AS 'Фитопланктон, кг'
        ,[br].[MC]      AS 'Макрофиты, кг'
    FROM [t_biological_resources] AS [br]
        JOIN [t_regions] AS [rg] ON [rg].[ID] = [br].[ID];

CREATE VIEW [v_t_territorial_resources] AS
    SELECT
         [rg].[Name]    AS 'Район'
        ,[tr].[GA]      AS 'Общая площадь земель, м2'
        ,[tr].[AA]      AS 'Площадь с/х земель, м2'
        ,[tr].[LA]      AS 'Площадь озер, м2'
    FROM [t_territorial_resources] AS [tr]
        JOIN [t_regions] AS [rg] ON [rg].[ID] = [tr].[ID];

--------------------------------------------------------


INSERT INTO [t_unit_categories] ([ID], [CName], [UName], [SUName]) VALUES(1, 'Масса', 'грамм', 'г');
INSERT INTO [t_unit_categories] ([ID], [CName], [UName], [SUName]) VALUES(2, 'Объем', 'метр3', 'м3');
INSERT INTO [t_unit_categories] ([ID], [CName], [UName], [SUName]) VALUES(3, 'Количество', 'штука', 'шт');

INSERT INTO [t_units] ([ID], [CID]) VALUES(1, 1);
INSERT INTO [t_units] ([ID], [CID], [Factor], [Prefix], [SPrefix]) VALUES(2, 1, 1000, 'кило', 'к');
INSERT INTO [t_units] ([ID], [CID], [Factor], [Prefix], [SPrefix], [AlterName]) VALUES(3, 1, 1000000, 'Мега', 'М', 'тонна');
INSERT INTO [t_units] ([ID], [CID], [Factor], [AlterName]) VALUES(4, 1, 100000, 'центнер');
INSERT INTO [t_units] ([ID], [CID]) VALUES(5, 2);
INSERT INTO [t_units] ([ID], [CID], [Factor], [AlterName]) VALUES(6, 2, 0.001, 'литр');
INSERT INTO [t_units] ([ID], [CID], [Factor], [Prefix], [SPrefix]) VALUES(7, 2, 1000000000, 'кило', 'к');
INSERT INTO [t_units] ([ID], [CID]) VALUES(8, 3);

INSERT INTO [t_regions] ([ID], [Name]) VALUES (1, 'Бешенковичский');
INSERT INTO [t_regions] ([ID], [Name]) VALUES (2, 'Браславский');
INSERT INTO [t_regions] ([ID], [Name]) VALUES (3, 'Верхнедвинский');
INSERT INTO [t_regions] ([ID], [Name]) VALUES (4, 'Витебский');
INSERT INTO [t_regions] ([ID], [Name]) VALUES (5, 'Глубокский');
INSERT INTO [t_regions] ([ID], [Name]) VALUES (6, 'Городокский');
INSERT INTO [t_regions] ([ID], [Name]) VALUES (7, 'Лепельский');
INSERT INTO [t_regions] ([ID], [Name]) VALUES (8, 'Лиозненский');
INSERT INTO [t_regions] ([ID], [Name]) VALUES (9, 'Миорский');
INSERT INTO [t_regions] ([ID], [Name]) VALUES (10, 'Мядельский');
INSERT INTO [t_regions] ([ID], [Name]) VALUES (11, 'Полоцкий');
INSERT INTO [t_regions] ([ID], [Name]) VALUES (12, 'Поставский');
INSERT INTO [t_regions] ([ID], [Name]) VALUES (13, 'Россонский');
INSERT INTO [t_regions] ([ID], [Name]) VALUES (14, 'Сенненский');
INSERT INTO [t_regions] ([ID], [Name]) VALUES (15, 'Ушачский');
INSERT INTO [t_regions] ([ID], [Name]) VALUES (16, 'Чашникский');
INSERT INTO [t_regions] ([ID], [Name]) VALUES (17, 'Шарковщинский');
INSERT INTO [t_regions] ([ID], [Name]) VALUES (18, 'Шумилинский');

INSERT INTO [t_water_resources] ([ID], [RW], [UW], [LW]) VALUES (1, 246375000, 0.26, 123810000);
INSERT INTO [t_water_resources] ([ID], [RW], [UW], [LW]) VALUES (2, 465314000, 0.11, 973780000);
INSERT INTO [t_water_resources] ([ID], [RW], [UW], [LW]) VALUES (3, 472409000, 0.21, 190860000);
INSERT INTO [t_water_resources] ([ID], [RW], [UW], [LW]) VALUES (4, 559606000, 3.43, 72000000);
INSERT INTO [t_water_resources] ([ID], [RW], [UW], [LW]) VALUES (5, 346896000, 0.49, 163780000);
INSERT INTO [t_water_resources] ([ID], [RW], [UW], [LW]) VALUES (6, 634504000, 0.28, 384920000);
INSERT INTO [t_water_resources] ([ID], [RW], [UW], [LW]) VALUES (7, 373071000, 0.27, 249840000);
INSERT INTO [t_water_resources] ([ID], [RW], [UW], [LW]) VALUES (8, 291077000, 0.31, 8710000);
INSERT INTO [t_water_resources] ([ID], [RW], [UW], [LW]) VALUES (9, 380955000, 0.16, 149580000);
INSERT INTO [t_water_resources] ([ID], [RW], [UW], [LW]) VALUES (10, 466102000, 0.37, 987980000);
INSERT INTO [t_water_resources] ([ID], [RW], [UW], [LW]) VALUES (11, 690954000, 1.79, 305540000);
INSERT INTO [t_water_resources] ([ID], [RW], [UW], [LW]) VALUES (12, 480293000, 0.18, 103620000);
INSERT INTO [t_water_resources] ([ID], [RW], [UW], [LW]) VALUES (13, 441189000, 0.07, 303740000);
INSERT INTO [t_water_resources] ([ID], [RW], [UW], [LW]) VALUES (14, 388208000, 0.20, 84880000);
INSERT INTO [t_water_resources] ([ID], [RW], [UW], [LW]) VALUES (15, 293600000, 0.16, 326910000);
INSERT INTO [t_water_resources] ([ID], [RW], [UW], [LW]) VALUES (16, 303376000, 0.11, 300410000);
INSERT INTO [t_water_resources] ([ID], [RW], [UW], [LW]) VALUES (17, 234628000, 0.12, 8060000);
INSERT INTO [t_water_resources] ([ID], [RW], [UW], [LW]) VALUES (18, 375278000, 0.33, 49870000);

INSERT INTO [t_territorial_resources] ([ID], [GA], [AA], [LA]) VALUES (1, 1231060000, 604090000, 18590000);
INSERT INTO [t_territorial_resources] ([ID], [GA], [AA], [LA]) VALUES (2, 2087500000, 897990000, 182570000);
INSERT INTO [t_territorial_resources] ([ID], [GA], [AA], [LA]) VALUES (3, 2051280000, 795850000, 88770000);
INSERT INTO [t_territorial_resources] ([ID], [GA], [AA], [LA]) VALUES (4, 2713700000, 1139750000, 18280000);
INSERT INTO [t_territorial_resources] ([ID], [GA], [AA], [LA]) VALUES (5, 1726080000, 957700000, 33500000);
INSERT INTO [t_territorial_resources] ([ID], [GA], [AA], [LA]) VALUES (6, 2891890000, 791740000, 88580000);
INSERT INTO [t_territorial_resources] ([ID], [GA], [AA], [LA]) VALUES (7, 1769370000, 526400000, 52850000);
INSERT INTO [t_territorial_resources] ([ID], [GA], [AA], [LA]) VALUES (8, 1414260000, 595190000, 3710000);
INSERT INTO [t_territorial_resources] ([ID], [GA], [AA], [LA]) VALUES (9, 1740260000, 809310000, 46380000);
INSERT INTO [t_territorial_resources] ([ID], [GA], [AA], [LA]) VALUES (10, 1820920000, 741300000, 147530000);
INSERT INTO [t_territorial_resources] ([ID], [GA], [AA], [LA]) VALUES (11, 3038700000, 740240000, 93620000);
INSERT INTO [t_territorial_resources] ([ID], [GA], [AA], [LA]) VALUES (12, 2060990000, 964690000, 35450000);
INSERT INTO [t_territorial_resources] ([ID], [GA], [AA], [LA]) VALUES (13, 1848830000, 261730000, 78750000);
INSERT INTO [t_territorial_resources] ([ID], [GA], [AA], [LA]) VALUES (14, 1947860000, 845960000, 18190000);
INSERT INTO [t_territorial_resources] ([ID], [GA], [AA], [LA]) VALUES (15, 1413270000, 503340000, 76110000);
INSERT INTO [t_territorial_resources] ([ID], [GA], [AA], [LA]) VALUES (16, 1421850000, 709790000, 59310000);
INSERT INTO [t_territorial_resources] ([ID], [GA], [AA], [LA]) VALUES (17, 1186100000, 696370000, 3080000);
INSERT INTO [t_territorial_resources] ([ID], [GA], [AA], [LA]) VALUES (18, 1680280000, 586200000, 15120000);

INSERT INTO [t_biological_resources] ([ID], [WO], [MP], [FP], [MU], [PH], [MC]) VALUES (1, 5785300, 1387000000, 168200000, 133000000, 46350000, 1396790000);
INSERT INTO [t_biological_resources] ([ID], [WO], [MP], [FP], [MU], [PH], [MC]) VALUES (2, 14001200, 2538500000, 350600000, 368000000, 646400000, 17767900000);
INSERT INTO [t_biological_resources] ([ID], [WO], [MP], [FP], [MU], [PH], [MC]) VALUES (3, 16162700, 3267600000, 422400000, 212000000, 72160000, 11685700000);
INSERT INTO [t_biological_resources] ([ID], [WO], [MP], [FP], [MU], [PH], [MC]) VALUES (4, 19879500, 4258000000, 558200000, 440000000, 140170000, 751260000);
INSERT INTO [t_biological_resources] ([ID], [WO], [MP], [FP], [MU], [PH], [MC]) VALUES (5, 9418400, 2401800000, 312800000, 342000000, 195780000, 3693510000);
INSERT INTO [t_biological_resources] ([ID], [WO], [MP], [FP], [MU], [PH], [MC]) VALUES (6, 27273100, 5846700000, 718900000, 483000000, 311950000, 7944240000);
INSERT INTO [t_biological_resources] ([ID], [WO], [MP], [FP], [MU], [PH], [MC]) VALUES (7, 18554200, 4749100000, 541700000, 526000000, 262570000, 4013970000);
INSERT INTO [t_biological_resources] ([ID], [WO], [MP], [FP], [MU], [PH], [MC]) VALUES (8, 11178200, 2125800000, 272300000, 212000000, 4510000, 18400000);
INSERT INTO [t_biological_resources] ([ID], [WO], [MP], [FP], [MU], [PH], [MC]) VALUES (9, 7751200, 2245500000, 457100000, 211000000, 283430000, 3859070000);
INSERT INTO [t_biological_resources] ([ID], [WO], [MP], [FP], [MU], [PH], [MC]) VALUES (10, 16872300, 4463300000, 640000000, 11000000, 78140000, 10970150000);
INSERT INTO [t_biological_resources] ([ID], [WO], [MP], [FP], [MU], [PH], [MC]) VALUES (11, 32663000, 7593300000, 1029800000, 696000000, 430460000, 5044270000);
INSERT INTO [t_biological_resources] ([ID], [WO], [MP], [FP], [MU], [PH], [MC]) VALUES (12, 14173900, 3229000000, 522100000, 267000000, 271220000, 4554200000);
INSERT INTO [t_biological_resources] ([ID], [WO], [MP], [FP], [MU], [PH], [MC]) VALUES (13, 22425300, 6080300000, 814800000, 509000000, 95500000, 474110000);
INSERT INTO [t_biological_resources] ([ID], [WO], [MP], [FP], [MU], [PH], [MC]) VALUES (14, 14015800, 2674000000, 300400000, 166000000, 79850000, 842000000);
INSERT INTO [t_biological_resources] ([ID], [WO], [MP], [FP], [MU], [PH], [MC]) VALUES (15, 11186600, 2813500000, 392900000, 231000000, 469730000, 5591270000);
INSERT INTO [t_biological_resources] ([ID], [WO], [MP], [FP], [MU], [PH], [MC]) VALUES (16, 8315200, 1509200000, 180500000, 214000000, 173680000, 6330900000);
INSERT INTO [t_biological_resources] ([ID], [WO], [MP], [FP], [MU], [PH], [MC]) VALUES (17, 4557800, 1351500000, 184100000, 91000000, 36000000, 1440000);
INSERT INTO [t_biological_resources] ([ID], [WO], [MP], [FP], [MU], [PH], [MC]) VALUES (18, 11012000, 2869300000, 406200000, 270000000, 110020000, 712850000);

INSERT INTO [t_animals] ([ID]) VALUES (1);
INSERT INTO [t_animals] ([ID]) VALUES (2);
INSERT INTO [t_animals] ([ID]) VALUES (3);
INSERT INTO [t_animals] ([ID]) VALUES (4);
INSERT INTO [t_animals] ([ID]) VALUES (5);
INSERT INTO [t_animals] ([ID]) VALUES (6);
INSERT INTO [t_animals] ([ID]) VALUES (8);
INSERT INTO [t_animals] ([ID]) VALUES (9);
INSERT INTO [t_animals] ([ID]) VALUES (10);
INSERT INTO [t_animals] ([ID]) VALUES (11);
INSERT INTO [t_animals] ([ID]) VALUES (12);
INSERT INTO [t_animals] ([ID]) VALUES (13);
INSERT INTO [t_animals] ([ID]) VALUES (14);
INSERT INTO [t_animals] ([ID]) VALUES (15);
INSERT INTO [t_animals] ([ID]) VALUES (16);
INSERT INTO [t_animals] ([ID]) VALUES (17);
INSERT INTO [t_animals] ([ID]) VALUES (18);

INSERT INTO [t_animal_orders] ([ID], [Name]) VALUES(1, 'Копытные');
INSERT INTO [t_animal_orders] ([ID], [Name]) VALUES(2, 'Пушные');
INSERT INTO [t_animal_orders] ([ID], [Name]) VALUES(3, 'Дикие птицы');

INSERT INTO [t_animal_species] ([ID], [OID], [Name]) VALUES (1, 1, 'Лось');
INSERT INTO [t_animal_species] ([ID], [OID], [Name]) VALUES (2, 1, 'Олень');
INSERT INTO [t_animal_species] ([ID], [OID], [Name]) VALUES (3, 1, 'Косуля');
INSERT INTO [t_animal_species] ([ID], [OID], [Name]) VALUES (4, 1, 'Кабан');
INSERT INTO [t_animal_species] ([ID], [OID], [Name]) VALUES (5, 2, 'Заяц-беляк');
INSERT INTO [t_animal_species] ([ID], [OID], [Name]) VALUES (6, 2, 'Заяц-русак');
INSERT INTO [t_animal_species] ([ID], [OID], [Name]) VALUES (7, 2, 'Куница');
INSERT INTO [t_animal_species] ([ID], [OID], [Name]) VALUES (8, 2, 'Лисица');
INSERT INTO [t_animal_species] ([ID], [OID], [Name]) VALUES (9, 2, 'Ондатра');
INSERT INTO [t_animal_species] ([ID], [OID], [Name]) VALUES (10, 2, 'Норка');
INSERT INTO [t_animal_species] ([ID], [OID], [Name]) VALUES (11, 2, 'Бобр');
INSERT INTO [t_animal_species] ([ID], [OID], [Name]) VALUES (12, 2, 'Волк');
INSERT INTO [t_animal_species] ([ID], [OID], [Name]) VALUES (13, 2, 'Барсук');
INSERT INTO [t_animal_species] ([ID], [OID], [Name]) VALUES (14, 2, 'Выдра');
INSERT INTO [t_animal_species] ([ID], [OID], [Name]) VALUES (15, 2, 'Енотовидная собака');
INSERT INTO [t_animal_species] ([ID], [OID], [Name]) VALUES (16, 2, 'Рысь');
INSERT INTO [t_animal_species] ([ID], [OID], [Name]) VALUES (17, 2, 'Белка');
INSERT INTO [t_animal_species] ([ID], [OID], [Name]) VALUES (18, 3, 'Глухарь');
INSERT INTO [t_animal_species] ([ID], [OID], [Name]) VALUES (19, 3, 'Тетерев');
INSERT INTO [t_animal_species] ([ID], [OID], [Name]) VALUES (20, 3, 'Рябчик');
INSERT INTO [t_animal_species] ([ID], [OID], [Name]) VALUES (21, 3, 'Серая куропатка');
INSERT INTO [t_animal_species] ([ID], [OID], [Name]) VALUES (22, 3, 'Утки');

INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (1, 1, 149);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (2, 1, 107);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (3, 1, 437);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (4, 1, 406);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (5, 1, 378);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (6, 1, 479);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (7, 1, 103);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (8, 1, 169);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (9, 1, 100);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (10, 1, 99);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (11, 1, 592);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (12, 1, 4);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (13, 1, 17);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (14, 1, 26);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (15, 1, 126);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (16, 1, 0);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (17, 1, 443);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (18, 1, 19);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (19, 1, 292);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (20, 1, 215);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (21, 1, 297);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (22, 1, 0);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (1, 2, 430);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (2, 2, 0);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (3, 2, 1245);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (4, 2, 880);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (5, 2, 305);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (6, 2, 370);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (7, 2, 240);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (8, 2, 355);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (9, 2, 305);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (10, 2, 340);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (11, 2, 2260);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (12, 2, 11);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (13, 2, 79);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (14, 2, 55);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (15, 2, 440);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (16, 2, 16);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (17, 2, 380);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (18, 2, 13);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (19, 2, 136);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (20, 2, 565);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (21, 2, 210);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (22, 2, 0);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (1, 3, 353);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (2, 3, 0);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (3, 3, 616);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (4, 3, 712);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (5, 3, 700);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (6, 3, 450);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (7, 3, 430);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (8, 3, 420);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (9, 3, 450);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (10, 3, 585);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (11, 3, 710);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (12, 3, 19);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (13, 3, 65);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (14, 3, 100);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (15, 3, 550);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (16, 3, 28);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (17, 3, 690);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (18, 3, 142);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (19, 3, 470);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (20, 3, 530);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (21, 3, 340);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (22, 3, 0);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (1, 4, 257);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (2, 4, 0);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (3, 4, 321);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (4, 4, 467);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (5, 4, 729);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (6, 4, 636);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (7, 4, 109);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (8, 4, 368);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (9, 4, 758);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (10, 4, 120);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (11, 4, 501);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (12, 4, 11);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (13, 4, 25);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (14, 4, 45);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (15, 4, 79);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (16, 4, 7);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (17, 4, 1099);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (18, 4, 260);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (19, 4, 340);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (20, 4, 1400);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (21, 4, 1310);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (22, 4, 0);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (1, 5, 149);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (2, 5, 0);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (3, 5, 472);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (4, 5, 232);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (5, 5, 970);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (6, 5, 1295);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (7, 5, 150);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (8, 5, 555);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (9, 5, 373);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (10, 5, 280);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (11, 5, 715);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (12, 5, 1);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (13, 5, 72);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (14, 5, 89);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (15, 5, 190);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (16, 5, 3);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (17, 5, 1500);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (18, 5, 170);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (19, 5, 750);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (20, 5, 1980);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (21, 5, 420);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (22, 5, 7020);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (1, 6, 663);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (2, 6, 0);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (3, 6, 505);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (4, 6, 1180);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (5, 6, 1334);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (6, 6, 386);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (7, 6, 125);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (8, 6, 209);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (9, 6, 616);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (10, 6, 707);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (11, 6, 904);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (12, 6, 32);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (13, 6, 93);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (14, 6, 128);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (15, 6, 598);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (16, 6, 28);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (17, 6, 1327);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (18, 6, 708);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (19, 6, 954);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (20, 6, 2881);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (21, 6, 1210);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (22, 6, 12590);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (1, 7, 349);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (2, 7, 47);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (3, 7, 806);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (4, 7, 847);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (5, 7, 1815);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (6, 7, 744);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (7, 7, 224);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (8, 7, 230);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (9, 7, 430);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (10, 7, 335);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (11, 7, 1010);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (12, 7, 12);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (13, 7, 26);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (14, 7, 60);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (15, 7, 216);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (16, 7, 9);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (17, 7, 1445);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (18, 7, 155);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (19, 7, 678);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (20, 7, 1640);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (21, 7, 280);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (22, 7, 1250);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (1, 8, 138);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (2, 8, 50);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (3, 8, 282);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (4, 8, 353);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (5, 8, 464);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (6, 8, 309);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (7, 8, 57);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (8, 8, 267);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (9, 8, 70);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (10, 8, 150);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (11, 8, 140);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (12, 8, 8);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (13, 8, 7);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (14, 8, 6);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (15, 8, 170);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (16, 8, 8);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (17, 8, 370);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (18, 8, 170);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (19, 8, 170);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (20, 8, 800);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (21, 8, 925);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (22, 8, 0);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (1, 9, 234);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (2, 9, 0);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (3, 9, 565);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (4, 9, 744);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (5, 9, 561);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (6, 9, 555);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (7, 9, 110);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (8, 9, 347);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (9, 9, 89);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (10, 9, 265);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (11, 9, 1400);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (12, 9, 7);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (13, 9, 41);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (14, 9, 73);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (15, 9, 278);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (16, 9, 0);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (17, 9, 789);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (18, 9, 0);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (19, 9, 333);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (20, 9, 147);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (21, 9, 241);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (22, 9, 396);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (1, 10, 255);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (2, 10, 83);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (3, 10, 688);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (4, 10, 673);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (5, 10, 1613);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (6, 10, 1680);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (7, 10, 177);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (8, 10, 956);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (9, 10, 116);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (10, 10, 157);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (11, 10, 1035);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (12, 10, 7);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (13, 10, 115);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (14, 10, 134);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (15, 10, 240);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (16, 10, 0);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (17, 10, 5385);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (18, 10, 20);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (19, 10, 536);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (20, 10, 544);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (21, 10, 294);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (22, 10, 5983);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (1, 11, 748);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (2, 11, 0);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (3, 11, 742);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (4, 11, 1506);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (5, 11, 1131);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (6, 11, 203);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (7, 11, 245);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (8, 11, 342);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (9, 11, 576);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (10, 11, 552);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (11, 11, 2519);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (12, 11, 18);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (13, 11, 74);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (14, 11, 162);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (15, 11, 328);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (16, 11, 44);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (17, 11, 2013);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (18, 11, 635);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (19, 11, 1017);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (20, 11, 1958);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (21, 11, 1332);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (22, 11, 4510);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (1, 12, 222);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (2, 12, 0);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (3, 12, 762);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (4, 12, 630);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (5, 12, 990);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (6, 12, 1150);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (7, 12, 625);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (8, 12, 355);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (9, 12, 263);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (10, 12, 380);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (11, 12, 1150);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (12, 12, 6);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (13, 12, 104);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (14, 12, 143);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (15, 12, 270);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (16, 12, 1);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (17, 12, 1640);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (18, 12, 0);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (19, 12, 455);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (20, 12, 1675);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (21, 12, 465);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (22, 12, 436);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (1, 13, 879);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (2, 13, 220);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (3, 13, 774);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (4, 13, 1491);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (5, 13, 1667);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (6, 13, 590);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (7, 13, 483);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (8, 13, 242);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (9, 13, 60);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (10, 13, 645);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (11, 13, 880);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (12, 13, 27);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (13, 13, 95);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (14, 13, 125);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (15, 13, 322);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (16, 13, 64);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (17, 13, 2036);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (18, 13, 1108);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (19, 13, 810);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (20, 13, 2926);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (21, 13, 243);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (22, 13, 3210);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (1, 14, 655);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (2, 14, 241);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (3, 14, 1025);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (4, 14, 1581);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (5, 14, 2487);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (6, 14, 1371);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (7, 14, 355);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (8, 14, 626);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (9, 14, 90);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (10, 14, 513);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (11, 14, 320);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (12, 14, 14);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (13, 14, 32);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (14, 14, 39);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (15, 14, 81);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (16, 14, 15);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (17, 14, 1675);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (18, 14, 150);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (19, 14, 570);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (20, 14, 1500);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (21, 14, 450);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (22, 14, 0);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (1, 15, 252);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (2, 15, 80);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (3, 15, 411);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (4, 15, 330);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (5, 15, 1090);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (6, 15, 985);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (7, 15, 130);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (8, 15, 225);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (9, 15, 350);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (10, 15, 470);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (11, 15, 850);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (12, 15, 3);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (13, 15, 0);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (14, 15, 73);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (15, 15, 100);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (16, 15, 1);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (17, 15, 2070);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (18, 15, 216);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (19, 15, 397);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (20, 15, 650);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (21, 15, 330);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (22, 15, 790);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (1, 16, 105);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (2, 16, 0);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (3, 16, 260);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (4, 16, 220);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (5, 16, 1570);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (6, 16, 1460);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (7, 16, 280);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (8, 16, 210);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (9, 16, 600);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (10, 16, 530);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (11, 16, 180);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (12, 16, 6);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (13, 16, 20);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (14, 16, 70);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (15, 16, 100);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (16, 16, 2);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (17, 16, 1340);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (18, 16, 0);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (19, 16, 680);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (20, 16, 800);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (21, 16, 610);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (22, 16, 3690);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (1, 17, 67);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (2, 17, 0);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (3, 17, 198);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (4, 17, 226);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (5, 17, 159);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (6, 17, 316);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (7, 17, 125);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (8, 17, 80);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (9, 17, 50);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (10, 17, 54);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (11, 17, 360);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (12, 17, 4);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (13, 17, 10);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (14, 17, 40);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (15, 17, 60);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (16, 17, 1);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (17, 17, 405);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (18, 17, 0);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (19, 17, 60);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (20, 17, 200);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (21, 17, 150);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (22, 17, 0);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (1, 18, 435);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (2, 18, 29);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (3, 18, 461);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (4, 18, 820);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (5, 18, 936);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (6, 18, 1099);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (7, 18, 148);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (8, 18, 153);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (9, 18, 1070);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (10, 18, 1054);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (11, 18, 604);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (12, 18, 9);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (13, 18, 55);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (14, 18, 105);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (15, 18, 169);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (16, 18, 17);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (17, 18, 1772);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (18, 18, 776);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (19, 18, 1570);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (20, 18, 1950);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (21, 18, 3710);
INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES (22, 18, 0);

INSERT INTO [t_animal_others] ([ID], [Name]) VALUES(1, 'Зоопланктон');
INSERT INTO [t_animal_others] ([ID], [Name]) VALUES(2, 'Бентос');
INSERT INTO [t_animal_others] ([ID], [Name]) VALUES(3, 'Рыба');

INSERT INTO [t_animal_others_to_animals] ([OID], [AID], [Quantity]) VALUES (1, 1, 80.4);
INSERT INTO [t_animal_others_to_animals] ([OID], [AID], [Quantity]) VALUES (2, 1, 110.81);
INSERT INTO [t_animal_others_to_animals] ([OID], [AID], [Quantity]) VALUES (3, 1, 1630.55);
INSERT INTO [t_animal_others_to_animals] ([OID], [AID], [Quantity]) VALUES (1, 2, 170.92);
INSERT INTO [t_animal_others_to_animals] ([OID], [AID], [Quantity]) VALUES (2, 2, 573.08);
INSERT INTO [t_animal_others_to_animals] ([OID], [AID], [Quantity]) VALUES (3, 2, 18338.11);
INSERT INTO [t_animal_others_to_animals] ([OID], [AID], [Quantity]) VALUES (1, 3, 6.33);
INSERT INTO [t_animal_others_to_animals] ([OID], [AID], [Quantity]) VALUES (2, 3, 85.86);
INSERT INTO [t_animal_others_to_animals] ([OID], [AID], [Quantity]) VALUES (3, 3, 7698.51);
INSERT INTO [t_animal_others_to_animals] ([OID], [AID], [Quantity]) VALUES (1, 4, 52.22);
INSERT INTO [t_animal_others_to_animals] ([OID], [AID], [Quantity]) VALUES (2, 4, 102.02);
INSERT INTO [t_animal_others_to_animals] ([OID], [AID], [Quantity]) VALUES (3, 4, 1590.57);
INSERT INTO [t_animal_others_to_animals] ([OID], [AID], [Quantity]) VALUES (1, 5, 40.53);
INSERT INTO [t_animal_others_to_animals] ([OID], [AID], [Quantity]) VALUES (2, 5, 163.34);
INSERT INTO [t_animal_others_to_animals] ([OID], [AID], [Quantity]) VALUES (3, 5, 2708.93);
INSERT INTO [t_animal_others_to_animals] ([OID], [AID], [Quantity]) VALUES (1, 6, 98.3);
INSERT INTO [t_animal_others_to_animals] ([OID], [AID], [Quantity]) VALUES (2, 6, 267.41);
INSERT INTO [t_animal_others_to_animals] ([OID], [AID], [Quantity]) VALUES (3, 6, 9368.98);
INSERT INTO [t_animal_others_to_animals] ([OID], [AID], [Quantity]) VALUES (1, 7, 107.03);
INSERT INTO [t_animal_others_to_animals] ([OID], [AID], [Quantity]) VALUES (2, 7, 167.95);
INSERT INTO [t_animal_others_to_animals] ([OID], [AID], [Quantity]) VALUES (3, 7, 4360.16);
INSERT INTO [t_animal_others_to_animals] ([OID], [AID], [Quantity]) VALUES (1, 8, 1.12);
INSERT INTO [t_animal_others_to_animals] ([OID], [AID], [Quantity]) VALUES (2, 8, 3.14);
INSERT INTO [t_animal_others_to_animals] ([OID], [AID], [Quantity]) VALUES (3, 8, 520.78);
INSERT INTO [t_animal_others_to_animals] ([OID], [AID], [Quantity]) VALUES (1, 9, 95.49);
INSERT INTO [t_animal_others_to_animals] ([OID], [AID], [Quantity]) VALUES (2, 9, 144.92);
INSERT INTO [t_animal_others_to_animals] ([OID], [AID], [Quantity]) VALUES (3, 9, 4015.41);
INSERT INTO [t_animal_others_to_animals] ([OID], [AID], [Quantity]) VALUES (1, 10, 16.61);
INSERT INTO [t_animal_others_to_animals] ([OID], [AID], [Quantity]) VALUES (2, 10, 67.5);
INSERT INTO [t_animal_others_to_animals] ([OID], [AID], [Quantity]) VALUES (3, 10, 12833.13);
INSERT INTO [t_animal_others_to_animals] ([OID], [AID], [Quantity]) VALUES (1, 11, 170.42);
INSERT INTO [t_animal_others_to_animals] ([OID], [AID], [Quantity]) VALUES (2, 11, 369.72);
INSERT INTO [t_animal_others_to_animals] ([OID], [AID], [Quantity]) VALUES (3, 11, 8767.01);
INSERT INTO [t_animal_others_to_animals] ([OID], [AID], [Quantity]) VALUES (1, 12, 45.36);
INSERT INTO [t_animal_others_to_animals] ([OID], [AID], [Quantity]) VALUES (2, 12, 206.46);
INSERT INTO [t_animal_others_to_animals] ([OID], [AID], [Quantity]) VALUES (3, 12, 2998.48);
INSERT INTO [t_animal_others_to_animals] ([OID], [AID], [Quantity]) VALUES (1, 13, 90.21);
INSERT INTO [t_animal_others_to_animals] ([OID], [AID], [Quantity]) VALUES (2, 13, 159.39);
INSERT INTO [t_animal_others_to_animals] ([OID], [AID], [Quantity]) VALUES (3, 13, 7965.76);
INSERT INTO [t_animal_others_to_animals] ([OID], [AID], [Quantity]) VALUES (1, 14, 25.42);
INSERT INTO [t_animal_others_to_animals] ([OID], [AID], [Quantity]) VALUES (2, 14, 44.76);
INSERT INTO [t_animal_others_to_animals] ([OID], [AID], [Quantity]) VALUES (3, 14, 1773.27);
INSERT INTO [t_animal_others_to_animals] ([OID], [AID], [Quantity]) VALUES (1, 15, 138.4);
INSERT INTO [t_animal_others_to_animals] ([OID], [AID], [Quantity]) VALUES (2, 15, 358.63);
INSERT INTO [t_animal_others_to_animals] ([OID], [AID], [Quantity]) VALUES (3, 15, 7430.11);
INSERT INTO [t_animal_others_to_animals] ([OID], [AID], [Quantity]) VALUES (1, 16, 69.07);
INSERT INTO [t_animal_others_to_animals] ([OID], [AID], [Quantity]) VALUES (2, 16, 41.88);
INSERT INTO [t_animal_others_to_animals] ([OID], [AID], [Quantity]) VALUES (3, 16, 6119.64);
INSERT INTO [t_animal_others_to_animals] ([OID], [AID], [Quantity]) VALUES (1, 17, 1.2);
INSERT INTO [t_animal_others_to_animals] ([OID], [AID], [Quantity]) VALUES (2, 17, 6.6);
INSERT INTO [t_animal_others_to_animals] ([OID], [AID], [Quantity]) VALUES (3, 17, 266.7);
INSERT INTO [t_animal_others_to_animals] ([OID], [AID], [Quantity]) VALUES (1, 18, 18.25);
INSERT INTO [t_animal_others_to_animals] ([OID], [AID], [Quantity]) VALUES (2, 18, 45.96);
INSERT INTO [t_animal_others_to_animals] ([OID], [AID], [Quantity]) VALUES (3, 18, 1421.52);

INSERT INTO [t_mineral_resources] ([ID], [DL], [AR], [GS], [SA], [PE], [SP]) VALUES (1, 0, 336000, 3085000, 26557000, 27883000000000, 24393000);
INSERT INTO [t_mineral_resources] ([ID], [DL], [AR], [GS], [SA], [PE], [SP]) VALUES (2, 0, 1023000, 12886000, 2623000, 53697000000000, 261697000);
INSERT INTO [t_mineral_resources] ([ID], [DL], [AR], [GS], [SA], [PE], [SP]) VALUES (3, 0, 6687000, 0, 12057000, 38345000000000, 190635000);
INSERT INTO [t_mineral_resources] ([ID], [DL], [AR], [GS], [SA], [PE], [SP]) VALUES (4, 1744223000000000, 11390000, 4120000, 12479000, 37890000000000, 16226000);
INSERT INTO [t_mineral_resources] ([ID], [DL], [AR], [GS], [SA], [PE], [SP]) VALUES (5, 0, 909000, 147430000, 0, 32093000000000, 51307000);
INSERT INTO [t_mineral_resources] ([ID], [DL], [AR], [GS], [SA], [PE], [SP]) VALUES (6, 0, 2014000, 82440000, 0, 83071000000000, 169568000);
INSERT INTO [t_mineral_resources] ([ID], [DL], [AR], [GS], [SA], [PE], [SP]) VALUES (7, 0, 901000, 31050000, 0, 92091000000000, 63534000);
INSERT INTO [t_mineral_resources] ([ID], [DL], [AR], [GS], [SA], [PE], [SP]) VALUES (8, 0, 433000, 46516000, 351000, 8687000000000, 9650000);
INSERT INTO [t_mineral_resources] ([ID], [DL], [AR], [GS], [SA], [PE], [SP]) VALUES (9, 0, 1389000, 430000, 118000, 148366000000000, 66754000);
INSERT INTO [t_mineral_resources] ([ID], [DL], [AR], [GS], [SA], [PE], [SP]) VALUES (10, 0, 0, 1739000, 540000, 99874000000000, 118493000);
INSERT INTO [t_mineral_resources] ([ID], [DL], [AR], [GS], [SA], [PE], [SP]) VALUES (11, 0, 6614000, 58000, 10397000, 89975000000000, 150253000);
INSERT INTO [t_mineral_resources] ([ID], [DL], [AR], [GS], [SA], [PE], [SP]) VALUES (12, 0, 48858000, 13480000, 3500000, 16948000000000, 54697000);
INSERT INTO [t_mineral_resources] ([ID], [DL], [AR], [GS], [SA], [PE], [SP]) VALUES (13, 0, 617000, 7292000, 0, 47546000000000, 135172000);
INSERT INTO [t_mineral_resources] ([ID], [DL], [AR], [GS], [SA], [PE], [SP]) VALUES (14, 0, 3895000, 8058000, 5148000, 36727000000000, 31203000);
INSERT INTO [t_mineral_resources] ([ID], [DL], [AR], [GS], [SA], [PE], [SP]) VALUES (15, 0, 545000, 64664000, 0, 32254000000000, 143034000);
INSERT INTO [t_mineral_resources] ([ID], [DL], [AR], [GS], [SA], [PE], [SP]) VALUES (16, 0, 158395000, 41569000, 0, 30138000000000, 126228000);
INSERT INTO [t_mineral_resources] ([ID], [DL], [AR], [GS], [SA], [PE], [SP]) VALUES (17, 0, 449000, 0, 0, 34677000000000, 5006000);
INSERT INTO [t_mineral_resources] ([ID], [DL], [AR], [GS], [SA], [PE], [SP]) VALUES (18, 0, 51151000, 618000, 9050000, 59604000000000, 31437000);


