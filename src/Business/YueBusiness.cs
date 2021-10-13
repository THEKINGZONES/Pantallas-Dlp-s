using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Business
{
    public class YueBusiness
    {
        DA.Yue _YueConexion;
        DA.ConexionIBM _Conexion;
        DA.SqlConexion _ECO2_Log;

        public YueBusiness()
        {
            _YueConexion = new DA.Yue();           
            _Conexion = new DA.ConexionIBM("Eco2");
            _ECO2_Log = new DA.SqlConexion("ECO2_Log", true);
           
        }
        
        public YueGroupStation GetYueGroupStation(string GroupStation)
        {
            return _YueConexion.GetGroupStation(GroupStation); 
        }
        public DataTable YueHrxhrDecide(string estaciones,DateTime BeginDay,DateTime EndDay)
        {
            var GrupA = _YueConexion.GetGroupStation(estaciones);
            var query = _YueConexion.GetReport(DA.enumYueReport.HRXHRDECIDE);
            query = query.Replace("@Estaciones", GrupA.GetListString);
            query = query.Replace("@BeginDay", BeginDay.ToString("MM-dd-yyyy"));
            query = query.Replace("@EndDay", EndDay.ToString("MM-dd-yyyy"));
            var result = _ECO2_Log.GetDataTable(query);            
            return result;
        }  
        public DataTable BrkOpti(DateTime beginDate)
        {        
            var EndDate = beginDate.AddDays(1);
            var query = _YueConexion.GetReport(DA.enumYueReport.BRKPANTALLASHIST);
            query = query.Replace("@BeginDate", beginDate.ToString("MM-dd-yyyy"));
            query = query.Replace("@EndDate", EndDate.ToString("MM-dd-yyyy"));
            var result = _Conexion.GetDataTable(query);             
            return result;
        }

        public DataTable GetStationsInStations(string BeginStation,string EndStation)
        {
            var GrupA = _YueConexion.GetGroupStation(BeginStation);

            var query = _YueConexion.GetReport(DA.enumYueReport.STATIONSINSTATIONS);
            query = query.Replace("@StationsA", GrupA.GetListString);
            var GrupB = _YueConexion.GetGroupStation(EndStation);
            query = query.Replace("@StationsB", GrupB.GetListString);

            var result = _Conexion.GetDataTable(query);
            return result;

        }
        public DataTable DashBoard(DateTime beginDate)
        {         
            var EndDate = beginDate.AddDays(1);
            var query = _YueConexion.GetReport(DA.enumYueReport.LINEVALIDATIONTEST);
            query = query.Replace("@BeginDate", beginDate.ToString("MM-dd-yyyy"));
            query = query.Replace("@EndDate", EndDate.ToString("MM-dd-yyyy"));
            var Result = _ECO2_Log.GetDataTable(query);
            return Result;
        }            
    }
}
