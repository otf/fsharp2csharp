using System;
using System.Collections.Generic;
// using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LangExt; // install by the nuget (search "langext")

namespace CSharpCode
{
    class Program
    {
        public static void PlayFunction()
        {
            // id : a -> a
            Console.WriteLine(Func.Id("john")); // "john"

            // string * int -> string
            Func<string, int, string> join = (x, i) => String.Join(" ", Seq.Repeat(i, x));

            Console.WriteLine(join("john", 3)); // "john john john"

            // string -> int -> string
            Func<string, Func<int, string>> curriedJoin = Func.Curry(join);
            Console.WriteLine(curriedJoin("john")(3)); // "john john john"

            // string * int -> string
            Func<string, int, string> uncurriedJoin = Func.Uncurry(curriedJoin);
            Console.WriteLine(uncurriedJoin("john", 3)); // "john john john"

            // int -> string -> string
            Func<int, Func<string, string>> flipedJoin = Func.Flip(curriedJoin);
            Console.WriteLine(flipedJoin(3)("john")); // "john john john"
        }

        public static void PlayMaybeBindAndReturn()
        {
            var mRed = Option.Some("red");

            Func<string, string> addBrane = c => "[" + c + "]";

            // bind : M a -> (a -> M b) -> M b
            // return : a -> M a
            //
            // when M is Maybe, return means Some function.
            var result = mRed.Bind(red => Option.Some(addBrane(red)));
            Console.WriteLine(result);
        }

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

        public static void PlaySeqBindAndReturn()
        {
            var mColors = new []{"red" , "green", "blue"}.ToSeq();

            Func<string, string> addBrane = c => "[" + c + "]";

            // bind : M a -> (a -> M b) -> M b
            // return : a -> M a
            //
            // when M is Seq, return means Singleton function.
            var result = mColors.Bind(red => Seq.Singleton((addBrane(red))));
            Console.WriteLine(result);
        }

        public static void PlaySeqMonad()
        {
            // init the seq. 1..5
            var mNums1 = Seq.Init(5, i => i + 1);
            // init the seq. 6..10
            var mNums2 = Seq.Init(5, i => i + 6);

            Func<int, int, int> multipleNumber = (n1, n2) => n1 * n2;

            // can't do that:
            // multipleNumber(seqNums1, seqNums2);
            var result =
                    from num1 in mNums1
                    from num2 in mNums2
                    select multipleNumber(num1, num2);

            Console.WriteLine(result);
        }


        static void Main(string[] args)
        {
            PlayFunction();
            PlayMaybeBindAndReturn();
            PlayMaybeMonad();
            PlaySeq();
            PlaySeqBindAndReturn();
            PlaySeqMonad();
        }
    }
}
