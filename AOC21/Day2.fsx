open System

let input =
    IO.File.ReadAllLines "Day2.txt"
    |> Array.map (fun s ->
        let split = s.Split ' '
        split[0], Int32.Parse split[1])

//part1
let getPos forward down up =
    input
    |> Array.fold (fun pos (s,x) ->
        match s with
        |f when f = "forward" -> forward pos x
        |d when d = "down" -> down pos x
        |u when u = "up" -> up pos x) (0,0,0)

let f' (h,d,a) x =
    h + x, d, a
let d' (h,d,a) x =
    h, d + x, a
let u' (h,d,a) x =
    h, d - x, a

match getPos f' d' u' with
|h,d,a -> printfn "Part 1: %i" (h * d)

//part2
let d'' (h,d,a) x =
    h, d, a + x
let u'' (h,d,a) x =
    h, d, a - x
let f'' (h,d,a) x =
    h + x, d + a * x, a

match getPos f'' d'' u'' with
|h,d,a -> printfn "Part 2: %i" (h * d)
