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

        public YueBusiness()
        {
            _YueConexion = new DA.Yue();           
            _Conexion = new DA.ConexionIBM("Eco2");
           
        }
        
        public YueGroupStation GetYueGroupStation(string GroupStation)
        {
            return _YueConexion.GetGroupStation(GroupStation); 
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
    }
}
