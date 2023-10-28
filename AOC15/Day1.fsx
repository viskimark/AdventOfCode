open System

let getFloor floor c =
    match c with
    |'(' -> floor + 1
    |')' -> floor - 1

let input = (IO.File.ReadAllText "Day1.txt").ToCharArray()

//Part1
input |> Array.fold (fun floor c -> getFloor floor c) 0
|> printfn "Part 1: %i"

//Part2
input |> Array.fold (fun (floor, i) c ->
    if floor >= 0 then
        let newFloor = getFloor floor c
        if newFloor < 0 then newFloor, i
        else newFloor, i + 1
    else floor, i) (0, 1)
|> snd |> printfn "Part 2: %i"