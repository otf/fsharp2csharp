using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LangExt;

namespace CSharpCode
{
    class Program
    {
        public static void PlayMaybeMonad()
        {
            var mNone = Option.None;
            var mRed = Option.Some("red");
            var mGreen = Option.Some("green");

            Func<string, string, string> plusColor = (color1, color2) => color1 + " " + color2;

            // can't
            // plusColor(mRed, mGreen);
            var successResult =
                    from red in mRed
                    from green in mGreen
                    select plusColor(red, green);

            Console.WriteLine(successResult);

            var failResult =
                from red in mRed
                from green in mGreen
                from none in mNone
                select plusColor(red, green);

            Console.WriteLine(failResult);
        }


        static void Main(string[] args)
        {
            PlayMaybeMonad();
        }
    }
}
