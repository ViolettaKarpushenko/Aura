
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
         a.[ID]
        ,(SELECT sum(asp.[Avg]) FROM [v_a_animal_orders] AS asp WHERE asp.[AID] = a.[ID]) + (SELECT sum(aot.[Avg]) FROM [v_a_animal_others] AS aot WHERE aot.[AID] = a.[ID]) AS 'Q1'
        ,(SELECT sum(aot.[Avg]) FROM [v_a_animal_others] AS aot WHERE aot.[AID] = a.[ID]) AS 'Q2'
        ,(SELECT sum(aot.[Avg]) FROM [v_a_animal_others] AS aot WHERE aot.[AID] = a.[ID]) * 100 / ((SELECT sum(asp.[Avg]) FROM [v_a_animal_orders] AS asp WHERE asp.[AID] = a.[ID]) + (SELECT sum(aot.[Avg]) FROM [v_a_animal_others] AS aot WHERE aot.[AID] = a.[ID])) AS 'Q3'
        ,(SELECT sum(aot.[Avg]) FROM [v_a_animal_others] AS aot WHERE aot.[AID] = a.[ID]) / (SELECT sum(asp.[Avg]) FROM [v_a_animal_orders] AS asp WHERE asp.[AID] = a.[ID]) AS 'Q4'
    FROM [t_animals] AS a;