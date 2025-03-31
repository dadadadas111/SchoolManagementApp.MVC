-- Update StartDate and EndDate for classes with IDs from 16 to 34
UPDATE Classes
SET StartDate = CASE 
                   WHEN Id % 2 = 0 THEN DATEADD(DAY, (Id - 15) * -7, GETDATE()) -- Past dates for even IDs
                   ELSE DATEADD(DAY, (Id - 15) * 7, GETDATE()) -- Future dates for odd IDs
               END,
    EndDate = CASE 
                  WHEN Id % 2 = 0 THEN DATEADD(DAY, (Id - 15) * -7 + 30, GETDATE()) -- Past EndDate for even IDs
                  ELSE DATEADD(DAY, (Id - 15) * 7 + 30, GETDATE()) -- Future EndDate for odd IDs
              END
WHERE Id BETWEEN 16 AND 34;
