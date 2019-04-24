using System;
using System.Collections.Generic;
using System.Text;

namespace BoletoNetCore.Util
{
    public class DefaultOfPagination
    {
        public DefaultOfPagination(dynamic obj)
        {
            Resultado = obj;
        }

        public DefaultOfPagination()
        {
        }


        public bool Status { get; set; }
        public dynamic Resultado { get; set; }
        public string Descricao { get; set; }
    }
}
