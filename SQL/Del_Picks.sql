
Use MyRealWorld

DECLARE @picId INT = 1
DECLARE @proj INT = 1
Delete from [Project_Picture] Where [PictureID]=@picId AND ProjectId = @proj
DELETE from Pictures Where Id = @picId

DELEte from projects where Id = @proj


