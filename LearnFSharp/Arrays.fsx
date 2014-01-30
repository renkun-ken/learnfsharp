// Arrays
// Arrays are fixed-size, zero-based, mutable collections of consecutive data elements that are all of the same type.
let array1 = [| 1; 2; 3 |]
let array2 = [| 1; 2; 3 |]
let element1 = array1.[0]
let elements = array1.[0..2]
let elements1 = array1.[..1]
let elements2 = array1.[1..]

// Causes an error.
// let array2 = [| 1.0; 2; 3 |]
let array3 = 
    [| for i in 1..10 -> i * i |]

let arrayOfTenZeroes : int array = Array.zeroCreate 10
let initArray = Array.create 10 5.0
let a1 = [| 0..15 |]
let a2 = Array.sub a1 5 3 // 5 6 7
let a3 = a1.[5..7]
let a4 = Array.append a1 a3

let arrayChoose = 
    [| 1..10 |] |> Array.choose (fun n -> 
                       if n % 2 = 0 then Some(float (n * n - 1))
                       else None)

let arrayCollect = [| 1..10 |] |> Array.collect (fun n -> [| 0..n |])

let stringReverse = 
    let s = "Hello, world!"
    System.String(Array.rev (s.ToCharArray()))

let arrayPipeline = 
    [| 1..10 |]
    |> Array.filter (fun n -> n % 2 = 0)
    |> Array.choose (fun n -> 
           if n <> 8 then Some(n * n)
           else None)
    |> Array.rev
    |> printfn "%A"

// Multidimensional Arrays
let array2d1 = 
    array2D [ [ 1; 0 ]
              [ 0; 1 ] ]

let array2d2 = Array2D.init 2 2 (fun i j -> i + j)

array2d2.[0, 1] <- 3

let arrayExist = 
    let allNegative = Array.exists (fun x -> abs (x) = x) >> not
    printfn "%A" (allNegative [| -1; -2; -3 |])
    printfn "%A" (allNegative [| -10; -1; 5 |])
    printfn "%A" (allNegative [| 0 |])
    let haveEqualElement = Array.exists2 (fun x1 x2 -> x1 = x2)
    printfn "%A" (haveEqualElement [| 1; 2; 3 |] [| 3; 2; 1 |])

let arrayForall = 
    let allPositive = Array.forall (fun x -> x > 0)
    printfn "%A" (allPositive [| 0; 1; 2; 3 |])
    printfn "%A" (allPositive [| 1; 2; 3 |])
    let allEqual = Array.forall2 (fun x1 x2 -> x1 = x2)
    printfn "%A" (allEqual [| 1; 2 |] [| 1; 2 |])
    printfn "%A" (allEqual [| 1; 2 |] [| 2; 1 |])

let arrayFind = 
    let arrayA = [| 2..100 |] // How about 2..50?
    let delta = 1.0e-10
    
    let isPerfectSquare (x : int) = 
        let y = sqrt (float x)
        abs (y - round y) < delta
    
    let isPerfectCube (x : int) = 
        let y = (float x) ** (1.0 / 3.0)
        abs (y - round y) < delta
    
    let element = arrayA |> Array.find (fun x -> isPerfectSquare x && isPerfectCube x)
    let index = arrayA |> Array.findIndex (fun x -> isPerfectSquare x && isPerfectCube x)
    (index, element)

let arrayTryFind = 
    let delta = 1.0e-10
    
    let isPerfectSquare (x : int) = 
        let y = sqrt (float x)
        abs (y - round y) < delta
    
    let isPerfectCube (x : int) = 
        let y = (float x) ** (1.0 / 3.0)
        abs (y - round y) < delta
    
    let look array1 = 
        let result = array1 |> Array.tryFind (fun x -> isPerfectSquare x && isPerfectCube x)
        match result with
        | Some x -> printfn "Found an element: %d" x
        | None -> printfn "Failed to find such an element."
    
    look [| 1..10 |]
    look [| 100..1000 |]
    look [| 2..50 |]

let arrayPick = 
    let delta = 1.0e-10
    
    let isPerfectSquare (x : int) = 
        let y = sqrt (float x)
        abs (y - round y) < delta
    
    let isPerfectCube (x : int) = 
        let y = (float x) ** (1.0 / 3.0)
        abs (y - round y) < delta
    
    let intFun fun1 n = int (round (fun1 (float n)))
    let cubeRoot x = x ** (1.0 / 3.0)
    
    let test x = 
        if isPerfectSquare x && isPerfectCube x then Some(x, intFun sqrt x, intFun cubeRoot x)
        else None
    
    let pick array1 = 
        match Array.tryPick test array1 with
        | Some(n, sqrt, cuberoot) -> printfn "%d, sqrt=%d, cuberoot=%d" n sqrt cuberoot
        | None -> printfn "Not found."
    
    pick [| 1..10 |]
    pick [| 2..100 |]
    pick [| 100..1000 |]
    pick [| 1000..10000 |]
    pick [| 2..50 |]

let arraySum = 
    [| for i in 1..100 -> exp (-float i) |]
    |> Array.sum

let arraySumBy = 
    [| for i in 1..100 -> (i, exp (-float i)) |]
    |> Array.sumBy (fun (w, x) -> float w * x)

let arrayAverage = 
    [| for i in 1..100 -> exp (-float i) |]
    |> Array.average

let arrayAverageBy = 
    [| for i in 1..100 -> (i, exp (-float i)) |]
    |> Array.averageBy (fun (w, x) -> float w * x)

// Modifying arrays
let arrayFill = 
    let array1 = [| 1..25 |]
    Array.fill array1 2 20 0
    printfn "%A" array1
