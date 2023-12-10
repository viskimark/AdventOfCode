open System

let input =
    IO.File.ReadAllLines "Day2.txt"
    |> Array.map (fun line ->
        ((line.Split ": ")[1]).Split ([|"; "; ", "|], StringSplitOptions.None)
        |> Array.map (fun s ->
            let cubes = s.Split " "
            Int32.Parse cubes[0], cubes[1]
        )
    )

//part1
input
|> Array.map (fun arr ->
    arr
    |> Array.fold (fun b (num, color) ->
        if b then
            match color with
            |"red" -> not (num > 12)
            |"green" -> not (num > 13)
            |"blue" -> not (num > 14)
        else b
    ) true
)
|> Array.indexed |> Array.fold (fun sum (i, b) -> if b then sum + i + 1 else sum ) 0
|> printfn "Part 1: %i"

//part2
input
|> Array.fold (fun sum game ->
    game
    |> Array.fold (fun (r, g, b) (num, color) ->
        match color with
        |"red" ->
            if num > r then (num, g, b) else (r, g, b)
        |"green" ->
            if num > g then (r, num, b) else (r, g, b)
        |_ ->
            if num > b then (r, g, num) else (r, g, b)
    ) (0, 0, 0)
    |> fun (r, g, b) -> r * g * b + sum
) 0
|> printfn "Part 2: %i"