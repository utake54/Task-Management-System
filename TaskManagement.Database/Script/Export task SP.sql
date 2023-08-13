

ALTER proc [dbo].[USP_GetTaskExport]
(
--USP_GetTaskExport 1
@CompanyId INT
)
AS
BEGIN

SET NOCOUNT ON;

create table #TempTaskData(
Title varchar(50),
Description varchar(max),
Priority varchar(25),
AssignedTo varchar(25),
AssignedBy varchar(25),
DueDate DateTime,
Status varchar(25),
CompletionDate varchar(max)
)

Insert into #TempTaskData
select tm.Title,
	   tm.Description,
	   tm.Priority,
	   um.FirstName as AssignedTo,
	   um1.FirstName as AssignedBy,
	   tm.DueDate,
	   tsm.Status as [Status],
	   ast.EndDate as StatusUpdated
from TaskMaster as tm
inner join AssignedTask as ast on tm.Id=ast.TaskId
inner join TaskStatusMaster as tsm on ast.Status=tsm.StatusId
inner join UserMaster as um on um.Id=ast.UserId
inner join UserMAster as um1 on um1.Id=ast.AssignedBy
where tm.CompanyId=@CompanyId

UPDATE #TempTaskData
SET CompletionDate='-'
where Status != 'Complete'

select * from #TempTaskData;

END