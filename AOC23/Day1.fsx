open System

let input = IO.File.ReadAllLines "Day1.txt"

//part1
let findDigits (s: string) =
    s.ToCharArray()
    |> Array.fold (fun arr c -> if Char.IsDigit c then Array.append arr [|c|] else arr) Array.empty
    |> fun s ->
        match s.Length with
        |1 -> sprintf "%c" s[0]
        |2 -> sprintf "%c" s[1]
        |_ -> sprintf "%c" (Array.last s)
        |> sprintf "%c%s" s[0]
    |> Int32.Parse

input |> Array.map (fun s -> findDigits s) |> Array.sum |> printfn "Part 1: %i"

//part2
let numbers = [| "zero"; "one"; "two"; "three"; "four"; "five"; "six"; "seven"; "eight"; "nine" |]
let parse (s: byref<string>) =
    for i in 0..numbers.Length - 1 do
        for j in 0..s.Length - 1 do
            for k in j..s.Length - 1 do
                if s[j..k].Equals numbers[i] then
                    s <- s.Replace(s[j..k], s[j..k - 1] + i.ToString() + s[j + 1..k])

for i in 0..input.Length - 1 do parse &input[i]
input |> Array.map (fun s -> findDigits s) |> Array.sum |> printfn "Part 2: %i"