open Prelude // by https://code.google.com/p/fsharp-typeclasses
open Control.Applicative

let playMaybeMonad () =
  let none = None
  let mRed = Some "red"
  let mGreen = Some "green"

  let plusColor = fun c1 c2 -> c1 + " " + c2

  let successResult = do' {
    let! red = mRed
    let! green = mGreen
    return plusColor red green
  }

  printfn "%A" successResult

  let failResult = do' {
    let! red = mRed
    let! none = none
    return plusColor red none
  }

  printfn "%A" failResult

// only fsharp
let playMaybeMonad_with_applicative_style () =
  let none = None
  let mRed = Some "red"
  let mGreen = Some "green"

  let plusColor = fun c1 c2 -> c1 + " " + c2

  // look like:
  // plusColor <| mred mGreen
  let successResult = plusColor <<|> mRed <*> mGreen

  printfn "%A" successResult

  let failResult = plusColor <<|> mRed <*> none

  printfn "%A" failResult

let playSeq () =
  // init the seq. 0, 1, 2, 3 ... inifinite
  let seqNums = Seq.initInfinite(id)

  let take10 = seqNums |> Seq.take 10
  let sum = take10 |> Seq.fold (fun s i -> s + i) 0

  printfn "sum is %A" sum

  let take10afterSkip10 = seqNums |> Seq.skip 10 |> Seq.take 10
  let result =
        take10 |> Seq.zip take10afterSkip10 |> Seq.map (function (left, right) -> left + right)

  printfn "%A" result

let playSeqMonad () =
  // init the seq. 1..5
  // same `seq [1..5]`
  let mNums1 = Seq.init 5 ((+) 1)
  // init the seq. 6..10
  // same `seq [6..10]`
  let mNums2 = Seq.init 5 ((+) 6)

  let multipleNumbers = fun n1 n2 -> n1 * n2

  let result = seq {
    for num1 in mNums1 do
      for num2 in mNums2 do
        yield multipleNumbers num1 num2
  }

  printfn "%A" result

[<EntryPoint>]
let main argv = 
  playMaybeMonad () 
  playMaybeMonad_with_applicative_style ()
  playSeq ()
  playSeqMonad ()
  0
