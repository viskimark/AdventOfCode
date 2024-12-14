type Level = Increasing of int list | Decreasing of int list
type Report = Safe | Unsafe | Continue of Level
let checkLevel (head :: (head' :: tail)) =
    match head' - head with
    | incr when incr >= 1 && incr <= 3 -> head, head' :: tail |> Increasing |> Continue
    | decr when decr <= -1 && decr >= -3 -> head, head' :: tail |> Decreasing |> Continue
    | _ -> head, Unsafe

let scanLevel (head :: tail) =
    let deconstructList fn ret =
        function
        | head :: tail -> fn head tail
        | [] -> ret

    let rec inner (prev, report) =
        match report with
        | Continue list ->
            match list with
            | Increasing list ->
                list |> deconstructList (fun head tail ->
                    if head - prev >= 1 && head - prev <= 3 then (head, Increasing tail |> Continue) |> inner else Unsafe
                ) Safe
            | Decreasing list ->
                list |> deconstructList (fun head tail ->
                    if prev - head >= 1 && prev - head <= 3 then (head, Decreasing tail |> Continue) |> inner else Unsafe
                ) Safe
        | _ -> report

    (head :: tail) |> checkLevel |> inner

System.IO.File.ReadAllLines "Day2.txt"
|> Array.map (fun level -> level.Split ' ' |> Array.map int |> List.ofArray) |> Array.map scanLevel
|> Array.filter (fun report -> report = Safe)
|> Array.length |> printfn "Part 1: %i"