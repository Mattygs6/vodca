USE [EmbraceHomeLoans]
GO

/****** Object:  Trigger [Live].[BackUpMenusInsertUpdate]    Script Date: 11/27/2011 17:57:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--============================================================================
--  Author: 	J.Baltikauskas
--  Date:    	08/15/2009
--============================================================================
CREATE TRIGGER [Live].[BackUpMenusInsertUpdate] ON [Live].[Menus]
    AFTER INSERT, UPDATE
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
    SET NOCOUNT ON ;

    DECLARE @action VARCHAR(15)
    IF EXISTS ( SELECT
                    MenuNodeID
                FROM
                    DELETED ) 
        SET @action = 'update'
    ELSE 
        SET @action = 'insert'

    -- Insert statements for trigger here
    INSERT  INTO [Backup].[Menus]
            ( [MenuNodeID]
            ,[Title]
            ,[PageAbsoluteUrl]
            ,[ParentNodeID]
            ,[LeafOridinal]
            ,[LeafLevel]
            ,[CategoryName]
            ,[ExcludeFromMenu]
            ,[IsArchive]
            ,[Date]
            ,[Action]
                
            )
            ( SELECT
                [MenuNodeID]
               ,[Title]
               ,[PageAbsoluteUrl]
               ,[ParentNodeID]
               ,[LeafOridinal]
               ,[LeafLevel]
               ,[CategoryName]
               ,[ExcludeFromMenu]
               ,[IsArchived]
               ,[Date]
               ,@action
              FROM
                inserted
            )     

END


GO


