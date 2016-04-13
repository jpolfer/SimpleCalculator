-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jon Polfer
-- Create date: 4/13/2016
-- Description:	Deletes all history records
-- =============================================
CREATE PROCEDURE DeleteAllHistoryEntries 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @timestamp VARCHAR(50) -- timestamp
	DECLARE db_cursor CURSOR FOR  
			SELECT [Timestamp] 
			FROM MASTER.dbo.HistoryEntries   

OPEN db_cursor   
FETCH NEXT FROM db_cursor INTO @timestamp   

WHILE @@FETCH_STATUS = 0   
BEGIN   
       DELETE FROM master.dbo.HistoryEntries WHERE [Timestamp] = @timestamp 

       FETCH NEXT FROM db_cursor INTO @timestamp   
END   

CLOSE db_cursor   
DEALLOCATE db_cursor
END
GO
