// -----------------------------------------------------------------------------------
// Copyright (C) 2015 Kieback&Peter GmbH & Co KG All Rights Reserved
// 
// Kieback&Peter Confidential Proprietary Information
// 
// This Software is confidential and proprietary to Kieback&Peter. 
// The reproduction or disclosure in whole or part to anyone outside of Kieback&Peter
// without the written approval of an officer of Kieback&Peter GmbH & Co.KG,
// under a Non-Disclosure Agreement, or to any employee who has not previously
// obtained a written authorization for access from the individual responsible
// for the software will have a significant detrimental effect on
// Kieback&Peter and is expressly PROHIBITED.
// -----------------------------------------------------------------------------------

namespace ConnectTools.BACnet
{
    public class BaCnetEnums
    {
        public const int BacnetMaxObject = 0x3FF;

        public const int BacnetMaxInstance = 0x3FFFFF;
        public const int BacnetInstanceBits = 22;

        public const int BacnetProtocolVersion = 0x01;

        public enum BacnetPropertyId
        {
            PropAckedTransitions = 0,
            PropAckRequired = 1,
            PropAction = 2,
            PropActionText = 3,
            PropActiveText = 4,
            PropActiveVtSessions = 5,
            PropAlarmValue = 6,
            PropAlarmValues = 7,
            PropAll = 8,
            PropAllWritesSuccessful = 9,
            PropApduSegmentTimeout = 10,
            PropApduTimeout = 11,
            PropApplicationSoftwareVersion = 12,
            PropArchive = 13,
            PropBias = 14,
            PropChangeOfStateCount = 15,
            PropChangeOfStateTime = 16,
            PropNotificationClass = 17,
            PropBlank1 = 18,
            PropControlledVariableReference = 19,
            PropControlledVariableUnits = 20,
            PropControlledVariableValue = 21,
            PropCovIncrement = 22,
            PropDateList = 23,
            PropDaylightSavingsStatus = 24,
            PropDeadband = 25,
            PropDerivativeConstant = 26,
            PropDerivativeConstantUnits = 27,
            PropDescription = 28,
            PropDescriptionOfHalt = 29,
            PropDeviceAddressBinding = 30,
            PropDeviceType = 31,
            PropEffectivePeriod = 32,
            PropElapsedActiveTime = 33,
            PropErrorLimit = 34,
            PropEventEnable = 35,
            PropEventState = 36,
            PropEventType = 37,
            PropExceptionSchedule = 38,
            PropFaultValues = 39,
            PropFeedbackValue = 40,
            PropFileAccessMethod = 41,
            PropFileSize = 42,
            PropFileType = 43,
            PropFirmwareRevision = 44,
            PropHighLimit = 45,
            PropInactiveText = 46,
            PropInProcess = 47,
            PropInstanceOf = 48,
            PropIntegralConstant = 49,
            PropIntegralConstantUnits = 50,
            PropIssueConfirmedNotifications = 51,
            PropLimitEnable = 52,
            PropListOfGroupMembers = 53,
            PropListOfObjectPropertyReferences = 54,
            PropListOfSessionKeys = 55,
            PropLocalDate = 56,
            PropLocalTime = 57,
            PropLocation = 58,
            PropLowLimit = 59,
            PropManipulatedVariableReference = 60,
            PropMaximumOutput = 61,
            PropMaxApduLengthAccepted = 62,
            PropMaxInfoFrames = 63,
            PropMaxMaster = 64,
            PropMaxPresValue = 65,
            PropMinimumOffTime = 66,
            PropMinimumOnTime = 67,
            PropMinimumOutput = 68,
            PropMinPresValue = 69,
            PropModelName = 70,
            PropModificationDate = 71,
            PropNotifyType = 72,
            PropNumberOfApduRetries = 73,
            PropNumberOfStates = 74,
            PropObjectIdentifier = 75,
            PropObjectList = 76,
            PropObjectName = 77,
            PropObjectPropertyReference = 78,
            PropObjectType = 79,
            PropOptional = 80,
            PropOutOfService = 81,
            PropOutputUnits = 82,
            PropEventParameters = 83,
            PropPolarity = 84,
            PropPresentValue = 85,
            PropPriority = 86,
            PropPriorityArray = 87,
            PropPriorityForWriting = 88,
            PropProcessIdentifier = 89,
            PropProgramChange = 90,
            PropProgramLocation = 91,
            PropProgramState = 92,
            PropProportionalConstant = 93,
            PropProportionalConstantUnits = 94,
            PropProtocolConformanceClass = 95,       /* deleted in version 1 revision 2 */
            PropProtocolObjectTypesSupported = 96,
            PropProtocolServicesSupported = 97,
            PropProtocolVersion = 98,
            PropReadOnly = 99,
            PropReasonForHalt = 100,
            PropRecipient = 101,
            PropRecipientList = 102,
            PropReliability = 103,
            PropRelinquishDefault = 104,
            PropRequired = 105,
            PropResolution = 106,
            PropSegmentationSupported = 107,
            PropSetpoint = 108,
            PropSetpointReference = 109,
            PropStateText = 110,
            PropStatusFlags = 111,
            PropSystemStatus = 112,
            PropTimeDelay = 113,
            PropTimeOfActiveTimeReset = 114,
            PropTimeOfStateCountReset = 115,
            PropTimeSynchronizationRecipients = 116,
            PropUnits = 117,
            PropUpdateInterval = 118,
            PropUtcOffset = 119,
            PropVendorIdentifier = 120,
            PropVendorName = 121,
            PropVtClassesSupported = 122,
            PropWeeklySchedule = 123,
            PropAttemptedSamples = 124,
            PropAverageValue = 125,
            PropBufferSize = 126,
            PropClientCovIncrement = 127,
            PropCovResubscriptionInterval = 128,
            PropCurrentNotifyTime = 129,
            PropEventTimeStamps = 130,
            PropLogBuffer = 131,
            PropLogDeviceObject = 132,
            /* The enable property is renamed from log-enable in
               Addendum b to ANSI/ASHRAE 135-2004(135b-2) */
            PropEnable = 133,
            PropLogInterval = 134,
            PropMaximumValue = 135,
            PropMinimumValue = 136,
            PropNotificationThreshold = 137,
            PropPreviousNotifyTime = 138,
            PropProtocolRevision = 139,
            PropRecordsSinceNotification = 140,
            PropRecordCount = 141,
            PropStartTime = 142,
            PropStopTime = 143,
            PropStopWhenFull = 144,
            PropTotalRecordCount = 145,
            PropValidSamples = 146,
            PropWindowInterval = 147,
            PropWindowSamples = 148,
            PropMaximumValueTimestamp = 149,
            PropMinimumValueTimestamp = 150,
            PropVarianceValue = 151,
            PropActiveCovSubscriptions = 152,
            PropBackupFailureTimeout = 153,
            PropConfigurationFiles = 154,
            PropDatabaseRevision = 155,
            PropDirectReading = 156,
            PropLastRestoreTime = 157,
            PropMaintenanceRequired = 158,
            PropMemberOf = 159,
            PropMode = 160,
            PropOperationExpected = 161,
            PropSetting = 162,
            PropSilenced = 163,
            PropTrackingValue = 164,
            PropZoneMembers = 165,
            PropLifeSafetyAlarmValues = 166,
            PropMaxSegmentsAccepted = 167,
            PropProfileName = 168,
            PropAutoSlaveDiscovery = 169,
            PropManualSlaveAddressBinding = 170,
            PropSlaveAddressBinding = 171,
            PropSlaveProxyEnable = 172,
            PropLastNotifyTime = 173,
            PropScheduleDefault = 174,
            PropAcceptedModes = 175,
            PropAdjustValue = 176,
            PropCount = 177,
            PropCountBeforeChange = 178,
            PropCountChangeTime = 179,
            PropCovPeriod = 180,
            PropInputReference = 181,
            PropLimitMonitoringInterval = 182,
            PropLoggingDevice = 183,
            PropLoggingRecord = 184,
            PropPrescale = 185,
            PropPulseRate = 186,
            PropScale = 187,
            PropScaleFactor = 188,
            PropUpdateTime = 189,
            PropValueBeforeChange = 190,
            PropValueSet = 191,
            PropValueChangeTime = 192,
            /* enumerations 193-206 are new */
            PropAlignIntervals = 193,
            PropGroupMemberNames = 194,
            PropIntervalOffset = 195,
            PropLastRestartReason = 196,
            PropLoggingType = 197,
            PropMemberStatusFlags = 198,
            PropNotificationPeriod = 199,
            PropPreviousNotifyRecord = 200,
            PropRequestedUpdateInterval = 201,
            PropRestartNotificationRecipients = 202,
            PropTimeOfDeviceRestart = 203,
            PropTimeSynchronizationInterval = 204,
            PropTrigger = 205,
            PropUtcTimeSynchronizationRecipients = 206,
            /* enumerations 207-211 are used in Addendum d to ANSI/ASHRAE 135-2004 */
            PropNodeSubtype = 207,
            PropNodeType = 208,
            PropStructuredObjectList = 209,
            PropSubordinateAnnotations = 210,
            PropSubordinateList = 211,
            /* enumerations 212-225 are used in Addendum e to ANSI/ASHRAE 135-2004 */
            PropActualShedLevel = 212,
            PropDutyWindow = 213,
            PropExpectedShedLevel = 214,
            PropFullDutyBaseline = 215,
            /* enumerations 216-217 are used in Addendum i to ANSI/ASHRAE 135-2004 */
            PropBlinkPriorityThreshold = 216,
            PropBlinkTime = 217,
            /* enumerations 212-225 are used in Addendum e to ANSI/ASHRAE 135-2004 */
            PropRequestedShedLevel = 218,
            PropShedDuration = 219,
            PropShedLevelDescriptions = 220,
            PropShedLevels = 221,
            PropStateDescription = 222,
            /* enumerations 223-225 are used in Addendum i to ANSI/ASHRAE 135-2004 */
            PropFadeTime = 223,
            PropLightingCommand = 224,
            PropLightingCommandPriority = 225,
            /* enumerations 226-235 are used in Addendum f to ANSI/ASHRAE 135-2004 */
            /* enumerations 236-243 are used in Addendum i to ANSI/ASHRAE 135-2004 */
            PropOffDelay = 236,
            PropOnDelay = 237,
            PropPower = 238,
            PropPowerOnValue = 239,
            PropProgressValue = 240,
            PropRampRate = 241,
            PropStepIncrement = 242,
            PropSystemFailureValue = 243,
            /* The special property identifiers all, optional, and required  */
            /* are reserved for use in the ReadPropertyConditional and */
            /* ReadPropertyMultiple services or services not defined in this standard. */
            /* Enumerated values 0-511 are reserved for definition by ASHRAE.  */
            /* Enumerated values 512-4194303 may be used by others subject to the  */
            /* procedures and constraints described in Clause 23.  */
            PropBaudRate = 9600,
            PropSerialNumber = 9701
        }
        public const int MaxBacnetPropertyId = 4194303;

        public enum BacnetAction
        {
            ActionDirect = 0,
            ActionReverse = 1
        }

        public enum BacnetBinaryPv
        {
            MinBinaryPv = 0,  /* for validating incoming values */
            BinaryInactive = 0,
            BinaryActive = 1,
            MaxBinaryPv = 1,  /* for validating incoming values */
            BinaryNull = 2     /* our homemade way of storing this info */
        }

        public enum BacnetActionValueType
        {
            ActionBinaryPv,
            ActionUnsigned,
            ActionFloat
        }

        public enum BacnetEventState
        {
            EventStateNormal = 0,
            EventStateFault = 1,
            EventStateOffnormal = 2,
            EventStateHighLimit = 3,
            EventStateLowLimit = 4
        }

        public enum BacnetDeviceStatus
        {
            StatusOperational = 0,
            StatusOperationalReadOnly = 1,
            StatusDownloadRequired = 2,
            StatusDownloadInProgress = 3,
            StatusNonOperational = 4,
            MaxDeviceStatus = 5
        }

        public enum BacnetEngineeringUnits
        {
            /* Acceleration */
            UnitsMetersPerSecondPerSecond = 166,
            /* Area */
            UnitsSquareMeters = 0,
            UnitsSquareCentimeters = 116,
            UnitsSquareFeet = 1,
            UnitsSquareInches = 115,
            /* Currency */
            UnitsCurrency1 = 105,
            UnitsCurrency2 = 106,
            UnitsCurrency3 = 107,
            UnitsCurrency4 = 108,
            UnitsCurrency5 = 109,
            UnitsCurrency6 = 110,
            UnitsCurrency7 = 111,
            UnitsCurrency8 = 112,
            UnitsCurrency9 = 113,
            UnitsCurrency10 = 114,
            /* Electrical */
            UnitsMilliamperes = 2,
            UnitsAmperes = 3,
            UnitsAmperesPerMeter = 167,
            UnitsAmperesPerSquareMeter = 168,
            UnitsAmpereSquareMeters = 169,
            UnitsFarads = 170,
            UnitsHenrys = 171,
            UnitsOhms = 4,
            UnitsOhmMeters = 172,
            UnitsMilliohms = 145,
            UnitsKilohms = 122,
            UnitsMegohms = 123,
            UnitsSiemens = 173,        /* 1 mho equals 1 siemens */
            UnitsSiemensPerMeter = 174,
            UnitsTeslas = 175,
            UnitsVolts = 5,
            UnitsMillivolts = 124,
            UnitsKilovolts = 6,
            UnitsMegavolts = 7,
            UnitsVoltAmperes = 8,
            UnitsKilovoltAmperes = 9,
            UnitsMegavoltAmperes = 10,
            UnitsVoltAmperesReactive = 11,
            UnitsKilovoltAmperesReactive = 12,
            UnitsMegavoltAmperesReactive = 13,
            UnitsVoltsPerDegreeKelvin = 176,
            UnitsVoltsPerMeter = 177,
            UnitsDegreesPhase = 14,
            UnitsPowerFactor = 15,
            UnitsWebers = 178,
            /* Energy */
            UnitsJoules = 16,
            UnitsKilojoules = 17,
            UnitsKilojoulesPerKilogram = 125,
            UnitsMegajoules = 126,
            UnitsWattHours = 18,
            UnitsKilowattHours = 19,
            UnitsMegawattHours = 146,
            UnitsBtus = 20,
            UnitsKiloBtus = 147,
            UnitsMegaBtus = 148,
            UnitsTherms = 21,
            UnitsTonHours = 22,
            /* Enthalpy */
            UnitsJoulesPerKilogramDryAir = 23,
            UnitsKilojoulesPerKilogramDryAir = 149,
            UnitsMegajoulesPerKilogramDryAir = 150,
            UnitsBtusPerPoundDryAir = 24,
            UnitsBtusPerPound = 117,
            /* Entropy */
            UnitsJoulesPerDegreeKelvin = 127,
            UnitsKilojoulesPerDegreeKelvin = 151,
            UnitsMegajoulesPerDegreeKelvin = 152,
            UnitsJoulesPerKilogramDegreeKelvin = 128,
            /* Force */
            UnitsNewton = 153,
            /* Frequency */
            UnitsCyclesPerHour = 25,
            UnitsCyclesPerMinute = 26,
            UnitsHertz = 27,
            UnitsKilohertz = 129,
            UnitsMegahertz = 130,
            UnitsPerHour = 131,
            /* Humidity */
            UnitsGramsOfWaterPerKilogramDryAir = 28,
            UnitsPercentRelativeHumidity = 29,
            /* Length */
            UnitsMillimeters = 30,
            UnitsCentimeters = 118,
            UnitsMeters = 31,
            UnitsInches = 32,
            UnitsFeet = 33,
            /* Light */
            UnitsCandelas = 179,
            UnitsCandelasPerSquareMeter = 180,
            UnitsWattsPerSquareFoot = 34,
            UnitsWattsPerSquareMeter = 35,
            UnitsLumens = 36,
            UnitsLuxes = 37,
            UnitsFootCandles = 38,
            /* Mass */
            UnitsKilograms = 39,
            UnitsPoundsMass = 40,
            UnitsTons = 41,
            /* Mass Flow */
            UnitsGramsPerSecond = 154,
            UnitsGramsPerMinute = 155,
            UnitsKilogramsPerSecond = 42,
            UnitsKilogramsPerMinute = 43,
            UnitsKilogramsPerHour = 44,
            UnitsPoundsMassPerSecond = 119,
            UnitsPoundsMassPerMinute = 45,
            UnitsPoundsMassPerHour = 46,
            UnitsTonsPerHour = 156,
            /* Power */
            UnitsMilliwatts = 132,
            UnitsWatts = 47,
            UnitsKilowatts = 48,
            UnitsMegawatts = 49,
            UnitsBtusPerHour = 50,
            UnitsKiloBtusPerHour = 157,
            UnitsHorsepower = 51,
            UnitsTonsRefrigeration = 52,
            /* Pressure */
            UnitsPascals = 53,
            UnitsHectopascals = 133,
            UnitsKilopascals = 54,
            UnitsMillibars = 134,
            UnitsBars = 55,
            UnitsPoundsForcePerSquareInch = 56,
            UnitsCentimetersOfWater = 57,
            UnitsInchesOfWater = 58,
            UnitsMillimetersOfMercury = 59,
            UnitsCentimetersOfMercury = 60,
            UnitsInchesOfMercury = 61,
            /* Temperature */
            UnitsDegreesCelsius = 62,
            UnitsDegreesKelvin = 63,
            UnitsDegreesKelvinPerHour = 181,
            UnitsDegreesKelvinPerMinute = 182,
            UnitsDegreesFahrenheit = 64,
            UnitsDegreeDaysCelsius = 65,
            UnitsDegreeDaysFahrenheit = 66,
            UnitsDeltaDegreesFahrenheit = 120,
            UnitsDeltaDegreesKelvin = 121,
            /* Time */
            UnitsYears = 67,
            UnitsMonths = 68,
            UnitsWeeks = 69,
            UnitsDays = 70,
            UnitsHours = 71,
            UnitsMinutes = 72,
            UnitsSeconds = 73,
            UnitsHundredthsSeconds = 158,
            UnitsMilliseconds = 159,
            /* Torque */
            UnitsNewtonMeters = 160,
            /* Velocity */
            UnitsMillimetersPerSecond = 161,
            UnitsMillimetersPerMinute = 162,
            UnitsMetersPerSecond = 74,
            UnitsMetersPerMinute = 163,
            UnitsMetersPerHour = 164,
            UnitsKilometersPerHour = 75,
            UnitsFeetPerSecond = 76,
            UnitsFeetPerMinute = 77,
            UnitsMilesPerHour = 78,
            /* Volume */
            UnitsCubicFeet = 79,
            UnitsCubicMeters = 80,
            UnitsImperialGallons = 81,
            UnitsLiters = 82,
            UnitsUsGallons = 83,
            /* Volumetric Flow */
            UnitsCubicFeetPerSecond = 142,
            UnitsCubicFeetPerMinute = 84,
            UnitsCubicMetersPerSecond = 85,
            UnitsCubicMetersPerMinute = 165,
            UnitsCubicMetersPerHour = 135,
            UnitsImperialGallonsPerMinute = 86,
            UnitsLitersPerSecond = 87,
            UnitsLitersPerMinute = 88,
            UnitsLitersPerHour = 136,
            UnitsUsGallonsPerMinute = 89,
            /* Other */
            UnitsDegreesAngular = 90,
            UnitsDegreesCelsiusPerHour = 91,
            UnitsDegreesCelsiusPerMinute = 92,
            UnitsDegreesFahrenheitPerHour = 93,
            UnitsDegreesFahrenheitPerMinute = 94,
            UnitsJouleSeconds = 183,
            UnitsKilogramsPerCubicMeter = 186,
            UnitsKwHoursPerSquareMeter = 137,
            UnitsKwHoursPerSquareFoot = 138,
            UnitsMegajoulesPerSquareMeter = 139,
            UnitsMegajoulesPerSquareFoot = 140,
            UnitsNoUnits = 95,
            UnitsNewtonSeconds = 187,
            UnitsNewtonsPerMeter = 188,
            UnitsPartsPerMillion = 96,
            UnitsPartsPerBillion = 97,
            UnitsPercent = 98,
            UnitsPercentObscurationPerFoot = 143,
            UnitsPercentObscurationPerMeter = 144,
            UnitsPercentPerSecond = 99,
            UnitsPerMinute = 100,
            UnitsPerSecond = 101,
            UnitsPsiPerDegreeFahrenheit = 102,
            UnitsRadians = 103,
            UnitsRadiansPerSecond = 184,
            UnitsRevolutionsPerMinute = 104,
            UnitsSquareMetersPerNewton = 185,
            UnitsWattsPerMeterPerDegreeKelvin = 189,
            UnitsWattsPerSquareMeterDegreeKelvin = 141
            /* Enumerated values 0-255 are reserved for definition by ASHRAE. */
            /* Enumerated values 256-65535 may be used by others subject to */
            /* the procedures and constraints described in Clause 23. */
            /* The last enumeration used in this version is 189. */
        }

        public enum BacnetPolarity
        {
            PolarityNormal = 0,
            PolarityReverse = 1
        }

        public enum BacnetProgramRequest
        {
            ProgramRequestReady = 0,
            ProgramRequestLoad = 1,
            ProgramRequestRun = 2,
            ProgramRequestHalt = 3,
            ProgramRequestRestart = 4,
            ProgramRequestUnload = 5
        }

        public enum BacnetProgramState
        {
            ProgramStateIdle = 0,
            ProgramStateLoading = 1,
            ProgramStateRunning = 2,
            ProgramStateWaiting = 3,
            ProgramStateHalted = 4,
            ProgramStateUnloading = 5
        }

        public enum BacnetProgramError
        {
            ProgramErrorNormal = 0,
            ProgramErrorLoadFailed = 1,
            ProgramErrorInternal = 2,
            ProgramErrorProgram = 3,
            ProgramErrorOther = 4
            /* Enumerated values 0-63 are reserved for definition by ASHRAE.  */
            /* Enumerated values 64-65535 may be used by others subject to  */
            /* the procedures and constraints described in Clause 23. */
        }

        public enum BacnetReliability
        {
            ReliabilityNoFaultDetected = 0,
            ReliabilityNoSensor = 1,
            ReliabilityOverRange = 2,
            ReliabilityUnderRange = 3,
            ReliabilityOpenLoop = 4,
            ReliabilityShortedLoop = 5,
            ReliabilityNoOutput = 6,
            ReliabilityUnreliableOther = 7,
            ReliabilityProcessError = 8,
            ReliabilityMultiStateFault = 9,
            ReliabilityConfigurationError = 10,
            ReliabilityCommunicationFailure = 12,
            ReliabilityTripped = 13
            /* Enumerated values 0-63 are reserved for definition by ASHRAE.  */
            /* Enumerated values 64-65535 may be used by others subject to  */
            /* the procedures and constraints described in Clause 23. */
        }

        public enum BacnetEventType
        {
            EventChangeOfBitstring = 0,
            EventChangeOfState = 1,
            EventChangeOfValue = 2,
            EventCommandFailure = 3,
            EventFloatingLimit = 4,
            EventOutOfRange = 5,
            /*  complex-event-type        (6), -- see comment below */
            /*  event-buffer-ready   (7), -- context tag 7 is deprecated */
            EventChangeOfLifeSafety = 8,
            EventExtended = 9,
            EventBufferReady = 10,
            EventUnsignedRange = 11
            /* Enumerated values 0-63 are reserved for definition by ASHRAE.  */
            /* Enumerated values 64-65535 may be used by others subject to  */
            /* the procedures and constraints described in Clause 23.  */
            /* It is expected that these enumerated values will correspond to  */
            /* the use of the complex-event-type CHOICE [6] of the  */
            /* BACnetNotificationParameters production. */
            /* The last enumeration used in this version is 11. */
        }

        public enum BacnetFileAccessMethod
        {
            FileRecordAccess = 0,
            FileStreamAccess = 1,
            FileRecordAndStreamAccess = 2
        }

        public enum BacnetLifeSafetyMode
        {
            MinLifeSafetyMode = 0,
            LifeSafetyModeOff = 0,
            LifeSafetyModeOn = 1,
            LifeSafetyModeTest = 2,
            LifeSafetyModeManned = 3,
            LifeSafetyModeUnmanned = 4,
            LifeSafetyModeArmed = 5,
            LifeSafetyModeDisarmed = 6,
            LifeSafetyModePrearmed = 7,
            LifeSafetyModeSlow = 8,
            LifeSafetyModeFast = 9,
            LifeSafetyModeDisconnected = 10,
            LifeSafetyModeEnabled = 11,
            LifeSafetyModeDisabled = 12,
            LifeSafetyModeAutomaticReleaseDisabled = 13,
            LifeSafetyModeDefault = 14,
            MaxLifeSafetyMode = 14
            /* Enumerated values 0-255 are reserved for definition by ASHRAE.  */
            /* Enumerated values 256-65535 may be used by others subject to  */
            /* procedures and constraints described in Clause 23. */
        }

        public enum BacnetLifeSafetyOperation
        {
            LifeSafetyOpNone = 0,
            LifeSafetyOpSilence = 1,
            LifeSafetyOpSilenceAudible = 2,
            LifeSafetyOpSilenceVisual = 3,
            LifeSafetyOpReset = 4,
            LifeSafetyOpResetAlarm = 5,
            LifeSafetyOpResetFault = 6,
            LifeSafetyOpUnsilence = 7,
            LifeSafetyOpUnsilenceAudible = 8,
            LifeSafetyOpUnsilenceVisual = 9
            /* Enumerated values 0-63 are reserved for definition by ASHRAE.  */
            /* Enumerated values 64-65535 may be used by others subject to  */
            /* procedures and constraints described in Clause 23. */
        }

        public enum BacnetLifeSafetyState
        {
            MinLifeSafetyState = 0,
            LifeSafetyStateQuiet = 0,
            LifeSafetyStatePreAlarm = 1,
            LifeSafetyStateAlarm = 2,
            LifeSafetyStateFault = 3,
            LifeSafetyStateFaultPreAlarm = 4,
            LifeSafetyStateFaultAlarm = 5,
            LifeSafetyStateNotReady = 6,
            LifeSafetyStateActive = 7,
            LifeSafetyStateTamper = 8,
            LifeSafetyStateTestAlarm = 9,
            LifeSafetyStateTestActive = 10,
            LifeSafetyStateTestFault = 11,
            LifeSafetyStateTestFaultAlarm = 12,
            LifeSafetyStateHoldup = 13,
            LifeSafetyStateDuress = 14,
            LifeSafetyStateTamperAlarm = 15,
            LifeSafetyStateAbnormal = 16,
            LifeSafetyStateEmergencyPower = 17,
            LifeSafetyStateDelayed = 18,
            LifeSafetyStateBlocked = 19,
            LifeSafetyStateLocalAlarm = 20,
            LifeSafetyStateGeneralAlarm = 21,
            LifeSafetyStateSupervisory = 22,
            LifeSafetyStateTestSupervisory = 23,
            MaxLifeSafetyState = 0
            /* Enumerated values 0-255 are reserved for definition by ASHRAE.  */
            /* Enumerated values 256-65535 may be used by others subject to  */
            /* procedures and constraints described in Clause 23. */
        }

        public enum BacnetSilencedState
        {
            SilencedStateUnsilenced = 0,
            SilencedStateAudibleSilenced = 1,
            SilencedStateVisibleSilenced = 2,
            SilencedStateAllSilenced = 3
            /* Enumerated values 0-63 are reserved for definition by ASHRAE. */
            /* Enumerated values 64-65535 may be used by others subject to */
            /* procedures and constraints described in Clause 23. */
        }

        public enum BacnetMaintenance
        {
            MaintenanceNone = 0,
            MaintenancePeriodicTest = 1,
            AintenanceNeedServiceOperational = 2,
            MaintenanceNeedServiceInoperative = 3
            /* Enumerated values 0-255 are reserved for definition by ASHRAE.  */
            /* Enumerated values 256-65535 may be used by others subject to  */
            /* procedures and constraints described in Clause 23. */
        }

        public enum BacnetNotifyType
        {
            NotifyAlarm = 0,
            NotifyEvent = 1,
            NotifyAckNotification = 2
        }

        public enum BacnetObjectType
        {
            //OBJECT_NULL = -1,
            ObjectAnalogInput = 0,
            ObjectAnalogOutput = 1,
            ObjectAnalogValue = 2,
            ObjectBinaryInput = 3,
            ObjectBinaryOutput = 4,
            ObjectBinaryValue = 5,
            ObjectCalendar = 6,
            ObjectCommand = 7,
            ObjectDevice = 8,
            ObjectEventEnrollment = 9,
            ObjectFile = 10,
            ObjectGroup = 11,
            ObjectLoop = 12,
            ObjectMultiStateInput = 13,
            ObjectMultiStateOutput = 14,
            ObjectNotificationClass = 15,
            ObjectProgram = 16,
            ObjectSchedule = 17,
            ObjectAveraging = 18,
            ObjectMultiStateValue = 19,
            ObjectTrendlog = 20,
            ObjectLifeSafetyPoint = 21,
            ObjectLifeSafetyZone = 22,
            ObjectAccumulator = 23,
            ObjectPulseConverter = 24,
            ObjectEventLog = 25,
            ObjectGlobalGroup = 26,
            ObjectTrendLogMultiple = 27,
            ObjectLoadControl = 28,
            ObjectStructuredView = 29,
            /* what is object type 30? */
            ObjectLightingOutput = 31,
            /* Enumerated values 0-127 are reserved for definition by ASHRAE. */
            /* Enumerated values 128-1023 may be used by others subject to  */
            /* the procedures and constraints described in Clause 23. */
            MaxAshraeObjectType = 32,        /* used for bit string loop */
            MaxBacnetObjectType = 1023
        }

        public enum BacnetSegmentation
        {
            SegmentationBoth = 0,
            SegmentationTransmit = 1,
            SegmentationReceive = 2,
            SegmentationNone = 3,
            MaxBacnetSegmentation = 4
        }

        public enum BacnetVtClass
        {
            VtClassDefault = 0,
            VtClassAnsiX34 = 1,      /* real name is ANSI X3.64 */
            VtClassDecVt52 = 2,
            VtClassDecVt100 = 3,
            VtClassDecVt220 = 4,
            VtClassHp70094 = 5,     /* real name is HP 700/94 */
            VtClassIbm3130 = 6
            /* Enumerated values 0-63 are reserved for definition by ASHRAE.  */
            /* Enumerated values 64-65535 may be used by others subject to  */
            /* the procedures and constraints described in Clause 23. */
        }

        public enum BacnetCharacterStringEncoding
        {
            CharacterAnsiX34 = 0,
            CharacterMsDbcs = 1,
            CharacterJisc6226 = 2,
            CharacterUcs4 = 3,
            CharacterUcs2 = 4,
            CharacterIso8859 = 5
        }

        public enum BacnetApplicationTag
        {
            BacnetApplicationTagNull = 0,
            BacnetApplicationTagBoolean = 1,
            BacnetApplicationTagUnsignedInt = 2,
            BacnetApplicationTagSignedInt = 3,
            BacnetApplicationTagReal = 4,
            BacnetApplicationTagDouble = 5,
            BacnetApplicationTagOctetString = 6,
            BacnetApplicationTagCharacterString = 7,
            BacnetApplicationTagBitString = 8,
            BacnetApplicationTagEnumerated = 9,
            BacnetApplicationTagDate = 10,
            BacnetApplicationTagTime = 11,
            BacnetApplicationTagObjectId = 12,
            BacnetApplicationTagReserve1 = 13,
            BacnetApplicationTagReserve2 = 14,
            BacnetApplicationTagReserve3 = 15,
            MaxBacnetApplicationTag = 16
        }

        /* note: these are not the real values, */
        /* but are shifted left for easy encoding */
        public enum BacnetPduType
        {
            PduTypeConfirmedServiceRequest = 0,
            PduTypeUnconfirmedServiceRequest = 0x10,
            PduTypeSimpleAck = 0x20,
            PduTypeComplexAck = 0x30,
            PduTypeSegmentAck = 0x40,
            PduTypeError = 0x50,
            PduTypeReject = 0x60,
            PduTypeAbort = 0x70
        }

        public enum BacnetConfirmedService
        {
            /* Alarm and Event Services */
            ServiceConfirmedAcknowledgeAlarm = 0,
            ServiceConfirmedCovNotification = 1,
            ServiceConfirmedEventNotification = 2,
            ServiceConfirmedGetAlarmSummary = 3,
            ServiceConfirmedGetEnrollmentSummary = 4,
            ServiceConfirmedGetEventInformation = 29,
            ServiceConfirmedSubscribeCov = 5,
            ServiceConfirmedSubscribeCovProperty = 28,
            ServiceConfirmedLifeSafetyOperation = 27,
            /* File Access Services */
            ServiceConfirmedAtomicReadFile = 6,
            ServiceConfirmedAtomicWriteFile = 7,
            /* Object Access Services */
            ServiceConfirmedAddListElement = 8,
            ServiceConfirmedRemoveListElement = 9,
            ServiceConfirmedCreateObject = 10,
            ServiceConfirmedDeleteObject = 11,
            ServiceConfirmedReadProperty = 12,
            ServiceConfirmedReadPropConditional = 13,
            ServiceConfirmedReadPropMultiple = 14,
            ServiceConfirmedReadRange = 26,
            ServiceConfirmedWriteProperty = 15,
            ServiceConfirmedWritePropMultiple = 16,
            /* Remote Device Management Services */
            ServiceConfirmedDeviceCommunicationControl = 17,
            ServiceConfirmedPrivateTransfer = 18,
            ServiceConfirmedTextMessage = 19,
            ServiceConfirmedReinitializeDevice = 20,
            /* Virtual Terminal Services */
            ServiceConfirmedVtOpen = 21,
            ServiceConfirmedVtClose = 22,
            ServiceConfirmedVtData = 23,
            /* Security Services */
            ServiceConfirmedAuthenticate = 24,
            ServiceConfirmedRequestKey = 25,
            /* Services added after 1995 */
            /* readRange (26) see Object Access Services */
            /* lifeSafetyOperation (27) see Alarm and Event Services */
            /* subscribeCOVProperty (28) see Alarm and Event Services */
            /* getEventInformation (29) see Alarm and Event Services */
            MaxBacnetConfirmedService = 30
        }

        public enum BacnetUnconfirmedService
        {
            ServiceUnconfirmedIAm = 0,
            ServiceUnconfirmedIHave = 1,
            ServiceUnconfirmedCovNotification = 2,
            ServiceUnconfirmedEventNotification = 3,
            ServiceUnconfirmedPrivateTransfer = 4,
            ServiceUnconfirmedTextMessage = 5,
            ServiceUnconfirmedTimeSynchronization = 6,
            ServiceUnconfirmedWhoHas = 7,
            ServiceUnconfirmedWhoIs = 8,
            ServiceUnconfirmedUtcTimeSynchronization = 9,
            /* Other services to be added as they are defined. */
            /* All choice values in this production are reserved */
            /* for definition by ASHRAE. */
            /* Proprietary extensions are made by using the */
            /* UnconfirmedPrivateTransfer service. See Clause 23. */
            MaxBacnetUnconfirmedService = 10
        }

        /* Bit String Enumerations */
        public enum BacnetServicesSupported
        {
            /* Alarm and Event Services */
            ServiceSupportedAcknowledgeAlarm = 0,
            ServiceSupportedConfirmedCovNotification = 1,
            ServiceSupportedConfirmedEventNotification = 2,
            ServiceSupportedGetAlarmSummary = 3,
            ServiceSupportedGetEnrollmentSummary = 4,
            ServiceSupportedGetEventInformation = 39,
            ServiceSupportedSubscribeCov = 5,
            ServiceSupportedSubscribeCovProperty = 38,
            ServiceSupportedLifeSafetyOperation = 37,
            /* File Access Services */
            ServiceSupportedAtomicReadFile = 6,
            ServiceSupportedAtomicWriteFile = 7,
            /* Object Access Services */
            ServiceSupportedAddListElement = 8,
            ServiceSupportedRemoveListElement = 9,
            ServiceSupportedCreateObject = 10,
            ServiceSupportedDeleteObject = 11,
            ServiceSupportedReadProperty = 12,
            ServiceSupportedReadPropConditional = 13,
            ServiceSupportedReadPropMultiple = 14,
            ServiceSupportedReadRange = 35,
            ServiceSupportedWriteProperty = 15,
            ServiceSupportedWritePropMultiple = 16,
            /* Remote Device Management Services */
            ServiceSupportedDeviceCommunicationControl = 17,
            ServiceSupportedPrivateTransfer = 18,
            ServiceSupportedTextMessage = 19,
            ServiceSupportedReinitializeDevice = 20,
            /* Virtual Terminal Services */
            ServiceSupportedVtOpen = 21,
            ServiceSupportedVtClose = 22,
            ServiceSupportedVtData = 23,
            /* Security Services */
            ServiceSupportedAuthenticate = 24,
            ServiceSupportedRequestKey = 25,
            ServiceSupportedIAm = 26,
            ServiceSupportedIHave = 27,
            ServiceSupportedUnconfirmedCovNotification = 28,
            ServiceSupportedUnconfirmedEventNotification = 29,
            ServiceSupportedUnconfirmedPrivateTransfer = 30,
            ServiceSupportedUnconfirmedTextMessage = 31,
            ServiceSupportedTimeSynchronization = 32,
            ServiceSupportedUtcTimeSynchronization = 36,
            ServiceSupportedWhoHas = 33,
            ServiceSupportedWhoIs = 34,
            /* Other services to be added as they are defined. */
            /* All values in this production are reserved */
            /* for definition by ASHRAE. */
            MaxBacnetServicesSupported = 40
        }

        public enum BacnetBvlcFunction
        {
            BvlcResult = 0,
            BvlcWriteBroadcastDistributionTable = 1,
            BvlcReadBroadcastDistTable = 2,
            BvlcReadBroadcastDistTableAck = 3,
            BvlcForwardedNpdu = 4,
            BvlcRegisterForeignDevice = 5,
            BvlcReadForeignDeviceTable = 6,
            BvlcReadForeignDeviceTableAck = 7,
            BvlcDeleteForeignDeviceTableEntry = 8,
            BvlcDistributeBroadcastToNetwork = 9,
            BvlcOriginalUnicastNpdu = 10,
            BvlcOriginalBroadcastNpdu = 11,
            MaxBvlcFunction = 12
        }

        public enum BacnetBvlcResult
        {
            BvlcResultSuccessfulCompletion = 0x0000,
            BvlcResultWriteBroadcastDistributionTableNak = 0x0010,
            BvlcResultReadBroadcastDistributionTableNak = 0x0020,
            BvlcResultRegisterForeignDeviceNak = 0X0030,
            BvlcResultReadForeignDeviceTableNak = 0x0040,
            BvlcResultDeleteForeignDeviceTableEntryNak = 0x0050,
            BvlcResultDistributeBroadcastToNetworkNak = 0x0060
        }

        /* Bit String Enumerations */
        public enum BacnetStatusFlags
        {
            StatusFlagInAlarm = 0,
            StatusFlagFault = 1,
            StatusFlagOverridden = 2,
            StatusFlagOutOfService = 3
        }

        public enum BacnetAcknowledgmentFilter
        {
            AcknowledgmentFilterAll = 0,
            AcknowledgmentFilterAcked = 1,
            AcknowledgmentFilterNotAcked = 2
        }

        public enum BacnetEventStateFilter
        {
            EventStateFilterOffnormal = 0,
            EventStateFilterFault = 1,
            EventStateFilterNormal = 2,
            EventStateFilterAll = 3,
            EventStateFilterActive = 4
        }

        public enum BacnetSelectionLogic
        {
            SelectionLogicAnd = 0,
            SelectionLogicOr = 1,
            SelectionLogicAll = 2
        }

        public enum BacnetRelationSpecifier
        {
            RelationSpecifierEqual = 0,
            RelationSpecifierNotEqual = 1,
            RelationSpecifierLessThan = 2,
            RelationSpecifierGreaterThan = 3,
            RelationSpecifierLessThanOrEqual = 4,
            RelationSpecifierGreaterThanOrEqual = 5
        }

        public enum BacnetCommunicationEnableDisable
        {
            CommunicationEnable = 0,
            CommunicationDisable = 1,
            CommunicationDisableInitiation = 2,
            MaxBacnetCommunicationEnableDisable = 3
        }

        public enum BacnetMessagePriority
        {
            MessagePriorityNormal = 0,
            MessagePriorityUrgent = 1,
            MessagePriorityCriticalEquipment = 2,
            MessagePriorityLifeSafety = 3
        }

        /*Network Layer Message Type */
        /*If Bit 7 of the control octet described in 6.2.2 is 1, */
        /* a message type octet shall be present as shown in Figure 6-1. */
        /* The following message types are indicated: */
        public enum BacnetNetworkMessageType
        {
            NetworkMessageWhoIsRouterToNetwork = 0,
            NetworkMessageIAmRouterToNetwork = 1,
            NetworkMessageICouldBeRouterToNetwork = 2,
            NetworkMessageRejectMessageToNetwork = 3,
            NetworkMessageRouterBusyToNetwork = 4,
            NetworkMessageRouterAvailableToNetwork = 5,
            NetworkMessageInitRtTable = 6,
            NetworkMessageInitRtTableAck = 7,
            NetworkMessageEstablishConnectionToNetwork = 8,
            NetworkMessageDisconnectConnectionToNetwork = 9,
            /* X'0A' to X'7F': Reserved for use by ASHRAE, */
            /* X'80' to X'FF': Available for vendor proprietary messages */
            NetworkMessageInvalid = 0x100
        }


        public enum BacnetReinitializedStateOfDevice
        {
            ReinitializedStateColdStart = 0,
            ReinitializedStateWarmStart = 1,
            ReinitializedStateStartBackup = 2,
            ReinitializedStateEndBackup = 3,
            ReinitializedStateStartRestore = 4,
            ReinitializedStateEndRestore = 5,
            ReinitializedStateAbortRestore = 6,
            ReinitializedStateIdle = 255
        }

        public enum BacnetAbortReason
        {
            AbortReasonOther = 0,
            AbortReasonBufferOverflow = 1,
            AbortReasonInvalidApduInThisState = 2,
            AbortReasonPreemptedByHigherPriorityTask = 3,
            AbortReasonSegmentationNotSupported = 4,
            /* Enumerated values 0-63 are reserved for definition by ASHRAE. */
            /* Enumerated values 64-65535 may be used by others subject to */
            /* the procedures and constraints described in Clause 23. */
            MaxBacnetAbortReason = 5,
            FirstProprietaryAbortReason = 64,
            LastProprietaryAbortReason = 65535
        }

        public enum BacnetBacnetRejectReason
        {
            RejectReasonOther = 0,
            RejectReasonBufferOverflow = 1,
            RejectReasonInconsistentParameters = 2,
            RejectReasonInvalidParameterDataType = 3,
            RejectReasonInvalidTag = 4,
            RejectReasonMissingRequiredParameter = 5,
            RejectReasonParameterOutOfRange = 6,
            RejectReasonTooManyArguments = 7,
            RejectReasonUndefinedEnumeration = 8,
            RejectReasonUnrecognizedService = 9,
            /* Enumerated values 0-63 are reserved for definition by ASHRAE. */
            /* Enumerated values 64-65535 may be used by others subject to */
            /* the procedures and constraints described in Clause 23. */
            MaxBacnetRejectReason = 10,
            FirstProprietaryRejectReason = 64,
            LastProprietaryRejectReason = 65535
        }

        public enum BacnetErrorClass
        {
            ErrorClassDevice = 0,
            ErrorClassObject = 1,
            ErrorClassProperty = 2,
            ErrorClassResources = 3,
            ErrorClassSecurity = 4,
            ErrorClassServices = 5,
            ErrorClassVt = 6,
            /* Enumerated values 0-63 are reserved for definition by ASHRAE. */
            /* Enumerated values 64-65535 may be used by others subject to */
            /* the procedures and constraints described in Clause 23. */
            MaxBacnetErrorClass = 7,
            FirstProprietaryErrorClass = 64,
            LastProprietaryErrorClass = 65535
        }

        /* These are sorted in the order given in
           Clause 18. ERROR, REJECT AND ABORT CODES
           The Class and Code pairings are required
           to be used in accordance with Clause 18. */
        public enum BacnetErrorCode
        {
            /* valid for all classes */
            ErrorCodeOther = 0,

            /* Error Class - Device */
            ErrorCodeDeviceBusy = 3,
            ErrorCodeConfigurationInProgress = 2,
            ErrorCodeOperationalProblem = 25,

            /* Error Class - Object */
            ErrorCodeDynamicCreationNotSupported = 4,
            ErrorCodeNoObjectsOfSpecifiedType = 17,
            ErrorCodeObjectDeletionNotPermitted = 23,
            ErrorCodeObjectIdentifierAlreadyExists = 24,
            ErrorCodeReadAccessDenied = 27,
            ErrorCodeUnknownObject = 31,
            ErrorCodeUnsupportedObjectType = 36,

            /* Error Class - Property */
            ErrorCodeCharacterSetNotSupported = 41,
            ErrorCodeDatatypeNotSupported = 47,
            ErrorCodeInconsistentSelectionCriterion = 8,
            ErrorCodeInvalidArrayIndex = 42,
            ErrorCodeInvalidDataType = 9,
            ErrorCodeNotCovProperty = 44,
            ErrorCodeOptionalFunctionalityNotSupported = 45,
            ErrorCodePropertyIsNotAnArray = 50,
            /* ERROR_CODE_READ_ACCESS_DENIED = 27, */
            ErrorCodeUnknownProperty = 32,
            ErrorCodeValueOutOfRange = 37,
            ErrorCodeWriteAccessDenied = 40,

            /* Error Class - Resources */
            ErrorCodeNoSpaceForObject = 18,
            ErrorCodeNoSpaceToAddListElement = 19,
            ErrorCodeNoSpaceToWriteProperty = 20,

            /* Error Class - Security */
            ErrorCodeAuthenticationFailed = 1,
            /* ERROR_CODE_CHARACTER_SET_NOT_SUPPORTED = 41, */
            ErrorCodeIncompatibleSecurityLevels = 6,
            ErrorCodeInvalidOperatorName = 12,
            ErrorCodeKeyGenerationError = 15,
            ErrorCodePasswordFailure = 26,
            ErrorCodeSecurityNotSupported = 28,
            ErrorCodeTimeout = 30,

            /* Error Class - Services */
            /* ERROR_CODE_CHARACTER_SET_NOT_SUPPORTED = 41, */
            ErrorCodeCovSubscriptionFailed = 43,
            ErrorCodeDuplicateName = 48,
            ErrorCodeDuplicateObjectId = 49,
            ErrorCodeFileAccessDenied = 5,
            ErrorCodeInconsistentParameters = 7,
            ErrorCodeInvalidConfigurationData = 46,
            ErrorCodeInvalidFileAccessMethod = 10,
            ErrorCodeErrorCodeInvalidFileStartPosition = 11,
            ErrorCodeInvalidParameterDataType = 13,
            ErrorCodeInvalidTimeStamp = 14,
            ErrorCodeMissingRequiredParameter = 16,
            /* ERROR_CODE_OPTIONAL_FUNCTIONALITY_NOT_SUPPORTED = 45, */
            ErrorCodePropertyIsNotAList = 22,
            ErrorCodeServiceRequestDenied = 29,

            /* Error Class - VT */
            ErrorCodeUnknownVtClass = 34,
            ErrorCodeUnknownVtSession = 35,
            ErrorCodeNoVtSessionsAvailable = 21,
            ErrorCodeVtSessionAlreadyClosed = 38,
            ErrorCodeVtSessionTerminationFailure = 39,

            /* unused */
            ErrorCodeReserved1 = 33,
            /* Enumerated values 0-255 are reserved for definition by ASHRAE. */
            /* Enumerated values 256-65535 may be used by others subject to */
            /* the procedures and constraints described in Clause 23. */
            /* The last enumeration used in this version is 50. */
            MaxBacnetErrorCode = 51,
            FirstProprietaryErrorCode = 256,
            LastProprietaryErrorCode = 65535
        }

        public enum BacnetReinitializedState
        {
            BacnetReinitColdstart = 0,
            BacnetReinitWarmstart = 1,
            BacnetReinitStartbackup = 2,
            BacnetReinitEndbackup = 3,
            BacnetReinitStartrestore = 4,
            BacnetReinitEndrestore = 5,
            BacnetReinitAbortrestore = 6,
            MaxBacnetReinitializedState = 7
        }

        public enum BacnetNodeType
        {
            BacnetNodeUnknown = 0,
            BacnetNodeSystem = 1,
            BacnetNodeNetwork = 2,
            BacnetNodeDevice = 3,
            BacnetNodeOrganizational = 4,
            BacnetNodeArea = 5,
            BacnetNodeEquipment = 6,
            BacnetNodePoint = 7,
            BacnetNodeCollection = 8,
            BacnetNodeProperty = 9,
            BacnetNodeFunctional = 10,
            BacnetNodeOther = 11
        }

        public enum BacnetShedState
        {
            BacnetShedInactive = 0,
            BacnetShedRequestPending = 1,
            BacnetShedCompliant = 2,
            BacnetShedNonCompliant = 3
        }

        public enum BacnetLightingOperation
        {
            BacnetLightsStop = 0,
            BacnetLightsFadeTo = 1,
            BacnetLightsFadeToOver = 2,
            BacnetLightsRampTo = 3,
            BacnetLightsRampToAtRate = 4,
            BacnetLightsRampUp = 5,
            BacnetLightsRampUpAtRate = 6,
            BacnetLightsRampDown = 7,
            BacnetLightsRampDownAtRate = 8,
            BacnetLightsStepUp = 9,
            BacnetLightsStepDown = 10,
            BacnetLightsStepUpBy = 11,
            BacnetLightsStepDownBy = 12,
            BacnetLightsGotoLevel = 13,
            BacnetLightsRelinquish = 14
        }

        /* NOTE: BACNET_DAYS_OF_WEEK is different than BACNET_WEEKDAY */
        /* 0=Monday-6=Sunday */
        public enum BacnetDaysOfWeek
        {
            BacnetDaysOfWeekMonday = 0,
            BacnetDaysOfWeekTuesday = 1,
            BacnetDaysOfWeekWednesday = 2,
            BacnetDaysOfWeekThursday = 3,
            BacnetDaysOfWeekFriday = 4,
            BacnetDaysOfWeekSaturday = 5,
            BacnetDaysOfWeekSunday = 6
        }
    }
}