
--USP_GetAllTask 1,'P',1,5,null,'EndDate'
ALTER PROC [dbo].[USP_GetAllTask](
    @CompanyId int,
    @Search VARCHAR(max) = NULL,
    @PageNumber int = 1,
    @PageSize int = 5,
    @Status VARCHAR(max) = NULL,
    @OrderBy VARCHAR(max) = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @GetAllTaskQuery NVARCHAR(MAX);
    SET @GetAllTaskQuery = 'SELECT tm.Id, 
                            tm.Title,
                            tm.Description,
                            tcm.Category,
                            tm.Priority,
                            um.FirstName as AssignedTo,
                            tm.DueDate,
                            tsm.Status,
                            atm.EndDate FROM TaskMaster tm  
                            inner join TaskCategoryMaster tcm on tm.CategoryId=tcm.Id  
                            inner join AssignedTask atm on atm.TaskId=tm.Id  
                            inner join USerMaster um on um.Id = atm.UserId 
                            left join TaskStatusMaster tsm on tsm.StatusId=atm.Status
                            WHERE tm.IsActive = 1 AND tm.CompanyId = ' + CAST(@CompanyId AS NVARCHAR(10));

    IF @Search IS NOT NULL
    BEGIN
        SET @GetAllTaskQuery = @GetAllTaskQuery + ' AND (um.Firstname LIKE ''' + @Search + '%'' 
													OR  tcm.Category LIKE ''' + @Search + '%''
													OR  tm.Title LIKE ''' + @Search + '%''
												        )';
    END


    IF @Status IS NOT NULL
    BEGIN
        SET @GetAllTaskQuery = @GetAllTaskQuery + ' AND atm.Status = ''' + @Status + '''';
    END


    SET @GetAllTaskQuery = @GetAllTaskQuery + 'ORDER BY ' + COALESCE(@OrderBy, 'tm.Id') + 
                            'OFFSET ' + CAST(((@PageNumber - 1) * @PageSize) AS NVARCHAR(10)) +
                            ' ROWS FETCH NEXT ' + CAST(@PageSize AS NVARCHAR(10)) + ' ROWS ONLY';

    EXEC sp_executesql @GetAllTaskQuery;
END;
