using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExemploAlbertoFeres.MVC.Model
{
    public class Pessoa
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        //public string ImageBase64 { get; set; }
    }
}
