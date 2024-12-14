let lList, rList =
    System.IO.File.ReadAllLines "Day1.txt"
    |> Array.fold (fun (lList, rList) line ->
        line.Split "   "
        |> fun arr -> (arr[0] |> int) :: lList, (arr[1] |> int) :: rList
    ) ([], [])

//part1
List.zip (lList |> List.sort) (rList |> List.sort)
|> List.fold (fun res (l, r) -> System.Math.Abs(l - r) + res) 0
|> printfn "Part 1: %i"

//part2
let counts = rList |> List.countBy id

lList |> List.fold (fun res num ->
    counts |> List.tryFind (fun (num', _) -> num' = num)
    |> function
    | Some (_, count) -> num * count + res
    | None -> 0 + res
) 0
|> printfn "Part 2: %i"