open System

let input = IO.File.ReadAllLines "Day1.txt" |> Array.map (fun x -> Int32.Parse x)

//part1
let countGreaterPairs arr =
    arr
    |> Array.pairwise
    |> Array.fold (fun c (p,x) -> if p < x then c + 1 else c) 0

countGreaterPairs input |> printfn "Part 1: %i"

//part2
input
|> Array.mapi (fun i x -> if i < input.Length - 2 then [|x; input[i + 1]; input[i + 2]|] else [||])
|> Array.filter (fun x -> x.Length = 3)
|> Array.map (fun x -> Array.sum x)
|> countGreaterPairs
|> printfn "Part 2: %i"
