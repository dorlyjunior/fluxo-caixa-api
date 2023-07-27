using System;

namespace FluxoCaixa.Projeto.Biblioteca.Especificacoes
{
    public class DiaDaSemanaEspecificacao
    {
        public static string RecuperarDiaDaSemana(DateTime data)
        {
            var diaDaSemana = (int)data.DayOfWeek;
            
            switch (diaDaSemana)
            {
                case 0:
                    return "Domingo";
                case 1:
                    return "Segunda-feira";
                case 2:
                    return "Terça-feira";
                case 3:
                    return "Quarta-feira";
                case 4:
                    return "Quinta-feira";
                case 5:
                    return "Sexta-feira";
                case 6:
                    return "Sábado";
                default:
                    return "";
            }
        }
    }
}
