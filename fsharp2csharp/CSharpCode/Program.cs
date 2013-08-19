using System;
using System.Collections.Generic;
// using System.Linq;
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

            Func<string, string, string> plusColor = (c1, c2) => c1 + " " + c2;

            // can't do that:
            // plusColor(mRed, mGreen);
            var successResult =
                    from red in mRed
                    from green in mGreen
                    select plusColor(red, green);

            Console.WriteLine(successResult);

            var failResult =
                from red in mRed
                from none in (Option<string>)mNone
                select plusColor(red, none);

            Console.WriteLine(failResult);
        }

        public static void PlaySeq()
        {
            // init the seq. 0, 1, 2, 3 ... inifinite
            var seqNums = Seq.InitInfinite(Func.Id);

            var take10 = seqNums.Take(10);
            var sum = take10.Fold(0, (s, i) => s + i);

            Console.WriteLine("sum is " + sum);

            var take10afterSkip10 = seqNums.Skip(10).Take(10);
            var result =
                take10.Zip(take10afterSkip10).Map(tup => tup.Match((left, right) => left + right));

            Console.WriteLine(result.ToStr());
        }


        static void Main(string[] args)
        {
            PlayMaybeMonad();
            PlaySeq();
        }
    }
}
