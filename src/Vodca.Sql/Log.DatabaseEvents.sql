
CREATE SCHEMA [Log] AUTHORIZATION [dbo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [Log].[DatabaseEvents]
    (
     [DatabaseLogID] [bigint] IDENTITY(1, 1)
                              NOT NULL
    ,[PostTime] [datetime] NOT NULL
    ,[DatabaseUser] [sysname] NOT NULL
    ,[Event] [sysname] NOT NULL
    ,[Schema] [sysname] NULL
    ,[Object] [sysname] NULL
    ,[TSQL] [nvarchar](MAX) NOT NULL
    ,[XmlEvent] [xml] NOT NULL
    ,CONSTRAINT [PK_DatabaseEvents] PRIMARY KEY CLUSTERED ( [DatabaseLogID] DESC )
        WITH ( PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
    )
ON  [PRIMARY]

GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE TRIGGER [LogDatabaseEventsTrigger] ON DATABASE
    FOR DDL_DATABASE_LEVEL_EVENTS
AS
BEGIN
    SET NOCOUNT ON ;

    DECLARE @data XML ;
    DECLARE @schema SYSNAME ;
    DECLARE @object SYSNAME ;
    DECLARE @eventType SYSNAME ;

    SET @data = EVENTDATA() ;
    SET @eventType = @data.value('(/EVENT_INSTANCE/EventType)[1]', 'sysname') ;
    SET @schema = @data.value('(/EVENT_INSTANCE/SchemaName)[1]', 'sysname') ;
    SET @object = @data.value('(/EVENT_INSTANCE/ObjectName)[1]', 'sysname') 

    IF @object IS NOT NULL 
        PRINT '  ' + @eventType + ' - ' + @schema + '.' + @object ;
    ELSE 
        PRINT '  ' + @eventType + ' - ' + @schema ;

    IF @eventType IS NULL 
        PRINT CONVERT(NVARCHAR(MAX), @data) ;

    INSERT  Log.DatabaseEvents
            ( 
             [PostTime]
            ,[DatabaseUser]
            ,[Event]
            ,[Schema]
            ,[Object]
            ,[TSQL]
            ,[XmlEvent]
            )
    VALUES
            ( 
             GETDATE()
            ,CONVERT(SYSNAME, CURRENT_USER)
            ,@eventType
            ,CONVERT(SYSNAME, @schema)
            ,CONVERT(SYSNAME, @object)
            ,@data.value('(/EVENT_INSTANCE/TSQLCommand)[1]', 'nvarchar(max)')
            ,@data
            ) ;
END ;




GO

SET ANSI_NULLS OFF
GO