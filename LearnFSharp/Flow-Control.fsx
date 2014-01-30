// Conditional statements
// int -> unit
// Single conditional statements and all situations of x are not exhausted.
let add x = 
    if x > 0 then printfn "positive"

let doNothing x = 
    if x > 0 then printfn "x is greater than 0."

// Two conditional statements and all situations of x must be exhausted.
// int -> unit
let u input = 
    if input > 0 then printfn "hi"
    else ()

// int -> string
let bigOrSmall x = 
    if x > 60 then "Big"
    else "Small"

// int -> string
// Multiple conditional statements and all situations of score must be exhausted.
let level score = 
    if score > 90 then "A"
    else if score > 80 then "B"
    else if score > 70 then "C"
    else if score > 60 then "D"
    else "F"

let level2 score = 
    if score > 90 then "A"
    elif score > 80 then "B"
    elif score > 70 then "C"
    elif score > 60 then "D"
    else "F"

// int -> unit
// Multiple conditional statements and all situations of date
// are not exhausted.
let report (date : System.DateTime) = 
    if date.Date = System.DateTime.Today then printfn "The date is today."
    else if date.Date.AddDays(1.0) = System.DateTime.Today then printfn "The date is yesterday."
    else 
        if date.Date.AddDays(-1.0) = System.DateTime.Today then printfn "The date is tomorrow."

// Loop statements
// for..to loop
// http://msdn.microsoft.com/en-us/library/dd233236.aspx
for i = 1 to 10 do
    printf "%d," i
printfn "(end)"
for i = 10 downto 1 do
    printf "%d," i
printfn "(end)"

// for..in loop
// http://msdn.microsoft.com/en-us/library/dd233227.aspx
let list1 = [ 1; 5; 10; 15; 20; 30 ]

for i in list1 do
    printf "%d," i
printfn "(end)"

let reportNumbers lines = 
    for i in 1..lines do
        for j in 1..i do
            printf "%d\t" j
        printf "\n"

let grade scores = 
    seq { 
        for score in scores do
            if score > 90 then yield "A"
            else if score > 80 then yield "B"
            else if score > 70 then yield "C"
            else if score > 60 then yield "D"
            else yield "F"
    }

let grade1 scores = 
    seq { 
        for score in scores do
            if score >= 60 then yield "Pass"
            else yield "Fail"
    }

let grade2 scores = 
    seq { 
        for score in scores do
            yield level score
    }

let grade3 scores = scores |> Seq.map level

let getPassedLevels scores = 
    scores
    |> Seq.map level
    |> Seq.filter (fun level -> not (level = "F"))

let grade4 (scores : seq<float * float>) = 
    seq { 
        for math, english in scores do
            let avg = (math + english) / 2.0
            if avg > 80.0 then yield "Great"
            else if avg > 60.0 then yield "Good"
            else if avg > 40.0 then yield "Bad"
            else yield "Failed"
    }

let grades = 
    seq { 
        for i = 1 to 20 do
            yield (i, 2 * i - 1)
    }

let pgrades = 
    seq { 
        for i, j in grades do
            yield j - i
    }

// while..do loop
// http://msdn.microsoft.com/en-us/library/dd233208.aspx
let getBig0 a b x = 
    let rnd = System.Random()
    let mutable num = a
    while num <= x do
        num <- b * rnd.NextDouble() + a
        printf "%f," num
    printfn "(end)"

let getBig a b x = 
    let rnd = System.Random()
    let mutable n = 0
    while b * rnd.NextDouble() + a <= x do
        n <- n + 1
    n

// Pipeline Operator
let fun1 x = 
    if x > 0 then 
        do printfn "Positive"
        1
    elif x = 0 then 
        do printfn "Zero"
        0
    else 
        do printfn "Negative"
        -1

let num1 = fun1 10
let num2 = 10 |> fun1
let num3 = 100 |> fun x -> 2 * x + 1
let num4 = (0.5, 1.5) |> function 
           | x, y -> x + y
let num5 = (0.5, 1.5) |> fun (x, y) -> x + y
