open System

let input = IO.File.ReadAllLines "Day3.txt"

//part1
let checkForNum i' j' =
    let coords = [ i' - 1, j'; i' - 1, j' - 1; i' - 1, j' + 1; i', j' - 1; i', j' + 1; i' + 1, j' - 1; i' + 1, j'; i' + 1, j' + 1]
    let arr = ResizeArray<int * int>()
    for i, j in coords do
        try
            let c = input[i][j]
            if Char.IsDigit c then arr.Add (i, j)
        with ex -> ()
    arr

let readNum i startIndex =
    let rec loop j num =
        let c = input[i][j]
        if Char.IsDigit c then
            let newNum = num * 10 + (int)(Char.GetNumericValue c)
            try
                loop (j + 1) (newNum)
            with ex -> newNum
        else num
    loop startIndex 0

let getNums i j =
    let adjacentNums = checkForNum i j
    let nums = ResizeArray<int>()
    for ni, nj in adjacentNums do
        if adjacentNums.Contains (ni, nj - 1) then
            ()
        else
            let rec loop j' =
                if Char.IsDigit (input[ni][j']) then
                    try
                        loop (j' - 1)
                    with ex -> j'
                else j' + 1
            let startIndex = loop nj
            nums.Add (readNum ni startIndex)
    nums

let mutable sum = 0
for i in 0..input.Length - 1 do
    for j in 0..input[i].Length - 1 do
        let c = input[i][j]
        if Char.IsDigit c || c = '.' then
            //Not symbol
            ()
        else
            //Symbol
            let nums = getNums i j
            for num in nums do
                sum <- sum + num
printfn "Part 1: %i" sum

//part2
let mutable sum2 = 0
for i in 0..input.Length - 1 do
    for j in 0..input[i].Length - 1 do
        let c = input[i][j]
        if c = '*' then
            let nums = getNums i j
            if nums.Count = 2 then
                sum2 <- sum2 + nums[0] * nums[1]
printfn "Part 2: %i" sum2