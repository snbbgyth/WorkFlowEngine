

namespace DBHelp
{
    using System.Collections.Generic;
    using System.Data;
    using CommonLibrary.Help;
    using MySql.Data.MySqlClient;
    using Oracle.DataAccess.Client;

    public static class DBUntilHelp
    {
        public static Dictionary<string, OracleDbType> OracleDataTypeMapsDic = new Dictionary<string, OracleDbType>
                                                                                  {
                                                                                      {"bfile",OracleDbType.BFile},
                                                                                      {"binary_double",OracleDbType.BinaryDouble},
                                                                                      {"binary_float",OracleDbType.BinaryFloat},
                                                                                      {"blob",OracleDbType.Blob},
                                                                                      {"char",OracleDbType.Char},
                                                                                      {"clob",OracleDbType.Clob},
                                                                                      {"date",OracleDbType.Date},
                                                                                      {"number",OracleDbType.Decimal},
                                                                                      //{"float",OracleDbType.Double},   8-byte FLOAT type
                                                                                      //{"integer",OracleDbType.Int16},  2-byte INTEGER type
                                                                                      //{"integer",OracleDbType.Int32},  4-byte INTEGER type
                                                                                      //{"integer",OracleDbType.Int64},  8-byte INTEGER type
                                                                                      {"interval day to second",OracleDbType.IntervalDS},
                                                                                      {"interval year to month",OracleDbType.IntervalYM},
                                                                                      {"long",OracleDbType.Long},
                                                                                      {"long raw",OracleDbType.LongRaw},
                                                                                      //{"mlslabel",OracleDbType},
                                                                                      {"nchar",OracleDbType.NChar},
                                                                                      {"nclob",OracleDbType.NClob},
                                                                                      {"nvarchar2",OracleDbType.NVarchar2},
                                                                                      {"raw",OracleDbType.Raw},
                                                                                      //{"rowid",OracleDbType},
                                                                                      //{"urowid",OracleDbType},
                                                                                      //{"float",OracleDbType.Single},   4-byte FLOAT type, supports 6 precisions
                                                                                      {"timestamp",OracleDbType.TimeStamp},
                                                                                      {"timestamp with local time zone",OracleDbType.TimeStampLTZ},
                                                                                      {"timestamp with time zone",OracleDbType.TimeStampTZ},
                                                                                      {"varchar2",OracleDbType.Varchar2},
                                                                                      {"xmltype",OracleDbType.XmlType}
                                                                                  };

        public static Dictionary<string, MySqlDbType> OracleTypeMapsMySqlDBTypeDic = new Dictionary<string, MySqlDbType>
                                                                                  {
                                                                                      //{"bfile",MySqlDbType.VarBinary},
                                                                                      //{"binary_double",MySqlDbType.Float},
                                                                                      //{"binary_float",MySqlDbType.Float},
                                                                                      {"blob",MySqlDbType.Blob},
                                                                                      {"char",MySqlDbType.VarChar},
                                                                                      {"clob",MySqlDbType.LongText},
                                                                                      {"date",MySqlDbType.DateTime},
                                                                                      {"number",MySqlDbType.Decimal},
                                                                                      //{"float",MySqlDbType.Double},   8-byte FLOAT type
                                                                                      //{"integer",MySqlDbType.Int16},  2-byte INTEGER type
                                                                                      //{"integer",MySqlDbType.Int32},  4-byte INTEGER type
                                                                                      //{"integer",MySqlDbType.Int64},  8-byte INTEGER type
                                                                                      //{"interval day to second",MySqlDbType},
                                                                                      //{"interval year to month",MySqlDbType},
                                                                                      {"long",MySqlDbType.LongText},
                                                                                      //{"long raw",MySqlDbType.VarBinary},
                                                                                      //{"mlslabel",MySqlDbType},
                                                                                      {"nchar",MySqlDbType.VarChar},
                                                                                      {"nclob",MySqlDbType.LongText},
                                                                                      {"nvarchar2",MySqlDbType.VarChar},
                                                                                      //{"raw",MySqlDbType.VarBinary},
                                                                                      //{"rowid",MySqlDbType.Guid},
                                                                                      //{"urowid",MySqlDbType.Guid},
                                                                                      //{"float",MySqlDbType.Float},   4-byte FLOAT type, supports 6 precisions
                                                                                      {"timestamp",MySqlDbType.Timestamp},
                                                                                      //{"timestamp with local time zone",MySqlDbType.Timestamp},
                                                                                      //{"timestamp with time zone",MySqlDbType.Timestamp},
                                                                                      {"varchar2",MySqlDbType.VarChar},
                                                                                      {"xmltype",MySqlDbType.Geometry}
                                                                                  };

        public static Dictionary<string, SqlDbType> OracleTypeMapsMsSqlDBTypeDic = new Dictionary<string, SqlDbType> //MSSQL2008
                                                                                 {
                                                                                        //{"bfile",SqlDbType.VarBinary },
                                                                                        //{"binary_double",SqlDbType.Float},
                                                                                        //{"binary_float",SqlDbType.Float},
                                                                                        {"blob",SqlDbType.Float},
                                                                                        {"char",SqlDbType.Char},
                                                                                        {"clob",SqlDbType.Text},
                                                                                        {"date",SqlDbType.DateTime2},
                                                                                        {"number",SqlDbType.Decimal},
                                                                                        {"float",SqlDbType.Float},
                                                                                        {"integer",SqlDbType.Int},
                                                                                        //{"interval day to second",SqlDbType},
                                                                                        //{"interval year to month",SqlDbType},
                                                                                        {"long",SqlDbType.Text},
                                                                                        //{"long raw",SqlDbType.VarBinary},
                                                                                        //{"mlslabel",SqlDbType},
                                                                                        {"nchar",SqlDbType.NChar},
                                                                                        {"nclob",SqlDbType.NText},
                                                                                        {"nvarchar2",SqlDbType.NVarChar},
                                                                                        //{"raw",SqlDbType.VarBinary},
                                                                                        {"rowid",SqlDbType.UniqueIdentifier},
                                                                                        {"urowid",SqlDbType.UniqueIdentifier},
                                                                                        {"timestamp",SqlDbType.DateTime2},
                                                                                        {"timestamp with local time zone",SqlDbType.DateTimeOffset},
                                                                                        {"timestamp with time zone",SqlDbType.DateTimeOffset},
                                                                                        {"varchar2",SqlDbType.VarChar},
                                                                                        {"xmltype",SqlDbType.Xml}
                                                                                  };

        public static OracleDbType GetOracleDBTypeByOracleType(string oracleType)
        {
            foreach (var dic in OracleDataTypeMapsDic)
            {
                if (dic.Key.CompareEqualIgnoreCase(oracleType))
                {
                    return dic.Value;
                }
            }
            return OracleDbType.NVarchar2;
        }
    }
}
