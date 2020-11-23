using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business
{
    public class YueBusiness
    {
        DA.Yue _YueConexion;
        public YueBusiness()
        {
            _YueConexion = new DA.Yue();


        }
        public YueGroupStation GetYueGroupStation(string GroupStation)
        {
            return _YueConexion.GetGroupStation(GroupStation);
        }

    }
}
