open System
open System.Linq

let e = 2.7182
let lne = log e
let f5 = float 5
let f1 x y = x * y - x / y
let f2 x y = x / y + x / y ** 2.0
let areaEclipse a b = 3.1416 * a * b
let volPyramid a b h = a * b * h / 3.0

let countChars (str : string) = 
    [ 'a'..'z' ]
    |> List.map (fun c -> c, str.Count(fun x -> x = c))
    |> List.filter (fun (c, n) -> n > 0)

let chars = countChars "hello world"
let countWords (str : string) = str.Split([| ' ' |], StringSplitOptions.RemoveEmptyEntries).Length

let printer1 n = 
    [ 1..n ] |> List.iter (fun i -> 
                    [ 1..i ] |> List.iter (fun _ -> printf "*")
                    printf "\n")

let printer2 n = 
    [ for i in 1..n do
          for j in i..n do
              printf "*"
          printf "\n"
          yield 0 ]
    |> ignore
