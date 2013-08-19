open Prelude


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

[<EntryPoint>]
let main argv = 
  playMaybeMonad () 
  0
