/****** Object:  Database [VHS]    Script Date: 2021-12-17 09:18:28 ******/
CREATE DATABASE [VHS]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'VHS', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\VHS.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'VHS_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\VHS_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [VHS] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [VHS].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [VHS] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [VHS] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [VHS] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [VHS] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [VHS] SET ARITHABORT OFF 
GO
ALTER DATABASE [VHS] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [VHS] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [VHS] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [VHS] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [VHS] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [VHS] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [VHS] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [VHS] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [VHS] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [VHS] SET  DISABLE_BROKER 
GO
ALTER DATABASE [VHS] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [VHS] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [VHS] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [VHS] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [VHS] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [VHS] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [VHS] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [VHS] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [VHS] SET  MULTI_USER 
GO
ALTER DATABASE [VHS] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [VHS] SET DB_CHAINING OFF 
GO
ALTER DATABASE [VHS] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [VHS] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [VHS] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [VHS] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [VHS] SET QUERY_STORE = OFF
GO
/****** Object:  UserDefinedFunction [dbo].[fEmptyGuid]    Script Date: 2021-12-17 09:18:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fEmptyGuid] 
 ()
RETURNS uniqueidentifier
AS
BEGIN
	return '00000000-0000-0000-0000-000000000000';

END
GO
/****** Object:  Table [dbo].[Commands]    Script Date: 2021-12-17 09:18:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Commands](
	[vin] [nvarchar](50) NOT NULL,
	[lights] [bit] NULL,
	[honk] [bit] NULL,
	[door] [bit] NULL,
	[heat] [bit] NULL,
	[ac] [bit] NULL,
	[trunk] [bit] NULL,
	[getDest] [bit] NULL,
	[DateLastModified] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Destinations]    Script Date: 2021-12-17 09:18:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Destinations](
	[vin] [nvarchar](50) NOT NULL,
	[latitude] [float] NOT NULL,
	[longitude] [float] NOT NULL,
	[timestamp] [datetime] NOT NULL,
 CONSTRAINT [PK_Destinations] PRIMARY KEY CLUSTERED 
(
	[vin] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DrivingJournal]    Script Date: 2021-12-17 09:18:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DrivingJournal](
	[vin] [nvarchar](50) NOT NULL,
	[journal_id] [uniqueidentifier] NOT NULL,
	[startTime] [datetime] NOT NULL,
	[endTime] [datetime] NULL,
	[tripStatus] [int] NOT NULL,
	[tripDistance] [int] NULL,
	[tripEnergyConsumption] [int] NULL,
	[tripAverageEnergyCons] [int] NULL,
	[tripAverageSpeed] [int] NULL,
	[tripDate] [datetime] NULL,
 CONSTRAINT [PK_DrivingJournal] PRIMARY KEY CLUSTERED 
(
	[vin] ASC,
	[journal_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DrivingLogs]    Script Date: 2021-12-17 09:18:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DrivingLogs](
	[vin] [nvarchar](50) NOT NULL,
	[log_id] [bigint] IDENTITY(1,1) NOT NULL,
	[journal_id] [uniqueidentifier] NOT NULL,
	[longitude] [float] NOT NULL,
	[latitude] [float] NOT NULL,
	[battery_level] [int] NOT NULL,
	[current_milage] [int] NOT NULL,
	[created_at] [datetime] NOT NULL,
 CONSTRAINT [PK_DrivingLogs] PRIMARY KEY CLUSTERED 
(
	[vin] ASC,
	[log_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Status]    Script Date: 2021-12-17 09:18:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Status](
	[vin] [nvarchar](50) NOT NULL,
	[lock] [bit] NULL,
	[battery] [int] NULL,
	[gps_longitude] [float] NULL,
	[gps_latitude] [float] NULL,
	[alarm] [bit] NULL,
	[tirepressure] [varchar](50) NULL,
	[milage] [float] NULL,
	[DateLastModified] [datetime] NOT NULL,
 CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED 
(
	[vin] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vehicles]    Script Date: 2021-12-17 09:18:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vehicles](
	[vin] [nvarchar](50) NOT NULL,
	[id] [uniqueidentifier] NOT NULL,
	[displayName] [nvarchar](50) NULL,
	[regNo] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Vehicles] PRIMARY KEY CLUSTERED 
(
	[vin] ASC,
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Index [IX_DrivingLogs]    Script Date: 2021-12-17 09:18:29 ******/
CREATE NONCLUSTERED INDEX [IX_DrivingLogs] ON [dbo].[DrivingLogs]
(
	[journal_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[sAddDestination]    Script Date: 2021-12-17 09:18:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sAddDestination]
	@vin varchar (50),
	@latitude float,
	@longitude float,
	@timestamp datetime

AS
BEGIN
	INSERT INTO
		dbo.Destinations
	VALUES
		(
			@vin,
			@latitude,
			@longitude,
			GETDATE()
		);

END
GO
/****** Object:  StoredProcedure [dbo].[sAddVehicle]    Script Date: 2021-12-17 09:18:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sAddVehicle]
	@vin varchar (50), 
	@id uniqueidentifier, 
	@displayName nvarchar (50),
	@regNo nvarchar (50)
AS
BEGIN

	INSERT INTO 
		dbo.Vehicles
	VALUES
		(
			@vin, 
			@id, 
			@displayName,
			@regNo
		);

END
GO
/****** Object:  StoredProcedure [dbo].[sCreateCars]    Script Date: 2021-12-17 09:18:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sCreateCars]
	@vin varchar (50), 
	@id varchar (50), 
	@displayName nvarchar (50)
AS
BEGIN

	INSERT INTO 
		dbo.Cars
	VALUES
		(
			@vin, 
			@id, 
			@displayName
		);

END
GO
/****** Object:  StoredProcedure [dbo].[sDeleteDestination]    Script Date: 2021-12-17 09:18:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sDeleteDestination]
	@vin nvarchar (50)

AS

BEGIN
	DELETE FROM
		dbo.Destinations
	WHERE
		vin = @vin
END
GO
/****** Object:  StoredProcedure [dbo].[sGetCommands]    Script Date: 2021-12-17 09:18:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sGetCommands]
	@vin nvarchar (50),
	@result bit OUTPUT,
	@lights bit OUTPUT,
	@honk bit OUTPUT, 
	@door bit OUTPUT,
	@heat bit OUTPUT,
	@ac bit OUTPUT,
	@trunk bit OUTPUT,
	@getDest bit OUTPUT,
	@DateLastModified datetime OUTPUT

AS

BEGIN

	SET @result = 0;
	SELECT
		@result = 1,
		@lights = c.lights,
		@honk = c.honk,
		@door = c.door,
		@heat = c.heat,
		@ac = c.ac,
		@trunk = c.trunk,
		@getDest = c.getDest,
		@DateLastModified = c.DateLastModified
	FROM
		dbo.Commands as c
	WHERE
		c.vin = @vin
END
GO
/****** Object:  StoredProcedure [dbo].[sGetDestination]    Script Date: 2021-12-17 09:18:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sGetDestination]
	@vin nvarchar (50),
	@result bit OUTPUT,
	@latitude float OUTPUT,
	@longitude float OUTPUT,
	@timestamp datetime OUTPUT

AS

BEGIN
	
	SET @result = 0;
	SELECT
		@result = 1,
		@latitude = d.latitude,
		@longitude = d.longitude,
		@timestamp = d.timestamp

	FROM
		dbo.Destinations as d
	WHERE
		d.vin = @vin
END
GO
/****** Object:  StoredProcedure [dbo].[sGetLatitude]    Script Date: 2021-12-17 09:18:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sGetLatitude]
	@vin nvarchar (50),
	@result bit OUTPUT,
	@gps_latitude float OUTPUT

AS

BEGIN
	SET @result = 0;
	SELECT 
		@result = 1,
		@gps_latitude = s.gps_latitude

	FROM 
		dbo.Status as s
	WHERE 
		s.vin = @vin

END
GO
/****** Object:  StoredProcedure [dbo].[sGetLongitude]    Script Date: 2021-12-17 09:18:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sGetLongitude]
	@vin nvarchar (50),
	@result bit OUTPUT,
	@gps_longitude float OUTPUT

AS

BEGIN
	SET @result = 0;
	SELECT 
		@result = 1,
		@gps_longitude = s.gps_longitude

	FROM 
		dbo.Status as s
	WHERE 
		s.vin = @vin

END
GO
/****** Object:  StoredProcedure [dbo].[sGetRegNo]    Script Date: 2021-12-17 09:18:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sGetRegNo]
	@vin nvarchar (50),
	@regNo nvarchar (50) output,
	@Result bit output

AS
BEGIN
	SET @Result = CAST(0 as bit);

	SELECT
		@Result = CAST(1 as bit),
		@regNo = v.regNo
	FROM
		dbo.Vehicles as v
WHERE
	v.vin = @vin;

END
GO
/****** Object:  StoredProcedure [dbo].[sGetTripLogs]    Script Date: 2021-12-17 09:18:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sGetTripLogs]
	@vin nvarchar (50),
	@journal_id uniqueidentifier

AS
BEGIN
	SELECT
		d.log_id,
		d.longitude,
		d.latitude,
		d.battery_level,
		d.current_milage,
		d.created_at

	FROM
		dbo.DrivingLogs as d
	
	WHERE
		d.vin = @vin AND d.journal_id = @journal_id
	ORDER BY
		d.log_id
END
GO
/****** Object:  StoredProcedure [dbo].[sSaveTripData]    Script Date: 2021-12-17 09:18:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sSaveTripData]
	@vin nvarchar (50),
	@journal_id uniqueidentifier,
	@startTime datetime,
	@endTime datetime,
	@tripStatus int,
	@tripDistance int,
	@tripEnergyConsumption int,
	@tripAverageEnergyCons int,
	@tripAverageSpeed int,
	@tripDate datetime

AS

BEGIN
	UPDATE
		dbo.DrivingJournal
	SET
		endTime = @endTime,
		tripStatus = @tripStatus,
		tripDistance = @tripDistance,
		tripEnergyConsumption = @tripEnergyConsumption,
		tripAverageEnergyCons = @tripAverageEnergyCons,
		tripAverageSpeed = @tripAverageSpeed,
		tripDate = @tripDate

	WHERE
		vin = @vin AND
		journal_id = @journal_id

	IF @@ROWCOUNT = 0 BEGIN
		INSERT INTO	
			dbo.DrivingJournal
		VALUES
			(
				@vin,
				@journal_id,
				@startTime,
				@endTime,
				@tripStatus,
				@tripDistance,
				@tripEnergyConsumption,
				@tripAverageEnergyCons,
				@tripAverageSpeed,
				@tripDate
			);
	END

END
GO
/****** Object:  StoredProcedure [dbo].[sSendDrivingLogs]    Script Date: 2021-12-17 09:18:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sSendDrivingLogs]
	@vin nvarchar (50),
	@journal_id uniqueidentifier,
	@longitude float,
	@latitude float, 
	@battery_level int,
	@current_milage int,
	@created_at datetime

AS

BEGIN
	INSERT INTO
		dbo.DrivingLogs
	VALUES
		(
			@vin,
			@journal_id,
			@longitude,
			@latitude,
			@battery_level,
			@current_milage,
			GETDATE()
		);

END
GO
/****** Object:  StoredProcedure [dbo].[sStartDrivingJournal]    Script Date: 2021-12-17 09:18:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sStartDrivingJournal]
	@vin nvarchar (50),
	@journal_id uniqueidentifier,
	@startTime datetime,
	@endTime datetime,
	@tripStatus int,
	@tripDistance int,
	@tripEnergyConsumption int,
	@tripAverageEnergyCons int,
	@tripAverageSpeed int,
	@tripDate datetime

AS

BEGIN
	UPDATE
		dbo.DrivingJournal
	SET
		journal_id = @journal_id,
		startTime = @startTime,
		endTime = @endTime,
		tripStatus = @tripStatus,
		tripDistance = @tripDistance,
		tripEnergyConsumption = @tripEnergyConsumption,
		tripAverageEnergyCons = @tripAverageEnergyCons,
		tripAverageSpeed = @tripAverageSpeed,
		tripDate = @tripDate

	WHERE
		vin = @vin

	IF @@ROWCOUNT = 0 BEGIN
		INSERT INTO	
			dbo.DrivingJournal
		VALUES
			(
				@vin,
				@journal_id,
				@startTime,
				@endTime,
				@tripStatus,
				@tripDistance,
				@tripEnergyConsumption,
				@tripAverageEnergyCons,
				@tripAverageSpeed,
				@tripDate
			);
	END

END
GO
/****** Object:  StoredProcedure [dbo].[sStatusGet]    Script Date: 2021-12-17 09:18:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sStatusGet]
	@vin nvarchar (50),
	@result bit OUTPUT,
	@lock bit OUTPUT,
	@battery int OUTPUT,
	@gps_longitude float OUTPUT,
	@gps_latitude float OUTPUT,
	@alarm bit OUTPUT,
	@tirepressure varchar (50) OUTPUT,
	@milage float OUTPUT,
	@DateLastModified datetime OUTPUT

AS

BEGIN
	SET @result = 0;
	SELECT 
		@result = 1,
		@lock = s.lock,
		@battery = s.battery,
		@gps_longitude = s.gps_longitude,
		@gps_latitude = s.gps_latitude,
		@alarm = s.alarm,
		@tirepressure = s.tirepressure,
		@milage = s.milage,
		@DateLastModified = s.DateLastModified

	FROM 
		dbo.Status as s
	WHERE 
		s.vin = @vin


END
GO
/****** Object:  StoredProcedure [dbo].[sStatusUpdate]    Script Date: 2021-12-17 09:18:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sStatusUpdate]
	@vin nvarchar (50),
	@lock bit,
	@battery int,
	@gps_longitude float,
	@gps_latitude float,
	@alarm bit,
	@tirepressure varchar (50),
	@milage float

AS
	
BEGIN
	UPDATE 
		dbo.Status

	SET
		lock = @lock,
		battery = @battery,
		gps_longitude = @gps_longitude,
		gps_latitude = @gps_latitude,
		alarm = @alarm,
		tirepressure = @tirepressure,
		milage = @milage,
		DateLastModified = GETDATE()

	WHERE
		vin = @vin;
	
	IF @@ROWCOUNT = 0 BEGIN
		INSERT INTO
			dbo.Status
		VALUES
			(
				@vin,
				@lock,
				@battery,
				@gps_longitude,
				@gps_latitude,
				@alarm,
				@tirepressure,
				@milage,
				GETDATE()
			);
	END

END
GO
/****** Object:  StoredProcedure [dbo].[sUpdateCommands]    Script Date: 2021-12-17 09:18:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sUpdateCommands]
	@vin nvarchar (50),
	@lights bit,
	@honk bit, 
	@door bit,
	@heat bit,
	@ac bit,
	@trunk bit,
	@getDest bit

AS

BEGIN
	UPDATE
		dbo.Commands

	SET
		lights = @lights,
		honk = @honk,
		door = @door,
		heat = @heat,
		ac = @ac,
		trunk = @trunk,
		getDest = @getDest,
		DateLastModified = GETDATE()

	WHERE
		vin = @vin;

	IF @@ROWCOUNT = 0 BEGIN
		INSERT INTO	
			dbo.Commands
		VALUES
			(
			@vin,
			@lights,
			@honk, 
			@door,
			@heat,
			@ac,
			@trunk,
			@getDest,
			GETDATE()
			);
	END
END

GO
/****** Object:  StoredProcedure [dbo].[sUpdateDestination]    Script Date: 2021-12-17 09:18:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sUpdateDestination]
	@vin nvarchar (50),
	@latitude float,
	@longitude float

AS

BEGIN
	UPDATE
		dbo.Destinations

	SET
		latitude = @latitude,
		longitude = @longitude,
		timestamp = GETDATE()

	WHERE
		vin = @vin;

END
GO
/****** Object:  StoredProcedure [dbo].[sUpdateVehicleList]    Script Date: 2021-12-17 09:18:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sUpdateVehicleList]
	@vin varchar (50),
	@id uniqueidentifier,
	@displayName varchar (50),
	@regNo nvarchar (50)
AS

BEGIN
	UPDATE
		dbo.Vehicles

	SET
		vin = @vin,
		id = @id,
		displayName = @displayName,
		regNo = @regNo

	WHERE
		vin = @vin AND 
		id = @id;

	IF @@ROWCOUNT = 0 BEGIN
		INSERT INTO
			dbo.Vehicles
		VALUES
			(
			@vin,
			@id,
			@displayName,
			@regNo
			);
	END

END
GO
/****** Object:  StoredProcedure [dbo].[sVehicleHonk]    Script Date: 2021-12-17 09:18:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sVehicleHonk]
    @Vin varchar (50), 
    @Latitude float, 
    @Longitude float, 
    @Radius float,
	@Result bit Output

AS
BEGIN
	SET @Result = 0;

    DECLARE @Point geography;
    SET @Point = geography::Point(@Latitude, @Longitude, 4326);

    SELECT 
        @Result = 1

    FROM 
        dbo.Status AS s
    WHERE
        s.vin = @Vin
        AND geography::Point(s.gps_latitude, s.gps_longitude, 4326).STDistance(@Point) <= @Radius;

	IF (@Result = 1) BEGIN
		UPDATE
			dbo.Commands
		SET
			lights = 1,
			honk = 1,
			DateLastModified = GETDATE()
		WHERE
			vin = @Vin
	END
END
GO
ALTER DATABASE [VHS] SET  READ_WRITE 
GO
