/*
Modèle de script de post-déploiement							
--------------------------------------------------------------------------------------
 Ce fichier contient des instructions SQL qui seront ajoutées au script de compilation.		
 Utilisez la syntaxe SQLCMD pour inclure un fichier dans le script de post-déploiement.			
 Exemple :      :r .\monfichier.sql								
 Utilisez la syntaxe SQLCMD pour référencer une variable dans le script de post-déploiement.		
 Exemple :      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

EXEC SP_Product_Create N'Ecran 27 pouces', N'Ecran incurvé de haute résolution'
EXEC SP_Product_Create N'Souris Gaming', N'Souris filaire à 8000 dpi, RGB'
EXEC SP_Product_Create N'Clavier Gaming', N'Clavier mécanique, filaire, RGB'

INSERT INTO [PriceHistory] ([ProductId],[Price],[StartDate],[EndDate])
    VALUES (1, 499.99, '2025-03-01','2025-06-30'),
           (1, 399.99, '2025-07-01','2025-07-31'),
           (1, 449.99, '2025-08-01',NULL),
           (2, 49.99, '2025-03-01','2025-06-30'),
           (2, 39.99, '2025-07-01','2025-07-31'),
           (2, 44.99, '2025-08-01',NULL),
           (3, 69.99, '2025-03-01','2025-06-30'),
           (3, 49.99, '2025-07-01','2025-07-31'),
           (3, 59.99, '2025-08-01',NULL)