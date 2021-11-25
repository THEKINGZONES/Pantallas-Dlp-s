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
       public DataTable HrxHr(string GroupStation, DateTime Day)
        {
            
            var queryShift = _YueConexion.GetReport(DA.enumYueReport.GETSHIFT);
            var resultShift = _Sql.GetDataRow(queryShift);
            var YueStations = _YueConexion.GetGroupStation(GroupStation);

            var query = _YueConexion.GetReport(DA.enumYueReport.HRXHR);
            query = query.Replace("@Stations", YueStations.GetListString);
            query = query.Replace("@Day", string.Format("'{0}'", Day.ToString("MM-dd-yyyy")));
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
        public DataTable LeadTimeSurfacing()
        {
            var Day = DateTime.Now.AddDays(-2);
            var Hour = DateTime.Now;            
           var query =_YueConexion.GetReport(DA.enumYueReport.REPORTLEADTIME);
            query = query.Replace("@Day", string.Format("{0}", Day.ToString("yyyy-MM-dd")));
            query = query.Replace("@Hour", string.Format("{0}", Hour.ToString("HH:mm:ss")));
            query = query.Replace("@Hou2", string.Format("{0}", Hour.AddHours(1).ToString("HH:mm:ss")));
            query = query.Replace("@InitialStation", "6151,6152");
            query = query.Replace("@SURFACING", "6091,6020,6021,8010,8011,8012,8013,8014,8015,8016,8017,8018,8019,8020,8021,8022,8023,8024,8025,8026,8027,8030,8031,8032,8033,8034,8035," +
                "6711, 6712, 6713, 6714, 6715, 6716, 6717, 6718, 6719, 6720, 6721, 6722, 6723, 6724, 6725, 6726, 6727, 6728, 6729, 6730, 6731, 6732, 6733," +
                "8081, 8082, 8083, 8084, 8085, 8086, 8087, 8088, 8089, 8090, 8091, 8092, 8093, 8094, 8095, 8096, 8097, 8098, 8099, 8100, 8101, 8102, 8103," +
                "6311, 6312, 6313, 6314, 6315," +
                "6316, 6317, 6318, 6319, 6320," +
                "6321, 6322, 6323, 6324, 6325," +
                "6326, 6327, 6328, 6329, 6330," +
                "6331, 6332, 6333, 6334, 6335," +
                "6336, 6337, 6338, 6339, 6340," +
                "6341, 6342, 6343, 6344, 6345," +
                "6346, 6347, 6348, 6349, 6350," +
                "6351, 6352, 6353, 6354, 6355," +
                "6356, 6357, 6358, 6359, 6360," +
                "6361, 6362, 6363, 6364, 6365," +
                "6366, 6367, 6368, 6369, 6370," +
                "6371, 6372, 6373, 6374, 6375," +
                "6376, 6377, 6378, 6379, 6380," +
                "6381, 6382, 6383, 6384, 6385," +
                "6386, 6387, 6388, 6389, 6390," +
                "6410, 6411, 6412, 6413, 6414," +
                "6415, 6416, 6417, 6418, 6419," +
                "8078, 8079, 8080, 8077," +
                "6043, 8231, 6050, 6080, 6097, 2311," +
                "8233, 6820, 6821, 6822," +
                "6071, 6072");
          
            var result = _Conexion.GetDataTable(query);
            return result;
        }
       
    }
    
}
