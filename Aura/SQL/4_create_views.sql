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
        ,r.[Name] AS 'Region'
        ,a.[B] + a.[C] + a.[D] + a.[E] + a.[F] + a.[G] AS 'AreaIndex'
        ,a.[G] AS 'ResourcesIndex'
        ,a.[G] * 100 / (a.[B] + a.[C] + a.[D] + a.[E] + a.[F] + a.[G]) AS 'ResourcesShare'
        ,a.[G] / (a.[B] + a.[C] + a.[D] + a.[E] + a.[F]) AS 'ResourcesRatio'
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
        ,r.[Name] AS 'Region'
        ,a.[B] + a.[C] + a.[D] AS 'AreaIndex'
        ,a.[D] AS 'ResourcesIndex'
        ,a.[D] * 100 / (a.[B] + a.[C] + a.[D]) AS 'ResourcesShare'
        ,a.[D] / (a.[B] + a.[C]) AS 'ResourcesRatio'
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
        ,r.[Name] AS 'Region'
        ,a.[C] + a.[E] AS 'AreaIndex'
        ,a.[E] AS 'ResourcesIndex'
        ,a.[E] / (a.[C] + a.[E]) AS 'ResourcesShare'
        ,a.[E] / a.[C] AS 'ResourcesRatio'
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
        ,r.[Name] AS 'Region'
        ,a.[B] + a.[C] + a.[D] + a.[E] + a.[F] + a.[G] AS 'AreaIndex'
        ,a.[F] + a.[G] AS 'ResourcesIndex'
        ,(a.[F] + a.[G]) * 100 / (a.[B] + a.[C] + a.[D] + a.[E] + a.[F] + a.[G]) AS 'ResourcesShare'
        ,(a.[F] + a.[G]) / (a.[B] + a.[C] + a.[D] + a.[E]) AS 'ResourcesRatio'
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
        ,r.[Name] AS 'Region'
        ,(SELECT sum(asp.[Avg]) FROM [v_a_animal_orders] AS asp WHERE asp.[AID] = a.[ID]) + (SELECT sum(aot.[Avg]) FROM [v_a_animal_others] AS aot WHERE aot.[AID] = a.[ID]) AS 'AreaIndex'
        ,(SELECT sum(aot.[Avg]) FROM [v_a_animal_others] AS aot WHERE aot.[AID] = a.[ID]) AS 'ResourcesIndex'
        ,(SELECT sum(aot.[Avg]) FROM [v_a_animal_others] AS aot WHERE aot.[AID] = a.[ID]) * 100 / ((SELECT sum(asp.[Avg]) FROM [v_a_animal_orders] AS asp WHERE asp.[AID] = a.[ID]) + (SELECT sum(aot.[Avg]) FROM [v_a_animal_others] AS aot WHERE aot.[AID] = a.[ID])) AS 'ResourcesShare'
        ,(SELECT sum(aot.[Avg]) FROM [v_a_animal_others] AS aot WHERE aot.[AID] = a.[ID]) / (SELECT sum(asp.[Avg]) FROM [v_a_animal_orders] AS asp WHERE asp.[AID] = a.[ID]) AS 'ResourcesRatio'
    FROM [t_animals] AS a
    JOIN [t_regions] AS r ON r.[ID] = a.[ID];
    
CREATE VIEW [v_resut] AS
    SELECT
         r.[ID]
        ,r.[Name] AS 'Region'
        ,m.[AreaIndex] + w.[AreaIndex] + t.[AreaIndex] + b.[AreaIndex] + a.[AreaIndex] AS 'AreaIndex'
        ,m.[ResourcesIndex] + w.[ResourcesIndex] + t.[ResourcesIndex] + b.[ResourcesIndex] + a.[ResourcesIndex] AS 'ResourcesIndex'
        ,(m.[ResourcesIndex] + w.[ResourcesIndex] + t.[ResourcesIndex] + b.[ResourcesIndex] + a.[ResourcesIndex]) * 100 / (m.[AreaIndex] + w.[AreaIndex] + t.[AreaIndex] + b.[AreaIndex] + a.[AreaIndex]) AS 'ResourcesShare'
        ,(m.[ResourcesRatio] + w.[ResourcesRatio] + t.[ResourcesRatio] + b.[ResourcesRatio] + a.[ResourcesRatio]) / 5 AS 'ResourcesRatio'
    FROM [t_regions] AS r
    INNER JOIN [v_result_mineral_resources]     AS m ON m.[ID] = r.[ID]
    INNER JOIN [v_result_water_resources]       AS w ON w.[ID] = r.[ID]
    INNER JOIN [v_result_territorial_resources] AS t ON t.[ID] = r.[ID]
    INNER JOIN [v_result_biological_resources]  AS b ON b.[ID] = r.[ID]
    INNER JOIN [v_result_animals_resorces]      AS a ON a.[ID] = r.[ID];

CREATE VIEW [v_t_mineral_resources] AS
    SELECT
         [rg].[Name]    AS 'Region'
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
         [rg].[Name]    AS 'Region'
        ,[wr].[RW]      AS 'Речной сток, м3'
        ,[wr].[UW]      AS 'Подземные воды, м3/ч'
        ,[wr].[LW]      AS 'Объем воды в озерах, м3'
    FROM [t_water_resources] AS [wr]
        JOIN [t_regions] AS [rg] ON [rg].[ID] = [wr].[ID];

CREATE VIEW [v_t_biological_resources] AS
    SELECT
         [rg].[Name]    AS 'Region'
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
         [rg].[Name]    AS 'Region'
        ,[tr].[GA]      AS 'Общая площадь земель, м2'
        ,[tr].[AA]      AS 'Площадь с/х земель, м2'
        ,[tr].[LA]      AS 'Площадь озер, м2'
    FROM [t_territorial_resources] AS [tr]
        JOIN [t_regions] AS [rg] ON [rg].[ID] = [tr].[ID];

--------------------------------------------------------