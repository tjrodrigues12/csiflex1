-- MySQL dump 10.13  Distrib 8.0.23, for Win64 (x86_64)
--
-- Host: localhost    Database: csi_auth
-- ------------------------------------------------------
-- Server version	8.0.18

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `auto_report_config`
--

DROP TABLE IF EXISTS `auto_report_config`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `auto_report_config` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Task_name` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `Day_` text CHARACTER SET utf8 COLLATE utf8_unicode_ci,
  `Time_` text CHARACTER SET utf8 COLLATE utf8_unicode_ci,
  `ReportType` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL DEFAULT '',
  `ReportPeriod` varchar(20) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `ReportTitle` varchar(200) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL DEFAULT '',
  `Output_Folder` text CHARACTER SET utf8 COLLATE utf8_unicode_ci,
  `MachineToReport` text CHARACTER SET utf8 COLLATE utf8_unicode_ci,
  `MailTo` text CHARACTER SET utf8 COLLATE utf8_unicode_ci,
  `Email_Time` text CHARACTER SET utf8 COLLATE utf8_unicode_ci,
  `done` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL DEFAULT '10',
  `StartDay` varchar(10) COLLATE utf8_unicode_ci DEFAULT NULL,
  `dayback` text CHARACTER SET utf8 COLLATE utf8_unicode_ci,
  `timeback` text CHARACTER SET utf8 COLLATE utf8_unicode_ci,
  `CustomMsg` text CHARACTER SET utf8 COLLATE utf8_unicode_ci,
  `Scale` varchar(10) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `Production` tinyint(1) DEFAULT '1',
  `Setup` tinyint(1) DEFAULT '1',
  `OnlySummary` tinyint(1) DEFAULT '0',
  `Enabled` tinyint(1) DEFAULT '1',
  `Sorting` varchar(20) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL DEFAULT 'Value',
  `EventMinMinutes` int(11) DEFAULT '0',
  `shift_number` varchar(45) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `shift_starttime` varchar(45) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `shift_endtime` varchar(45) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `short_filename` varchar(45) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL DEFAULT 'False',
  `ShowConInSetup` int(11) DEFAULT '2',
  `ExcludedStates` varchar(100) COLLATE utf8_unicode_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Task_name_UNIQUE` (`Task_name`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `auto_report_status`
--

DROP TABLE IF EXISTS `auto_report_status`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `auto_report_status` (
  `ReportId` int(11) NOT NULL,
  `Status` varchar(45) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  PRIMARY KEY (`ReportId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `ftpconfig`
--

DROP TABLE IF EXISTS `ftpconfig`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ftpconfig` (
  `ftpip` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `ftppwd` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `EnetPCName` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `EnetPCPwd` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_autoreport_status`
--

DROP TABLE IF EXISTS `tbl_autoreport_status`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_autoreport_status` (
  `autoreport_status` varchar(5) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_csiconnector`
--

DROP TABLE IF EXISTS `tbl_csiconnector`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_csiconnector` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `MachineId` int(11) NOT NULL DEFAULT '0',
  `MachineName` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `MTCMachine` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `MachineIP` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `ConnectorType` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `CurrentStatus` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `ConditionStr` text CHARACTER SET utf8 COLLATE utf8_unicode_ci,
  `PartnoID` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `ProgramID` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `FeedOverrideID` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `SpindleOverrideID` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `eNETMachineName` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `FocasPort` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `ControllerType` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `AgentIP` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `AgentPort` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `Manufacturer` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `AgentServiceName` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `AgentExeLocation` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `AdapterServiceName` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `AdapterExeLocation` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `AdapterPort` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `MonitoringUnitId` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UniqueMachineName` (`MachineName`),
  UNIQUE KEY `UniqueEnetMachine` (`eNETMachineName`)
) ENGINE=InnoDB AUTO_INCREMENT=42 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_csiothersettings`
--

DROP TABLE IF EXISTS `tbl_csiothersettings`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_csiothersettings` (
  `ConnectorID` int(11) NOT NULL,
  `Machine_Name` varchar(200) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `IP_Address` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `PartNumber_Variable` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `PartNumber_Prefix1` varchar(45) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `PartNumber_Filter1Start` varchar(45) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `PartNumber_Filter1End` varchar(45) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `PartNumber_Filter2Apply` tinyint(4) DEFAULT '0',
  `PartNumber_Prefix2` varchar(45) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `PartNumber_Filter2Start` varchar(45) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `PartNumber_Filter2End` varchar(45) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `PartNumber_Filter3Apply` tinyint(4) DEFAULT '0',
  `PartNumber_Prefix3` varchar(45) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `PartNumber_Filter3Start` varchar(45) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `PartNumber_Filter3End` varchar(45) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `ProgramNumber_Variable` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `ProgramNumber_FilterStart` varchar(45) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `ProgramNumber_FilterEnd` varchar(45) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `Operation_Available` tinyint(4) DEFAULT '0',
  `Operation_FilterStart` varchar(45) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Operation_FilterEnd` varchar(45) COLLATE utf8_unicode_ci DEFAULT NULL,
  `PartRequired_Variable` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `PartCount_Variable` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `Feedrate_Variable` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `Feedrate_Min` varchar(45) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `Feedrate_Max` varchar(45) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `Rapid_Variable` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `Spindle_MIN` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL DEFAULT '5',
  `Spindle_MAX` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL DEFAULT '150',
  `Rapid_MIN` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL DEFAULT '5',
  `Rapid_MAX` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL DEFAULT '100',
  `Spindle_Variable` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `ActivePallet_Var` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `ActivePallet_StartWith` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `ActivePallet_EndWith` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `ActivePallet_ToMU` tinyint(4) NOT NULL DEFAULT '0',
  `WarningPressure` int(11) DEFAULT '0',
  `CriticalPressure` int(11) DEFAULT '0',
  `WarningTemperature` int(11) DEFAULT '0',
  `CriticalTemperature` int(11) DEFAULT '0',
  `MCSDelay` int(11) DEFAULT '0',
  `DelayScale` varchar(3) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT 'sec',
  `EnableMCS` tinyint(4) DEFAULT '0',
  `SaveDataRaw` tinyint(4) DEFAULT '0',
  `COnDuringSetup` tinyint(4) DEFAULT '1',
  `SaveProdOnly` int(11) DEFAULT '0',
  `DelayForCycleOff` int(11) DEFAULT '0',
  `DelayForCycleOffScale` varchar(3) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT 'sec',
  PRIMARY KEY (`ConnectorID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_csiothersettingsvalues`
--

DROP TABLE IF EXISTS `tbl_csiothersettingsvalues`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_csiothersettingsvalues` (
  `ConnectorID` int(11) NOT NULL,
  `Machine_Name` varchar(200) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `PartNumber_Value` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `ProgramNumber_Value` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `Operation_Value` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `PartRequired_Value` int(11) DEFAULT '0',
  `PartCount_Value` int(11) DEFAULT '0',
  `Feedrate_Value` varchar(10) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `Spindle_Value` varchar(10) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `Rapid_Value` varchar(10) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `ActivePallet_Value` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`ConnectorID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_ehub_conf`
--

DROP TABLE IF EXISTS `tbl_ehub_conf`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_ehub_conf` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `monitoring_id` varchar(20) NOT NULL,
  `machine_name` varchar(200) NOT NULL,
  `EnetMachineName` varchar(100) NOT NULL DEFAULT '',
  `machine_label` varchar(200) NOT NULL,
  `Con_type` int(2) NOT NULL,
  `monitoring_filename` varchar(200) NOT NULL,
  `Monstate` int(2) NOT NULL,
  `MCH_DailyTarget` int(11) NOT NULL DEFAULT '0',
  `MCH_WeeklyTarget` int(11) NOT NULL DEFAULT '0',
  `MCH_MonthlyTarget` int(11) NOT NULL DEFAULT '0',
  `ftpfilename` varchar(100) NOT NULL,
  `CurrentStatus` varchar(200) NOT NULL,
  `CurrentPartNumber` varchar(200) NOT NULL,
  `EnetDept` varchar(45) NOT NULL DEFAULT ' ',
  `MTC_Machine_name` varchar(200) NOT NULL DEFAULT ' ',
  `TH_State` int(11) NOT NULL DEFAULT '0',
  `Redirect` varchar(10) NOT NULL DEFAULT '0,0',
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`),
  UNIQUE KEY `monitoring_id` (`monitoring_id`)
) ENGINE=InnoDB AUTO_INCREMENT=102407 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_emailreports`
--

DROP TABLE IF EXISTS `tbl_emailreports`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_emailreports` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `senderemail` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `senderpwd` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `smtphost` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `smtpport` int(11) DEFAULT NULL,
  `requirecred` tinyint(1) DEFAULT NULL,
  `usessl` tinyint(1) NOT NULL DEFAULT '1',
  `isdefault` tinyint(1) NOT NULL DEFAULT '0',
  `isused` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`),
  UNIQUE KEY `senderemail_UNIQUE` (`senderemail`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_file_status`
--

DROP TABLE IF EXISTS `tbl_file_status`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_file_status` (
  `num_id_` int(11) NOT NULL AUTO_INCREMENT,
  `log_date` datetime DEFAULT NULL,
  `file_name` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `file_hash` varbinary(1000) DEFAULT NULL,
  PRIMARY KEY (`num_id_`),
  UNIQUE KEY `num_id_` (`num_id_`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_license`
--

DROP TABLE IF EXISTS `tbl_license`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_license` (
  `UniqueId` char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `CompanyId` char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `ProductId` int(11) DEFAULT NULL,
  `ProductName` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `LicensesQuantity` int(11) DEFAULT NULL,
  `CompanyName` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `ComputerName` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `ComputerId` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `ContactName` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `ContactEmail` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `ContactPhone` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `LicenseType` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `LicenseDate` datetime DEFAULT NULL,
  `ExpiryDate` datetime DEFAULT NULL,
  `HashCode` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `LicenseStatus` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`UniqueId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_mach_status`
--

DROP TABLE IF EXISTS `tbl_mach_status`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_mach_status` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `MachineId` int(11) NOT NULL,
  `Status` varchar(45) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Id_UNIQUE` (`Id`),
  UNIQUE KEY `MachId_Status_Unique` (`MachineId`,`Status`)
) ENGINE=InnoDB AUTO_INCREMENT=175956 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_machine_commands`
--

DROP TABLE IF EXISTS `tbl_machine_commands`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_machine_commands` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `MachineId` int(11) DEFAULT NULL,
  `StatusId` int(11) DEFAULT NULL,
  `CommandTypeId` int(11) DEFAULT NULL,
  `CommandName` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Command` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Id_Unique` (`Id`),
  UNIQUE KEY `Machine_Status_Unique` (`MachineId`,`StatusId`)
) ENGINE=InnoDB AUTO_INCREMENT=87658 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_machines_settings`
--

DROP TABLE IF EXISTS `tbl_machines_settings`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_machines_settings` (
  `MachineId` int(11) NOT NULL,
  `CurrentStatus` varchar(200) NOT NULL DEFAULT '',
  `NextStatus` varchar(200) NOT NULL DEFAULT '',
  `CurrentPartNumber` varchar(200) NOT NULL DEFAULT '',
  `AllowsComments` tinyint(4) DEFAULT '0',
  `AllowComments` tinyint(4) NOT NULL DEFAULT '0',
  `Comments` varchar(500) NOT NULL DEFAULT '',
  `RequestSetupOperator` tinyint(4) NOT NULL DEFAULT '0',
  `RequestProdOperator` tinyint(4) NOT NULL DEFAULT '0',
  `MultipleOperators` tinyint(4) NOT NULL DEFAULT '0',
  `NewMobileVersion` tinyint(4) NOT NULL DEFAULT '0',
  `DefaultTimeUnit` varchar(1) DEFAULT '0',
  `DisplayTimeline` tinyint(4) NOT NULL DEFAULT '1',
  `DisplayBarChart` tinyint(4) NOT NULL DEFAULT '1',
  `DisplayOverrideChart` tinyint(4) NOT NULL DEFAULT '0',
  `DisplayQuickButtons` tinyint(4) NOT NULL DEFAULT '0',
  PRIMARY KEY (`MachineId`),
  UNIQUE KEY `MachineId` (`MachineId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_mtcfocasconditions`
--

DROP TABLE IF EXISTS `tbl_mtcfocasconditions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_mtcfocasconditions` (
  `ConnectorID` int(11) DEFAULT NULL,
  `Machine_Name` varchar(200) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `IP_Address` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `Status` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `Condition` varchar(500) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `Machine_Type` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `delay` varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `delayScale` varchar(3) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT 'sec',
  `CsdOnSetup` tinyint(4) DEFAULT '0',
  `StatusDisabled` tinyint(4) DEFAULT '0',
  UNIQUE KEY `unique_status_for_machine` (`Machine_Name`,`Status`),
  UNIQUE KEY `machine_condition_exists` (`Machine_Name`,`Condition`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_serviceconfig`
--

DROP TABLE IF EXISTS `tbl_serviceconfig`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_serviceconfig` (
  `loadingAsCON` tinyint(1) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_states`
--

DROP TABLE IF EXISTS `tbl_states`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_states` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Status` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `unique_status` (`Status`)
) ENGINE=InnoDB AUTO_INCREMENT=5776 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_updatestatus`
--

DROP TABLE IF EXISTS `tbl_updatestatus`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_updatestatus` (
  `initialdbload` tinyint(1) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `user_machines`
--

DROP TABLE IF EXISTS `user_machines`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user_machines` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `UserId` int(11) NOT NULL,
  `MachineId` int(11) NOT NULL,
  `Index` int(11) NOT NULL DEFAULT '999',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Id_UNIQUE` (`Id`),
  UNIQUE KEY `User_Machine_UNIQUE` (`UserId`,`MachineId`)
) ENGINE=InnoDB AUTO_INCREMENT=175 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `users` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `username_` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `Name_` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `firstname_` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `displayname` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `password_` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `salt_` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `email_` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `usertype` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `machines` text CHARACTER SET utf8 COLLATE utf8_unicode_ci,
  `Ovr_interval` int(11) DEFAULT NULL,
  `refId` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `title` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `dept` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `phoneext` varchar(20) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `EditTimeline` tinyint(4) NOT NULL DEFAULT '0',
  `EditMasterPartData` tinyint(4) NOT NULL DEFAULT '0',
  `EditSetup` tinyint(4) NOT NULL DEFAULT '0',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Name_UNIQUE` (`username_`),
  UNIQUE KEY `refId_UNIQUE` (`refId`)
) ENGINE=InnoDB AUTO_INCREMENT=20 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Temporary view structure for view `view_auto_report_config`
--

DROP TABLE IF EXISTS `view_auto_report_config`;
/*!50001 DROP VIEW IF EXISTS `view_auto_report_config`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `view_auto_report_config` AS SELECT 
 1 AS `id`,
 1 AS `Task_name`,
 1 AS `Day_`,
 1 AS `Time_`,
 1 AS `ReportType`,
 1 AS `ReportPeriod`,
 1 AS `ReportTitle`,
 1 AS `Output_Folder`,
 1 AS `MachineToReport`,
 1 AS `MailTo`,
 1 AS `Email_Time`,
 1 AS `Done`,
 1 AS `StartDay`,
 1 AS `dayback`,
 1 AS `timeback`,
 1 AS `CustomMsg`,
 1 AS `Scale`,
 1 AS `Production`,
 1 AS `Setup`,
 1 AS `OnlySummary`,
 1 AS `Enabled`,
 1 AS `Sorting`,
 1 AS `EventMinMinutes`,
 1 AS `shift_number`,
 1 AS `shift_starttime`,
 1 AS `shift_endtime`,
 1 AS `short_filename`,
 1 AS `ShowConInSetup`,
 1 AS `ExcludedStates`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `view_connectors_values`
--

DROP TABLE IF EXISTS `view_connectors_values`;
/*!50001 DROP VIEW IF EXISTS `view_connectors_values`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `view_connectors_values` AS SELECT 
 1 AS `Id`,
 1 AS `MachineId`,
 1 AS `Feedrate_MIN`,
 1 AS `Feedrate_MAX`,
 1 AS `FeedRate_Value`,
 1 AS `FeedRateOver_Value`,
 1 AS `Spindle_MIN`,
 1 AS `Spindle_MAX`,
 1 AS `Spindle_Value`,
 1 AS `SpindleOver_Value`,
 1 AS `Rapid_MIN`,
 1 AS `Rapid_MAX`,
 1 AS `Rapid_Value`,
 1 AS `RapidOver_Value`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `view_csiothersettings`
--

DROP TABLE IF EXISTS `view_csiothersettings`;
/*!50001 DROP VIEW IF EXISTS `view_csiothersettings`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `view_csiothersettings` AS SELECT 
 1 AS `ConnectorID`,
 1 AS `Machine_Name`,
 1 AS `IP_Address`,
 1 AS `PartNumber_Variable`,
 1 AS `PartNumber_Value`,
 1 AS `PartNumber_Prefix1`,
 1 AS `PartNumber_Filter1Start`,
 1 AS `PartNumber_Filter1End`,
 1 AS `PartNumber_Filter2Apply`,
 1 AS `PartNumber_Prefix2`,
 1 AS `PartNumber_Filter2Start`,
 1 AS `PartNumber_Filter2End`,
 1 AS `PartNumber_Filter3Apply`,
 1 AS `PartNumber_Prefix3`,
 1 AS `PartNumber_Filter3Start`,
 1 AS `PartNumber_Filter3End`,
 1 AS `ProgramNumber_Variable`,
 1 AS `ProgramNumber_Value`,
 1 AS `ProgramNumber_FilterStart`,
 1 AS `ProgramNumber_FilterEnd`,
 1 AS `Operation_Available`,
 1 AS `Operation_Value`,
 1 AS `Operation_FilterStart`,
 1 AS `Operation_FilterEnd`,
 1 AS `PartRequired_Variable`,
 1 AS `PartRequired_Value`,
 1 AS `PartCount_Variable`,
 1 AS `PartCount_Value`,
 1 AS `FeedRate_Variable`,
 1 AS `Feedrate_Value`,
 1 AS `Feedrate_Min`,
 1 AS `Feedrate_Max`,
 1 AS `Spindle_Variable`,
 1 AS `Spindle_Value`,
 1 AS `Spindle_Min`,
 1 AS `Spindle_Max`,
 1 AS `Rapid_Variable`,
 1 AS `Rapid_Value`,
 1 AS `Rapid_Min`,
 1 AS `Rapid_Max`,
 1 AS `ActivePallet_Var`,
 1 AS `ActivePallet_Value`,
 1 AS `ActivePallet_StartWith`,
 1 AS `ActivePallet_EndWith`,
 1 AS `ActivePallet_ToMU`,
 1 AS `WarningPressure`,
 1 AS `CriticalPressure`,
 1 AS `WarningTemperature`,
 1 AS `CriticalTemperature`,
 1 AS `MCSDelay`,
 1 AS `DelayScale`,
 1 AS `DelayForCycleOff`,
 1 AS `DelayForCycleOffScale`,
 1 AS `EnableMCS`,
 1 AS `SaveDataRaw`,
 1 AS `COnDuringSetup`,
 1 AS `SaveProdOnly`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `view_machines_settings_values`
--

DROP TABLE IF EXISTS `view_machines_settings_values`;
/*!50001 DROP VIEW IF EXISTS `view_machines_settings_values`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `view_machines_settings_values` AS SELECT 
 1 AS `id`,
 1 AS `machine_name`,
 1 AS `EnetMachineName`,
 1 AS `monitoring_filename`,
 1 AS `EnetDept`,
 1 AS `machine_label`,
 1 AS `Monstate`,
 1 AS `CurrentStatus`,
 1 AS `NextStatus`,
 1 AS `ConnectorId`,
 1 AS `Feed_MIN`,
 1 AS `Feed_MAX`,
 1 AS `Feed_Value`,
 1 AS `Spindle_Min`,
 1 AS `Spindle_Max`,
 1 AS `Spindle_Value`,
 1 AS `Rapid_Min`,
 1 AS `Rapid_Max`,
 1 AS `Rapid_Value`*/;
SET character_set_client = @saved_cs_client;

--
-- Final view structure for view `view_auto_report_config`
--

/*!50001 DROP VIEW IF EXISTS `view_auto_report_config`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `view_auto_report_config` AS select `c`.`Id` AS `id`,`c`.`Task_name` AS `Task_name`,`c`.`Day_` AS `Day_`,`c`.`Time_` AS `Time_`,`c`.`ReportType` AS `ReportType`,`c`.`ReportPeriod` AS `ReportPeriod`,`c`.`ReportTitle` AS `ReportTitle`,`c`.`Output_Folder` AS `Output_Folder`,`c`.`MachineToReport` AS `MachineToReport`,`c`.`MailTo` AS `MailTo`,`c`.`Email_Time` AS `Email_Time`,`s`.`Status` AS `Done`,`c`.`StartDay` AS `StartDay`,`c`.`dayback` AS `dayback`,`c`.`timeback` AS `timeback`,`c`.`CustomMsg` AS `CustomMsg`,`c`.`Scale` AS `Scale`,`c`.`Production` AS `Production`,`c`.`Setup` AS `Setup`,`c`.`OnlySummary` AS `OnlySummary`,`c`.`Enabled` AS `Enabled`,`c`.`Sorting` AS `Sorting`,`c`.`EventMinMinutes` AS `EventMinMinutes`,`c`.`shift_number` AS `shift_number`,`c`.`shift_starttime` AS `shift_starttime`,`c`.`shift_endtime` AS `shift_endtime`,`c`.`short_filename` AS `short_filename`,`c`.`ShowConInSetup` AS `ShowConInSetup`,`c`.`ExcludedStates` AS `ExcludedStates` from (`auto_report_config` `c` left join `auto_report_status` `s` on((`c`.`Id` = `s`.`ReportId`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `view_connectors_values`
--

/*!50001 DROP VIEW IF EXISTS `view_connectors_values`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `view_connectors_values` AS select `a`.`Id` AS `Id`,`a`.`MachineId` AS `MachineId`,`b`.`Feedrate_Min` AS `Feedrate_MIN`,`b`.`Feedrate_Max` AS `Feedrate_MAX`,`c`.`Feedrate_Value` AS `FeedRate_Value`,`c`.`Feedrate_Value` AS `FeedRateOver_Value`,`b`.`Spindle_MIN` AS `Spindle_MIN`,`b`.`Spindle_MAX` AS `Spindle_MAX`,`c`.`Spindle_Value` AS `Spindle_Value`,`c`.`Spindle_Value` AS `SpindleOver_Value`,`b`.`Rapid_MIN` AS `Rapid_MIN`,`b`.`Rapid_MAX` AS `Rapid_MAX`,`c`.`Rapid_Value` AS `Rapid_Value`,`c`.`Rapid_Value` AS `RapidOver_Value` from ((`tbl_csiconnector` `a` left join `tbl_csiothersettings` `b` on((`a`.`Id` = `b`.`ConnectorID`))) join `tbl_csiothersettingsvalues` `c` on((`a`.`Id` = `c`.`ConnectorID`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `view_csiothersettings`
--

/*!50001 DROP VIEW IF EXISTS `view_csiothersettings`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `view_csiothersettings` AS select `s`.`ConnectorID` AS `ConnectorID`,`s`.`Machine_Name` AS `Machine_Name`,`s`.`IP_Address` AS `IP_Address`,`s`.`PartNumber_Variable` AS `PartNumber_Variable`,`v`.`PartNumber_Value` AS `PartNumber_Value`,`s`.`PartNumber_Prefix1` AS `PartNumber_Prefix1`,`s`.`PartNumber_Filter1Start` AS `PartNumber_Filter1Start`,`s`.`PartNumber_Filter1End` AS `PartNumber_Filter1End`,`s`.`PartNumber_Filter2Apply` AS `PartNumber_Filter2Apply`,`s`.`PartNumber_Prefix2` AS `PartNumber_Prefix2`,`s`.`PartNumber_Filter2Start` AS `PartNumber_Filter2Start`,`s`.`PartNumber_Filter2End` AS `PartNumber_Filter2End`,`s`.`PartNumber_Filter3Apply` AS `PartNumber_Filter3Apply`,`s`.`PartNumber_Prefix3` AS `PartNumber_Prefix3`,`s`.`PartNumber_Filter3Start` AS `PartNumber_Filter3Start`,`s`.`PartNumber_Filter3End` AS `PartNumber_Filter3End`,`s`.`ProgramNumber_Variable` AS `ProgramNumber_Variable`,`v`.`ProgramNumber_Value` AS `ProgramNumber_Value`,`s`.`ProgramNumber_FilterStart` AS `ProgramNumber_FilterStart`,`s`.`ProgramNumber_FilterEnd` AS `ProgramNumber_FilterEnd`,`s`.`Operation_Available` AS `Operation_Available`,`v`.`Operation_Value` AS `Operation_Value`,`s`.`Operation_FilterStart` AS `Operation_FilterStart`,`s`.`Operation_FilterEnd` AS `Operation_FilterEnd`,`s`.`PartRequired_Variable` AS `PartRequired_Variable`,`v`.`PartRequired_Value` AS `PartRequired_Value`,`s`.`PartCount_Variable` AS `PartCount_Variable`,`v`.`PartCount_Value` AS `PartCount_Value`,`s`.`Feedrate_Variable` AS `FeedRate_Variable`,`v`.`Feedrate_Value` AS `Feedrate_Value`,`s`.`Feedrate_Min` AS `Feedrate_Min`,`s`.`Feedrate_Max` AS `Feedrate_Max`,`s`.`Spindle_Variable` AS `Spindle_Variable`,`v`.`Spindle_Value` AS `Spindle_Value`,`s`.`Spindle_MIN` AS `Spindle_Min`,`s`.`Spindle_MAX` AS `Spindle_Max`,`s`.`Rapid_Variable` AS `Rapid_Variable`,`v`.`Rapid_Value` AS `Rapid_Value`,`s`.`Rapid_MIN` AS `Rapid_Min`,`s`.`Rapid_MAX` AS `Rapid_Max`,`s`.`ActivePallet_Var` AS `ActivePallet_Var`,`v`.`ActivePallet_Value` AS `ActivePallet_Value`,`s`.`ActivePallet_StartWith` AS `ActivePallet_StartWith`,`s`.`ActivePallet_EndWith` AS `ActivePallet_EndWith`,`s`.`ActivePallet_ToMU` AS `ActivePallet_ToMU`,`s`.`WarningPressure` AS `WarningPressure`,`s`.`CriticalPressure` AS `CriticalPressure`,`s`.`WarningTemperature` AS `WarningTemperature`,`s`.`CriticalTemperature` AS `CriticalTemperature`,`s`.`MCSDelay` AS `MCSDelay`,`s`.`DelayScale` AS `DelayScale`,`s`.`DelayForCycleOff` AS `DelayForCycleOff`,`s`.`DelayForCycleOffScale` AS `DelayForCycleOffScale`,`s`.`EnableMCS` AS `EnableMCS`,`s`.`SaveDataRaw` AS `SaveDataRaw`,`s`.`COnDuringSetup` AS `COnDuringSetup`,`s`.`SaveProdOnly` AS `SaveProdOnly` from (`tbl_csiothersettings` `s` left join `tbl_csiothersettingsvalues` `v` on((`s`.`ConnectorID` = `v`.`ConnectorID`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `view_machines_settings_values`
--

/*!50001 DROP VIEW IF EXISTS `view_machines_settings_values`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `view_machines_settings_values` AS select `m`.`id` AS `id`,`m`.`machine_name` AS `machine_name`,`m`.`EnetMachineName` AS `EnetMachineName`,`m`.`monitoring_filename` AS `monitoring_filename`,`m`.`EnetDept` AS `EnetDept`,`m`.`machine_label` AS `machine_label`,`m`.`Monstate` AS `Monstate`,`s`.`CurrentStatus` AS `CurrentStatus`,`s`.`NextStatus` AS `NextStatus`,`c`.`Id` AS `ConnectorId`,`c`.`Feedrate_MIN` AS `Feed_MIN`,`c`.`Feedrate_MAX` AS `Feed_MAX`,`c`.`FeedRate_Value` AS `Feed_Value`,`c`.`Spindle_MIN` AS `Spindle_Min`,`c`.`Spindle_MAX` AS `Spindle_Max`,`c`.`Spindle_Value` AS `Spindle_Value`,`c`.`Rapid_MIN` AS `Rapid_Min`,`c`.`Rapid_MAX` AS `Rapid_Max`,`c`.`Rapid_Value` AS `Rapid_Value` from ((`tbl_ehub_conf` `m` left join `tbl_machines_settings` `s` on((`m`.`id` = `s`.`MachineId`))) left join `view_connectors_values` `c` on((`m`.`id` = `c`.`MachineId`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2026-02-04 20:40:11
-- MySQL dump 10.13  Distrib 8.0.23, for Win64 (x86_64)
--
-- Host: localhost    Database: monitoring
-- ------------------------------------------------------
-- Server version	8.0.18

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `metrics`
--

DROP TABLE IF EXISTS `metrics`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `metrics` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) COLLATE utf8_unicode_ci NOT NULL,
  `Unit` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Code` varchar(10) COLLATE utf8_unicode_ci NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `metric` (`Name`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `monitoringboards`
--

DROP TABLE IF EXISTS `monitoringboards`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `monitoringboards` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CompanyId` int(11) NOT NULL DEFAULT '0',
  `SerialNumber` varchar(100) NOT NULL,
  `Model` varchar(50) NOT NULL,
  `Mac` varchar(20) NOT NULL,
  `Name` varchar(50) NOT NULL,
  `Manufacturer` varchar(100) NOT NULL,
  `Description` text,
  `IpAddress` varchar(20) DEFAULT NULL,
  `Firmware` varchar(20) NOT NULL,
  `Target` varchar(100) DEFAULT NULL,
  `Tags` text,
  `data` text,
  `CreatedAt` datetime NOT NULL,
  `Enabled` tinyint(4) NOT NULL DEFAULT '1',
  `Deleted` tinyint(4) NOT NULL DEFAULT '0',
  UNIQUE KEY `Id` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `sensorcurrentreadings`
--

DROP TABLE IF EXISTS `sensorcurrentreadings`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `sensorcurrentreadings` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `MachineId` int(11) NOT NULL DEFAULT '0',
  `BoardName` varchar(45) DEFAULT NULL,
  `Timestamp` datetime NOT NULL,
  `CurrentTime` datetime NOT NULL,
  `IsMonitoring` tinyint(4) NOT NULL,
  `IsSensorAvailable` tinyint(4) NOT NULL,
  `IsOverride` tinyint(4) NOT NULL,
  `IsAlarming` tinyint(4) NOT NULL,
  `IsCSD` tinyint(4) NOT NULL,
  `CurrentPallet` varchar(45) DEFAULT NULL,
  `SensorName` varchar(45) DEFAULT NULL,
  `SensorGroup` varchar(45) DEFAULT NULL,
  `Metric` varchar(45) DEFAULT NULL,
  `Value` decimal(10,3) NOT NULL,
  UNIQUE KEY `Id` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `sensorreadings`
--

DROP TABLE IF EXISTS `sensorreadings`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `sensorreadings` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `SensorId` int(11) NOT NULL DEFAULT '0',
  `OccuredAt` datetime NOT NULL,
  `MetricId` int(11) NOT NULL DEFAULT '0',
  `Value` decimal(10,3) NOT NULL,
  `Data` varchar(100) DEFAULT NULL,
  UNIQUE KEY `Id` (`Id`),
  KEY `IX_SensorReadings_MetricId` (`MetricId`),
  KEY `IX_SensorReadings_SensorId` (`SensorId`),
  CONSTRAINT `FK_SensorReadings_Metrics` FOREIGN KEY (`MetricId`) REFERENCES `metrics` (`Id`),
  CONSTRAINT `FK_SensorReadings_Sensors` FOREIGN KEY (`SensorId`) REFERENCES `sensors` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `sensors`
--

DROP TABLE IF EXISTS `sensors`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `sensors` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `BoardId` int(11) NOT NULL DEFAULT '0',
  `Mac` varchar(20) NOT NULL,
  `Name` varchar(50) NOT NULL,
  `Description` text,
  `SerialNumber` varchar(50) NOT NULL,
  `Manufacturer` varchar(50) NOT NULL,
  `Model` varchar(50) DEFAULT NULL,
  `Type` varchar(50) DEFAULT NULL,
  `Group` varchar(50) DEFAULT NULL,
  `Target` varchar(100) DEFAULT NULL,
  `Tags` text,
  `data` text,
  `Deleted` tinyint(4) NOT NULL DEFAULT '0',
  UNIQUE KEY `Id` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2026-02-04 20:40:11
-- MySQL dump 10.13  Distrib 8.0.23, for Win64 (x86_64)
--
-- Host: localhost    Database: csi_machineperf
-- ------------------------------------------------------
-- Server version	8.0.18

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `tbl_as_c45_200`
--

DROP TABLE IF EXISTS `tbl_as_c45_200`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_as_c45_200` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `status` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `time` int(11) DEFAULT NULL,
  `cycletime` int(11) DEFAULT NULL,
  `shift` int(11) DEFAULT NULL,
  `date` datetime DEFAULT NULL,
  `No_of_Head_Pallet` int(11) DEFAULT '0',
  `partnumber` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT '',
  `operator` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT '',
  `comments` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT '',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Id_UNIQUE` (`Id`),
  UNIQUE KEY `time` (`time`,`status`,`No_of_Head_Pallet`)
) ENGINE=InnoDB AUTO_INCREMENT=9681 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_citizen`
--

DROP TABLE IF EXISTS `tbl_citizen`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_citizen` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `status` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `time` int(11) DEFAULT NULL,
  `cycletime` int(11) DEFAULT NULL,
  `shift` int(11) DEFAULT NULL,
  `date` datetime DEFAULT NULL,
  `No_of_Head_Pallet` int(11) DEFAULT '0',
  `partnumber` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT '',
  `operator` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT '',
  `comments` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT '',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Id_UNIQUE` (`Id`),
  UNIQUE KEY `time` (`time`,`status`,`No_of_Head_Pallet`)
) ENGINE=InnoDB AUTO_INCREMENT=9681 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_conex_mt`
--

DROP TABLE IF EXISTS `tbl_conex_mt`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_conex_mt` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `status` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `time` int(11) DEFAULT NULL,
  `cycletime` int(11) DEFAULT NULL,
  `shift` int(11) DEFAULT NULL,
  `date` datetime DEFAULT NULL,
  `No_of_Head_Pallet` int(11) DEFAULT '0',
  `partnumber` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT '',
  `operator` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT '',
  `comments` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT '',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Id_UNIQUE` (`Id`),
  UNIQUE KEY `time` (`time`,`status`,`No_of_Head_Pallet`)
) ENGINE=InnoDB AUTO_INCREMENT=9681 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_cs_c45_300iil`
--

DROP TABLE IF EXISTS `tbl_cs_c45_300iil`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_cs_c45_300iil` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `status` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `time` int(11) DEFAULT NULL,
  `cycletime` int(11) DEFAULT NULL,
  `shift` int(11) DEFAULT NULL,
  `date` datetime DEFAULT NULL,
  `No_of_Head_Pallet` int(11) DEFAULT '0',
  `partnumber` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT '',
  `operator` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT '',
  `comments` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT '',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Id_UNIQUE` (`Id`),
  UNIQUE KEY `time` (`time`,`status`,`No_of_Head_Pallet`)
) ENGINE=InnoDB AUTO_INCREMENT=9681 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_fanuc_c32_enet`
--

DROP TABLE IF EXISTS `tbl_fanuc_c32_enet`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_fanuc_c32_enet` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `status` varchar(45) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `time` int(11) DEFAULT NULL,
  `cycletime` int(11) DEFAULT NULL,
  `shift` int(11) DEFAULT NULL,
  `date` datetime DEFAULT NULL,
  `No_of_Head_Pallet` int(11) DEFAULT '0',
  `partnumber` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT '',
  `operator` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT '',
  `comments` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT '',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Id_UNIQUE` (`Id`),
  UNIQUE KEY `time` (`time`,`status`,`No_of_Head_Pallet`)
) ENGINE=InnoDB AUTO_INCREMENT=9681 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_fanuc_c45_cnc`
--

DROP TABLE IF EXISTS `tbl_fanuc_c45_cnc`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_fanuc_c45_cnc` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `status` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `time` int(11) DEFAULT NULL,
  `cycletime` int(11) DEFAULT NULL,
  `shift` int(11) DEFAULT NULL,
  `date` datetime DEFAULT NULL,
  `No_of_Head_Pallet` int(11) DEFAULT '0',
  `partnumber` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT '',
  `operator` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT '',
  `comments` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT '',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Id_UNIQUE` (`Id`),
  UNIQUE KEY `time` (`time`,`status`,`No_of_Head_Pallet`)
) ENGINE=InnoDB AUTO_INCREMENT=9681 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_fanuc_c45_serial`
--

DROP TABLE IF EXISTS `tbl_fanuc_c45_serial`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_fanuc_c45_serial` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `status` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `time` int(11) DEFAULT NULL,
  `cycletime` int(11) DEFAULT NULL,
  `shift` int(11) DEFAULT NULL,
  `date` datetime DEFAULT NULL,
  `No_of_Head_Pallet` int(11) DEFAULT '0',
  `partnumber` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT '',
  `operator` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT '',
  `comments` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT '',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Id_UNIQUE` (`Id`),
  UNIQUE KEY `time` (`time`,`status`,`No_of_Head_Pallet`)
) ENGINE=InnoDB AUTO_INCREMENT=9681 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_fanuc_c45_sim`
--

DROP TABLE IF EXISTS `tbl_fanuc_c45_sim`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_fanuc_c45_sim` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `status` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `time` int(11) DEFAULT NULL,
  `cycletime` int(11) DEFAULT NULL,
  `shift` int(11) DEFAULT NULL,
  `date` datetime DEFAULT NULL,
  `No_of_Head_Pallet` int(11) DEFAULT '0',
  `partnumber` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT '',
  `operator` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT '',
  `comments` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT '',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Id_UNIQUE` (`Id`),
  UNIQUE KEY `time` (`time`,`status`,`No_of_Head_Pallet`)
) ENGINE=InnoDB AUTO_INCREMENT=2856 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_fanuc_test`
--

DROP TABLE IF EXISTS `tbl_fanuc_test`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_fanuc_test` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `status` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `time` int(11) DEFAULT NULL,
  `cycletime` int(11) DEFAULT NULL,
  `shift` int(11) DEFAULT NULL,
  `date` datetime DEFAULT NULL,
  `No_of_Head_Pallet` int(11) DEFAULT '0',
  `partnumber` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT '',
  `operator` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT '',
  `comments` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT '',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Id_UNIQUE` (`Id`),
  UNIQUE KEY `time` (`time`,`status`,`No_of_Head_Pallet`)
) ENGINE=InnoDB AUTO_INCREMENT=9681 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_focas`
--

DROP TABLE IF EXISTS `tbl_focas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_focas` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `status` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `time` int(11) DEFAULT NULL,
  `cycletime` int(11) DEFAULT NULL,
  `shift` int(11) DEFAULT NULL,
  `date` datetime DEFAULT NULL,
  `No_of_Head_Pallet` int(11) DEFAULT '0',
  `partnumber` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT '',
  `operator` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT '',
  `comments` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT '',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Id_UNIQUE` (`Id`),
  UNIQUE KEY `time` (`time`,`status`,`No_of_Head_Pallet`)
) ENGINE=InnoDB AUTO_INCREMENT=9681 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_haas_c45_sim`
--

DROP TABLE IF EXISTS `tbl_haas_c45_sim`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_haas_c45_sim` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `status` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `time` int(11) DEFAULT NULL,
  `cycletime` int(11) DEFAULT NULL,
  `shift` int(11) DEFAULT NULL,
  `date` datetime DEFAULT NULL,
  `No_of_Head_Pallet` int(11) DEFAULT '0',
  `partnumber` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT '',
  `operator` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT '',
  `comments` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT '',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Id_UNIQUE` (`Id`),
  UNIQUE KEY `time` (`time`,`status`,`No_of_Head_Pallet`)
) ENGINE=InnoDB AUTO_INCREMENT=9681 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_mfms10_c45_mc1`
--

DROP TABLE IF EXISTS `tbl_mfms10_c45_mc1`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_mfms10_c45_mc1` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `status` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `time` int(11) DEFAULT NULL,
  `cycletime` int(11) DEFAULT NULL,
  `shift` int(11) DEFAULT NULL,
  `date` datetime DEFAULT NULL,
  `No_of_Head_Pallet` int(11) DEFAULT '0',
  `partnumber` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT '',
  `operator` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT '',
  `comments` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT '',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Id_UNIQUE` (`Id`),
  UNIQUE KEY `time` (`time`,`status`,`No_of_Head_Pallet`)
) ENGINE=InnoDB AUTO_INCREMENT=9681 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_mfms10_c45_mc3`
--

DROP TABLE IF EXISTS `tbl_mfms10_c45_mc3`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_mfms10_c45_mc3` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `status` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `time` int(11) DEFAULT NULL,
  `cycletime` int(11) DEFAULT NULL,
  `shift` int(11) DEFAULT NULL,
  `date` datetime DEFAULT NULL,
  `No_of_Head_Pallet` int(11) DEFAULT '0',
  `partnumber` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT '',
  `operator` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT '',
  `comments` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT '',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Id_UNIQUE` (`Id`),
  UNIQUE KEY `time` (`time`,`status`,`No_of_Head_Pallet`)
) ENGINE=InnoDB AUTO_INCREMENT=9681 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_mx_c45_330`
--

DROP TABLE IF EXISTS `tbl_mx_c45_330`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_mx_c45_330` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `status` varchar(45) CHARACTER SET utf8 COLLATE utf8_bin DEFAULT NULL,
  `time` int(11) DEFAULT NULL,
  `cycletime` int(11) DEFAULT NULL,
  `shift` int(11) DEFAULT NULL,
  `date` datetime DEFAULT NULL,
  `No_of_Head_Pallet` int(11) DEFAULT '0',
  `partnumber` varchar(255) CHARACTER SET utf8 COLLATE utf8_bin DEFAULT '',
  `operator` varchar(255) CHARACTER SET utf8 COLLATE utf8_bin DEFAULT '',
  `comments` varchar(255) CHARACTER SET utf8 COLLATE utf8_bin DEFAULT '',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Id_UNIQUE` (`Id`),
  UNIQUE KEY `time` (`time`,`status`,`No_of_Head_Pallet`)
) ENGINE=InnoDB AUTO_INCREMENT=9681 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_mx_c45_520`
--

DROP TABLE IF EXISTS `tbl_mx_c45_520`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_mx_c45_520` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `status` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `time` int(11) DEFAULT NULL,
  `cycletime` int(11) DEFAULT NULL,
  `shift` int(11) DEFAULT NULL,
  `date` datetime DEFAULT NULL,
  `No_of_Head_Pallet` int(11) DEFAULT '0',
  `partnumber` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT '',
  `operator` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT '',
  `comments` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT '',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Id_UNIQUE` (`Id`),
  UNIQUE KEY `time` (`time`,`status`,`No_of_Head_Pallet`)
) ENGINE=InnoDB AUTO_INCREMENT=9681 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_perf`
--

DROP TABLE IF EXISTS `tbl_perf`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_perf` (
  `machinename_` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `weekly_` mediumtext COLLATE utf8_unicode_ci,
  `monthly_` mediumtext COLLATE utf8_unicode_ci,
  PRIMARY KEY (`machinename_`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_scaner`
--

DROP TABLE IF EXISTS `tbl_scaner`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_scaner` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `status` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `time` int(11) DEFAULT NULL,
  `cycletime` int(11) DEFAULT NULL,
  `shift` int(11) DEFAULT NULL,
  `date` datetime DEFAULT NULL,
  `No_of_Head_Pallet` int(11) DEFAULT '0',
  `partnumber` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT '',
  `operator` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT '',
  `comments` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT '',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Id_UNIQUE` (`Id`),
  UNIQUE KEY `time` (`time`,`status`,`No_of_Head_Pallet`)
) ENGINE=InnoDB AUTO_INCREMENT=9681 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_simulator`
--

DROP TABLE IF EXISTS `tbl_simulator`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_simulator` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `status` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `time` int(11) DEFAULT NULL,
  `cycletime` int(11) DEFAULT NULL,
  `shift` int(11) DEFAULT NULL,
  `date` datetime DEFAULT NULL,
  `No_of_Head_Pallet` int(11) DEFAULT '0',
  `partnumber` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT '',
  `operator` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT '',
  `comments` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT '',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Id_UNIQUE` (`Id`),
  UNIQUE KEY `time` (`time`,`status`,`No_of_Head_Pallet`)
) ENGINE=InnoDB AUTO_INCREMENT=9681 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_wt_c45_300`
--

DROP TABLE IF EXISTS `tbl_wt_c45_300`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_wt_c45_300` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `status` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `time` int(11) DEFAULT NULL,
  `cycletime` int(11) DEFAULT NULL,
  `shift` int(11) DEFAULT NULL,
  `date` datetime DEFAULT NULL,
  `No_of_Head_Pallet` int(11) DEFAULT '0',
  `partnumber` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT '',
  `operator` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT '',
  `comments` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT '',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Id_UNIQUE` (`Id`),
  UNIQUE KEY `time` (`time`,`status`,`No_of_Head_Pallet`)
) ENGINE=InnoDB AUTO_INCREMENT=9681 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2026-02-04 20:40:11
