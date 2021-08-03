using System;
using System.Collections.Generic;
using System.Text;
using Model;

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

        public YueGroupStation GetGroupStation(string GroupStation)
        {
            var query = GetReport(enumYueReport.GETGROUP);
            query = query.Replace("@GroupStation",string.Format("'{0}'",GroupStation));
            var Dr = _sqlConexion.GetDataRow(query);
            var result = new YueGroupStation()
            {
                Code = GroupStation,
                Name = Dr["Name"].ToString(),
                Meta = Convert.ToInt32(Dr["Meta"]),
                Stations = Dr["Stations"].ToString()
            };


            return result;
        }
    }
    public enum enumYueReport
    {
        R06,
        R05,
        P04,
        GETSHIFT,
        GETGROUP,
        STATIONSINSTATIONS,
        R_18,
        REPORTTEST,
        HRXHRDECIDE


    }


}
