using System;
using System.Collections.Generic;
using System.Text;

namespace DA
{
   public class Yue
    {
        SqlConexion _sqlConexion;
        public Yue()
        {
            _sqlConexion = new SqlConexion("Yue", true);
        }
        public string GetReport(enumYueReport report)
        {
            string result = "";
            var query = string.Format("SELECT QUERY FROM REPORTS WHERE CODE = '{0}'", report.ToString());
            result = _sqlConexion.ExecuteScalar(query).ToString();
            return result;
        }

    }
    public enum enumYueReport
    {
        R06, 
        R05,
            DLPSPANTALLAS_JUAN



    }
}
