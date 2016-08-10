using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlchemyLanguage;

namespace ConsoleDevApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            AlchemyLanguageService service = new AlchemyLanguageService("0ea4a12f30bf7745d366f69a95deff2c478d6257");

            var response =
                service.GetSentiment(
                    "Holy hell, these guys are killing me. I emailed them a pic with the part I needed and had to call back a week later to remind them to send it to me because they forgot." +
                    " When the part was finally supposed to arrive (the second time) I got the email confirmation that the part had been shipped....." +
                    "so after almost three weeks of waiting the big day arrives and it's the wrong part." +
                    "Thanks for going above and beyond guys. God in Heaven.");

            Console.WriteLine(response.ToString());

            Console.ReadLine();
        }
    }
}
