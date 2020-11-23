using System;
using System.Data;

namespace Business
{

    public class TvBusiness
    {
        DA.ConexionIBM _Conexion;
        DA.Yue _YueConexion;
        DA.SqlConexion _Sql;
        public TvBusiness()
        {
            _Conexion = new DA.ConexionIBM("Eco2");
            _YueConexion = new DA.Yue();
            _Sql = new DA.SqlConexion("TrayTrack", true);
        }

        
        public DataTable brkDlps()
        {
            string query = "SELECT LIMIT 5 * FROM TRAY ";
            var result = _Conexion.GetDataTable(query);
            return result;
        }
        public DataTable ListStations()
        {
            var query = _YueConexion.GetReport(DA.enumYueReport.R05);
            var result = _Conexion.GetDataTable(query);
            return result;
        }
        public DataTable HrxHr(string GroupStation,DateTime BeginDay, DateTime EndDay)
        {
            var queryShift = _YueConexion.GetReport(DA.enumYueReport.GETSHIFT);
            var resultShift = _Sql.GetDataRow(queryShift);
            var YueStations = _YueConexion.GetGroupStation(GroupStation);

            var query = _YueConexion.GetReport(DA.enumYueReport.P04);
            query = query.Replace("@Stations", YueStations.GetListString);
            query = query.Replace("@BEGINDAY", string.Format("'{0} {1}'", BeginDay.ToString("yyyy-MM-dd"), resultShift["ShiftStart"].ToString()));
            query = query.Replace("@ENDDAY", string.Format("'{0} {1}'", EndDay.ToString("yyyy-MM-dd"), resultShift["ShiftEnd"].ToString()));



            var result = _Conexion.GetDataTable(query);
            return result;
        }
    }

}
