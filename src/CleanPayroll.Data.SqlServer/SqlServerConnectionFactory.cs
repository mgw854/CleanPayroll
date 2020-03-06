using System.Data.SqlClient;
using System.Globalization;
using System.Threading.Tasks;

namespace CleanPayroll.Data.SqlServer
{
  public sealed class SqlServerConnectionFactory
  {
    private readonly SqlConnectionStringBuilder _builder = new SqlConnectionStringBuilder();

    public SqlServerConnectionFactory(string host, ushort port, string username, string password, string database)
    {
      _builder.DataSource = host + "," + port.ToString(CultureInfo.InvariantCulture);
      _builder.UserID = username;
      _builder.Password = password;
      _builder.InitialCatalog = database;
    }

    internal async Task<SqlConnection> OpenConnectionAsync()
    {
      SqlConnection conn = new SqlConnection(_builder.ToString());
      await conn.OpenAsync();
      return conn;
    }
  }
}
