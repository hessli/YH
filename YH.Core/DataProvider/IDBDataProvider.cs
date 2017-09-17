using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace YH.Core.DataProvider
{
    public  interface IDBDataProvider:ICloneable
    {
        bool Exists(string strSql, params IDataParameter[] cmdParms);
        bool TabExists(string TableName);
        int GetMaxID(string FieldName, string TableName);

        bool Exists(string strSql);

        int ExecuteSql(string SQLString);

        int ExecuteSqlByTime(string SQLString, int Times);

        void ExecuteSqlTran(ArrayList SQLStringList);

        int ExecuteSqlTran(List<String> SQLStringList);

        int ExecuteSql(string SQLString, string content);

        object ExecuteSqlGet(string SQLString, string content);

        int ExecuteSqlInsertImg(string strSQL, byte[] fs);

        object GetSingle(string SQLString);

        object GetSingle(string SQLString, int Times);
         IDataReader ExecuteReader(string strSQL);

        DataSet Query(string SQLString);

        DataSet Query(string SQLString, int Times);


        DataSet Query(string SQLString, int startRecord, int maxRecord, string srcTable);


        int ExecuteSql(string SQLString, params IDataParameter[] cmdParms);


        void ExecuteSqlTran(Hashtable SQLStringList);


        void ExecuteSqlTran(System.Collections.Generic.List<CommandInfo> SQLStringList);


        void ExecuteSqlTranWithIndentity(System.Collections.Generic.List<CommandInfo> SQLStringList);


        IDataReader ExecuteReader(string SQLString, params IDataParameter[] cmdParms);

        object GetSingle(string SQLString, params IDataParameter[] cmdParms);

        DataSet Query(string SQLString, params IDataParameter[] cmdParms);

        DataSet Query(string SQLString, int startRecord, int maxRecord, string srcTable, params IDataParameter[] cmdParms);


        IDataReader RunProcedure(string storedProcName, IDataParameter[] parameters);

        DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName);

        DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName, int Times);

        DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, int startRecord, int maxRecord, string tableName);


        int RunProcedure(string storedProcName, IDataParameter[] parameters, out int rowsAffected);


        DataSet Query(IDbCommand cmd, IDataParameter[] parameters);

        object GetSingle(IDbCommand cmd, IDataParameter[] parameters);

        ArrayList GetArrayList(IDbCommand cmd, IDataParameter[] parameters);

        int ExecuteTran(IDbCommand cmd, IDataParameter[] parameters);

        int ExecuteTranByUpdate(IDbCommand cmd, IDataParameter[] parameters);

        int ExecuteTrans(IDbCommand cmd, IDataParameter[] parameters);

        int ExecuteTranNO(IDbCommand cmd, IDataParameter[] parameters);

    }
}
