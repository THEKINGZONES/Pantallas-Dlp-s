using System;
using System.Data;

namespace Business
{
    
    public class TvBusiness
    {
        DA.ConexionIBM _Conexion;
        DA.Yue _YueConexion;
        DA.SqlConexion _Sql;
        DA.SqlConexion _Decide;
        public TvBusiness()
        {
            _Conexion = new DA.ConexionIBM("Eco2");
            _YueConexion = new DA.Yue();
            _Sql = new DA.SqlConexion("TrayTrack", true);
            _Decide = new DA.SqlConexion("ECO2_Log", true);
        }       


        public DataTable DefectsDecide()
        {
            var query = _YueConexion.GetReport(DA.enumYueReport.R_18);
            var result = _Decide.GetDataTable(query);
            return result;

        }
       public DataTable HrxHr(string GroupStation,DateTime BeginDay, DateTime EndDay)
        {
            
            var queryShift = _YueConexion.GetReport(DA.enumYueReport.GETSHIFT);
            var resultShift = _Sql.GetDataRow(queryShift);
            var YueStations = _YueConexion.GetGroupStation(GroupStation);

            var query = _YueConexion.GetReport(DA.enumYueReport.HRXHR);
            query = query.Replace("@Stations", YueStations.GetListString);
            query = query.Replace("@Day", string.Format("'{0}'", EndDay.ToString("MM-dd-yyyy")));
            //query = query.Replace("@BEGINDAY", string.Format("'{0} {1}'", BeginDay.ToString("yyyy-MM-dd"), resultShift["ShiftStart"].ToString()));
            //query = query.Replace("@ENDDAY", string.Format("'{0} {1}'", EndDay.ToString("yyyy-MM-dd"), resultShift["ShiftEnd"].ToString()));

            var result = _Conexion.GetDataTable(query);
            return result;
        }       

        public DataTable HRXHRAR(string GroupStation)
        {
            var YueStations = _YueConexion.GetGroupStation(GroupStation);

            var query = _YueConexion.GetReport(DA.enumYueReport.HRXHRAR);
            query = query.Replace("@Stations", YueStations.GetListString);          



            var result = _Conexion.GetDataTable(query);
            return result;
        }
       
    }

}
