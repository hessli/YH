using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YH.Core.Comm;
using YH.Core.DataProvider;
using YH.Core.Logs;
using YH.Service.DataProvider;

namespace YH.Service.Logs
{
    public class LogDatabaseService : IYHLog
    {
        private IDBDataProvider _dbDataProvider;

        private SqlProviderType _providerType;

        public event NotificationHandler Notification;


        public LogDatabaseService(IDBDataProvider dataProvider)
        {
            _dbDataProvider = dataProvider;
            var currentType = dataProvider.GetType();
            if (currentType == typeof(MySqlDataProvider))
            {
                _providerType = SqlProviderType.Mysql;
            }

            else if (currentType == typeof(SqlServerDataProvider))
            {
                _providerType = SqlProviderType.SQLServer;

            }
            else
            {
                throw new NotSupportedException("不支持的数据库类型");
            }
        }

     

        public void Error(ILogParatmer paramter)
        {
            LogDatabaseSQLCommdInfo sql = new LogDatabaseSQLCommdInfo(SqlProviderType.Mysql, paramter.Type);

            var commdInfo = sql.GetCommdInfo(paramter);

            _dbDataProvider.ExecuteSql(commdInfo.CommandText,commdInfo.Parameters);

            if (Notification != null)
            {
                OnNotification(paramter);
            }

        }

        public void ErrorAsych(ILogParatmer paramter)
        {
           var  dataProvider= _dbDataProvider.Clone();

            Task.Run(()=> {

                try
                {
                    LogDatabaseSQLCommdInfo sql = new LogDatabaseSQLCommdInfo(_providerType, paramter.Type);

                    var commdInfo = sql.GetCommdInfo(paramter);

                    ((IDBDataProvider)dataProvider).ExecuteSql(commdInfo.CommandText, commdInfo.Parameters);

                    if (Notification != null)
                    {
                        OnNotification(paramter);
                    }
                }
                catch (AggregateException ex) {

                    var e = ex;

                }
            });

        }

        public void Info(ILogParatmer paramter)
        {
            LogDatabaseSQLCommdInfo sql = new LogDatabaseSQLCommdInfo(_providerType, paramter.Type);

            var commdInfo = sql.GetCommdInfo(paramter);

            _dbDataProvider.ExecuteSql(commdInfo.CommandText, commdInfo.Parameters);

            if (Notification != null)
            {
                OnNotification(paramter);
            }
        }

        public void InfoAsych(ILogParatmer paramter)
        {
            var dataProvider = _dbDataProvider.Clone();

            Task.Run(() => {
                try
                {
                    LogDatabaseSQLCommdInfo sql = new LogDatabaseSQLCommdInfo(_providerType, paramter.Type);

                    var commdInfo = sql.GetCommdInfo(paramter);

                    ((IDBDataProvider)dataProvider).ExecuteSql(commdInfo.CommandText, commdInfo.Parameters);

                    if (Notification != null)
                    {
                        OnNotification(paramter);
                    }
                }
                catch (AggregateException exception)
                {
                    var e = exception;
                }
            });
        }

        private void OnNotification(object value)
        {

            if (this.Notification != null)
            {
                this.Notification(this, value);
            }
        }

        private class LogDatabaseSQLCommdInfo
        {
            SqlProviderType _providerType;

            LogType _logType;

            string tableName = string.Empty;


            public LogDatabaseSQLCommdInfo(SqlProviderType providerType, LogType type)
            {
                _providerType = providerType;

                _logType = type;

                switch (_logType)
                {
                    case LogType.调用第三方接口日志:
                        tableName = "request_remoting_logs";
                        break;
                    default:
                        tableName = "system_logs";
                        break;
                }
            }
            public CommandInfo GetCommdInfo(ILogParatmer paramter)
            {
                var sqlBuilder = new StringBuilder();

                var commdInfo = new CommandInfo();

                sqlBuilder.AppendFormat("insert into {0} paramters,excuteResult,requestTime,responseTime,runTime,module,excuteObject,level ", tableName);

                sqlBuilder.Append("values(@paramters,@excuteResult,@requestTime,@responseTime,@runTime,@module,@excuteObject,@level)");

                commdInfo.CommandText = sqlBuilder.ToString();

                switch (_providerType)
                {
                    case SqlProviderType.Mysql:
                        commdInfo.Parameters = new MySql.Data.MySqlClient.MySqlParameter[] {
                            new MySql.Data.MySqlClient.MySqlParameter { ParameterName="@paramters",Value=paramter.Paramters },
                            new MySql.Data.MySqlClient.MySqlParameter { ParameterName="@excuteResult",Value=paramter.ExcuteResult },
                            new MySql.Data.MySqlClient.MySqlParameter { ParameterName="@requestTime",Value=paramter.RequestTime },
                            new MySql.Data.MySqlClient.MySqlParameter { ParameterName="@responseTime",Value=paramter.ResponseTime },
                            new MySql.Data.MySqlClient.MySqlParameter { ParameterName="@runTime",Value=paramter.RunTime },
                            new MySql.Data.MySqlClient.MySqlParameter { ParameterName="@module",Value=paramter.Module },
                            new MySql.Data.MySqlClient.MySqlParameter { ParameterName="@excuteObject",Value=paramter.ExcuteObject },
                            new MySql.Data.MySqlClient.MySqlParameter { ParameterName="@level",Value=paramter.Level }
                        };
                        break;
                    case SqlProviderType.SQLServer:
                        commdInfo.Parameters = new SqlParameter[] {
                             new SqlParameter { ParameterName="@paramters",Value=paramter.Paramters },
                            new SqlParameter { ParameterName="@excuteResult",Value=paramter.ExcuteResult },
                            new SqlParameter { ParameterName="@requestTime",Value=paramter.RequestTime },
                            new SqlParameter { ParameterName="@responseTime",Value=paramter.ResponseTime },
                            new SqlParameter { ParameterName="@runTime",Value=paramter.RunTime },
                            new SqlParameter { ParameterName="@module",Value=paramter.Module },
                            new SqlParameter{ ParameterName="@excuteObject",Value=paramter.ExcuteObject },
                            new SqlParameter{ ParameterName="@level",Value=paramter.Level }

                        };
                        break;
                }
                return commdInfo;
            }
        }
    }
}
