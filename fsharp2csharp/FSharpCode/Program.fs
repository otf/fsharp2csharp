open Prelude
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
    let! green = mGreen
    let! none = none
    return plusColor red green
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

[<EntryPoint>]
let main argv = 
  playMaybeMonad () 
  playMaybeMonad_with_applicative_style ()
  0
