using System;
using System.Data;

namespace Business
{

    public class TvBusiness
    {
        DA.ConexionIBM _Conexion;
        DA.Yue _YueConexion;
        public TvBusiness()
        {
            _Conexion = new DA.ConexionIBM("Eco2");
            _YueConexion = new DA.Yue();
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
        public DataTable HrxHr(int stationcode,DateTime BeginDay, DateTime EndDay)
        {
            var query = _YueConexion.GetReport(DA.enumYueReport.DLPSPANTALLAS_JUAN);
            query = query.Replace("@stationcode", stationcode.ToString());
            query = query.Replace("@BeginDay", string.Format("'{0}'", BeginDay.ToString("MM-dd-yyyy")));
            query = query.Replace("@EndDay", string.Format("'{0}'", EndDay.ToString("MM-dd-yyyy")));

            var result = _Conexion.GetDataTable(query);
            return result;
        }


    }

}
