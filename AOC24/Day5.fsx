#nowarn 25
let validate rules list =
    let rec loop (head :: tail) sorted valid =
        if tail.IsEmpty
        then sorted |> List.distinct, valid
        else
            Map.tryFind head rules
            |> function
            | Some rules ->
                let l, r = sorted |> List.partition (fun num -> List.contains num rules)
                let valid = if tail |> List.exists (fun num -> List.contains num rules) then false else valid
                loop tail (l @ [ head ] @ r) valid
            | None -> loop tail sorted valid

    loop list list true

let sumByMiddle (lists: list<int list * bool>) =
    let fst, snd = lists |> List.partition snd
    fst |> List.map (fun (list, _) -> list[list.Length / 2]) |> List.sum,
    snd |> List.map (fun (list, _) -> list[list.Length / 2]) |> List.sum

System.IO.File.ReadAllText "Day5.txt"
|> fun str -> str.Split "\r\n\r\n"
|> fun arr -> arr[0].Split "\r\n" |> List.ofArray, arr[1].Split "\r\n" |> List.ofArray
|> fun (rules, orders) ->
    let rules =
        rules |> List.map (fun rule ->
            let split = rule.Split '|'
            split[0] |> int, split[1] |> int
        )
        |> List.groupBy fst |> List.map (fun (k, v) -> k, v |> List.map snd) |> Map

    orders |> List.map (fun str -> str.Split ',' |> List.ofArray |> List.map int |> List.rev)
    |> List.map (validate rules)
    |> sumByMiddle |> fun (fst, snd) -> printfn "Part 1: %i\nPart 2: %i" fst snd