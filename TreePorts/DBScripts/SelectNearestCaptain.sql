CREATE PROCEDURE SelectNearestCaptain @lat nvarchar(100), @lng nvarchar(100)
AS
declare @my_lat as float
declare @my_lng as float
declare @dist as float

set @my_lat = convert(float,@lat);
set @my_lng=convert(float,@lng);
set @dist=10;

SELECT top 1 users.*
FROM (SELECT dest.UserID as userId,convert(float,dest.lat) as lat,convert(float,dest.Long) as long, (3956 * 2 * ASIN(SQRT(POWER(SIN((@my_lat -abs(convert(float,dest.lat))) * pi()/180 / 2),2) + COS(@my_lat * pi()/180 ) * COS(abs(convert(float,dest.Lat)) *  pi()/180) * POWER(SIN((@my_lng - abs(convert(float,dest.Long))) *  pi()/180 / 2), 2))
)) as distance
FROM UserCurrentLocation as dest) tempTable
JOIN Users as users on ID = tempTable.userId
JOIN UserActivities as userActivities on userActivities.UserID = users.ID
and userActivities.IsCurrent = 1 and  userActivities.StatusTypeID = 11
WHERE users.ID not in (SELECT running.UserID FROM RunningOrders as running) and tempTable.distance < @dist
ORDER BY distance;
go

