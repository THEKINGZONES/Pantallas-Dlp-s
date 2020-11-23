using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
   public class YueGroupStation
    {
        
            public string Code { get; set; }
            public string Name { get; set; }
            public string Area { get; set; }
            public double Meta { get; set; }
            public string Stations { get; set; }
            public int Type { get; set; }
            public string Operation { get; set; }
            public int Sec { get; set; }
            public bool VisibleHrxHr { get; set; }
            public string GroupAssign { get; set; }
            /// <summary>
            /// Get All Stations
            /// </summary>
            public virtual List<int> GetToList
            {
                get
                {
                    var result = new List<int>();
                    var group = GetByGroup();
                    foreach (var gp in group)
                    {
                        foreach (var item in gp.GetToList)
                        {
                            result.Add(item);
                        }
                    }
                    return result;
                }
            }
            public virtual string GetListString
            {
                get
                {
                    string result = string.Empty;
                    var group = GetByGroup();
                    foreach (var gp in group)
                    {
                        result += string.Format("{0},", gp.GetStation);
                    }
                    if (result.Length > 0 && result[result.Length - 1] == ',')
                        result = result.Substring(0, result.Length - 1);
                    return result;
                }
            }
        public virtual List<GroupVM> GetByGroup()
        {
            List<GroupVM> lst = new List<GroupVM>();
            try
            {
                lst = JsonConvert.DeserializeObject<List<GroupVM>>(Stations);
            }
            catch (Exception)
            {
            }
            return lst;
        }
        
    }
    public class GroupVM
    {
        public string Name { get; set; }
        public string Stations { get; set; }
        public bool MetaIsbyGroup { get; set; }
        public virtual List<int> GetToList
        {
            get
            {
                var result = new List<int>();
                string[] lst = Stations.Split(',', '[', ']', '(', ')', '+', '-', '/', '*');
                int val = 0;
                foreach (var item in lst)
                {
                    if (Int32.TryParse(item, out val))
                        result.Add(val);
                }
                return result;
            }
        }
        public virtual string GetStation
        {
            get
            {
                var result = string.Empty;
                string[] lst = Stations.Split(',', '[', ']', '(', ')', '+', '-', '/', '*');
                int val = 0;
                foreach (var item in lst)
                {
                    if (Int32.TryParse(item, out val))
                        result += val + ",";
                }
                if (result[result.Length - 1] == ',')
                    result = result.Substring(0, result.Length - 1);
                return result;
            }
        }
    }
}
