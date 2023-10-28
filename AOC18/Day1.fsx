open System
open System.Collections.Generic

let input = IO.File.ReadAllLines "Day1.txt" |> Seq.map (fun s -> Int32.Parse s)

//Part1
input
|> Seq.sum
|> printfn "Part 1: %i"

//Part2
seq { while true do yield! input }
|> Seq.scan (fun (sum, set) num ->
    sum + num, Set.add sum set) (0, Set.empty)
|> Seq.find (fun (sum,set) -> Set.contains sum set)
|> fst |> printfn "Part 2: %i"