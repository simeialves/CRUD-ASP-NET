using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_standby.Models
{
    public class Cliente
    {
        private int id;
        private string razao_social;
        private string cnpj;
        private DateTime data_fundacao;
        private decimal capital;
        private bool quarentena;
        private bool status_cliente;
        private char classificacao;

        public int Id { get => id; set => id = value; }
        public string Razao_social { get => razao_social; set => razao_social = value; }
        public string Cnpj { get => cnpj; set => cnpj = value; }
        public DateTime Data_fundacao { get => data_fundacao; set => data_fundacao = value; }
        public decimal Capital { get => capital; set => capital = value; }
        public bool Quarentena { get => quarentena; set => quarentena = value; }
        public bool Status_cliente { get => status_cliente; set => status_cliente = value; }
        public char Classificacao { get => classificacao; set => classificacao = value; }
    }
}
