// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Transactions;

namespace Microsoft.Data.Common
{
    internal static partial class ADP
    {
        // The class ADP defines the exceptions that are specific to the Adapters.
        // The class contains functions that take the proper informational variables and then construct
        // the appropriate exception with an error string obtained from the resource framework.
        // The exception is then returned to the caller, so that the caller may then throw from its
        // location so that the catcher of the exception will have the appropriate call stack.
        // This class is used so that there will be compile time checking of error messages.
        internal static Exception ExceptionWithStackTrace(Exception e)
        {
            try
            {
                throw e;
            }
            catch (Exception caught)
            {
                return caught;
            }
        }

        //
        // COM+ exceptions
        //
        internal static IndexOutOfRangeException IndexOutOfRange(int value)
        {
            IndexOutOfRangeException e = new IndexOutOfRangeException(value.ToString(CultureInfo.InvariantCulture));
            return e;
        }
        internal static IndexOutOfRangeException IndexOutOfRange()
        {
            IndexOutOfRangeException e = new IndexOutOfRangeException();
            return e;
        }
        internal static TimeoutException TimeoutException(string error)
        {
            TimeoutException e = new TimeoutException(error);
            return e;
        }
        internal static InvalidOperationException InvalidOperation(string error, Exception inner)
        {
            InvalidOperationException e = new InvalidOperationException(error, inner);
            return e;
        }
        internal static OverflowException Overflow(string error)
        {
            return Overflow(error, null);
        }
        internal static OverflowException Overflow(string error, Exception inner)
        {
            OverflowException e = new OverflowException(error, inner);
            return e;
        }
        internal static TypeLoadException TypeLoad(string error)
        {
            TypeLoadException e = new TypeLoadException(error);
            TraceExceptionAsReturnValue(e);
            return e;
        }
        internal static PlatformNotSupportedException DbTypeNotSupported(string dbType)
        {
            PlatformNotSupportedException e = new PlatformNotSupportedException(StringsHelper.GetString(Strings.SQL_DbTypeNotSupportedOnThisPlatform, dbType));
            return e;
        }
        internal static InvalidCastException InvalidCast()
        {
            InvalidCastException e = new InvalidCastException();
            return e;
        }
        internal static IOException IO(string error)
        {
            IOException e = new IOException(error);
            return e;
        }
        internal static IOException IO(string error, Exception inner)
        {
            IOException e = new IOException(error, inner);
            return e;
        }
        internal static ObjectDisposedException ObjectDisposed(object instance)
        {
            ObjectDisposedException e = new ObjectDisposedException(instance.GetType().Name);
            return e;
        }

        internal static Exception DataTableDoesNotExist(string collectionName)
        {
            return Argument(StringsHelper.GetString(Strings.MDF_DataTableDoesNotExist, collectionName));
        }

        internal static InvalidOperationException MethodCalledTwice(string method)
        {
            InvalidOperationException e = new InvalidOperationException(StringsHelper.GetString(Strings.ADP_CalledTwice, method));
            return e;
        }


        // IDbCommand.CommandType
        internal static ArgumentOutOfRangeException InvalidCommandType(CommandType value)
        {
#if DEBUG
            switch (value)
            {
                case CommandType.Text:
                case CommandType.StoredProcedure:
                case CommandType.TableDirect:
                    Debug.Fail("valid CommandType " + value.ToString());
                    break;
            }
#endif
            return InvalidEnumerationValue(typeof(CommandType), (int)value);
        }

        // IDbConnection.BeginTransaction, OleDbTransaction.Begin
        internal static ArgumentOutOfRangeException InvalidIsolationLevel(System.Data.IsolationLevel value)
        {
#if DEBUG
            switch (value)
            {
                case System.Data.IsolationLevel.Unspecified:
                case System.Data.IsolationLevel.Chaos:
                case System.Data.IsolationLevel.ReadUncommitted:
                case System.Data.IsolationLevel.ReadCommitted:
                case System.Data.IsolationLevel.RepeatableRead:
                case System.Data.IsolationLevel.Serializable:
                case System.Data.IsolationLevel.Snapshot:
                    Debug.Fail("valid IsolationLevel " + value.ToString());
                    break;
            }
#endif
            return InvalidEnumerationValue(typeof(System.Data.IsolationLevel), (int)value);
        }


        // IDataParameter.Direction
        internal static ArgumentOutOfRangeException InvalidParameterDirection(ParameterDirection value)
        {
#if DEBUG
            switch (value)
            {
                case ParameterDirection.Input:
                case ParameterDirection.Output:
                case ParameterDirection.InputOutput:
                case ParameterDirection.ReturnValue:
                    Debug.Fail("valid ParameterDirection " + value.ToString());
                    break;
            }
#endif
            return InvalidEnumerationValue(typeof(ParameterDirection), (int)value);
        }

        internal static Exception TooManyRestrictions(string collectionName)
        {
            return Argument(StringsHelper.GetString(Strings.MDF_TooManyRestrictions, collectionName));
        }


        // IDbCommand.UpdateRowSource
        internal static ArgumentOutOfRangeException InvalidUpdateRowSource(UpdateRowSource value)
        {
#if DEBUG
            switch (value)
            {
                case UpdateRowSource.None:
                case UpdateRowSource.OutputParameters:
                case UpdateRowSource.FirstReturnedRecord:
                case UpdateRowSource.Both:
                    Debug.Fail("valid UpdateRowSource " + value.ToString());
                    break;
            }
#endif
            return InvalidEnumerationValue(typeof(UpdateRowSource), (int)value);
        }

        //
        // DbConnectionOptions, DataAccess
        //
        internal static ArgumentException InvalidMinMaxPoolSizeValues()
        {
            return ADP.Argument(StringsHelper.GetString(Strings.ADP_InvalidMinMaxPoolSizeValues));
        }


        //
        // DbConnection
        //
        internal static InvalidOperationException NoConnectionString()
        {
            return InvalidOperation(StringsHelper.GetString(Strings.ADP_NoConnectionString));
        }

        internal static Exception MethodNotImplemented([CallerMemberName] string methodName = "")
        {
            return NotImplemented.ByDesignWithMessage(methodName);
        }

        internal static Exception QueryFailed(string collectionName, Exception e)
        {
            return InvalidOperation(StringsHelper.GetString(Strings.MDF_QueryFailed, collectionName), e);
        }


        //
        // : DbConnectionOptions, DataAccess, SqlClient
        //
        internal static Exception InvalidConnectionOptionValueLength(string key, int limit)
        {
            return Argument(StringsHelper.GetString(Strings.ADP_InvalidConnectionOptionValueLength, key, limit));
        }
        internal static Exception MissingConnectionOptionValue(string key, string requiredAdditionalKey)
        {
            return Argument(StringsHelper.GetString(Strings.ADP_MissingConnectionOptionValue, key, requiredAdditionalKey));
        }


        //
        // DbConnectionPool and related
        //
        internal static Exception PooledOpenTimeout()
        {
            return ADP.InvalidOperation(StringsHelper.GetString(Strings.ADP_PooledOpenTimeout));
        }

        internal static Exception NonPooledOpenTimeout()
        {
            return ADP.TimeoutException(StringsHelper.GetString(Strings.ADP_NonPooledOpenTimeout));
        }

        //
        // DbProviderException
        //
        internal static InvalidOperationException TransactionConnectionMismatch()
        {
            return Provider(StringsHelper.GetString(Strings.ADP_TransactionConnectionMismatch));
        }
        internal static InvalidOperationException TransactionRequired(string method)
        {
            return Provider(StringsHelper.GetString(Strings.ADP_TransactionRequired, method));
        }


        internal static Exception CommandTextRequired(string method)
        {
            return InvalidOperation(StringsHelper.GetString(Strings.ADP_CommandTextRequired, method));
        }

        internal static Exception NoColumns()
        {
            return Argument(StringsHelper.GetString(Strings.MDF_NoColumns));
        }

        internal static InvalidOperationException ConnectionRequired(string method)
        {
            return InvalidOperation(StringsHelper.GetString(Strings.ADP_ConnectionRequired, method));
        }
        internal static InvalidOperationException OpenConnectionRequired(string method, ConnectionState state)
        {
            return InvalidOperation(StringsHelper.GetString(Strings.ADP_OpenConnectionRequired, method, ADP.ConnectionStateMsg(state)));
        }

        internal static Exception OpenReaderExists(bool marsOn)
        {
            return OpenReaderExists(null, marsOn);
        }

        internal static Exception OpenReaderExists(Exception e, bool marsOn)
        {
            return InvalidOperation(StringsHelper.GetString(Strings.ADP_OpenReaderExists, marsOn ? ADP.Command : ADP.Connection), e);
        }


        //
        // DbDataReader
        //
        internal static Exception NonSeqByteAccess(long badIndex, long currIndex, string method)
        {
            return InvalidOperation(StringsHelper.GetString(Strings.ADP_NonSeqByteAccess, badIndex.ToString(CultureInfo.InvariantCulture), currIndex.ToString(CultureInfo.InvariantCulture), method));
        }

        internal static Exception InvalidXml()
        {
            return Argument(StringsHelper.GetString(Strings.MDF_InvalidXml));
        }

        internal static Exception NegativeParameter(string parameterName)
        {
            return InvalidOperation(StringsHelper.GetString(Strings.ADP_NegativeParameter, parameterName));
        }

        internal static Exception InvalidXmlMissingColumn(string collectionName, string columnName)
        {
            return Argument(StringsHelper.GetString(Strings.MDF_InvalidXmlMissingColumn, collectionName, columnName));
        }

        //
        // SqlMetaData, SqlTypes, SqlClient
        //
        internal static Exception InvalidMetaDataValue()
        {
            return ADP.Argument(StringsHelper.GetString(Strings.ADP_InvalidMetaDataValue));
        }

        internal static InvalidOperationException NonSequentialColumnAccess(int badCol, int currCol)
        {
            return InvalidOperation(StringsHelper.GetString(Strings.ADP_NonSequentialColumnAccess, badCol.ToString(CultureInfo.InvariantCulture), currCol.ToString(CultureInfo.InvariantCulture)));
        }

        internal static Exception InvalidXmlInvalidValue(string collectionName, string columnName)
        {
            return Argument(StringsHelper.GetString(Strings.MDF_InvalidXmlInvalidValue, collectionName, columnName));
        }

        internal static Exception CollectionNameIsNotUnique(string collectionName)
        {
            return Argument(StringsHelper.GetString(Strings.MDF_CollectionNameISNotUnique, collectionName));
        }


        //
        // : IDbCommand
        //
        internal static Exception InvalidCommandTimeout(int value, [CallerMemberName] string property = "")
        {
            return Argument(StringsHelper.GetString(Strings.ADP_InvalidCommandTimeout, value.ToString(CultureInfo.InvariantCulture)), property);
        }
        internal static Exception UninitializedParameterSize(int index, Type dataType)
        {
            return InvalidOperation(StringsHelper.GetString(Strings.ADP_UninitializedParameterSize, index.ToString(CultureInfo.InvariantCulture), dataType.Name));
        }

        internal static Exception UnableToBuildCollection(string collectionName)
        {
            return Argument(StringsHelper.GetString(Strings.MDF_UnableToBuildCollection, collectionName));
        }

        internal static Exception PrepareParameterType(DbCommand cmd)
        {
            return InvalidOperation(StringsHelper.GetString(Strings.ADP_PrepareParameterType, cmd.GetType().Name));
        }

        internal static Exception UndefinedCollection(string collectionName)
        {
            return Argument(StringsHelper.GetString(Strings.MDF_UndefinedCollection, collectionName));
        }

        internal static Exception UnsupportedVersion(string collectionName)
        {
            return Argument(StringsHelper.GetString(Strings.MDF_UnsupportedVersion, collectionName));
        }

        internal static Exception AmbiguousCollectionName(string collectionName)
        {
            return Argument(StringsHelper.GetString(Strings.MDF_AmbiguousCollectionName, collectionName));
        }

        internal static Exception PrepareParameterSize(DbCommand cmd)
        {
            return InvalidOperation(StringsHelper.GetString(Strings.ADP_PrepareParameterSize, cmd.GetType().Name));
        }
        internal static Exception PrepareParameterScale(DbCommand cmd, string type)
        {
            return InvalidOperation(StringsHelper.GetString(Strings.ADP_PrepareParameterScale, cmd.GetType().Name, type));
        }

        internal static Exception MissingDataSourceInformationColumn()
        {
            return Argument(StringsHelper.GetString(Strings.MDF_MissingDataSourceInformationColumn));
        }

        internal static Exception IncorrectNumberOfDataSourceInformationRows()
        {
            return Argument(StringsHelper.GetString(Strings.MDF_IncorrectNumberOfDataSourceInformationRows));
        }

        internal static Exception MismatchedAsyncResult(string expectedMethod, string gotMethod)
        {
            return InvalidOperation(StringsHelper.GetString(Strings.ADP_MismatchedAsyncResult, expectedMethod, gotMethod));
        }

        //
        // : ConnectionUtil
        //
        internal static Exception ClosedConnectionError()
        {
            return InvalidOperation(StringsHelper.GetString(Strings.ADP_ClosedConnectionError));
        }
        internal static Exception ConnectionAlreadyOpen(ConnectionState state)
        {
            return InvalidOperation(StringsHelper.GetString(Strings.ADP_ConnectionAlreadyOpen, ADP.ConnectionStateMsg(state)));
        }
        internal static Exception TransactionPresent()
        {
            return InvalidOperation(StringsHelper.GetString(Strings.ADP_TransactionPresent));
        }
        internal static Exception LocalTransactionPresent()
        {
            return InvalidOperation(StringsHelper.GetString(Strings.ADP_LocalTransactionPresent));
        }
        internal static Exception OpenConnectionPropertySet(string property, ConnectionState state)
        {
            return InvalidOperation(StringsHelper.GetString(Strings.ADP_OpenConnectionPropertySet, property, ADP.ConnectionStateMsg(state)));
        }
        internal static Exception EmptyDatabaseName()
        {
            return Argument(StringsHelper.GetString(Strings.ADP_EmptyDatabaseName));
        }

        internal enum ConnectionError
        {
            BeginGetConnectionReturnsNull,
            GetConnectionReturnsNull,
            ConnectionOptionsMissing,
            CouldNotSwitchToClosedPreviouslyOpenedState,
        }

        internal static Exception MissingRestrictionColumn()
        {
            return Argument(StringsHelper.GetString(Strings.MDF_MissingRestrictionColumn));
        }

        internal static Exception InternalConnectionError(ConnectionError internalError)
        {
            return InvalidOperation(StringsHelper.GetString(Strings.ADP_InternalConnectionError, (int)internalError));
        }

        internal static Exception InvalidConnectRetryCountValue()
        {
            return Argument(StringsHelper.GetString(Strings.SQLCR_InvalidConnectRetryCountValue));
        }

        internal static Exception MissingRestrictionRow()
        {
            return Argument(StringsHelper.GetString(Strings.MDF_MissingRestrictionRow));
        }

        internal static Exception InvalidConnectRetryIntervalValue()
        {
            return Argument(StringsHelper.GetString(Strings.SQLCR_InvalidConnectRetryIntervalValue));
        }

        //
        // : DbDataReader
        //
        internal static InvalidOperationException AsyncOperationPending()
        {
            return InvalidOperation(StringsHelper.GetString(Strings.ADP_PendingAsyncOperation));
        }

        //
        // : Stream
        //
        internal static IOException ErrorReadingFromStream(Exception internalException)
        {
            return IO(StringsHelper.GetString(Strings.SqlMisc_StreamErrorMessage), internalException);
        }

        internal static ArgumentException InvalidDataType(TypeCode typecode)
        {
            return Argument(StringsHelper.GetString(Strings.ADP_InvalidDataType, typecode.ToString()));
        }

        internal static ArgumentException UnknownDataType(Type dataType)
        {
            return Argument(StringsHelper.GetString(Strings.ADP_UnknownDataType, dataType.FullName));
        }

        internal static ArgumentException DbTypeNotSupported(DbType type, Type enumtype)
        {
            return Argument(StringsHelper.GetString(Strings.ADP_DbTypeNotSupported, type.ToString(), enumtype.Name));
        }
        internal static ArgumentException UnknownDataTypeCode(Type dataType, TypeCode typeCode)
        {
            return Argument(StringsHelper.GetString(Strings.ADP_UnknownDataTypeCode, ((int)typeCode).ToString(CultureInfo.InvariantCulture), dataType.FullName));
        }
        internal static ArgumentException InvalidOffsetValue(int value)
        {
            return Argument(StringsHelper.GetString(Strings.ADP_InvalidOffsetValue, value.ToString(CultureInfo.InvariantCulture)));
        }
        internal static ArgumentException InvalidSizeValue(int value)
        {
            return Argument(StringsHelper.GetString(Strings.ADP_InvalidSizeValue, value.ToString(CultureInfo.InvariantCulture)));
        }
        internal static ArgumentException ParameterValueOutOfRange(decimal value)
        {
            return ADP.Argument(StringsHelper.GetString(Strings.ADP_ParameterValueOutOfRange, value.ToString((IFormatProvider)null)));
        }
        internal static ArgumentException ParameterValueOutOfRange(SqlDecimal value)
        {
            return ADP.Argument(StringsHelper.GetString(Strings.ADP_ParameterValueOutOfRange, value.ToString()));
        }
        internal static ArgumentException ParameterValueOutOfRange(String value)
        {
            return ADP.Argument(StringsHelper.GetString(Strings.ADP_ParameterValueOutOfRange, value));
        }
        internal static ArgumentException VersionDoesNotSupportDataType(string typeName)
        {
            return Argument(StringsHelper.GetString(Strings.ADP_VersionDoesNotSupportDataType, typeName));
        }
        internal static Exception ParameterConversionFailed(object value, Type destType, Exception inner)
        {
            Debug.Assert(null != value, "null value on conversion failure");
            Debug.Assert(null != inner, "null inner on conversion failure");

            Exception e;
            string message = StringsHelper.GetString(Strings.ADP_ParameterConversionFailed, value.GetType().Name, destType.Name);
            if (inner is ArgumentException)
            {
                e = new ArgumentException(message, inner);
            }
            else if (inner is FormatException)
            {
                e = new FormatException(message, inner);
            }
            else if (inner is InvalidCastException)
            {
                e = new InvalidCastException(message, inner);
            }
            else if (inner is OverflowException)
            {
                e = new OverflowException(message, inner);
            }
            else
            {
                e = inner;
            }
            return e;
        }

        //
        // : IDataParameterCollection
        //
        internal static Exception ParametersMappingIndex(int index, DbParameterCollection collection)
        {
            return CollectionIndexInt32(index, collection.GetType(), collection.Count);
        }
        internal static Exception ParametersSourceIndex(string parameterName, DbParameterCollection collection, Type parameterType)
        {
            return CollectionIndexString(parameterType, ADP.ParameterName, parameterName, collection.GetType());
        }
        internal static Exception ParameterNull(string parameter, DbParameterCollection collection, Type parameterType)
        {
            return CollectionNullValue(parameter, collection.GetType(), parameterType);
        }

        internal static Exception UndefinedPopulationMechanism(string populationMechanism)
        {
            throw new NotImplementedException();
        }

        internal static Exception InvalidParameterType(DbParameterCollection collection, Type parameterType, object invalidValue)
        {
            return CollectionInvalidType(collection.GetType(), parameterType, invalidValue);
        }

        //
        // : IDbTransaction
        //
        internal static Exception ParallelTransactionsNotSupported(DbConnection obj)
        {
            return InvalidOperation(StringsHelper.GetString(Strings.ADP_ParallelTransactionsNotSupported, obj.GetType().Name));
        }
        internal static Exception TransactionZombied(DbTransaction obj)
        {
            return InvalidOperation(StringsHelper.GetString(Strings.ADP_TransactionZombied, obj.GetType().Name));
        }

        // global constant strings
        internal const string ColumnEncryptionSystemProviderNamePrefix = "MSSQL_";
        internal const string Command = "Command";
        internal const string Connection = "Connection";
        internal const string Parameter = "Parameter";
        internal const string ParameterName = "ParameterName";
        internal const string ParameterSetPosition = "set_Position";

        internal const int DefaultCommandTimeout = 30;
        internal const float FailoverTimeoutStep = 0.08F;    // fraction of timeout to use for fast failover connections

        // security issue, don't rely upon public static readonly values
        internal static readonly string StrEmpty = ""; // String.Empty

        internal const int CharSize = sizeof(char);

        internal static Delegate FindBuilder(MulticastDelegate mcd)
        {
            if (null != mcd)
            {
                foreach (Delegate del in mcd.GetInvocationList())
                {
                    if (del.Target is DbCommandBuilder)
                        return del;
                }
            }

            return null;
        }

        internal static long TimerCurrent()
        {
            return DateTime.UtcNow.ToFileTimeUtc();
        }

        internal static long TimerFromSeconds(int seconds)
        {
            long result = checked((long)seconds * TimeSpan.TicksPerSecond);
            return result;
        }

        internal static long TimerFromMilliseconds(long milliseconds)
        {
            long result = checked(milliseconds * TimeSpan.TicksPerMillisecond);
            return result;
        }

        internal static bool TimerHasExpired(long timerExpire)
        {
            bool result = TimerCurrent() > timerExpire;
            return result;
        }

        internal static long TimerRemaining(long timerExpire)
        {
            long timerNow = TimerCurrent();
            long result = checked(timerExpire - timerNow);
            return result;
        }

        internal static long TimerRemainingMilliseconds(long timerExpire)
        {
            long result = TimerToMilliseconds(TimerRemaining(timerExpire));
            return result;
        }

        internal static long TimerRemainingSeconds(long timerExpire)
        {
            long result = TimerToSeconds(TimerRemaining(timerExpire));
            return result;
        }

        internal static long TimerToMilliseconds(long timerValue)
        {
            long result = timerValue / TimeSpan.TicksPerMillisecond;
            return result;
        }

        private static long TimerToSeconds(long timerValue)
        {
            long result = timerValue / TimeSpan.TicksPerSecond;
            return result;
        }

        internal static string MachineName()
        {
            return Environment.MachineName;
        }

        internal static Transaction GetCurrentTransaction()
        {
            return Transaction.Current;
        }

        internal static bool IsDirection(DbParameter value, ParameterDirection condition)
        {
#if DEBUG
            IsDirectionValid(condition);
#endif
            return (condition == (condition & value.Direction));
        }
#if DEBUG
        private static void IsDirectionValid(ParameterDirection value)
        {
            switch (value)
            { // @perfnote: Enum.IsDefined
                case ParameterDirection.Input:
                case ParameterDirection.Output:
                case ParameterDirection.InputOutput:
                case ParameterDirection.ReturnValue:
                    break;
                default:
                    throw ADP.InvalidParameterDirection(value);
            }
        }
#endif

        internal static void IsNullOrSqlType(object value, out bool isNull, out bool isSqlType)
        {
            if ((value == null) || (value == DBNull.Value))
            {
                isNull = true;
                isSqlType = false;
            }
            else
            {
                INullable nullable = (value as INullable);
                if (nullable != null)
                {
                    isNull = nullable.IsNull;
                    // Duplicated from DataStorage.cs
                    // For back-compat, SqlXml is not in this list
                    isSqlType = ((value is SqlBinary) ||
                                (value is SqlBoolean) ||
                                (value is SqlByte) ||
                                (value is SqlBytes) ||
                                (value is SqlChars) ||
                                (value is SqlDateTime) ||
                                (value is SqlDecimal) ||
                                (value is SqlDouble) ||
                                (value is SqlGuid) ||
                                (value is SqlInt16) ||
                                (value is SqlInt32) ||
                                (value is SqlInt64) ||
                                (value is SqlMoney) ||
                                (value is SqlSingle) ||
                                (value is SqlString));
                }
                else
                {
                    isNull = false;
                    isSqlType = false;
                }
            }
        }

        private static Version s_systemDataVersion;

        internal static Version GetAssemblyVersion()
        {
            // NOTE: Using lazy thread-safety since we don't care if two threads both happen to update the value at the same time
            if (s_systemDataVersion == null)
            {
                s_systemDataVersion = new Version(ThisAssembly.InformationalVersion);
            }

            return s_systemDataVersion;
        }


        internal static readonly string[] AzureSqlServerEndpoints = {StringsHelper.GetString(Strings.AZURESQL_GenericEndpoint),
                                                                     StringsHelper.GetString(Strings.AZURESQL_GermanEndpoint),
                                                                     StringsHelper.GetString(Strings.AZURESQL_UsGovEndpoint),
                                                                     StringsHelper.GetString(Strings.AZURESQL_ChinaEndpoint)};

        // This method assumes dataSource parameter is in TCP connection string format.
        internal static bool IsAzureSqlServerEndpoint(string dataSource)
        {
            int length = dataSource.Length;
            // remove server port
            int foundIndex = dataSource.LastIndexOf(',');
            if (foundIndex >= 0)
            {
                length = foundIndex;
            }

            // check for the instance name
            foundIndex = dataSource.LastIndexOf('\\', length - 1, length - 1);
            if (foundIndex > 0)
            {
                length = foundIndex;
            }

            // trim trailing whitespace
            while (length > 0 && char.IsWhiteSpace(dataSource[length - 1]))
            {
                length -= 1;
            }

            // check if servername end with any azure endpoints
            for (int index = 0; index < AzureSqlServerEndpoints.Length; index++)
            {
                string endpoint = AzureSqlServerEndpoints[index];
                if (length > endpoint.Length)
                {
                    if (string.Compare(dataSource, length - endpoint.Length, endpoint, 0, endpoint.Length, StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        internal static ArgumentOutOfRangeException InvalidDataRowVersion(DataRowVersion value)
        {
#if DEBUG
            switch (value)
            {
                case DataRowVersion.Default:
                case DataRowVersion.Current:
                case DataRowVersion.Original:
                case DataRowVersion.Proposed:
                    Debug.Fail($"Invalid DataRowVersion {value}");
                    break;
            }
#endif
            return InvalidEnumerationValue(typeof(DataRowVersion), (int)value);
        }

        internal static ArgumentException SingleValuedProperty(string propertyName, string value)
        {
            ArgumentException e = new ArgumentException(StringsHelper.GetString(Strings.ADP_SingleValuedProperty, propertyName, value));
            TraceExceptionAsReturnValue(e);
            return e;
        }

        internal static ArgumentException DoubleValuedProperty(string propertyName, string value1, string value2)
        {
            ArgumentException e = new ArgumentException(StringsHelper.GetString(Strings.ADP_DoubleValuedProperty, propertyName, value1, value2));
            TraceExceptionAsReturnValue(e);
            return e;
        }

        internal static ArgumentException InvalidPrefixSuffix()
        {
            ArgumentException e = new ArgumentException(StringsHelper.GetString(Strings.ADP_InvalidPrefixSuffix));
            TraceExceptionAsReturnValue(e);
            return e;
        }

        internal static ArgumentOutOfRangeException InvalidCommandBehavior(CommandBehavior value)
        {
            Debug.Assert((0 > (int)value) || ((int)value > 0x3F), "valid CommandType " + value.ToString());

            return InvalidEnumerationValue(typeof(CommandBehavior), (int)value);
        }

        internal static void ValidateCommandBehavior(CommandBehavior value)
        {
            if (((int)value < 0) || (0x3F < (int)value))
            {
                throw InvalidCommandBehavior(value);
            }
        }

        internal static ArgumentOutOfRangeException NotSupportedCommandBehavior(CommandBehavior value, string method)
        {
            return NotSupportedEnumerationValue(typeof(CommandBehavior), value.ToString(), method);
        }

        internal static ArgumentException BadParameterName(string parameterName)
        {
            ArgumentException e = new ArgumentException(StringsHelper.GetString(Strings.ADP_BadParameterName, parameterName));
            TraceExceptionAsReturnValue(e);
            return e;
        }

        internal static Exception DeriveParametersNotSupported(IDbCommand value)
        {
            return DataAdapter(StringsHelper.GetString(Strings.ADP_DeriveParametersNotSupported, value.GetType().Name, value.CommandType.ToString()));
        }

        internal static Exception NoStoredProcedureExists(string sproc)
        {
            return InvalidOperation(StringsHelper.GetString(Strings.ADP_NoStoredProcedureExists, sproc));
        }

        //
        // DbProviderException
        //
        internal static InvalidOperationException TransactionCompletedButNotDisposed()
        {
            return Provider(StringsHelper.GetString(Strings.ADP_TransactionCompletedButNotDisposed));
        }

        internal static ArgumentOutOfRangeException InvalidUserDefinedTypeSerializationFormat(Microsoft.Data.SqlClient.Server.Format value)
        {
            return InvalidEnumerationValue(typeof(Microsoft.Data.SqlClient.Server.Format), (int)value);
        }

        internal static ArgumentOutOfRangeException NotSupportedUserDefinedTypeSerializationFormat(Microsoft.Data.SqlClient.Server.Format value, string method)
        {
            return NotSupportedEnumerationValue(typeof(Microsoft.Data.SqlClient.Server.Format), value.ToString(), method);
        }

        internal static ArgumentOutOfRangeException ArgumentOutOfRange(string message, string parameterName, object value)
        {
            ArgumentOutOfRangeException e = new ArgumentOutOfRangeException(parameterName, value, message);
            TraceExceptionAsReturnValue(e);
            return e;
        }

        internal static ArgumentException InvalidArgumentLength(string argumentName, int limit)
        {
            return Argument(StringsHelper.GetString(Strings.ADP_InvalidArgumentLength, argumentName, limit));
        }

        internal static ArgumentException MustBeReadOnly(string argumentName)
        {
            return Argument(StringsHelper.GetString(Strings.ADP_MustBeReadOnly, argumentName));
        }

        internal static InvalidOperationException InvalidMixedUsageOfSecureAndClearCredential()
        {
            return InvalidOperation(StringsHelper.GetString(Strings.ADP_InvalidMixedUsageOfSecureAndClearCredential));
        }

        internal static ArgumentException InvalidMixedArgumentOfSecureAndClearCredential()
        {
            return Argument(StringsHelper.GetString(Strings.ADP_InvalidMixedUsageOfSecureAndClearCredential));
        }

        internal static InvalidOperationException InvalidMixedUsageOfSecureCredentialAndIntegratedSecurity()
        {
            return InvalidOperation(StringsHelper.GetString(Strings.ADP_InvalidMixedUsageOfSecureCredentialAndIntegratedSecurity));
        }

        internal static ArgumentException InvalidMixedArgumentOfSecureCredentialAndIntegratedSecurity()
        {
            return Argument(StringsHelper.GetString(Strings.ADP_InvalidMixedUsageOfSecureCredentialAndIntegratedSecurity));
        }
        internal static InvalidOperationException InvalidMixedUsageOfAccessTokenAndIntegratedSecurity()
        {
            return ADP.InvalidOperation(StringsHelper.GetString(Strings.ADP_InvalidMixedUsageOfAccessTokenAndIntegratedSecurity));
        }
        static internal InvalidOperationException InvalidMixedUsageOfAccessTokenAndUserIDPassword()
        {
            return ADP.InvalidOperation(StringsHelper.GetString(Strings.ADP_InvalidMixedUsageOfAccessTokenAndUserIDPassword));
        }

        static internal InvalidOperationException InvalidMixedUsageOfAccessTokenAndAuthentication()
        {
            return ADP.InvalidOperation(StringsHelper.GetString(Strings.ADP_InvalidMixedUsageOfAccessTokenAndAuthentication));
        }

        static internal Exception InvalidMixedUsageOfCredentialAndAccessToken()
        {
            return ADP.InvalidOperation(StringsHelper.GetString(Strings.ADP_InvalidMixedUsageOfCredentialAndAccessToken));
        }
    }
}
